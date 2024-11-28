
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        private KVObject objToEdit { get; set; }
        public KVObject? result { get; set; }

        public FabricatorCollectionEditor(KVObject obj)
        {
            InitializeComponent();

            CenterToScreen();

            objToEdit = obj;
            result = null;
            kvName = objToEdit.Name;
        }

        public FabricatorCollectionEditor(string name)
        {
            InitializeComponent();

            CenterToScreen();

            result = null;
            kvName = name;
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
            if (KeyValueSet.Rows.Count > 0)
            {
                List<KVObject>? list = FabricatorEditorFormHelpers.ListFromCurCellsLegacy(KeyValueSet);

                if (list != null)
                {
                    result = new KVObject(kvName, list);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void createRowFromKeyListToolStripMenuItem_Click(object sender, EventArgs e)
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
}
