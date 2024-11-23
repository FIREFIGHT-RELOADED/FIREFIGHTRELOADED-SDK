using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fabricator
{
    public partial class FabricatorLoadStringPrompt : Form
    {
        public string result = "";

        public FabricatorLoadStringPrompt()
        {
            InitializeComponent();

            CenterToScreen();
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            result = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
