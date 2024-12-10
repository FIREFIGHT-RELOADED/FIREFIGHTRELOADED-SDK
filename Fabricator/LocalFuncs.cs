using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;
using System.Xml.Linq;
using ValveKeyValue;

namespace Fabricator
{
    public class LocalFuncs
    {

        /// <summary>
        /// Reload the node list/tree view.
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="curFile"></param>
        public static void ReloadNodeList(TreeView nodeList, FileCreatorBase curFile)
        {
            //clear, and then add all of the nodes based on how many entries are in the list.
            nodeList.Nodes.Clear();

            for (var i = 0; i < curFile.entries.Count(); i++)
            {
                nodeList.Nodes.Add((i + 1).ToString());
            }
        }

        
        /// <summary>
        /// Create a KVObject list from the current selected cells. Does not handle collections
        /// </summary>
        /// <param name="keyValueSet"></param>
        /// <returns></returns>
        public static List<KVObject>? ListFromCurCellsLegacy(DataGridView keyValueSet)
        {
            //generate a KVObject list with the rows from a datagridview
            List<KVObject>? list = new List<KVObject>();

            foreach (DataGridViewRow row in keyValueSet.Rows)
            {
                if (row.Cells[0].Value == null)
                {
                    continue;
                }

                KVValue? value = null;

                if (row.Cells[1].Value == null)
                {
                    value = "";
                }
                else
                {
                    value = row.Cells[1].Value.ToString();
                }

                KVObject kVObject = new KVObject(row.Cells[0].Value.ToString(), value);
                list.Add(kVObject);
            }

            return list;
        }

        /// <summary>
        /// Create a KVObject list from the current selected cells.
        /// </summary>
        /// <param name="keyValueSet"></param>
        /// <param name="nodeIndex"></param>
        /// <param name="curFile"></param>
        /// <returns></returns>
        public static List<KVObject>? ListFromCurCells(DataGridView keyValueSet, int nodeIndex, FileCreatorBase curFile)
        {
            //generate a KVObject list with the rows from a datagridview
            List<KVObject>? list = new List<KVObject>();

            foreach (DataGridViewRow row in keyValueSet.Rows)
            {
                if (row.Cells[0].Value == null)
                {
                    continue;
                }

                KVValue? value = null; 

                if (row.Cells[1].Value == null)
                {
                    value = "";
                }
                else
                {
                    string? valString = row.Cells[1].Value.ToString();

                    if (valString != null)
                    {
                        if (valString.Contains("[Collection]", StringComparison.CurrentCultureIgnoreCase) && nodeIndex > -1)
                        {
                            value = curFile.entries[nodeIndex][row.Cells[0].Value.ToString()];
                        }
                        else
                        {
                            value = valString;
                        }
                    }
                }

                KVObject kVObject = new KVObject(row.Cells[0].Value.ToString(), value);
                list.Add(kVObject);
            }

            return list;
        }

        
        /// <summary>
        /// Convert the selected cells into a KVObject.
        /// </summary>
        /// <param name="keyValueSet"></param>
        /// <param name="nodeList"></param>
        /// <param name="nodeIndex"></param>
        /// <param name="curFile"></param>
        /// <returns></returns>
        public static KVObject? CurCellsToKVObject(DataGridView keyValueSet, TreeView nodeList, int nodeIndex, FileCreatorBase curFile)
        {
            // find any empty cells and check if all the cells are empty before saving.
            int emptyCells = 0;
            int cells = 0;

            for (int i = 0; i < keyValueSet.Rows.Count; i++)
            {
                if (keyValueSet.Rows[i].IsNewRow)
                    continue;

                cells++;

                var value = keyValueSet.Rows[i].Cells[0].Value.ToString();
                if (string.IsNullOrWhiteSpace(value))
                {
                    emptyCells++;
                    break;
                }
            }

            if (cells == emptyCells)
            {
                return null;
            }

            //save our current node as a kvobject by creating a list built from current cells shown in the datagridview.
            List<KVObject>? list = ListFromCurCells(keyValueSet, nodeIndex, curFile);
            KVObject? result = null;

            if (list != null)
            {
                int nodeNameIndex = Convert.ToInt32(nodeList.Nodes[nodeIndex].Text);
                result = new KVObject(nodeNameIndex.ToString(), list);
            }

            return result;
        }


        /// <summary>
        /// Save the last cells as KVObjects
        /// </summary>
        /// <param name="keyValueSet"></param>
        /// <param name="nodeList"></param>
        /// <param name="nodeIndex"></param>
        /// <param name="curFile"></param>
        /// <returns></returns>
        public static KVObject? SaveLastCells(DataGridView keyValueSet, TreeView nodeList, int nodeIndex, FileCreatorBase curFile)
        {
            // find any empty cells and check if all the cells are empty before saving.
            int emptyCells = 0;
            int cells = 0;

            for (int i = 0; i < keyValueSet.Rows.Count; i++)
            {
                if (keyValueSet.Rows[i].IsNewRow)
                    continue;

                cells++;

                var value = keyValueSet.Rows[i].Cells[0].Value.ToString();
                if (string.IsNullOrWhiteSpace(value))
                {
                    emptyCells++;
                    break;
                }
            }

            if (cells == emptyCells)
            {
                return null;
            }

            KVObject? saveObject = null;

            //save our current node using the index we saved below if
            //we have an index over -1
            if (nodeIndex > -1)
            {
                saveObject = CurCellsToKVObject(keyValueSet, nodeList, nodeIndex, curFile);

                if (saveObject != null)
                {
                    curFile.EditEntry(saveObject);
                }
            }

            return saveObject;
        }


        /// <summary>
        /// Open up the edit settings panel
        /// </summary>
        /// <param name="curFile"></param>
        public static void EditSettings(FileCreatorBase curFile)
        {
            //opens up the collection editor to edit the file settings.
            KVObject? kv = curFile.SettingsToKVObject();
            if (kv != null)
            {
                using (var kvl = new FabricatorCollectionEditor(kv))
                {
                    kvl.Text = "Edit Settings";
                    if (kvl.ShowDialog() == DialogResult.OK)
                    {
                        //after editing, set collection to our result and save it into the kv.
                        if (kvl.result != null)
                        {
                            curFile.settings = kvl.result.Children.ToList();
                        }
                    }
                }
            }
            else
            {
                curFile.ToggleSettings();
                EditSettings(curFile);
            }
        }

        
        /// <summary>
        /// Edit the selected collection.
        /// </summary>
        /// <param name="keyValueSet"></param>
        /// <param name="nodeIndex"></param>
        /// <param name="curFile"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        public static void AddCollection(DataGridView keyValueSet, int nodeIndex, FileCreatorBase curFile, int rowIndex, int columnIndex)
        {
            if (nodeIndex > -1)
            {
                //this opens up the collection editor and adds a new collection if [collection] is the current value text in a row. 

                DataGridViewRow row = keyValueSet.Rows[rowIndex];
                DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)row.Cells[columnIndex];

                if (cell.Value != null && row.Cells[0].Value != null && row.Cells[1].Value == cell.Value)
                {
                    string? valString = cell.Value.ToString();

                    if (valString != null && valString.Contains("[Collection]", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string? title = row.Cells[0].Value.ToString();
                        if (title != null)
                        {
                            KVValue kv = curFile.entries[nodeIndex][title];
                            if (kv == null)
                            {
                                using (var kvl = new FabricatorCollectionEditor(title))
                                {
                                    if (kvl.ShowDialog() == DialogResult.OK)
                                    {
                                        //after editing, set collection to our result and save it into the kv.
                                        if (kvl.result != null)
                                        {
                                            curFile.entries[nodeIndex][title] = kvl.result.Value;
                                            if (!row.ReadOnly)
                                            {
                                                row.ReadOnly = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Edit the selected collection.
        /// </summary>
        /// <param name="keyValueSet"></param>
        /// <param name="nodeIndex"></param>
        /// <param name="curFile"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        public static void EditCollection(DataGridView keyValueSet, int nodeIndex, FileCreatorBase curFile, int rowIndex, int columnIndex)
        {
            if (nodeIndex > -1)
            {
                //this opens up the collection editor and edits a collection if [collection] is the current value text in a row. 

                DataGridViewRow row = keyValueSet.Rows[rowIndex];
                DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)row.Cells[columnIndex];

                if (cell.Value != null && row.Cells[0].Value != null && row.Cells[1].Value == cell.Value)
                {
                    string? valString = cell.Value.ToString();

                    if (valString != null && valString.Contains("[Collection]", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string? title = row.Cells[0].Value.ToString();
                        if (title != null)
                        {
                            KVValue kv = curFile.entries[nodeIndex][title];
                            if (kv != null)
                            {
                                using (var kvl = new FabricatorCollectionEditor(new KVObject(title, kv)))
                                {
                                    if (kvl.ShowDialog() == DialogResult.OK)
                                    {
                                        //after editing, set collection to our result and save it into the kv.
                                        if (kvl.result != null)
                                        {
                                            curFile.entries[nodeIndex][title] = kvl.result.Value;
                                            if (!row.ReadOnly)
                                            {
                                                row.ReadOnly = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Check if the node is open.
        /// </summary>
        /// <param name="nodeList"></param>
        /// <returns></returns>
        public static bool IsNodeOpen(TreeView nodeList)
        {
            //if we have more than 0 nodes and we have a selected node, the node is open.
            if (nodeList.Nodes.Count > 0 && nodeList.SelectedNode != null)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Clear the panel.
        /// </summary>
        /// <param name="keyValueSet"></param>
        /// <param name="nodeList"></param>
        /// <param name="curFile"></param>
        public static void Clear(DataGridView keyValueSet, TreeView nodeList, FileCreatorBase curFile)
        {
            // clear all entries in the list, and any rows or nodes.
            curFile.entries.Clear();
            if (curFile.FileUsesSettings && curFile.settings != null)
            {
                curFile.settings.Clear();
            }
            keyValueSet.Rows.Clear();
            nodeList.Nodes.Clear();
        }


        /// <summary>
        /// Delete a node.
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="keyValueSet"></param>
        /// <param name="curFile"></param>
        public static void DeleteNode(TreeView nodeList, DataGridView keyValueSet, FileCreatorBase curFile)
        {
            if (nodeList.SelectedNode != null)
            {
                // Remove the node based on the index, then reload the list.
                int nodeIndex = Convert.ToInt32(nodeList.SelectedNode.Text);
                curFile.RemoveEntry(nodeIndex);
                keyValueSet.Rows.Clear();
                ReloadNodeList(nodeList, curFile);
            }
        }


        /// <summary>
        /// Add a node.
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="keyValueSet"></param>
        /// <param name="curFile"></param>
        /// <param name="node"></param>
        public static void AddNode(TreeView nodeList, DataGridView keyValueSet, FileCreatorBase curFile, FileCreatorBase.BaseNode node)
        {
            // add the node entry, then run AddNodeInternal.
            int count = curFile.entries.Count;
            curFile.AddEntry(node);
            AddNodeInternal(nodeList, keyValueSet, curFile, count);
        }

        /// <summary>
        /// Add a node.
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="keyValueSet"></param>
        /// <param name="curFile"></param>
        /// <param name="node"></param>
        public static void AddNode(TreeView nodeList, DataGridView keyValueSet, FileCreatorBase curFile, KVObject node)
        {
            // add the node entry, then run AddNodeInternal.
            int count = curFile.entries.Count;
            curFile.AddEntry(node);
            AddNodeInternal(nodeList, keyValueSet, curFile, count);
        }


        /// <summary>
        /// Finish adding a node.
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="keyValueSet"></param>
        /// <param name="curFile"></param>
        /// <param name="count"></param>
        private static void AddNodeInternal(TreeView nodeList, DataGridView keyValueSet, FileCreatorBase curFile, int count)
        {
            //reload the node list, then select the new node
            ReloadNodeList(nodeList, curFile);
            nodeList.SelectedNode = nodeList.Nodes[count];
        }


        /// <summary>
        /// Duplicate a selected node.
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="keyValueSet"></param>
        /// <param name="curFile"></param>
        public static void DuplicateNode(TreeView nodeList, DataGridView keyValueSet, FileCreatorBase curFile)
        {
            if (nodeList.SelectedNode != null)
            {
                //save the last node, then add that same node using the actual index.
                int nodeIndex = Convert.ToInt32(nodeList.SelectedNode.Text);
                int actualIndex = nodeIndex - 1;
                SaveLastCells(keyValueSet, nodeList, actualIndex, curFile);
                AddNode(nodeList, keyValueSet, curFile, curFile.entries[actualIndex]);
            }
        }


        /// <summary>
        /// Move a selected node.
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="keyValueSet"></param>
        /// <param name="curFile"></param>
        /// <param name="movedown"></param>
        /// <param name="moveToEnd"></param>
        public static void MoveNode(TreeView nodeList, DataGridView keyValueSet, FileCreatorBase curFile, bool movedown = false, bool moveToEnd = false)
        {
            if (nodeList.SelectedNode != null)
            {
                //get the actual index of the node, then use the MoveEntry method to move it up or down.
                int nodeIndex = Convert.ToInt32(nodeList.SelectedNode.Text);
                int actualIndex = nodeIndex - 1;
                SaveLastCells(keyValueSet, nodeList, actualIndex, curFile);
                int newIndex = -1;
                if (!moveToEnd)
                {
                    newIndex = curFile.MoveEntry(nodeIndex, movedown);
                }
                else
                {
                    newIndex = curFile.MoveEntryToEnd(nodeIndex, movedown);
                }
                keyValueSet.Rows.Clear();
                ReloadNodeList(nodeList, curFile);
                nodeList.SelectedNode = nodeList.Nodes[newIndex];
            }
        }


        /// <summary>
        /// Add a row from a list of options.
        /// </summary>
        /// <param name="keyValueSet"></param>
        public static void AddKeyValue(DataGridView keyValueSet)
        {
            using (var kvl = new FabricatorKeyvalueLoader())
            {
                if (kvl.ShowDialog() == DialogResult.OK)
                {
                    //this code determines the data type based on the schema. If it's a collection, make it read-only.
                    //note: we don't expect collections inside of collections for a good reason, so we don't have to re-iliterate
                    //multiple times. Fabricator is made for editing collections in individual nodes, and not for editing collections inside
                    //collections or nodes inside of nodes.

                    string type = kvl.selectedValType;
                    object? res = DataTypeForString(type);

                    int index = keyValueSet.Rows.Add(kvl.selectedKey, res);
                    DataGridViewRow? row = keyValueSet.Rows[index];

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


        /// <summary>
        /// Add rows based on a KVObject's children.
        /// </summary>
        /// <param name="keyValueSet"></param>
        /// <param name="kv"></param>
        public static void AddRows(DataGridView keyValueSet, KVObject kv)
        {
            if (kv != null)
            {
                foreach (var child in kv.Children)
                {
                    //This code is used for making collections read only when we load a node.
                    //this shouldn't fail...hopefully.
                    int index = keyValueSet.Rows.Add(child.Name, child.Value);
                    DataGridViewRow? row = keyValueSet.Rows[index];

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


        /// <summary>
        /// Output an object of a specific data type specified in a string.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object? DataTypeForString(string data)
        {
            object? res = null;

            // if the string contains a certain data type name, it must be that data type,
            // so return a representation of it in string form.
            switch (data)
            {
                case string j when j.Contains("Bool", StringComparison.CurrentCultureIgnoreCase):
                case string k when k.Contains("Int", StringComparison.CurrentCultureIgnoreCase):
                case string a when a.Contains("Boolean", StringComparison.CurrentCultureIgnoreCase):
                case string c when c.Contains("Integer", StringComparison.CurrentCultureIgnoreCase):
                    res = 0;
                    break;
                case string d when d.Contains("Collection", StringComparison.CurrentCultureIgnoreCase):
                case string m when m.Contains("List", StringComparison.CurrentCultureIgnoreCase):
                case string n when n.Contains("Array", StringComparison.CurrentCultureIgnoreCase):
                    res = "[Collection]";
                    break;
                case string f when f.Contains("Double", StringComparison.CurrentCultureIgnoreCase):
                    res = 0.00d;
                    break;
                case string g when g.Contains("Float", StringComparison.CurrentCultureIgnoreCase):
                    res = 0.0f;
                    break;
                case string b when b.Contains("String", StringComparison.CurrentCultureIgnoreCase):
                    res = "Hello World!";
                    break;
                case string h when h.Contains("Color", StringComparison.CurrentCultureIgnoreCase):
                    res = "255 255 255 255";
                    break;
                case string i when i.Contains("Vector", StringComparison.CurrentCultureIgnoreCase):
                    res = "0 0 0";
                    break;
                case string l when l.Contains("FormattedStr", StringComparison.CurrentCultureIgnoreCase):
                    {
                        string format = data.Split()[1];
                        res = format;
                    }
                    break;
                default:
                    break;
            }

            return res;
        }
    }
}
