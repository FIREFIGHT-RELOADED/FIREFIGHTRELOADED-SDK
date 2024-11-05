using System.Globalization;
using ValveKeyValue;

namespace Fabricator
{
    public class ShopCatalog : FileBase
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
            public string name { get; set; } = "";
            public int price { get; set; } = -1;
            public int limit { get; set; } = -1;
            public Command? command { get; set; } = null;
        }

        public override string Label { get; set; } = "ShopCatalog";

        public ShopCatalog(string filePath) : base(filePath)
        {
        }

        public override KVObject NodeToKVObject(BaseNode node, int index = -1)
        {
            CatalogNode classNode = node as CatalogNode;

            if (classNode != null)
            {
                AddKVObjectEntryStat("name", classNode.name);
                AddKVObjectEntryStat("price", classNode.price);
                AddKVObjectEntryStat("limit", classNode.limit);
                AddKVObjectEntryStat("command", classNode.command);
            }

            return base.NodeToKVObject(node, index);
        }

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
