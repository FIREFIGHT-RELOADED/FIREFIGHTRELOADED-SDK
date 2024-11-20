using System.Globalization;
using ValveKeyValue;

namespace Fabricator
{
    public class Loadout : FileCreatorBase
    {
        public class LoadoutNode : BaseNode
        {
            public BoolInt hardcore { get; set; } = BoolInt.Invalid;
            public BoolInt nodisconnect { get; set; } = BoolInt.Invalid;
            public string weapon { get; set; } = "weapon_smg1";
            public int weaponpreset { get; set; } = -1;
            public string ammotype { get; set; } = "SMG1";
            public int ammonum { get; set; } = 180;
            public string ammo2type { get; set; } = "SMG1_Grenade";
            public int ammo2num { get; set; } = 3;
            public int health { get; set; } = -1;
            public BoolInt setmaxhealth { get; set; } = BoolInt.Invalid;
            public BoolInt incrementhealth { get; set; } = BoolInt.Invalid;
            public BoolInt disableincrementhealthmax { get; set; } = BoolInt.Invalid;
            public BoolInt resetupgrades { get; set; } = BoolInt.Invalid;
            public int armor { get; set; } = -1;
            public BoolInt setmaxarmor { get; set; } = BoolInt.Invalid;
            public BoolInt incrementarmor { get; set; } = BoolInt.Invalid;
            public BoolInt disableincrementarmormax { get; set; } = BoolInt.Invalid;
            public int level { get; set; } = -1;
            public int kash { get; set; } = -1;
            public BoolInt ironkick { get; set; } = BoolInt.Invalid;
            public BoolInt preventweaponpickup { get; set; } = BoolInt.Invalid;
        }

        public override bool SetLabelToFileName { get; set; } = true;

        public Loadout() : base()
        {
        }

        public Loadout(string filePath) : base(filePath)
        {
        }

        public override KVObject NodeToKVObject(BaseNode node, int index = -1)
        {
            LoadoutNode classNode = node as LoadoutNode;

            if (classNode != null)
            {
                AddKVObjectEntryStat("hardcore", classNode.hardcore);
                AddKVObjectEntryStat("nodisconnect", classNode.nodisconnect);
                AddKVObjectEntryStat("weapon", classNode.weapon);
                AddKVObjectEntryStat("weaponpreset", classNode.weaponpreset);
                AddKVObjectEntryStat("ammotype", classNode.ammotype);
                AddKVObjectEntryStat("ammonum", classNode.ammonum);
                AddKVObjectEntryStat("ammo2type", classNode.ammo2type);
                AddKVObjectEntryStat("ammo2num", classNode.ammo2num);
                AddKVObjectEntryStat("health", classNode.health);
                AddKVObjectEntryStat("setmaxhealth", classNode.setmaxhealth);
                AddKVObjectEntryStat("incrementhealth", classNode.incrementhealth);
                AddKVObjectEntryStat("disableincrementhealthmax", classNode.disableincrementhealthmax);
                AddKVObjectEntryStat("resetupgrades", classNode.resetupgrades);
                AddKVObjectEntryStat("armor", classNode.armor);
                AddKVObjectEntryStat("setmaxarmor", classNode.setmaxarmor);
                AddKVObjectEntryStat("incrementarmor", classNode.incrementarmor);
                AddKVObjectEntryStat("disableincrementarmormax", classNode.disableincrementarmormax);
                AddKVObjectEntryStat("level", classNode.level);
                AddKVObjectEntryStat("kash", classNode.kash);
                AddKVObjectEntryStat("ironkick", classNode.ironkick);
                AddKVObjectEntryStat("preventweaponpickup", classNode.preventweaponpickup);
            }

            return base.NodeToKVObject(node, index);
        }

        public override LoadoutNode KVObjectToNode(int index)
        {
            int actualIndex = index - 1;

            LoadoutNode classNode = new LoadoutNode();

            if (entries[actualIndex] != null)
            {
                KVObject obj = entries[actualIndex];

                foreach (KVObject child in obj.Children)
                {
                    switch (child.Name)
                    {
                        case "hardcore":
                            classNode.hardcore = (BoolInt)child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "nodisconnect":
                            classNode.nodisconnect = (BoolInt)child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "weapon":
                            classNode.weapon = child.Value.ToString(CultureInfo.CurrentCulture);
                            break;
                        case "weaponpreset":
                            classNode.weaponpreset = child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "ammotype":
                            classNode.ammotype = child.Value.ToString(CultureInfo.CurrentCulture);
                            break;
                        case "ammonum":
                            classNode.ammonum = child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "ammo2type":
                            classNode.ammo2type = child.Value.ToString(CultureInfo.CurrentCulture);
                            break;
                        case "ammo2num":
                            classNode.ammo2num = child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "health":
                            classNode.health = child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "setmaxhealth":
                            classNode.setmaxhealth = (BoolInt)child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "incrementhealth":
                            classNode.incrementhealth = (BoolInt)child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "disableincrementhealthmax":
                            classNode.disableincrementhealthmax = (BoolInt)child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "resetupgrades":
                            classNode.resetupgrades = (BoolInt)child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "armor":
                            classNode.armor = child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "setmaxarmor":
                            classNode.setmaxarmor = (BoolInt)child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "incrementarmor":
                            classNode.incrementarmor = (BoolInt)child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "disableincrementarmormax":
                            classNode.disableincrementarmormax = (BoolInt)child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "level":
                            classNode.level = child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "kash":
                            classNode.kash = child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "ironkick":
                            classNode.ironkick = (BoolInt)child.Value.ToInt32(CultureInfo.CurrentCulture);
                            break;
                        case "preventweaponpickup":
                            classNode.preventweaponpickup = (BoolInt)child.Value.ToInt32(CultureInfo.CurrentCulture);
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
