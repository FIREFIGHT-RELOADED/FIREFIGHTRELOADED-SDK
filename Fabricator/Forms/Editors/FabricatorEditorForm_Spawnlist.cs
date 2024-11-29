using System;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;
using System.Xml.Linq;
using ValveKeyValue;

namespace Fabricator
{
    public partial class FabricatorEditorForm_Spawnlist : Form
    {
        Spawnlist curFile { get; set; }
        int nodeIndex { get; set; }
        string savedFileName { get; set; } = "";

        public FabricatorEditorForm_Spawnlist()
        {
            InitializeComponent();
            CenterToScreen();

            curFile = new Spawnlist();
            nodeIndex = -1;

            openFileDialog1.Filter = saveFileDialog1.Filter = $"Spawnlist Text Files|*.txt|All Files|*.*";
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedFileName = "";
            FabricatorEditorFormHelpers.Clear(KeyValueSet, NodeList, curFile);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var ofd = openFileDialog1)
            {
                ofd.FileName = "";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    nodeIndex = -1;
                    FabricatorEditorFormHelpers.Clear(KeyValueSet, NodeList, curFile);
                    savedFileName = ofd.SafeFileName;
                    curFile = new Spawnlist(ofd.FileName);
                    FabricatorEditorFormHelpers.ReloadNodeList(NodeList, curFile);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var sfd = saveFileDialog1)
            {
                sfd.FileName = savedFileName;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (FabricatorEditorFormHelpers.SaveLastCells(KeyValueSet, NodeList, nodeIndex, curFile) != null)
                    {
                        savedFileName = Path.GetFileName(sfd.FileName);
                        curFile.Save(sfd.FileName);
                    }
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
                        //this code determines the data type based on the schema. If it's a collection, make it read-only.

                        string type = kvl.selectedValType;
                        object? res = LocalVars.DataTypeForString(type);

                        int index = KeyValueSet.Rows.Add(kvl.selectedKey, res);
                        DataGridViewRow? row = KeyValueSet.Rows[index];

                        if (row != null)
                        {
                            //set the collection to read-only.
                            if (kvl.selectedValType.Contains("Collection", StringComparison.CurrentCultureIgnoreCase))
                            {
                                row.ReadOnly = true;
                            }
                        }
                    }
                }
            }
        }

        private void addNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Spawnlist.SpawnlistNode node = new Spawnlist.SpawnlistNode();
            int count = curFile.entries.Count;
            curFile.AddEntry(node);
            FabricatorEditorFormHelpers.ReloadNodeList(NodeList, curFile);
            NodeList.SelectedNode = NodeList.Nodes[count];
        }

        private void deleteNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FabricatorEditorFormHelpers.DeleteNode(NodeList, KeyValueSet, curFile);
            nodeIndex = -1;
        }

        private void NodeList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            FabricatorEditorFormHelpers.SaveLastCells(KeyValueSet, NodeList, nodeIndex, curFile);

            KeyValueSet.Rows.Clear();

            if (e.Node != null)
            {
                nodeIndex = e.Node.Index;
                KVObject kv = curFile.entries[nodeIndex];

                if (kv != null)
                {
                    foreach (var child in kv.Children)
                    {
                        //This code is used for making collections read only when we load a node.
                        //this shouldn't fail...hopefully.
                        int index = KeyValueSet.Rows.Add(child.Name, child.Value);
                        DataGridViewRow? row = KeyValueSet.Rows[index];

                        if (row != null)
                        {
                            //set the collection to read-only.
                            if (child.Value.ValueType == KVValueType.Collection)
                            {
                                row.ReadOnly = true;
                            }
                        }
                    }
                }
            }
        }

        private void KeyValueSet_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (nodeIndex > -1)
            {
                FabricatorEditorFormHelpers.AddCollection(KeyValueSet, nodeIndex, curFile, e.RowIndex, e.ColumnIndex);
            }
        }

        private void KeyValueSet_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (nodeIndex > -1)
            {
                FabricatorEditorFormHelpers.EditCollection(KeyValueSet, nodeIndex, curFile, e.RowIndex, e.ColumnIndex);
            }
        }

        private void editSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FabricatorEditorFormHelpers.EditSettings(curFile);
        }

        private void moveNodeUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FabricatorEditorFormHelpers.MoveNode(NodeList, KeyValueSet, curFile);
        }

        private void moveNodeDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FabricatorEditorFormHelpers.MoveNode(NodeList, KeyValueSet, curFile, true);
        }
    }
}
