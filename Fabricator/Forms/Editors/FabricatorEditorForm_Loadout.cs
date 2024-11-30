using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using ValveKeyValue;

namespace Fabricator
{
    public partial class FabricatorEditorForm_Loadout : Form
    {
        Loadout curFile { get; set; }
        int nodeIndex { get; set; }
        string savedFileName { get; set; } = "";

        public FabricatorEditorForm_Loadout()
        {
            InitializeComponent();
            CenterToScreen();

            curFile = new Loadout();
            nodeIndex = -1;

            openFileDialog1.Filter = saveFileDialog1.Filter = $"Loadout Text Files|*.txt|All Files|*.*";
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nodeIndex = -1;
            savedFileName = "";
            LocalFuncs.Clear(KeyValueSet, NodeList, curFile);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var ofd = openFileDialog1)
            {
                ofd.FileName = "";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    nodeIndex = -1;
                    LocalFuncs.Clear(KeyValueSet, NodeList, curFile);
                    savedFileName = ofd.SafeFileName;
                    curFile = new Loadout(ofd.FileName);
                    LocalFuncs.ReloadNodeList(NodeList, curFile);
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
                    if (LocalFuncs.SaveLastCells(KeyValueSet, NodeList, nodeIndex, curFile) != null)
                    {
                        savedFileName = Path.GetFileName(sfd.FileName);
                        curFile.Save(sfd.FileName);
                    }
                }
            }
        }

        private void createRowFromKeyListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LocalFuncs.IsNodeOpen(NodeList))
            {
                LocalFuncs.AddKeyValue(KeyValueSet);
            }
        }

        private void addNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Loadout.LoadoutNode node = new Loadout.LoadoutNode();
            LocalFuncs.AddNode(NodeList, KeyValueSet, curFile, node);
        }

        private void deleteNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalFuncs.DeleteNode(NodeList, KeyValueSet, curFile);
            nodeIndex = -1;
        }

        private void NodeList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                LocalFuncs.SaveLastCells(KeyValueSet, NodeList, nodeIndex, curFile);
                KeyValueSet.Rows.Clear();
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
        }

        private void moveNodeUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalFuncs.MoveNode(NodeList, KeyValueSet, curFile);
        }

        private void moveNodeDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalFuncs.MoveNode(NodeList, KeyValueSet, curFile, true);
        }

        private void duplicateNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalFuncs.DuplicateNode(NodeList, KeyValueSet, curFile);
        }
    }
}
