using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValveKeyValue;

namespace Fabricator
{
    public class RewardList
    {
        //Item Types
        //FR_HEALTHKIT = 1
        //FR_BATTERY = 2
        //FR_HEALTHKIT_BIG = 3
        //FR_BATTERY_BIG = 4
        //FR_WEAPON = 5
        //FR_AMMOWEAPON = 6
        //FR_AMMO = 7
        //FR_PERK = 8
        //FR_KASHBONUS = 9
        //FR_SERVERCMD = 10
        //FR_CLIENTCMD = 11
        public enum RewardItemTypes
        {
            FR_REWARD_INVALID,
            FR_HEALTHKIT,
            FR_BATTERY,
            FR_HEALTHKIT_BIG,
            FR_BATTERY_BIG,
            FR_WEAPON,
            FR_AMMOWEAPON,
            FR_AMMO,
            FR_PERK,
            FR_KASHBONUS,
            FR_SERVERCMD,
            FR_CLIENTCMD
        }

        //Perk IDs
        //FIREFIGHT_PERK_INFINITEAUXPOWER = 1
        //FIREFIGHT_PERK_INFINITEAMMO = 2
        //FIREFIGHT_PERK_HEALTHREGENERATIONRATE = 3
        //FIREFIGHT_PERK_HEALTHREGENERATION = 4

        public enum RewardPerkTypes
        {
            FIREFIGHT_PERK_INVALID,
            FIREFIGHT_PERK_INFINITEAUXPOWER,
            FIREFIGHT_PERK_INFINITEAMMO,
            FIREFIGHT_PERK_HEALTHREGENERATIONRATE,
            FIREFIGHT_PERK_HEALTHREGENERATION
        }

        public enum BoolInt
        {
            Invalid = -1,
            False,
            True
        }

        public class Node
        {
            public string name { get; set; } = "";
            public RewardItemTypes itemType { get; set; } = RewardItemTypes.FR_REWARD_INVALID;
            public string weaponClassName { get; set; } = "";
            public BoolInt ammoIsPrimary { get; set; } = BoolInt.Invalid;
            public int ammoNum { get; set; } = -1;
            public RewardPerkTypes perkID { get; set; } = RewardPerkTypes.FIREFIGHT_PERK_INVALID;
            public string command { get; set; } = "";
        }

        List<KVObject> entries { get; set; }

        public RewardList(string filePath)
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

        public KVObject NodetoKVObject(Node node, int index = -1)
        {
            List<KVObject> entryStats = new List<KVObject>();

            if (!string.IsNullOrWhiteSpace(node.name))
            {
                entryStats.Add(new KVObject("name", node.name));
            }

            if (node.itemType != RewardItemTypes.FR_REWARD_INVALID)
            {
                entryStats.Add(new KVObject("item_type", (int)node.itemType));
            }

            if (!string.IsNullOrWhiteSpace(node.weaponClassName))
            {
                entryStats.Add(new KVObject("weapon_classname", node.weaponClassName));
            }

            if (node.ammoIsPrimary != BoolInt.Invalid)
            {
                entryStats.Add(new KVObject("ammo_isprimary", (int)node.ammoIsPrimary));
            }

            if (node.ammoNum != -1)
            {
                entryStats.Add(new KVObject("ammo_num", node.ammoNum));
            }

            if (node.perkID != RewardPerkTypes.FIREFIGHT_PERK_INVALID)
            {
                entryStats.Add(new KVObject("perk_id", (int)node.perkID));
            }

            if (!string.IsNullOrWhiteSpace(node.command))
            {
                entryStats.Add(new KVObject("command", node.command));
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

        public KVObject ToKVObject()
        {
            List<KVObject> list = new List<KVObject>();
            list.AddRange(entries);

            KVObject finalFile = new KVObject("Rewards", list);
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
