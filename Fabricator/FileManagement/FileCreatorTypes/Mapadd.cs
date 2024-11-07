using System.Globalization;
using System.Windows.Forms;
using ValveKeyValue;

namespace Fabricator
{
    public class MapAdd : FileCreatorBase
    {
        public class MapAddLabel : BaseNode
        {
            public List<MapAddLabelNode> labelNodes { get; set; }
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
            public KVObject keyValues { get; set; }
        }

        public override string Label { get; set; } = "MapAdd";
        public Dictionary<int, KVObject> labelEntries { get; set; }

        public MapAdd(string filePath) : base(filePath)
        {
        }

        public override KVObject NodeToKVObject(BaseNode node, int index = -1)
        {
            MapAddLabel classNode = node as MapAddLabel;

            if (classNode != null)
            {
                foreach (MapAddLabelNode labelNode in classNode.labelNodes)
                {
                    AddKVObjectEntryStat(new KVObject(
                        labelNode.entityName, 
                        [
                             new KVObject("x", labelNode.x),
                             new KVObject("y", labelNode.y),
                             new KVObject("z", labelNode.z),
                             new KVObject("roll", labelNode.roll),
                             new KVObject("yaw", labelNode.yaw),
                             new KVObject("pitch", labelNode.pitch),
                             labelNode.keyValues
                        ]));
                }
            }

            return base.NodeToKVObject(node, index);
        }

        //TODO
        public override CatalogNode EntryToNode(int index)
        {
            int actualIndex = index - 1;

            CatalogNode classNode = new CatalogNode();

            if (entries[actualIndex] != null)
            {
                KVObject obj = entries[actualIndex];

                foreach (KVObject child in obj.Children)
                {
                    switch (child.Name)
                    {
                        case "name":
                            classNode.name = child.Value.ToString(CultureInfo.CurrentCulture);
                            break;
                        case "price":
                            classNode.price = child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "limit":
                            classNode.limit = child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "command":
                            {
                                string basecommandString = child.Value.ToString(CultureInfo.CurrentCulture);
                                classNode.command = new Command(basecommandString);
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
