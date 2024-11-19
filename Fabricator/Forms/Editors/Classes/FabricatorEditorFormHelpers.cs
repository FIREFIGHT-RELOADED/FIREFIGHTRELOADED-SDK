using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using ValveKeyValue;

namespace Fabricator
{
    public class FabricatorEditorFormHelpers
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
                    if (row.Cells[1].Value.ToString().Contains("[Collection]", StringComparison.CurrentCultureIgnoreCase) && nodeIndex > -1)
                    {
                        value = curFile.entries[nodeIndex][row.Cells[0].Value.ToString()];
                    }
                    else
                    {
                        value = row.Cells[1].Value.ToString();
                    }
                }

                KVObject kVObject = new KVObject(row.Cells[0].Value.ToString(), value);
                list.Add(kVObject);
            }

            return list;
        }

        public static KVObject? CurCellsToKVObject(DataGridView keyValueSet, TreeView nodeList, int nodeIndex, FileCreatorBase curFile)
        {
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
            KVObject kv = curFile.SettingsToKVObject();
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
                    if (cell.Value.ToString().Contains("[Collection]", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string title = row.Cells[0].Value.ToString();
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

        public static void EditCollection(DataGridView keyValueSet, int nodeIndex, FileCreatorBase curFile, int rowIndex, int columnIndex)
        {
            if (nodeIndex > -1)
            {
                DataGridViewRow row = keyValueSet.Rows[rowIndex];
                DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)row.Cells[columnIndex];

                if (cell.Value != null && row.Cells[0].Value != null && row.Cells[1].Value == cell.Value)
                {
                    if (cell.Value.ToString().Contains("[Collection]", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string title = row.Cells[0].Value.ToString();
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
            if (curFile.FileUsesSettings)
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
    }
}
