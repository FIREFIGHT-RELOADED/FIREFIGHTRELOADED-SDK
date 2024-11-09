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
    public partial class FabricatorOtherForm : Form
    {
        string kvName {  get; set; }

        public FabricatorOtherForm()
        {
            InitializeComponent();
        }

        private void FabricatorOtherForm_Load(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KeyValueSet.Rows.Clear();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var ofd = openFileDialog1)
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    KVObject? kv = FileCreatorBase.LoadKeyValuesFile(ofd.FileName);

                    if (kv != null)
                    {
                        kvName = kv.Name;

                        foreach (KVObject child in kv.Children)
                        {
                            if (child.Value.ValueType == KVValueType.Collection || child.Value.ValueType == KVValueType.Null)
                                continue;

                            KeyValueSet.Rows.Add(child.Name, child.Value);
                        }
                    }
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var sfd = saveFileDialog1)
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    List<KVObject> list = new List<KVObject>();

                    foreach (DataGridViewRow row in KeyValueSet.Rows)
                    {
                        if (row.Cells[0].Value == null || row.Cells[1].Value == null)
                            continue;

                        list.Add(new KVObject(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString()));
                    }

                    KVObject finalFile = new KVObject(kvName, list);

                    KVSerializer kv = KVSerializer.Create(KVSerializationFormat.KeyValues1Text);
                    using (FileStream stream = File.OpenWrite(sfd.FileName))
                    {
                        kv.Serialize(stream, finalFile);
                    }
                }
            }
        }
    }
}
