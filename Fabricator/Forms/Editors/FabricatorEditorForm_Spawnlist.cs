using System;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;
using ValveKeyValue;

namespace Fabricator
{
    public partial class FabricatorEditorForm_Spawnlist : Form
    {
        Spawnlist curFile { get; set; }
        int nodeIndex { get; set; }

        public FabricatorEditorForm_Spawnlist()
        {
            InitializeComponent();

            curFile = new Spawnlist();
            nodeIndex = -1;

            openFileDialog1.Filter = saveFileDialog1.Filter = $"Spawnlist Text Files|*.txt|All Files|*.*";
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FabricatorEditorFormHelpers.Clear(KeyValueSet, NodeList);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var ofd = openFileDialog1)
            {
                ofd.FileName = "";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    nodeIndex = -1;
                    FabricatorEditorFormHelpers.Clear(KeyValueSet, NodeList);
                    curFile = new Spawnlist(ofd.FileName);
                    FabricatorEditorFormHelpers.ReloadNodeList(NodeList, curFile);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var sfd = saveFileDialog1)
            {
                sfd.FileName = "";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    FabricatorEditorFormHelpers.SaveLastCells(KeyValueSet, NodeList, nodeIndex, curFile);
                    curFile.Save(sfd.FileName);
                }
            }
        }

        private void createRowFromKeyListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FabricatorEditorFormHelpers.IsNodeOpen(NodeList))
            {
                using (var kvl = new FabricatorKeyvalueLoader())
                {
                    if (kvl.ShowDialog() == DialogResult.OK)
                    {
                        KeyValueSet.Rows.Add(kvl.selectedKey);
                    }
                }
            }
        }

        private void addNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Spawnlist.SpawnlistNode node = new Spawnlist.SpawnlistNode();
            curFile.AddEntry(node);
            FabricatorEditorFormHelpers.ReloadNodeList(NodeList, curFile);
        }

        private void deleteNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FabricatorEditorFormHelpers.DeleteNode(NodeList, KeyValueSet, curFile);
            nodeIndex = -1;
        }

        private void NodeList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (nodeIndex == -2)
            {
                FabricatorEditorFormHelpers.SaveSettingsFromLastCells(KeyValueSet, NodeList, nodeIndex, curFile);
            }
            else
            {
                FabricatorEditorFormHelpers.SaveLastCells(KeyValueSet, NodeList, nodeIndex, curFile);
            }

            KeyValueSet.Rows.Clear();

            if (!NodeList.SelectedNode.Text.Contains("settings"))
            {
                nodeIndex = e.Node.Index;
                KVObject kv = curFile.entries[nodeIndex];

                if (kv != null)
                {
                    foreach (var child in kv.Children)
                    {
                        KeyValueSet.Rows.Add(child.Name, child.Value);
                    }
                }
            }
            else
            {
                nodeIndex = -2;
                foreach (KVObject child in curFile.settings)
                {
                    KeyValueSet.Rows.Add(child.Name, child.Value);
                }
            }
        }

        //todo: fix this fucking thing
        private void editCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (KeyValueSet.SelectedRows.Count > 0)
            {
                object name = KeyValueSet.SelectedRows[0].Cells[0].Value;
                object val = KeyValueSet.SelectedRows[0].Cells[1].Value;
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
                                    KeyValueSet.SelectedRows[0].Cells[0].Value = kvl.objToEdit.Name;
                                    KeyValueSet.SelectedRows[0].Cells[1].Value = kvl.objToEdit.Value;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
