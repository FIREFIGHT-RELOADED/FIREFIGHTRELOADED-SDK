using System.Collections.Generic;
using ValveKeyValue;

namespace Fabricator
{
    public class Spawnlist
    {
        public class GrenadeEntry
        {
            private int minGrenades {  get; set; }
            private int maxGrenades { get; set; }
            public GrenadeEntry(int min, int max)
            {
                minGrenades = min;
                maxGrenades = max;
            }

            public override string ToString()
            {
                return $"{minGrenades}-{maxGrenades}";
            }
        }

        public enum BoolInt
        {
            Invalid = -1,
            False,
            True
        }

        public class Node
        {
            public string classname { get; set; } = "";
            public int preset { get; set; } = -2;
            public int minLevel { get; set; } = -1;
            public BoolInt rare { get; set; } = BoolInt.Invalid;
            public int exp { get; set; } = -1;
            public int wildcard { get; set; } = -2;
            public List<string> mapspawn { get; set; } = null;
            public float weight { get; set; } = -1;
            public GrenadeEntry? grenades { get; set; } = null;
            public int kash { get; set; } = -1;
            public BoolInt subsitute { get; set; } = BoolInt.Invalid;
            public Dictionary<string, float>? equipment { get; set; } = null;
        }

        List<KVObject> settings {  get; set; }
        List<KVObject> entries { get; set; }

        public Spawnlist(string filePath)
        {
            entries = new List<KVObject>();

            using (var stream = File.OpenRead(filePath))
            {
                KVSerializer kv = KVSerializer.Create(KVSerializationFormat.KeyValues1Text);
                KVObject data = kv.Deserialize(stream);
                foreach (KVObject item in data)
                {
                    if (item.Name == "settings")
                    {
                        settings = item.Children.ToList();
                        continue;
                    }

                    entries.Add(item);
                }
            }
        }

        public KVObject NodetoKVObject(Node node, int index = -1)
        {
            List<KVObject> equipmentEntries = new List<KVObject>();

            bool equipment = false;
            if (node.equipment != null && node.equipment.Count > 0)
            {
                foreach (var item in node.equipment)
                {
                    equipmentEntries.Add(new KVObject(item.Key, item.Value));
                }

                equipment = true;
            }

            List<KVObject> mapEntries = new List<KVObject>();

            bool mapspawn = false;
            if (node.mapspawn != null && node.mapspawn.Count > 0)
            {
                foreach (var item in node.mapspawn)
                {
                    //the value won't be read but whatever, it works.
                    mapEntries.Add(new KVObject(item, 1));
                }

                mapspawn = true;
            }

            List<KVObject> entryStats = new List<KVObject>();

            if (!string.IsNullOrWhiteSpace(node.classname))
            {
                entryStats.Add(new KVObject("classname", node.classname));
            }

            if (node.preset != -2)
            {
                entryStats.Add(new KVObject("preset", node.preset));
            }

            if (node.minLevel != -1)
            {
                entryStats.Add(new KVObject("min_level", node.minLevel));
            }

            if (node.rare != BoolInt.Invalid)
            {
                entryStats.Add(new KVObject("rare", (int)node.rare));
            }

            if (node.exp != -1)
            {
                entryStats.Add(new KVObject("exp", node.exp));
            }

            if (node.wildcard != -2)
            {
                entryStats.Add(new KVObject("wildcard", node.wildcard));
            }

            if (node.weight != -1)
            {
                entryStats.Add(new KVObject("weight", node.weight));
            }

            if (node.grenades != null)
            {
                entryStats.Add(new KVObject("grenades", node.grenades.ToString()));
            }

            if (node.kash != -1)
            {
                entryStats.Add(new KVObject("kash", node.kash));
            }

            if (node.subsitute != BoolInt.Invalid)
            {
                entryStats.Add(new KVObject("subsitute", (int)node.subsitute));
            }

            if (mapspawn)
            {
                entryStats.Add(new KVObject("mapspawn", mapEntries));
            }

            if (equipment)
            {
                entryStats.Add(new KVObject("equipment", equipmentEntries));
            }

            if (index == -1)
            {
                index = entries.Count + 1;
            }

            KVObject kv = new KVObject(index.ToString(), entryStats);

            return kv;
        }

        public void AddEntry(Node node)
        {
            entries.Add(NodetoKVObject(node));
        }

        public void RemoveEntry(int index)
        {
            int actualIndex = index - 1;
            entries.RemoveAt(actualIndex);
        }

        public void EditEntry(int index, Node nodeEdited)
        {
            int actualIndex = index - 1;

            if (entries[actualIndex] != null)
            {
                entries[actualIndex] = NodetoKVObject(nodeEdited, index);
            }
        }

        public void AddSetting(string settingName, string settingValue)
        {
            settings.Add(new KVObject(settingName, settingValue));
        }

        public void EditSetting(string settingName, string settingValue)
        {
            var index = settings.FindIndex(x => x.Name == settingName);
            if (settings[index] != null)
            {
                settings[index] = new KVObject(settingName, settingValue);
            }
        }

        public void RemoveSetting(string settingName)
        {
            KVObject? query = settings.Find(x => x.Name == settingName);

            if (query != null)
            {
                settings.Remove(query);
            }
        }

        public KVObject ToKVObject(string label)
        {
            List<KVObject> list = new List<KVObject>();
            KVObject set = new KVObject("settings", settings);
            list.Add(set);
            list.AddRange(entries);

            KVObject finalFile = new KVObject(Path.GetFileNameWithoutExtension(label), list);
            return finalFile;
        }

        public void Save(string filePath)
        {
            KVObject finalFile = ToKVObject(filePath);
            KVSerializer kv = KVSerializer.Create(KVSerializationFormat.KeyValues1Text);
            using (FileStream stream = File.OpenWrite(filePath))
            {
                kv.Serialize(stream, finalFile);
            }
        }
    }
}
