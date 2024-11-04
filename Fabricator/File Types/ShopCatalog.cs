using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public class Node
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

        public KVObject NodetoKVObject(Node node, int index = -1)
        {
            List<KVObject> entryStats = new List<KVObject>();

            if (!string.IsNullOrWhiteSpace(node.name))
            {
                entryStats.Add(new KVObject("name", node.name));
            }

            if (node.price != -1)
            {
                entryStats.Add(new KVObject("price", node.price));
            }

            if (node.limit != -1)
            {
                entryStats.Add(new KVObject("limit", node.limit));
            }

            if (node.command != null)
            {
                entryStats.Add(new KVObject("command", node.command.ToString()));
            }

            if (index == -1)
            {
                index = entries.Count + 1;
            }

            KVObject kv = new KVObject(index.ToString(), entryStats);

            return kv;
        }

        public void AddEntry(Node node)
        {
            entries.Add(NodetoKVObject(node));
        }

        public void RemoveEntry(int index)
        {
            int actualIndex = index - 1;
            entries.RemoveAt(actualIndex);
        }

        public void EditEntry(int index, Node nodeEdited)
        {
            int actualIndex = index - 1;

            if (entries[actualIndex] != null)
            {
                entries[actualIndex] = NodetoKVObject(nodeEdited, index);
            }
        }
    }
}
