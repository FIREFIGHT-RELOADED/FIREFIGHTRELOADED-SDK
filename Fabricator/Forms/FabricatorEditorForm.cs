
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValveKeyValue;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Fabricator
{
    public partial class FabricatorEditorForm : Form
    {
        string kvName { get; set; }
        FileCreatorBase fileCreator { get; set; }
        Type fileType { get; set; }

        public FabricatorEditorForm(Type type)
        {
            InitializeComponent();

            fileType = type;

            switch (fileType)
            {
                case Type.Spawnlist:
                    fileCreator = new Spawnlist();
                    break;
                case Type.Loadout:
                    fileCreator = new Loadout();
                    break;
                case Type.Playlist:
                    fileCreator = new Playlist();
                    break;
                case Type.Catalog:
                    fileCreator = new ShopCatalog();
                    break;
                case Type.Reward:
                    fileCreator = new RewardList();
                    break;
                case Type.MapAdd:
                    fileCreator = new MapAdd();
                    break;
                default:
                    break;
            }

            openFileDialog1.Filter = saveFileDialog1.Filter = $"{fileType.ToString()} Text Files|*.txt|All Files|*.*";
        }

        private void FabricatorOtherForm_Load(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KeyValueSet.Rows.Clear();
            NodeList.Nodes.Clear();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var ofd = openFileDialog1)
            {
                ofd.FileName = "";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    switch (fileType)
                    {
                        case Type.Spawnlist:
                            fileCreator = new Spawnlist(ofd.FileName);
                            break;
                        case Type.Loadout:
                            fileCreator = new Loadout(ofd.FileName);
                            break;
                        case Type.Playlist:
                            fileCreator = new Playlist(ofd.FileName);
                            break;
                        case Type.Catalog:
                            fileCreator = new ShopCatalog(ofd.FileName);
                            break;
                        case Type.Reward:
                            fileCreator = new RewardList(ofd.FileName);
                            break;
                        case Type.MapAdd:
                            fileCreator = new MapAdd(ofd.FileName);
                            break;
                        default:
                            break;
                    }

                    KVObject? kv = fileCreator.rawData;

                    if (kv != null)
                    {
                        KVNameBox.Text = kv.Name;

                        int index = 1;

                        foreach (KVObject child in kv.Children)
                        {
                            if (fileCreator.FileUsesSettings)
                            {
                                //this SHOULD be the first one.....
                                //if it isn't, we shouldn't worry because 
                                //index doesn't increment.
                                if (child.Name == "settings")
                                {
                                    NodeList.Nodes.Add($"{child.Name}");
                                    continue;
                                }
                            }

                            NodeList.Nodes.Add($"{child.Name} ({index})");
                            index++;
                        }
                    }
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(KVNameBox.Text))
            {
                DialogResult result = MessageBox.Show("You are about to save the file without a name for the KeyValues file. Would you like to fix it? If not, the file will save with the assigned file name for the KeyValues file.", "Fabricator", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    return;
                }
            }

            using (var sfd = saveFileDialog1)
            {
                sfd.FileName = "";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    List<KVObject> list = new List<KVObject>();

                    foreach (DataGridViewRow row in KeyValueSet.Rows)
                    {
                        if (row.Cells[0].Value == null)
                        {
                            continue;
                        }

                        if (row.Cells[1].Value == null)
                        {
                            DialogResult result = MessageBox.Show($"Row #{row.Index + 1} '{row.Cells[0].Value}' has a missing value, or the value is being edited. Would you like to fix it? If not, the row will not be included in the saved file.", "Fabricator", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                return;
                            }
                            else
                            {
                                continue;
                            }
                        }

                        list.Add(new KVObject(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString()));
                    }

                    if (string.IsNullOrWhiteSpace(kvName))
                    {
                        kvName = Path.GetFileNameWithoutExtension(sfd.FileName);
                    }

                    KVObject finalFile = new KVObject(kvName, list);

                    //For some strange reason, the data will save DIRECTLY into a file if it exists.
                    //So, we should remove it so the file can actually work.
                    if (File.Exists(sfd.FileName))
                    {
                        File.Delete(sfd.FileName);
                    }

                    KVSerializer kv = KVSerializer.Create(KVSerializationFormat.KeyValues1Text);
                    using (FileStream stream = File.OpenWrite(sfd.FileName))
                    {
                        kv.Serialize(stream, finalFile);
                    }
                }
            }
        }

        private void createRowFromKeyListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var kvl = new FabricatorKeyvalueLoader())
            {
                if (kvl.ShowDialog() == DialogResult.OK)
                {
                    KeyValueSet.Rows.Add(kvl.selectedKey);
                }
            }
        }

        private void KVNameBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(KVNameBox.Text))
            {
                kvName = KVNameBox.Text;
            }
        }

        private void NodeList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            KeyValueSet.Rows.Clear();


        }

        private void KeyValueSet_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
