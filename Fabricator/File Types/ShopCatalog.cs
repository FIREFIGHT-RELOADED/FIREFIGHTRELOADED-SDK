using ValveKeyValue;

namespace Fabricator
{
    public class ShopCatalog : FileBase
    {
        public class Command
        {
            public string BaseCommand { get; set; }
            public string NameVal { get; set; }
            public int CountVal { get; set; }

            public override string ToString()
            {
                return $"{BaseCommand} {NameVal} {CountVal}";
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
                if (!string.IsNullOrWhiteSpace(classNode.name))
                {
                    entryStats.Add(new KVObject("name", classNode.name));
                }

                if (classNode.price != -1)
                {
                    entryStats.Add(new KVObject("price", classNode.price));
                }

                if (classNode.limit != -1)
                {
                    entryStats.Add(new KVObject("limit", classNode.limit));
                }

                if (classNode.command != null)
                {
                    entryStats.Add(new KVObject("command", classNode.command.ToString()));
                }
            }

            return base.NodeToKVObject(node, index);
        }
    }
}
