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
        string kvName { get; set; }

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
                ofd.FileName = "";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    KVObject? kv = FileCreatorBase.LoadKeyValuesFile(ofd.FileName);

                    if (kv != null)
                    {
                        KVNameBox.Text = kv.Name;

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
            if (string.IsNullOrWhiteSpace(KVNameBox.Text))
            {
                DialogResult result = MessageBox.Show("You are about to save the file without a name for the KeyValues file. Would you like to fix it? If not, the file will save with the assigned file name for the KeyValues file.", "Fabricator", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    return;
                }
            }

            using (var sfd = saveFileDialog1)
            {
                sfd.FileName = "";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    List<KVObject>? list = FabricatorEditorFormHelpers.ListFromCurCells(KeyValueSet);

                    if (list != null)
                    {
                        if (string.IsNullOrWhiteSpace(kvName))
                        {
                            kvName = Path.GetFileNameWithoutExtension(sfd.FileName);
                        }

                        KVObject finalFile = new KVObject(kvName, list);

                        //For some strange reason, the data will save DIRECTLY into a file if it exists.
                        //So, we should remove it so the file can actually work.
                        if (File.Exists(sfd.FileName))
                        {
                            File.Delete(sfd.FileName);
                        }

                        KVSerializer kv = KVSerializer.Create(KVSerializationFormat.KeyValues1Text);
                        using (FileStream stream = File.OpenWrite(sfd.FileName))
                        {
                            kv.Serialize(stream, finalFile);
                        }
                    }
                }
            }
        }

        private void createRowFromKeyListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var kvl = new FabricatorKeyvalueLoader())
            {
                if (kvl.ShowDialog() == DialogResult.OK)
                {
                    KeyValueSet.Rows.Add(kvl.selectedKey);
                }
            }
        }

        private void KVNameBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(KVNameBox.Text))
            {
                kvName = KVNameBox.Text;
            }
        }
    }
}
