using System.Data;
using System.Windows.Forms;
using ValveKeyValue;

namespace Fabricator
{
    public partial class FabricatorEditorForm_ShopCatalog : Form
    {
        ShopCatalog curFile { get; set; }
        int nodeIndex { get; set; }
        string savedFileName { get; set; } = "";

        public FabricatorEditorForm_ShopCatalog()
        {
            InitializeComponent();
            CenterToScreen();

            curFile = new ShopCatalog();
            nodeIndex = -1;

            openFileDialog1.Filter = saveFileDialog1.Filter = $"ShopCatalog Text Files|*.txt|All Files|*.*";
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
                    curFile = new ShopCatalog(ofd.FileName);
                    LocalFuncs.ReloadNodeList(NodeList, curFile);
                    //select the first node.
                    if (NodeList.Nodes.Count > 0)
                    {
                        NodeList.SelectedNode = NodeList.Nodes[0];
                    }
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
            ShopCatalog.CatalogNode node = new ShopCatalog.CatalogNode();
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

                LocalFuncs.AddRows(KeyValueSet, kv);
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FabricatorTextViewer ftv = new FabricatorTextViewer(Path.Combine(LocalVars.DataPath, "shopcatalog_help.txt"));
            ftv.Text = "ShopCatalog Help";
            ftv.Show();
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

        private void moveNodeToTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalFuncs.MoveNode(NodeList, KeyValueSet, curFile, false, true);
        }

        private void moveNodeToBottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalFuncs.MoveNode(NodeList, KeyValueSet, curFile, true, true);
        }
    }
}
