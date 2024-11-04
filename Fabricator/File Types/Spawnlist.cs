using System.Collections.Generic;
using System.IO;
using ValveKeyValue;

namespace Fabricator
{
    public class Spawnlist : FileBase
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

        public class SpawnlistNode : BaseNode
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

        public Spawnlist(string filePath) : base(filePath)
        {
        }

        public override void LoadFile(string filePath)
        {
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

        public override KVObject NodetoKVObject(BaseNode node, int index = -1)
        {
            SpawnlistNode classNode = node as SpawnlistNode;

            if (classNode != null)
            {
                List<KVObject> equipmentEntries = new List<KVObject>();

                bool equipment = false;
                if (classNode.equipment != null && classNode.equipment.Count > 0)
                {
                    foreach (var item in classNode.equipment)
                    {
                        equipmentEntries.Add(new KVObject(item.Key, item.Value));
                    }

                    equipment = true;
                }

                List<KVObject> mapEntries = new List<KVObject>();

                bool mapspawn = false;
                if (classNode.mapspawn != null && classNode.mapspawn.Count > 0)
                {
                    foreach (var item in classNode.mapspawn)
                    {
                        //the value won't be read but whatever, it works.
                        mapEntries.Add(new KVObject(item, 1));
                    }

                    mapspawn = true;
                }

                if (!string.IsNullOrWhiteSpace(classNode.classname))
                {
                    entryStats.Add(new KVObject("classname", classNode.classname));
                }

                if (classNode.preset != -2)
                {
                    entryStats.Add(new KVObject("preset", classNode.preset));
                }

                if (classNode.minLevel != -1)
                {
                    entryStats.Add(new KVObject("min_level", classNode.minLevel));
                }

                if (classNode.rare != BoolInt.Invalid)
                {
                    entryStats.Add(new KVObject("rare", (int)classNode.rare));
                }

                if (classNode.exp != -1)
                {
                    entryStats.Add(new KVObject("exp", classNode.exp));
                }

                if (classNode.wildcard != -2)
                {
                    entryStats.Add(new KVObject("wildcard", classNode.wildcard));
                }

                if (classNode.weight != -1)
                {
                    entryStats.Add(new KVObject("weight", classNode.weight));
                }

                if (classNode.grenades != null)
                {
                    entryStats.Add(new KVObject("grenades", classNode.grenades.ToString()));
                }

                if (classNode.kash != -1)
                {
                    entryStats.Add(new KVObject("kash", classNode.kash));
                }

                if (classNode.subsitute != BoolInt.Invalid)
                {
                    entryStats.Add(new KVObject("subsitute", (int)classNode.subsitute));
                }

                if (mapspawn)
                {
                    entryStats.Add(new KVObject("mapspawn", mapEntries));
                }

                if (equipment)
                {
                    entryStats.Add(new KVObject("equipment", equipmentEntries));
                }
            }

            return base.NodetoKVObject(node, index);
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

        public override KVObject ToKVObject()
        {
            List<KVObject> list = new List<KVObject>();
            KVObject set = new KVObject("settings", settings);
            list.Add(set);
            list.AddRange(entries);

            KVObject finalFile = new KVObject(Label, list);
            return finalFile;
        }

        public override void Save(string filePath)
        {
            Label = Path.GetFileNameWithoutExtension(filePath);
            base.Save(filePath);
        }
    }
}
