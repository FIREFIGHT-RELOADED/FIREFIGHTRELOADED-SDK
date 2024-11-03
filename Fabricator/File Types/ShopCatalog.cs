using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValveKeyValue;
using static Fabricator.Spawnlist;

namespace Fabricator
{
    public class ShopCatalog
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

        List<KVObject> entries { get; set; }

        public ShopCatalog(string filePath)
        {
            entries = new List<KVObject>();

            using (var stream = File.OpenRead(filePath))
            {
                KVSerializer kv = KVSerializer.Create(KVSerializationFormat.KeyValues1Text);
                KVObject data = kv.Deserialize(stream);
                foreach (KVObject item in data)
                {
                    entries.Add(item);
                }
            }
        }

        public void AddEntry(Node node)
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

            var kv = new KVObject((entries.Count + 1).ToString(), entryStats);

            entries.Add(kv);
        }

        public KVObject ToKVObject()
        {
            List<KVObject> list = new List<KVObject>();
            list.AddRange(entries);

            KVObject finalFile = new KVObject("ShopCatalog", list);
            return finalFile;
        }

        public void Save(string filePath)
        {
            KVObject finalFile = ToKVObject();
            KVSerializer kv = KVSerializer.Create(KVSerializationFormat.KeyValues1Text);
            using (FileStream stream = File.OpenWrite(filePath))
            {
                kv.Serialize(stream, finalFile);
            }
        }
    }
}
