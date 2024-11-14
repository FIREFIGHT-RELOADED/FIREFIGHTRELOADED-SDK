
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValveKeyValue;

namespace Fabricator
{
    public partial class FabricatorCollectionEditor : Form
    {
        string kvName { get; set; }
        public KVObject objToEdit { get; set; }

        public FabricatorCollectionEditor(KVObject obj)
        {
            InitializeComponent();

            CenterToScreen();

            objToEdit = obj;
            kvName = objToEdit.Name;
        }

        private void FabricatorCollectionEditor_Load(object sender, EventArgs e)
        {
            if (objToEdit != null)
            {
                foreach (KVObject child in objToEdit.Children)
                {
                    if (child.Value.ValueType == KVValueType.Collection || child.Value.ValueType == KVValueType.Null)
                        continue;

                    KeyValueSet.Rows.Add(child.Name, child.Value);
                }
            }
        }

        private void FabricatorCollectionEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (objToEdit != null)
            {
                List<KVObject> list = new List<KVObject>();

                foreach (DataGridViewRow row in KeyValueSet.Rows)
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
                            e.Cancel = true;
                            return;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    list.Add(new KVObject(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString()));
                }

                objToEdit = new KVObject(kvName, list);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
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
}
