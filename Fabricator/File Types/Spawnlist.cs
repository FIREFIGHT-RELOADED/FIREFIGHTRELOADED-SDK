using ValveKeyValue;

namespace Fabricator
{
    public class Spawnlist
    {
        public class Node
        {
            public string classname { get; set; } = "";
            public int preset { get; set; } = -2;
            public int minLevel { get; set; } = -1;
            public int rare { get; set; } = -1;
            public int exp { get; set; } = -1;
            public int wildcard { get; set; } = -1;
            public Dictionary<string, int>? equipment { get; set; } = null;
        }

        KVObject settings {  get; set; }
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
                        settings = item;
                        continue;
                    }

                    entries.Add(item);
                }
            }
        }

        public void AddEntry(Node node)
        {
            bool equipment = false;
            List<KVObject> equipmentEntries = new List<KVObject>();
            if (node.equipment.Count > 0)
            {
                foreach (var item in node.equipment)
                {
                    equipmentEntries.Add(new KVObject(item.Key, item.Value));
                }

                equipment = true;
            }

            List<KVObject> entryStats = new List<KVObject>();

            if(!string.IsNullOrWhiteSpace(node.classname))
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

            if (node.rare != -1)
            {
                entryStats.Add(new KVObject("rare", node.rare));
            }

            if (node.exp != -1)
            {
                entryStats.Add(new KVObject("exp", node.exp));
            }

            if (node.wildcard != -1)
            {
                entryStats.Add(new KVObject("wildcard", node.wildcard));
            }

            if (equipment)
            {
                entryStats.Add(new KVObject("equipment", equipmentEntries));
            }

            var kv = new KVObject((entries.Count + 1).ToString(), entryStats);

            entries.Add(kv);
        }

        public void AddSetting(string settingName, string settingValue)
        {
            settings.Add(new KVObject(settingName, settingValue));
        }

        public void EditSetting(string settingName, string settingValue)
        {
            settings[settingName] = settingValue;
        }

        public KVObject ToKVObject(string label)
        {
            List<KVObject> list = new List<KVObject>();
            list.Add(settings);
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
