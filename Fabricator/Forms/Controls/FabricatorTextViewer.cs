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
    public partial class FabricatorTextViewer : Form
    {
        public FabricatorTextViewer(string path)
        {
            InitializeComponent();
            CenterToScreen();
            richTextBox1.Text = File.ReadAllText(path);
        }
    }
}
