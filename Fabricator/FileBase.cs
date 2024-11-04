using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ValveKeyValue;
using static Fabricator.ShopCatalog;

namespace Fabricator
{
    public class FileBase
    {
        public enum BoolInt
        {
            Invalid = -1,
            False,
            True
        }

        public class BaseNode
        {
        }

        public List<KVObject> entries { get; set; }
        public List<KVObject> entryStats { get; set; }
        public virtual string Label { get; set; } = "Base";

        public FileBase(string filePath)
        {
            SetupLists();
            LoadFile(filePath);
        }

        public virtual void LoadFile(string filePath)
        {
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

        private void SetupLists()
        {
            entries = new List<KVObject>();
            entryStats = new List<KVObject>();
        }

        public virtual KVObject ToKVObject()
        {
            List<KVObject> list = new List<KVObject>();
            list.AddRange(entries);

            KVObject finalFile = new KVObject(Label, list);
            return finalFile;
        }

        public virtual KVObject NodetoKVObject(BaseNode node, int index = -1)
        {
            //Add at least ONE entry if we have none. Base nodes have no values.
            if (entries.Count <= 0)
            {
                entryStats.Add(new KVObject("HEY", "YOU FORGOT TO ADD SOMETHING"));
            }

            if (index == -1)
            {
                index = entries.Count + 1;
            }

            KVObject kv = new KVObject(index.ToString(), entryStats);

            entryStats.Clear();

            return kv;
        }

        public virtual void AddEntry(BaseNode node)
        {
            entries.Add(NodetoKVObject(node));
        }

        public virtual void RemoveEntry(int index)
        {
            int actualIndex = index - 1;
            entries.RemoveAt(actualIndex);
        }

        public virtual void EditEntry(int index, BaseNode nodeEdited)
        {
            int actualIndex = index - 1;

            if (entries[actualIndex] != null)
            {
                entries[actualIndex] = NodetoKVObject(nodeEdited, index);
            }
        }

        public virtual void Save(string filePath)
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
