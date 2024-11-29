﻿using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;
using System.Xml.Linq;
using ValveKeyValue;

namespace Fabricator
{
    public class LocalFuncs
    {
        public static void ReloadNodeList(TreeView nodeList, FileCreatorBase curFile)
        {
            nodeList.Nodes.Clear();

            for (var i = 0; i < curFile.entries.Count(); i++)
            {
                nodeList.Nodes.Add((i + 1).ToString());
            }
        }

        public static List<KVObject>? ListFromCurCellsLegacy(DataGridView keyValueSet)
        {
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

        public static List<KVObject>? ListFromCurCells(DataGridView keyValueSet, int nodeIndex, FileCreatorBase curFile)
        {
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

        public static KVObject? CurCellsToKVObject(DataGridView keyValueSet, TreeView nodeList, int nodeIndex, FileCreatorBase curFile)
        {
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

            List<KVObject>? list = ListFromCurCells(keyValueSet, nodeIndex, curFile);
            KVObject? result = null;

            if (list != null)
            {
                int nodeNameIndex = Convert.ToInt32(nodeList.Nodes[nodeIndex].Text);
                result = new KVObject(nodeNameIndex.ToString(), list);
            }

            return result;
        }

        public static KVObject? SaveLastCells(DataGridView keyValueSet, TreeView nodeList, int nodeIndex, FileCreatorBase curFile)
        {
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

        public static void EditSettings(FileCreatorBase curFile)
        {
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

        public static void AddCollection(DataGridView keyValueSet, int nodeIndex, FileCreatorBase curFile, int rowIndex, int columnIndex)
        {
            if (nodeIndex > -1)
            {
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

        public static void EditCollection(DataGridView keyValueSet, int nodeIndex, FileCreatorBase curFile, int rowIndex, int columnIndex)
        {
            if (nodeIndex > -1)
            {
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

        public static bool IsNodeOpen(TreeView nodeList)
        {
            if (nodeList.Nodes.Count > 0 && nodeList.SelectedNode != null)
            {
                return true;
            }

            return false;
        }

        public static void Clear(DataGridView keyValueSet, TreeView nodeList, FileCreatorBase curFile)
        {
            curFile.entries.Clear();
            if (curFile.FileUsesSettings && curFile.settings != null)
            {
                curFile.settings.Clear();
            }
            keyValueSet.Rows.Clear();
            nodeList.Nodes.Clear();
        }

        public static void DeleteNode(TreeView nodeList, DataGridView keyValueSet, FileCreatorBase curFile)
        {
            if (nodeList.SelectedNode != null)
            {
                int nodeIndex = Convert.ToInt32(nodeList.SelectedNode.Text);
                curFile.RemoveEntry(nodeIndex);
                keyValueSet.Rows.Clear();
                ReloadNodeList(nodeList, curFile);
            }
        }

        public static void AddNode(TreeView nodeList, DataGridView keyValueSet, FileCreatorBase curFile, FileCreatorBase.BaseNode node)
        {
            int count = curFile.entries.Count;
            curFile.AddEntry(node);
            AddNodeInternal(nodeList, keyValueSet, curFile, count);
        }

        public static void AddNode(TreeView nodeList, DataGridView keyValueSet, FileCreatorBase curFile, KVObject node)
        {
            int count = curFile.entries.Count;
            curFile.AddEntry(node);
            AddNodeInternal(nodeList, keyValueSet, curFile, count);
        }

        private static void AddNodeInternal(TreeView nodeList, DataGridView keyValueSet, FileCreatorBase curFile, int count)
        {
            ReloadNodeList(nodeList, curFile);
            nodeList.SelectedNode = nodeList.Nodes[count];
        }

        public static void DuplicateNode(TreeView nodeList, DataGridView keyValueSet, FileCreatorBase curFile)
        {
            if (nodeList.SelectedNode != null)
            {
                int nodeIndex = Convert.ToInt32(nodeList.SelectedNode.Text);
                int actualIndex = nodeIndex - 1;
                AddNode(nodeList, keyValueSet, curFile, curFile.entries[actualIndex]);
            }
        }

        public static void MoveNode(TreeView nodeList, DataGridView keyValueSet, FileCreatorBase curFile, bool movedown = false)
        {
            if (nodeList.SelectedNode != null)
            {
                int nodeIndex = Convert.ToInt32(nodeList.SelectedNode.Text);
                int newIndex = curFile.MoveEntry(nodeIndex, movedown);
                keyValueSet.Rows.Clear();
                ReloadNodeList(nodeList, curFile);
                nodeList.SelectedNode = nodeList.Nodes[newIndex];
            }
        }

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
                    object? res = LocalVars.DataTypeForString(type);

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
    }
}