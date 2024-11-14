
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValveKeyValue;

/*
 * TODO:
 * make it so this file USES THE NODES INSTEAD OF KVOBJECTS. we may need to rewrite all of this....
 * 
 * make edits get sent to the fileCreator object after exiting cell
 * allow the fileCreator object to save the file.
 */

namespace Fabricator
{
    public partial class FabricatorEditorForm : Form
    {
        string kvName { get; set; }
        FileCreatorBase fileCreator { get; set; }
        Type fileType { get; set; }
        int savedNodeIndex { get; set; }

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

            Text = $"{Text} - {fileType.ToString()}";
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
                    NodeList.Nodes.Clear();

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

                            if (fileType == Type.MapAdd)
                            {
                                int subindex = 1;

                                TreeNode root = NodeList.Nodes.Add($"{child.Name} ({index})");
                                if (child.Children.Count() > 0)
                                {
                                    foreach (KVObject child2 in child.Children)
                                    {
                                        if (child2.Value.ValueType == KVValueType.Collection)
                                        {
                                            root.Nodes.Add($"{child2.Name} ({index}.{subindex})");
                                            subindex++;
                                        }
                                    }

                                    subindex = 1;
                                }
                            }

                            index++;
                        }

                        NodeList.ExpandAll();
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
            if (fileCreator != null)
            {
                KeyValueSet.Rows.Clear();

                if (!NodeList.SelectedNode.Text.Contains("settings"))
                {
                    try
                    {
                        string cleanedString = NodeList.SelectedNode.Text.Substring(0, NodeList.SelectedNode.Text.Length - 4).TrimEnd();

                        savedNodeIndex = fileCreator.entries.FindIndex(x => x.Name == cleanedString);

                        if (fileCreator.entries[savedNodeIndex] != null)
                        {
                            foreach (KVObject child in fileCreator.entries[savedNodeIndex].Children)
                            {
                                KeyValueSet.Rows.Add(child.Name, child.Value);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        if (fileType == Type.MapAdd)
                        {
                            string indexString = NodeList.SelectedNode.Text.Split(" ")[1].Replace("(", "").Replace(")", "");
                            string[] splitIndex = indexString.Split(".");
                            savedNodeIndex = Convert.ToInt32(splitIndex[0]);
                            int index2 = Convert.ToInt32(splitIndex[1]) - 1;

                            MapAdd? mapadd = fileCreator as MapAdd;

                            if (mapadd != null)
                            {
                                MapAdd.MapAddLabel label = mapadd.EntryToNode(savedNodeIndex);

                                if (label != null && label.labelNodes != null)
                                {
                                    foreach (MapAdd.MapAddLabelNode node in label.labelNodes)
                                    {
                                        if (label.labelNodes.IndexOf(node) == index2)
                                        {
                                            KeyValueSet.Rows.Add("x", node.x);
                                            KeyValueSet.Rows.Add("y", node.y);
                                            KeyValueSet.Rows.Add("z", node.z);
                                            KeyValueSet.Rows.Add("roll", node.roll);
                                            KeyValueSet.Rows.Add("yaw", node.yaw);
                                            KeyValueSet.Rows.Add("pitch", node.pitch);
                                            KeyValueSet.Rows.Add("KeyValues", node.keyValues.Value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (KVObject child in fileCreator.settings)
                    {
                        KeyValueSet.Rows.Add(child.Name, child.Value);
                    }
                }
            }
        }

        private void KeyValueSet_CellLeave(object sender, EventArgs e)
        {

        }

        private void KeyValueSet_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void KeyValueSet_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            object name = KeyValueSet.Rows[e.RowIndex].Cells[0].Value;
            object val = KeyValueSet.Rows[e.RowIndex].Cells[1].Value;
            if (val != null)
            {
                KVValue kVObj = val as KVValue;
                if (kVObj != null)
                {
                    if (kVObj.ValueType == KVValueType.Collection)
                    {
                        KVObject genObj = new KVObject(name.ToString(), kVObj);
                        using (var kvl = new FabricatorCollectionEditor(genObj))
                        {
                            if (kvl.ShowDialog() == DialogResult.OK)
                            {
                                KeyValueSet.Rows[e.RowIndex].Cells[0].Value = kvl.objToEdit.Name;
                                KeyValueSet.Rows[e.RowIndex].Cells[1].Value = kvl.objToEdit.Value;
                            }
                        }
                    }
                }
            }
        }
    }
}
