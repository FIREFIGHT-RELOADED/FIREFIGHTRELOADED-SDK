using System.Globalization;
using ValveKeyValue;

namespace Fabricator
{
    public class ShopCatalog : FileCreatorBase
    {
        public class Command
        {
            public string BaseCommand { get; set; } = "";
            public string NameVal { get; set; } = "";
            public int CountVal { get; set; } = -1;

            public Command()
            {

            }

            public Command(string commandString)
            {
                string[] commandSections = commandString.Split(' ');
                if (commandSections.Length > 0)
                {
                    BaseCommand = commandSections[0];

                    if (commandSections.Length >= 2)
                    {
                        NameVal = commandSections[1];

                        if (commandSections.Length >= 3)
                        {
                            CountVal = Convert.ToInt32(commandSections[2]);
                        }
                    }
                }
            }

            public override string ToString()
            {
                string result = "";

                if (!string.IsNullOrWhiteSpace(BaseCommand))
                {
                    result += BaseCommand;

                    if (!string.IsNullOrWhiteSpace(NameVal))
                    {
                        result += $" {NameVal}";
                    }

                    if (CountVal != -1)
                    {
                        result += $" {CountVal}";
                    }
                }

                return result;
            }
        }

        public class CatalogNode : BaseNode
        {
            public string name { get; set; } = "HealthKit";
            public int preset { get; set; } = -1;
            public int price { get; set; } = 20;
            public int limit { get; set; } = -1;
            public Command? command { get; set; } = new Command("healthkit");
        }

        public override string Label { get; set; } = "ShopCatalog";

        public ShopCatalog() : base()
        {
        }

        public ShopCatalog(string filePath) : base(filePath)
        {
        }

        public override KVObject NodeToKVObject(BaseNode node, int index = -1)
        {
            CatalogNode? classNode = node as CatalogNode;

            if (classNode != null)
            {
                //load every entry into the entrystats list and convert them to KVObjects.
                AddKVObjectEntryStat("name", classNode.name);
                AddKVObjectEntryStat("preset", classNode.preset);
                AddKVObjectEntryStat("price", classNode.price);
                AddKVObjectEntryStat("limit", classNode.limit);
                AddKVObjectEntryStat("command", classNode.command);
            }

            return base.NodeToKVObject(node, index);
        }

        public override CatalogNode KVObjectToNode(int index)
        {
            int actualIndex = index - 1;

            CatalogNode classNode = new CatalogNode();

            if (entries[actualIndex] != null)
            {
                KVObject obj = entries[actualIndex];

                //go through the KVObject's children and fill in the entries of each node.
                foreach (KVObject child in obj.Children)
                {
                    switch (child.Name)
                    {
                        case "name":
                            classNode.name = child.Value.ToString(CultureInfo.CurrentCulture);
                            break;
                        case "preset":
                            classNode.preset = child.Value.ToInt32(CultureInfo.CurrentCulture);
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
