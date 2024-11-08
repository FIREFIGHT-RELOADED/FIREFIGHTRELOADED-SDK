using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using ValveKeyValue;
using System.Reflection;

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
                exp = 35,
                equipment = new Dictionary<string, float>
                {
                    {"weapon_shotgun", 4},
                    {"weapon_test", 8}
                }
            };

            spawnlist.EditEntry(8, editednode);

            Spawnlist.SpawnlistNode editednode1 = spawnlist.EntryToNode(8);
            editednode1.equipment.Add("weapon_dubstepgun", 10);
            spawnlist.EditEntry(8, editednode1);

            spawnlist.EditSetting(new KVObject("spawntime", "1"));
            spawnlist.RemoveSetting("spawntime_steamdeck");
            spawnlist.AddSetting(new KVObject("test", 5));

            Spawnlist.SpawnlistNode editednode2 = spawnlist.EntryToNode(1);
            editednode2.classname = "npc_combine_s";
            spawnlist.EditEntry(1, editednode2);

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

            ShopCatalog.CatalogNode shopnode2 = catalog.EntryToNode(5);
            shopnode2.name = "TESTY";
            catalog.EditEntry(5, shopnode2);

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

            Loadout loadout = new Loadout("D:\\SteamLibrary\\steamapps\\common\\FIREFIGHT RELOADED\\firefightreloaded\\scripts\\loadouts\\snipers.txt");
            Loadout.LoadoutNode nodeLoadout = loadout.EntryToNode(2);
            nodeLoadout.health = 1000;
            loadout.AddEntry(nodeLoadout);

            loadout.Save("C:\\Users\\Bitl\\Desktop\\test4.txt");

            Playlist playlist = new Playlist("D:\\SteamLibrary\\steamapps\\common\\FIREFIGHT RELOADED\\firefightreloaded\\scripts\\playlists\\firefight-reloaded-endgame.txt");
            Playlist.PlaylistNode nodePlaylist = playlist.EntryToNode(2);
            nodePlaylist.title = "The Foundations of Decay";
            nodePlaylist.artist = "My Chemical Romance";
            playlist.AddEntry(nodePlaylist);

            playlist.AddSetting(new KVObject("shuffle", 3));
            playlist.ToggleSettings();
            playlist.AddSetting(new KVObject("shuffle", "1"));

            playlist.Save("C:\\Users\\Bitl\\Desktop\\test5.txt");

            MapAdd mapaddtest = new MapAdd("D:\\SteamLibrary\\steamapps\\common\\FIREFIGHT RELOADED\\firefightreloaded\\scripts\\mapadd\\dm_halls3.txt");
            MapAdd.MapAddLabel label = mapaddtest.EntryToNode(1);
            label.labelNodes[0].keyValues["targetname"] = "test";
            label.labelNodes[0].roll = 45;
            mapaddtest.EditEntry(1, label);

            mapaddtest.Save("C:\\Users\\Bitl\\Desktop\\test6.txt");
        }
    }
}
