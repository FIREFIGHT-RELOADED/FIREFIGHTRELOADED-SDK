using System.Globalization;
using System.Windows.Forms;
using ValveKeyValue;

namespace Fabricator
{
    public class MapAdd : FileCreatorBase
    {
        public class MapAddLabelNode : BaseNode
        {
            public string entityName { get; set; } = "npc_maker_firefight";
            public string labelName { get; set; } = "";
            //Positions
            public float x { get; set; } = 0;
            public float y { get; set; } = 0;
            public float z { get; set; } = 0;
            //Rotations
            public float roll { get; set; } = 0;
            public float yaw { get; set; } = 0;
            public float pitch { get; set; } = 0;
            public Dictionary<string, KVValue>? keyValues { get; set; } = new Dictionary<string, KVValue> { 
                ["StartDisabled"] = 0,
                ["targetname"] = "Spawner",
                ["NPCSquadName"] = "Squad",
                ["RareNPCRarity"] = 5,
                ["SpawnFrequency"] = 5,
                ["MaxLiveChildren"] = 15,
                ["MaxLiveRareNPCs"] = 5,
            };
        }

        public override string Label { get; set; } = "MapAdd";

        public MapAdd() : base()
        {
        }

        public MapAdd(string filePath) : base(filePath)
        {
        }

        public override KVObject NodeToKVObject(BaseNode node, int index = -1)
        {
            MapAddLabelNode? classNode = node as MapAddLabelNode;

            if (classNode != null)
            {
                //add any keyvalues we need to add to a list.
                List<KVObject> keyvalueEntries = new List<KVObject>();

                bool keyvalues = false;
                if (classNode.keyValues != null && classNode.keyValues.Count > 0)
                {
                    foreach (var item in classNode.keyValues)
                    {
                        keyvalueEntries.Add(new KVObject(item.Key, item.Value));
                    }

                    keyvalues = true;
                }

                //load every entry into the entrystats list and convert them to KVObjects.
                //WHY CAN'T IT JUST LOAD A DOUBLE?
                AddKVObjectEntryStat("entity", classNode.entityName);
                AddKVObjectEntryStat("label", classNode.labelName);
                AddKVObjectEntryStat("x", classNode.x.ToString());
                AddKVObjectEntryStat("y", classNode.y.ToString());
                AddKVObjectEntryStat("z", classNode.z.ToString());
                AddKVObjectEntryStat("roll", classNode.roll.ToString());
                AddKVObjectEntryStat("yaw", classNode.yaw.ToString());
                AddKVObjectEntryStat("pitch", classNode.pitch.ToString());

                //convert the list of keyvalues into a kvobject and add to the entrystats list.
                if (keyvalues)
                {
                    entryStats.Add(new KVObject("KeyValues", keyvalueEntries));
                }
            }

            return base.NodeToKVObject(node, index);
        }


        public override MapAddLabelNode KVObjectToNode(int index)
        {
            int actualIndex = index - 1;

            MapAddLabelNode labelNode = new MapAddLabelNode();

            if (entries[actualIndex] != null)
            {
                KVObject obj = entries[actualIndex];

                //go through the KVObject's children and fill in the entries of each node.
                foreach (KVObject nodechild in obj.Children)
                {
                    switch (nodechild.Name)
                    {
                        case "entity":
                            labelNode.entityName = nodechild.Value.ToString(CultureInfo.CurrentCulture);
                            break;
                        case "label":
                            labelNode.labelName = nodechild.Value.ToString(CultureInfo.CurrentCulture);
                            break;
                        case "x":
                            labelNode.x = nodechild.Value.ToSingle(CultureInfo.CurrentCulture);
                            break;
                        case "y":
                            labelNode.y = nodechild.Value.ToSingle(CultureInfo.CurrentCulture);
                            break;
                        case "z":
                            labelNode.z = nodechild.Value.ToSingle(CultureInfo.CurrentCulture);
                            break;
                        case "roll":
                            labelNode.roll = nodechild.Value.ToSingle(CultureInfo.CurrentCulture);
                            break;
                        case "yaw":
                            labelNode.yaw = nodechild.Value.ToSingle(CultureInfo.CurrentCulture);
                            break;
                        case "pitch":
                            labelNode.pitch = nodechild.Value.ToSingle(CultureInfo.CurrentCulture);
                            break;
                        case "KeyValues":
                            {
                                Dictionary<string, KVValue> vals = new Dictionary<string, KVValue>();

                                foreach (KVObject val in nodechild.Children)
                                {
                                    vals.Add(val.Name, val.Value);
                                }

                                labelNode.keyValues = vals;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            return labelNode;
        }
    }
}
