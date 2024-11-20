using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fabricator
{
    public partial class FabricatorKeyvalueLoader : Form
    {
        List<JSONEntry>? JSONEntries { get; set; }
        public string selectedKey { get; set; }
        public string selectedValType { get; set; }
        public string fullItemName { get; set; }

        public FabricatorKeyvalueLoader()
        {
            InitializeComponent();

            CenterToScreen();

            JSONEntries = SchemeLoader.LoadFile(Path.Combine(LocalVars.DataPath, "schema.json"));

            if (JSONEntries != null)
            {
                foreach (var entry in JSONEntries)
                {
                    if (entry.FileType.Contains(LocalVars.SelectedType.ToString(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        AvailableKeys.Items.Add(entry.ToString());
                    }
                }
            }
        }

        private void AvailableKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AvailableKeys.Items.Count > 0)
            {
                fullItemName = AvailableKeys.SelectedItem.ToString();
                selectedKey = fullItemName.Split(' ')[0].Replace("(!)", "");
                selectedValType = fullItemName.Split(' ')[2].Replace(")", "");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void FabricatorKeyvalueLoader_Load(object sender, EventArgs e)
        {

        }
    }

    public class JSONEntry
    {
        public string Key { get; set; } = "";
        public string FileType { get; set; } = "";
        public string ValType { get; set; } = "";
        public bool Required { get; set; } = false;

        public override string ToString()
        {
            return $"{((Required) ? "(!)" : "")}{Key} ({FileType}, {ValType})";
        }
    }

    public class SchemeLoader
    {
        public static List<JSONEntry>? LoadFile(string filePath)
        {
            List<JSONEntry>? result = null;

            using (var stream = File.OpenRead(filePath))
            {
                result = JsonSerializer.Deserialize<List<JSONEntry>>(stream);
            }

            return result;
        }
    }
}
