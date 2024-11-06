using System.Globalization;
using ValveKeyValue;

namespace Fabricator
{
    public class RewardList : FileCreatorBase
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

        public override KVObject NodeToKVObject(BaseNode node, int index = -1)
        {
            RewardNode classNode = node as RewardNode;

            if (classNode != null)
            {
                AddKVObjectEntryStat("name", classNode.name);

                if (classNode.itemType != RewardItemTypes.FR_REWARD_INVALID)
                {
                    AddKVObjectEntryStat("item_type", (int)classNode.itemType);
                }

                AddKVObjectEntryStat("weapon_classname", classNode.weaponClassName);
                AddKVObjectEntryStat("ammo_isprimary", classNode.ammoIsPrimary);
                AddKVObjectEntryStat("ammo_num", classNode.ammoNum);

                if (classNode.perkID != RewardPerkTypes.FIREFIGHT_PERK_INVALID)
                {
                    AddKVObjectEntryStat("perk_id", (int)classNode.perkID);
                }

                AddKVObjectEntryStat("command", classNode.command);
            }

            return base.NodeToKVObject(node, index);
        }

        public override RewardNode EntryToNode(int index)
        {
            int actualIndex = index - 1;

            RewardNode classNode = new RewardNode();

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
                        case "item_type":
                            classNode.itemType = (RewardItemTypes)child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "weapon_classname":
                            classNode.weaponClassName = child.Value.ToString(CultureInfo.CurrentCulture);
                            break;
                        case "ammo_isprimary":
                            classNode.ammoIsPrimary = (BoolInt)child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "ammo_num":
                            classNode.ammoNum = child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "perk_id":
                            classNode.perkID = (RewardPerkTypes)child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "command":
                            classNode.command = child.Value.ToString(CultureInfo.CurrentCulture);
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
