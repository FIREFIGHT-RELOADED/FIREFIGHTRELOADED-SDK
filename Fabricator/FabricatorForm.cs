using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using ValveKeyValue;
using static Fabricator.ShopCatalog;

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

            Spawnlist.Node node = new Spawnlist.Node
            {
                classname = "npc_TEST",
                preset = 7,
                minLevel = 5,
                wildcard = 1,
                equipment = new Dictionary<string, int>
                {
                    {"weapon_shotgun", 4},
                    {"weapon_test", 8},
                }
            };

            spawnlist.AddEntry(node);
            spawnlist.EditSetting("spawntime", "1");
            spawnlist.AddSetting("test", "5");

            spawnlist.Save("C:\\Users\\Bitl\\Desktop\\test.txt");

            ShopCatalog catalog = new ShopCatalog("D:\\SteamLibrary\\steamapps\\common\\FIREFIGHT RELOADED\\firefightreloaded\\scripts\\shopcatalog_items.txt");

            ShopCatalog.Node shopnode = new ShopCatalog.Node
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

            catalog.Save("C:\\Users\\Bitl\\Desktop\\test2.txt");
        }
    }
}
