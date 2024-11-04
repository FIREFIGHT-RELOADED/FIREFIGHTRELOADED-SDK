using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ValveKeyValue;

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

        public List<KVObject> entries { get; set; }
        public virtual string Label { get; set; } = "Base";

        //If we have no initalizer, interpret it as just initiallizing the list entries.
        public FileBase()
        {
            entries = new List<KVObject>();
        }

        public FileBase(string filePath)
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

        public virtual KVObject ToKVObject()
        {
            List<KVObject> list = new List<KVObject>();
            list.AddRange(entries);

            KVObject finalFile = new KVObject(Label, list);
            return finalFile;
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
