using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        List<JSONEntry>? JSONEntries {  get; set; }
        public string selectedKey { get; set; }

        public FabricatorKeyvalueLoader()
        {
            InitializeComponent();

            CenterToScreen();

            JSONEntries = SchemeLoader.LoadFile(Path.Combine(GlobalVars.DataPath, "schema.json"));

            if (JSONEntries != null)
            {
                foreach (var entry in JSONEntries)
                {
                    AvailableKeys.Items.Add($"{entry.Key} ({entry.FileType})");
                }
            }
        }

        private void AvailableKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AvailableKeys.Items.Count > 0)
            {
                selectedKey = AvailableKeys.SelectedItem.ToString().Split(' ')[0];
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }

    public class JSONEntry
    {
        public string Key { get; set; } = "";
        public string FileType { get; set; } = "";
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
