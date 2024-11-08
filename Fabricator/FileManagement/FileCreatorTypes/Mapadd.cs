using System.Globalization;
using System.Windows.Forms;
using ValveKeyValue;

namespace Fabricator
{
    public class MapAdd : FileCreatorBase
    {
        public class MapAddLabel : BaseNode
        {
            public string labelName { get; set; } = "";
            public List<MapAddLabelNode>? labelNodes { get; set; } = null;
        }

        public class MapAddLabelNode : BaseNode
        {
            public string entityName { get; set; } = "";
            //Positions
            public float x { get; set; } = 0;
            public float y { get; set; } = 0;
            public float z { get; set; } = 0;
            //Rotations
            public float roll { get; set; } = 0;
            public float yaw { get; set; } = 0;
            public float pitch { get; set; } = 0;
            public KVObject? keyValues { get; set; } = null;
        }

        public override string Label { get; set; } = "MapAdd";
        public override bool PreserveNodeNamesOnRefresh { get; set; } = true;

        public MapAdd(string filePath) : base(filePath)
        {
        }

        public override KVObject NodeToKVObject(BaseNode node, int index = -1)
        {
            MapAddLabel classNode = node as MapAddLabel;

            string nodename = "";

            if (classNode != null)
            {
                nodename = classNode.labelName;

                foreach (MapAddLabelNode labelNode in classNode.labelNodes)
                {
                    KVObject obj = new KVObject(
                        labelNode.entityName,
                        [
                             //WHY CAN'T IT JUST LOAD A DOUBLE?
                             new KVObject("x", labelNode.x.ToString()),
                             new KVObject("y", labelNode.y.ToString()),
                             new KVObject("z", labelNode.z.ToString()),
                             new KVObject("roll", labelNode.roll.ToString()),
                             new KVObject("yaw", labelNode.yaw.ToString()),
                             new KVObject("pitch", labelNode.pitch.ToString()),
                             labelNode.keyValues
                        ]);

                    AddKVObjectEntryStat(obj);
                }
            }

            //Creates a KVObject, clears the entryStats List, and returns the object.
            KVObject kv = new KVObject(nodename, entryStats);

            entryStats.Clear();

            return kv;
        }


        public override MapAddLabel EntryToNode(int index)
        {
            int actualIndex = index - 1;

            MapAddLabel classNode = new MapAddLabel();

            if (entries[actualIndex] != null)
            {
                KVObject obj = entries[actualIndex];

                classNode.labelName = obj.Name;
                classNode.labelNodes = new List<MapAddLabelNode>();

                foreach (KVObject child in obj.Children)
                {
                    MapAddLabelNode labelNode = new MapAddLabelNode();
                    labelNode.entityName = child.Name;

                    foreach ( KVObject nodechild in child.Children)
                    {
                        switch (nodechild.Name)
                        {
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

                    classNode.labelNodes.Add(labelNode);
                }
            }

            return classNode;
        }
    }
}
