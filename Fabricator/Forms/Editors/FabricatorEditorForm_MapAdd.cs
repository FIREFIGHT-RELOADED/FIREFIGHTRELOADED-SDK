
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
                    curFile = new MapAdd(ofd.FileName);
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
            MapAdd.MapAddLabelNode node = new MapAdd.MapAddLabelNode();
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
            if (e.Node != null)
            {
                FabricatorEditorFormHelpers.SaveLastCells(KeyValueSet, NodeList, nodeIndex, curFile);

                KeyValueSet.Rows.Clear();

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

        private void moveNodeUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FabricatorEditorFormHelpers.MoveNode(NodeList, KeyValueSet, curFile);
        }

        private void moveNodeDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FabricatorEditorFormHelpers.MoveNode(NodeList, KeyValueSet, curFile, true);
        }

        private void addPositionFromStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NodeList.SelectedNode != null)
            {
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
                            FabricatorEditorFormHelpers.ReloadNodeList(NodeList, curFile);
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
    }
}
