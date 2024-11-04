using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValveKeyValue;

namespace Fabricator
{
    public class RewardList : FileBase
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

        public class RewardNode : BaseNode
        {
            public string name { get; set; } = "";
            public RewardItemTypes itemType { get; set; } = RewardItemTypes.FR_REWARD_INVALID;
            public string weaponClassName { get; set; } = "";
            public BoolInt ammoIsPrimary { get; set; } = BoolInt.Invalid;
            public int ammoNum { get; set; } = -1;
            public RewardPerkTypes perkID { get; set; } = RewardPerkTypes.FIREFIGHT_PERK_INVALID;
            public string command { get; set; } = "";
        }

        public override string Label { get; set; } = "Rewards";

        public RewardList(string filePath) : base(filePath)
        {
        }

        public override KVObject NodetoKVObject(BaseNode node, int index = -1)
        {
            RewardNode classNode = node as RewardNode;

            if (classNode != null)
            {
                if (!string.IsNullOrWhiteSpace(classNode.name))
                {
                    entryStats.Add(new KVObject("name", classNode.name));
                }

                if (classNode.itemType != RewardItemTypes.FR_REWARD_INVALID)
                {
                    entryStats.Add(new KVObject("item_type", (int)classNode.itemType));
                }

                if (!string.IsNullOrWhiteSpace(classNode.weaponClassName))
                {
                    entryStats.Add(new KVObject("weapon_classname", classNode.weaponClassName));
                }

                if (classNode.ammoIsPrimary != BoolInt.Invalid)
                {
                    entryStats.Add(new KVObject("ammo_isprimary", (int)classNode.ammoIsPrimary));
                }

                if (classNode.ammoNum != -1)
                {
                    entryStats.Add(new KVObject("ammo_num", classNode.ammoNum));
                }

                if (classNode.perkID != RewardPerkTypes.FIREFIGHT_PERK_INVALID)
                {
                    entryStats.Add(new KVObject("perk_id", (int)classNode.perkID));
                }

                if (!string.IsNullOrWhiteSpace(classNode.command))
                {
                    entryStats.Add(new KVObject("command", classNode.command));
                }
            }

            return base.NodetoKVObject(node, index);
        }
    }
}
