
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

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var kvl = new FabricatorKeyvalueLoader())
            {
                if (kvl.ShowDialog() == DialogResult.OK)
                {
                    string type = kvl.selectedValType;
                    object? res = null;

                    switch (type)
                    {
                        case string a when a.Contains("Boolean", StringComparison.CurrentCultureIgnoreCase):
                        case string c when c.Contains("Integer", StringComparison.CurrentCultureIgnoreCase):
                            res = 0;
                            break;
                        case string d when d.Contains("Collection", StringComparison.CurrentCultureIgnoreCase):
                            res = "[Collection]";
                            break;
                        case string f when f.Contains("Double", StringComparison.CurrentCultureIgnoreCase):
                            res = 0.0d;
                            break;
                        case string g when g.Contains("Float", StringComparison.CurrentCultureIgnoreCase):
                            res = 0.0f;
                            break;
                        case string h when h.Contains("FormattedStr", StringComparison.CurrentCultureIgnoreCase):
                            {
                                string format = kvl.selectedValType.Split()[1];
                                res = format;
                            }
                            break;
                        case string b when b.Contains("String", StringComparison.CurrentCultureIgnoreCase):
                            res = "Hello World!";
                            break;
                        default:
                            break;
                    }

                    KeyValueSet.Rows.Add(kvl.selectedKey, res);
                }
            }
        }
    }
}
