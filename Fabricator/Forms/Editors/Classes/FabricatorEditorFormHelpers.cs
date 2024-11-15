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

            if (curFile.FileUsesSettings)
            {
                //this SHOULD be the first one.....
                //if it isn't, we shouldn't worry because 
                //index doesn't increment.
                nodeList.Nodes.Add("settings");
            }

            for (var i = 0; i < curFile.entries.Count(); i++)
            {
                nodeList.Nodes.Add((i + 1).ToString());
            }
        }

        public static List<KVObject>? ListFromCurCells(DataGridView keyValueSet)
        {
            List<KVObject>? list = new List<KVObject>();

            foreach (DataGridViewRow row in keyValueSet.Rows)
            {
                if (row.Cells[0].Value == null)
                {
                    continue;
                }

                if (row.Cells[1].Value == null)
                {
                    DialogResult result = MessageBox.Show($"Row #{row.Index + 1} '{row.Cells[0].Value}' has a missing value, or the value is being edited. Would you like to fix it? If not, the row will not be included in the saved file.", "Fabricator", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        return null;
                    }
                    else
                    {
                        continue;
                    }
                }

                list.Add(new KVObject(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString()));
            }

            return list;
        }

        public static KVObject? CurCellsToKVObject(DataGridView keyValueSet, TreeView nodeList, int nodeIndex)
        {
            List<KVObject>? list = ListFromCurCells(keyValueSet);
            KVObject? result = null;

            if (list != null)
            {
                int nodeNameIndex = Convert.ToInt32(nodeList.Nodes[nodeIndex].Text);
                result = new KVObject(nodeNameIndex.ToString(), list);
            }

            return result;
        }

        public static void SaveLastCells(DataGridView keyValueSet, TreeView nodeList, int nodeIndex, FileCreatorBase curFile)
        {
            //save our current node using the index we saved below if
            //we have an index over -1
            if (nodeIndex > -1)
            {
                KVObject? saveObject = CurCellsToKVObject(keyValueSet, nodeList, nodeIndex);

                if (saveObject != null)
                {
                    curFile.EditEntry(saveObject);
                }
            }
        }

        public static void SaveSettingsFromLastCells(DataGridView keyValueSet, TreeView nodeList, int nodeIndex, FileCreatorBase curFile)
        {
            if (curFile.FileUsesSettings)
            {
                List<KVObject>? list = ListFromCurCells(keyValueSet);

                if (list != null)
                {
                    foreach (var obj in list)
                    {
                        curFile.EditSetting(obj);
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

        public static void Clear(DataGridView keyValueSet, TreeView nodeList)
        {
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
