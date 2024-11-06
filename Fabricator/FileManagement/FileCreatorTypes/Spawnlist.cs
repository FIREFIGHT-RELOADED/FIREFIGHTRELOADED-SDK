using System.Globalization;
using ValveKeyValue;

namespace Fabricator
{
    public class Spawnlist : FileCreatorBase
    {
        public class GrenadeEntry
        {
            private int minGrenades {  get; set; }
            private int maxGrenades { get; set; }

            public GrenadeEntry(string grenadeString)
            {
                string[] sections = grenadeString.Split('-');
                minGrenades = Convert.ToInt32(sections[0]);
                maxGrenades = Convert.ToInt32(sections[1]);
            }

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
            public List<string>? mapspawn { get; set; } = null;
            public float weight { get; set; } = -1;
            public GrenadeEntry? grenades { get; set; } = null;
            public int kash { get; set; } = -1;
            public BoolInt subsitute { get; set; } = BoolInt.Invalid;
            public Dictionary<string, float>? equipment { get; set; } = null;
        }

        
        public override bool SetLabelToFileName { get; set; } = true;
        public override bool FileUsesSettings { get; set; } = true;

        public Spawnlist(string filePath) : base(filePath)
        {
        }

        public override KVObject NodeToKVObject(BaseNode node, int index = -1)
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

                AddKVObjectEntryStat("classname", classNode.classname);
                AddKVObjectEntryStat("preset", classNode.preset, -2);
                AddKVObjectEntryStat("min_level", classNode.minLevel);
                AddKVObjectEntryStat("rare", classNode.rare);
                AddKVObjectEntryStat("exp", classNode.exp);
                AddKVObjectEntryStat("wildcard", classNode.wildcard, -2);
                AddKVObjectEntryStat("weight", classNode.weight);
                AddKVObjectEntryStat("grenades", classNode.grenades);
                AddKVObjectEntryStat("subsitute", classNode.subsitute);
                AddKVObjectEntryStat("kash", classNode.kash);

                if (mapspawn)
                {
                    entryStats.Add(new KVObject("mapspawn", mapEntries));
                }

                if (equipment)
                {
                    entryStats.Add(new KVObject("equipment", equipmentEntries));
                }
            }

            return base.NodeToKVObject(node, index);
        }

        public override SpawnlistNode EntryToNode(int index)
        {
            int actualIndex = index - 1;

            SpawnlistNode classNode = new SpawnlistNode();

            if (entries[actualIndex] != null)
            {
                KVObject obj = entries[actualIndex];

                foreach (KVObject child in obj.Children)
                {
                    switch (child.Name)
                    {
                        case "classname":
                            classNode.classname = child.Value.ToString(CultureInfo.CurrentCulture);
                            break;
                        case "preset":
                            classNode.preset = child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "min_level":
                            classNode.minLevel = child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "rare":
                            classNode.rare = (BoolInt)child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "exp":
                            classNode.exp = child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "wildcard":
                            classNode.wildcard = child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "weight":
                            classNode.weight = child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "subsitute":
                            classNode.subsitute = (BoolInt)child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "kash":
                            classNode.kash = child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "grenades":
                            {
                                string grenadeString = child.Value.ToString(CultureInfo.CurrentCulture);
                                classNode.grenades = new GrenadeEntry(grenadeString);
                            }
                            break;
                        case "mapspawn":
                            {
                                List<string> maps = new List<string>();

                                foreach (KVObject map in child.Children)
                                {
                                    maps.Add(map.Name);
                                }

                                classNode.mapspawn = maps;
                            }
                            break;
                        case "equipment":
                            {
                                Dictionary<string, float> weapons = new Dictionary<string, float>();

                                foreach (KVObject weapon in child.Children)
                                {
                                    weapons.Add(weapon.Name, weapon.Value.ToSingle(CultureInfo.CurrentCulture));
                                }

                                classNode.equipment = weapons;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            return classNode;
        }
    }
}
