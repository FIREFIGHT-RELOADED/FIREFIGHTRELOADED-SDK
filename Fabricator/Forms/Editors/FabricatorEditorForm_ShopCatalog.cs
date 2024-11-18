﻿using System.Data;
using System.Windows.Forms;
using ValveKeyValue;

namespace Fabricator
{
    public partial class FabricatorEditorForm_ShopCatalog : Form
    {
        ShopCatalog curFile { get; set; }
        int nodeIndex { get; set; }

        public FabricatorEditorForm_ShopCatalog()
        {
            InitializeComponent();

            curFile = new ShopCatalog();
            nodeIndex = -1;

            openFileDialog1.Filter = saveFileDialog1.Filter = $"ShopCatalog Text Files|*.txt|All Files|*.*";
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
                    curFile = new ShopCatalog(ofd.FileName);
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
                    if (FabricatorEditorFormHelpers.SaveLastCells(KeyValueSet, NodeList, nodeIndex, curFile) != null)
                    {
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
                        KeyValueSet.Rows.Add(kvl.selectedKey);
                    }
                }
            }
        }

        private void addNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShopCatalog.CatalogNode node = new ShopCatalog.CatalogNode();
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
            FabricatorEditorFormHelpers.SaveLastCells(KeyValueSet, NodeList, nodeIndex, curFile);
            KeyValueSet.Rows.Clear();
            nodeIndex = e.Node.Index;

            KVObject kv = curFile.entries[nodeIndex - 1];

            if (kv != null)
            {
                foreach (var child in kv.Children)
                {
                    KeyValueSet.Rows.Add(child.Name, child.Value);
                }
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FabricatorTextViewer ftv = new FabricatorTextViewer(Path.Combine(GlobalVars.DataPath, "shopcatalog_help.txt"));
            ftv.Text = "ShopCatalog Help";
            ftv.Show();
        }
    }
}