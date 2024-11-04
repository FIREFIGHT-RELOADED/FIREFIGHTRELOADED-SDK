using ValveKeyValue;

namespace Fabricator
{
    public class FileBase
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
        /// Constructor. Sets up the list objects and loads the file.
        /// </summary>
        /// <param name="filePath"></param>
        public FileBase(string filePath)
        {
            SetupLists();
            LoadFile(filePath);
        }


        /// <summary>
        /// Loads a file and adds entries from it.
        /// </summary>
        /// <param name="filePath"></param>
        public virtual void LoadFile(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            {
                KVSerializer kv = KVSerializer.Create(KVSerializationFormat.KeyValues1Text);
                KVObject data = kv.Deserialize(stream);
                foreach (KVObject item in data)
                {
                    entries.Add(item);
                }
            }
        }

        
        /// <summary>
        /// Sets up the lists. 
        /// </summary>
        private void SetupLists()
        {
            entries = new List<KVObject>();
            entryStats = new List<KVObject>();
        }


        /// <summary>
        /// Converts the file object into a KVObject.
        /// </summary>
        /// <returns></returns>
        public virtual KVObject ToKVObject()
        {
            List<KVObject> list = new List<KVObject>();
            list.AddRange(entries);

            KVObject finalFile = new KVObject(Label, list);
            return finalFile;
        }


        /// <summary>
        /// Converts a single BaseNode to a KVObject.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual KVObject NodeToKVObject(BaseNode node, int index = -1)
        {
            //Add at least ONE entry if we have none. Base nodes have no values.
            //This is so we don't directly assume that the list has entries in it.
            if (entries.Count <= 0)
            {
                entryStats.Add(new KVObject("HEY", "YOU FORGOT TO ADD SOMETHING"));
            }

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
        /// Adds a BaseNode to the entries list.
        /// </summary>
        /// <param name="filePath"></param>
        public virtual void AddEntry(BaseNode node)
        {
            entries.Add(NodeToKVObject(node));
            //This is unnessessary as it doesn't change indexes, but better safe than sorry.
            RefreshEntries();
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
        /// Refreshes all entries in the list.
        /// This is used for updating the index of each entry in the list,
        /// in case one is removed, for example.
        /// </summary>
        public virtual void RefreshEntries()
        {
            List<KVObject> newEntries = new List<KVObject>();

            for (int i = 1; i < entries.Count; i++)
            {
                newEntries.Add(new KVObject(i.ToString(), entries[i].Value));
            }

            entries = newEntries;
        }

        /// <summary>
        /// Edits an entry in the list using a BaseNode.
        /// </summary>
        /// <param name="filePath"></param>
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
        /// Saves the object to a file.
        /// </summary>
        public virtual void Save(string filePath)
        {
            KVObject finalFile = ToKVObject();
            KVSerializer kv = KVSerializer.Create(KVSerializationFormat.KeyValues1Text);
            using (FileStream stream = File.OpenWrite(filePath))
            {
                kv.Serialize(stream, finalFile);
            }
        }
    }
}
