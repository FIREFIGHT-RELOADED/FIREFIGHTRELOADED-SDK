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
            public KVObject? keyValues { get; set; } = new KVObject("KeyValues", [new KVObject("StartDisabled","0")]);
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
            MapAddLabelNode classNode = node as MapAddLabelNode;

            if (classNode != null)
            {
                //WHY CAN'T IT JUST LOAD A DOUBLE?
                AddKVObjectEntryStat("entity", classNode.entityName);
                AddKVObjectEntryStat("label", classNode.labelName);
                AddKVObjectEntryStat("x", classNode.x.ToString());
                AddKVObjectEntryStat("y", classNode.y.ToString());
                AddKVObjectEntryStat("z", classNode.z.ToString());
                AddKVObjectEntryStat("roll", classNode.roll.ToString());
                AddKVObjectEntryStat("yaw", classNode.yaw.ToString());
                AddKVObjectEntryStat("pitch", classNode.pitch.ToString());
                AddKVObjectEntryStat("KeyValues", classNode.keyValues);
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
                            labelNode.keyValues = nodechild;
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
