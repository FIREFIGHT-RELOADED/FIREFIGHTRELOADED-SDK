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
            public string name { get; set; } = "#GameUI_Store_Buy_HealthKit";
            public RewardItemTypes itemType { get; set; } = RewardItemTypes.FR_HEALTHKIT;
            public string weaponClassName { get; set; } = "";
            public BoolInt ammoIsPrimary { get; set; } = BoolInt.Invalid;
            public int ammoNum { get; set; } = -1;
            public int minLevel { get; set; } = 1;
            public BoolInt classic { get; set; } = BoolInt.Invalid;
            public BoolInt hardcore { get; set; } = BoolInt.Invalid;
            public BoolInt loneWolf { get; set; } = BoolInt.Invalid;
            public BoolInt ironKick { get; set; } = BoolInt.True;
            public RewardPerkTypes perkID { get; set; } = RewardPerkTypes.FIREFIGHT_PERK_INVALID;
            public string command { get; set; } = "";
        }

        public override string Label { get; set; } = "Rewards";

        public RewardList() : base()
        {
        }

        public RewardList(string filePath) : base(filePath)
        {
        }

        public override KVObject NodeToKVObject(BaseNode node, int index = -1)
        {
            RewardNode? classNode = node as RewardNode;

            if (classNode != null)
            {
                //load every entry into the entrystats list and convert them to KVObjects.
                //make sure certain custom values are valid.
                AddKVObjectEntryStat("name", classNode.name);

                if (classNode.itemType != RewardItemTypes.FR_REWARD_INVALID)
                {
                    AddKVObjectEntryStat("item_type", (int)classNode.itemType);
                }

                AddKVObjectEntryStat("weapon_classname", classNode.weaponClassName);
                AddKVObjectEntryStat("ammo_isprimary", classNode.ammoIsPrimary);
                AddKVObjectEntryStat("ammo_num", classNode.ammoNum);
                AddKVObjectEntryStat("min_level", classNode.minLevel);
                AddKVObjectEntryStat("unlocks_in_classic", classNode.classic);
                AddKVObjectEntryStat("unlocks_in_hardcore", classNode.hardcore);
                AddKVObjectEntryStat("unlocks_in_lone_wolf", classNode.loneWolf);
                AddKVObjectEntryStat("unlocks_in_iron_kick", classNode.ironKick);

                if (classNode.perkID != RewardPerkTypes.FIREFIGHT_PERK_INVALID)
                {
                    AddKVObjectEntryStat("perk_id", (int)classNode.perkID);
                }

                AddKVObjectEntryStat("command", classNode.command);
            }

            return base.NodeToKVObject(node, index);
        }

        public override RewardNode KVObjectToNode(int index)
        {
            int actualIndex = index - 1;

            RewardNode classNode = new RewardNode();

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
                        case "min_level":
                            classNode.minLevel = child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "unlocks_in_classic":
                            classNode.classic = (BoolInt)child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "unlocks_in_hardcore":
                            classNode.hardcore = (BoolInt)child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "unlocks_in_lone_wolf":
                            classNode.loneWolf = (BoolInt)child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "unlocks_in_iron_kick":
                            classNode.ironKick = (BoolInt)child.Value.ToInt32(CultureInfo.CurrentCulture);
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
