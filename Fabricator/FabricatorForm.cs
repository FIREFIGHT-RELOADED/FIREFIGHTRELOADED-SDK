using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using ValveKeyValue;

namespace Fabricator
{
    public partial class FabricatorForm : Form
    {

        public FabricatorForm()
        {
            InitializeComponent();
        }

        private void FabricatorForm_Load(object sender, EventArgs e)
        {

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var ofd = openFileDialog1)
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var sfd = saveFileDialog1)
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //spawnlist test
            Spawnlist spawnlist = new Spawnlist("D:\\SteamLibrary\\steamapps\\common\\FIREFIGHT RELOADED\\firefightreloaded\\scripts\\spawnlists\\default.txt");

            Spawnlist.SpawnlistNode node = new Spawnlist.SpawnlistNode
            {
                classname = "npc_TEST",
                preset = 7,
                minLevel = 5,
                wildcard = 1,
                grenades = new Spawnlist.GrenadeEntry(0,3),
                mapspawn = new List<string>
                {
                    "gm_flatgrass",
                    "dm_gayclub"
                },
                equipment = new Dictionary<string, float>
                {
                    {"weapon_shotgun", 4},
                    {"weapon_test", 8}
                }
            };

            spawnlist.AddEntry(node);

            Spawnlist.SpawnlistNode editednode = new Spawnlist.SpawnlistNode
            {
                classname = "npc_hgrunt",
                minLevel = 4,
                rare = Spawnlist.BoolInt.True,
                exp = 35
            };

            spawnlist.EditEntry(8, editednode);

            spawnlist.EditSetting("spawntime", "1");
            spawnlist.RemoveSetting("spawntime_steamdeck");
            spawnlist.AddSetting("test", "5");

            spawnlist.Save("C:\\Users\\Bitl\\Desktop\\test.txt");

            ShopCatalog catalog = new ShopCatalog("D:\\SteamLibrary\\steamapps\\common\\FIREFIGHT RELOADED\\firefightreloaded\\scripts\\shopcatalog_items.txt");

            ShopCatalog.CatalogNode shopnode = new ShopCatalog.CatalogNode
            {
                name = "test",
                price = -1,
                limit = -1,
                command = new ShopCatalog.Command
                {
                    BaseCommand = "Test1",
                    NameVal = "TestName1",
                    CountVal = 1,
                },
            };

            catalog.AddEntry(shopnode);
            catalog.EditEntry(4, shopnode);

            catalog.Save("C:\\Users\\Bitl\\Desktop\\test2.txt");

            RewardList reward = new RewardList("D:\\SteamLibrary\\steamapps\\common\\FIREFIGHT RELOADED\\firefightreloaded\\scripts\\rewards_weapons.txt");
            RewardList.RewardNode rewardNode = new RewardList.RewardNode
            {
                name = "TestReward",
                itemType = RewardList.RewardItemTypes.FR_CLIENTCMD,
                command = "sv_cheats 1;noclip"
            };

            reward.AddEntry(rewardNode);
            reward.EditEntry(4, rewardNode);
            reward.RemoveEntry(1);
            reward.AddEntry(rewardNode);

            reward.Save("C:\\Users\\Bitl\\Desktop\\test3.txt");
        }
    }
}
