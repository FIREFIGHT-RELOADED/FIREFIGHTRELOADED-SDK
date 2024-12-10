
using System;
using System.Data;
using System.Numerics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using ValveKeyValue;

namespace Fabricator
{
    public partial class FabricatorEditorForm_MapAdd : Form
    {
        MapAdd curFile { get; set; }
        int nodeIndex { get; set; }
        string savedFileName { get; set; } = "";

        public FabricatorEditorForm_MapAdd()
        {
            InitializeComponent();
            CenterToScreen();

            curFile = new MapAdd();
            nodeIndex = -1;

            openFileDialog1.Filter = saveFileDialog1.Filter = $"MapAdd Text Files|*.txt|All Files|*.*";
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
                    curFile = new MapAdd(ofd.FileName);
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
            MapAdd.MapAddLabelNode node = new MapAdd.MapAddLabelNode();
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

        private void KeyValueSet_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (nodeIndex > -1)
            {
                LocalFuncs.AddCollection(KeyValueSet, nodeIndex, curFile, e.RowIndex, e.ColumnIndex);
            }
        }

        private void KeyValueSet_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (nodeIndex > -1)
            {
                LocalFuncs.EditCollection(KeyValueSet, nodeIndex, curFile, e.RowIndex, e.ColumnIndex);
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

        private void addPositionFromStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NodeList.SelectedNode != null)
            {
                LocalFuncs.SaveLastCells(KeyValueSet, NodeList, nodeIndex, curFile);

                using (var kvl = new FabricatorLoadStringPrompt())
                {
                    kvl.Text = "Add Position from String";
                    if (kvl.ShowDialog() == DialogResult.OK)
                    {
                        //in FIREFIGHT RELOADED, there is a tool named the Position Grabber, that takes the player's position
                        //and puts it onto the console in the following format:
                        //
                        //Player position XYZ Coords: X: <X Position float> Y: <Y Position float> Z: <Z Position float>
                        //
                        //This code uses Regex patterns to extract the X/Y/Z portions of the string, verifies it, and converts the individual parts of the string
                        //to usable float values, then edits the entry being edited.

                        var match = Regex.Match(kvl.result, "X: ([+-]?(?=\\.\\d|\\d)(?:\\d+)?(?:\\.?\\d*))(?:[Ee]([+-]?\\d+))? Y: ([+-]?(?=\\.\\d|\\d)(?:\\d+)?(?:\\.?\\d*))(?:[Ee]([+-]?\\d+))? Z: ([+-]?(?=\\.\\d|\\d)(?:\\d+)?(?:\\.?\\d*))(?:[Ee]([+-]?\\d+))?", RegexOptions.IgnoreCase);
                        string res = match.Groups[0].Value;

                        if (!string.IsNullOrWhiteSpace(res))
                        {
                            var x = Convert.ToSingle(Regex.Match(kvl.result, "X: ([+-]?(?=\\.\\d|\\d)(?:\\d+)?(?:\\.?\\d*))(?:[Ee]([+-]?\\d+))?", RegexOptions.IgnoreCase).Groups[0].Value.Replace("X: ", ""));
                            var y = Convert.ToSingle(Regex.Match(kvl.result, "Y: ([+-]?(?=\\.\\d|\\d)(?:\\d+)?(?:\\.?\\d*))(?:[Ee]([+-]?\\d+))?", RegexOptions.IgnoreCase).Groups[0].Value.Replace("Y: ", ""));
                            var z = Convert.ToSingle(Regex.Match(kvl.result, "Z: ([+-]?(?=\\.\\d|\\d)(?:\\d+)?(?:\\.?\\d*))(?:[Ee]([+-]?\\d+))?", RegexOptions.IgnoreCase).Groups[0].Value.Replace("Z: ", ""));

                            int fakeIndex = nodeIndex + 1;

                            MapAdd.MapAddLabelNode node = curFile.KVObjectToNode(fakeIndex);

                            node.x = x;
                            node.y = y;
                            node.z = z;

                            curFile.EditEntry(fakeIndex, node);

                            KeyValueSet.Rows.Clear();
                            LocalFuncs.ReloadNodeList(NodeList, curFile);
                            NodeList.SelectedNode = NodeList.Nodes[fakeIndex - 1];
                        }
                        else
                        {
                            MessageBox.Show("Please enter a correct string. Make sure the string is in this format as it comes out of the weapon_positiongrabber's output:\n\"Player position XYZ Coords: X: (X Position) Y: (Y Position) Z: (Z Position)\"\nYou may also enter in your coordinates as:\n\"X: (X Position) Y: (Y Position) Z: (Z Position)\"");
                        }
                    }
                }
            }
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
