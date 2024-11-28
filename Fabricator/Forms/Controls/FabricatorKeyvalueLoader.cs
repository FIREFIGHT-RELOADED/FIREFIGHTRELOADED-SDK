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

            // load and deserialize the schema. While reading every entry, determine what should be shown by checking if the file type name
            // exists in the "FileType" string.
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
                // trim off some bits to get the key name and the value type so we can send it as a return output.
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

        // the (!) is a mark to the user that a value is required for a entry to work properly.
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
