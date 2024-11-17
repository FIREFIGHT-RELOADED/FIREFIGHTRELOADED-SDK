using System;
using System.CodeDom;
using System.Windows.Forms;
using ValveKeyValue;

namespace Fabricator
{
    public class FileCreatorBase
    {
        /// <summary>
        /// A special enum class used for "null booleans".
        /// A BoolInt set to Invalid is considered unusable, while False/True are the "real" values of it.
        /// </summary>
        public enum BoolInt
        {
            Invalid = -1,
            False,
            True
        }

        /// <summary>
        /// An empty class that represents a node in the KeyValues file. 
        /// </summary>
        public class BaseNode
        {
        }
        
        /// <summary>
        /// The list of file entries loaded from the file.
        /// These are all KeyValue objects.
        /// </summary>
        public List<KVObject> entries { get; set; }

        /// <summary>
        /// The stats for a specific entry when converting BaseNodes to a temporary object.
        /// MUST be wiped before doing any node conversion operation, see NodeToKVObject.
        /// </summary>
        public List<KVObject> entryStats { get; set; }

        
        /// <summary>
        /// The "header" of the finished KeyValue file.
        /// </summary>
        public virtual string Label { get; set; } = "Base";


        /// <summary>
        /// Sets the label to the file name upon saving.
        /// This means that Label is overriden with the file name.
        /// </summary>
        public virtual bool SetLabelToFileName { get; set; } = false;


        /// <summary>
        /// A List that handles settings if the file has any.
        /// </summary>
        public List<KVObject> settings { get; set; }

        /// <summary>
        /// Does the file we need have a special "settings" section?
        /// </summary>
        public virtual bool FileUsesSettings { get; set; } = false;

        /// <summary>
        /// Don't change the names of nodes upon entry refresh.
        /// </summary>
        public virtual bool PreserveNodeNamesOnRefresh { get; set; } = false;

        /// <summary>
        /// the raw data of the file.
        /// </summary>
        public KVObject rawData { get; set; }

        /// <summary>
        /// Constructor. Sets up the list objects and loads the file.
        /// </summary>
        public FileCreatorBase()
        {
            SetupLists();
        }

        /// <summary>
        /// Constructor. Sets up the list objects and loads the file.
        /// </summary>
        /// <param name="filePath"></param>
        public FileCreatorBase(string filePath)
        {
            SetupLists();
            LoadFile(filePath);
        }

        /// <summary>
        /// Loads a KeyValues file into a KVObject.
        /// This doesn't load any file data into FileCreatorBase.
        /// LoadFile does that purpose.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static KVObject? LoadKeyValuesFile(string filePath)
        {
            KVObject? data = null;

            using (var stream = File.OpenRead(filePath))
            {
                KVSerializer kv = KVSerializer.Create(KVSerializationFormat.KeyValues1Text);
                data = kv.Deserialize(stream);
            }

            return data;
        }

        /// <summary>
        /// Loads a file and adds entries from it.
        /// </summary>
        /// <param name="filePath"></param>
        public virtual void LoadFile(string filePath)
        {
            bool settingsAvailable = false;

            rawData = LoadKeyValuesFile(filePath);

            if (rawData != null)
            {
                foreach (KVObject item in rawData)
                {
                    if (FileUsesSettings)
                    {
                        if (item.Name == "settings")
                        {
                            settings = item.Children.ToList();
                            settingsAvailable = true;
                            continue;
                        }
                    }

                    entries.Add(item);
                }
            }

            // if we can't find the settings, completely disable it for this file.
            if (FileUsesSettings && !settingsAvailable)
            {
                ToggleSettings();
            }
        }

        
        /// <summary>
        /// Sets up the lists. 
        /// </summary>
        public virtual void SetupLists()
        {
            entries = new List<KVObject>();
            entryStats = new List<KVObject>();
            if (FileUsesSettings)
            {
                settings = new List<KVObject>();
            }
        }

        public virtual void ToggleSettings()
        {
            FileUsesSettings = !FileUsesSettings;

            if (settings == null)
            {
                settings = new List<KVObject>();
            }
        }

        /// <summary>
        /// Converts a single BaseNode to a KVObject.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual KVObject NodeToKVObject(BaseNode node, int index = -1)
        {
            //if we have an invalid index, give it a proper one by
            //specifying 1 to the entry count.
            if (index == -1)
            {
                index = entries.Count + 1;
            }

            //Creates a KVObject, clears the entryStats List, and returns the object.
            KVObject kv = new KVObject(index.ToString(), entryStats);

            entryStats.Clear();

            return kv;
        }


        /// <summary>
        /// Convert a KVObject to a BaseNode.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual BaseNode KVObjectToNode(int index)
        {
            return new BaseNode();
        }

        /// <summary>
        /// Adds a BaseNode to the entries list.
        /// </summary>
        /// <param name="node"></param>
        public virtual void AddEntry(BaseNode node)
        {
            entries.Add(NodeToKVObject(node));
            //This is unnessessary as it doesn't change indexes, but better safe than sorry.
            RefreshEntries();
        }

        /// <summary>
        /// Adds a KVObject to the entries list.
        /// </summary>
        /// <param name="kv"></param>
        public virtual void AddEntry(KVObject kv)
        {
            entries.Add(kv);
            //This is unnessessary as it doesn't change indexes, but better safe than sorry.
            RefreshEntries();
        }

        /// <summary>
        /// Adds a KVObject to the entryStats list.
        /// </summary>
        /// <param name="kv"></param>
        public virtual void AddKVObjectEntryStat(KVObject kv)
        {
            entryStats.Add(kv);
        }

        /// <summary>
        /// Adds a KVObject to the entryStats list.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void AddKVObjectEntryStat(string key, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                AddKVObjectEntryStat(new KVObject(key, value));
            }
        }

        /// <summary>
        /// Adds a KVObject to the entryStats list.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void AddKVObjectEntryStat(string key, BoolInt value)
        {
            if (value != BoolInt.Invalid)
            {
                AddKVObjectEntryStat(new KVObject(key, (int)value));
            }
        }

        /// <summary>
        /// Adds a KVObject to the entryStats list.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="min"></param>
        public virtual void AddKVObjectEntryStat(string key, int value, int min = -1)
        {
            if (value != min)
            {
                AddKVObjectEntryStat(new KVObject(key, value));
            }
        }

        /// <summary>
        /// Adds a KVObject to the entryStats list.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="min"></param>
        public virtual void AddKVObjectEntryStat(string key, float value, float min = -1)
        {
            if (value != min)
            {
                AddKVObjectEntryStat(new KVObject(key, value));
            }
        }

        /// <summary>
        /// Adds a KVObject to the entryStats list.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void AddKVObjectEntryStat(string key, object value)
        {
            if (value != null)
            {
                AddKVObjectEntryStat(new KVObject(key, value.ToString()));
            }
        }

        /// <summary>
        /// Removes an entry from the entries list.
        /// </summary>
        /// <param name="index"></param>
        public virtual void RemoveEntry(int index)
        {
            int actualIndex = index - 1;
            entries.RemoveAt(actualIndex);
            RefreshEntries();
        }

        /// <summary>
        /// Removes an entry from the entries list.
        /// </summary>
        /// <param name="settingName"></param>
        public virtual void RemoveEntry(string entryName)
        {
            KVObject? query = entries.Find(x => x.Name == entryName);

            if (query != null)
            {
                entries.Remove(query);
                RefreshEntries();
            }
        }

        /// <summary>
        /// Refreshes all entries in the list.
        /// This is used for updating the index of each entry in the list,
        /// in case one is removed, for example.
        /// </summary>
        public virtual void RefreshEntries()
        {
            List<KVObject> newEntries = new List<KVObject>();

            for (int i = 0; i < entries.Count; i++)
            {
                string entryName = entries[i].Name;

                //bad code alert
                if (!PreserveNodeNamesOnRefresh)
                {
                    entryName = (i + 1).ToString();
                }

                newEntries.Add(new KVObject(entryName, entries[i].Value));
            }

            entries = newEntries;
        }

        /// <summary>
        /// Edits an entry in the list using a BaseNode.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="nodeEdited"></param>
        public virtual void EditEntry(int index, BaseNode nodeEdited)
        {
            int actualIndex = index - 1;

            if (entries[actualIndex] != null)
            {
                entries[actualIndex] = NodeToKVObject(nodeEdited, index);
            }

            //This is unnessessary as it doesn't change indexes, but better safe than sorry.
            RefreshEntries();
        }

        /// <summary>
        /// Edits an entry in the list using a KVObject.
        /// </summary>
        /// <param name="kv"></param>
        public virtual void EditEntry(KVObject kv)
        {
            var index = entries.FindIndex(x => x.Name == kv.Name);
            EditEntry(index, kv);
        }

        /// <summary>
        /// Edits an entry in the list using a KVObject.
        /// </summary>
        /// <param name="kv"></param>
        /// <param name="index"></param>
        public virtual void EditEntry(int index, KVObject kv)
        {
            if (entries[index] != null)
            {
                entries[index] = kv;
            }

            //This is unnessessary as it doesn't change indexes, but better safe than sorry.
            RefreshEntries();
        }

        /// <summary>
        /// Adds a setting to the settings list.
        /// </summary>
        /// <param name="kv"></param>
        public virtual void AddSetting(KVObject kv)
        {
            if (FileUsesSettings)
            {
                settings.Add(kv);
            }
        }

        /// <summary>
        /// Edits a setting in the settings list.
        /// </summary>
        /// <param name="kv"></param>
        public virtual void EditSetting(KVObject kv)
        {
            if (FileUsesSettings)
            {
                try
                {
                    var index = settings.FindIndex(x => x.Name == kv.Name);
                    if (settings[index] != null)
                    {
                        settings[index] = kv;
                    }
                }
                catch (Exception)
                {
                    AddSetting(kv);
                }
            }
        }

        /// <summary>
        /// Removes a setting from the settings list.
        /// </summary>
        /// <param name="settingName"></param>
        public virtual void RemoveSetting(string settingName)
        {
            if (FileUsesSettings)
            {
                KVObject? query = settings.Find(x => x.Name == settingName);

                if (query != null)
                {
                    settings.Remove(query);
                }
            }
        }

        /// <summary>
        /// Converts the file object into a KVObject.
        /// </summary>
        /// <returns></returns>
        public virtual KVObject ToKVObject()
        {
            List<KVObject> list = new List<KVObject>();

            if (FileUsesSettings)
            {
                KVObject set = new KVObject("settings", settings);
                list.Add(set);
            }

            list.AddRange(entries);

            KVObject finalFile = new KVObject(Label, list);
            return finalFile;
        }

        /// <summary>
        /// Saves the object to a file.
        /// </summary>
        public virtual void Save(string filePath)
        {
            if (SetLabelToFileName)
            {
                Label = Path.GetFileNameWithoutExtension(filePath);
            }

            //For some strange reason, the data will save DIRECTLY into a file if it exists.
            //So, we should remove it so the file can actually work.
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Make our file a KVObject, then save using a FileStream.
            KVObject finalFile = ToKVObject();
            KVSerializer kv = KVSerializer.Create(KVSerializationFormat.KeyValues1Text);
            using (FileStream stream = File.OpenWrite(filePath))
            {
                kv.Serialize(stream, finalFile);
            }
        }
    }
}
