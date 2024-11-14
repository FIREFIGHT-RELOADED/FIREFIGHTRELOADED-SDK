namespace Fabricator
{
    partial class FabricatorEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FabricatorEditorForm));
            menu = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            createRowFromKeyListToolStripMenuItem = new ToolStripMenuItem();
            KVNameBox = new ToolStripTextBox();
            KeyValueSet = new DataGridView();
            KeyColumn = new DataGridViewTextBoxColumn();
            ValueColumn = new DataGridViewTextBoxColumn();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            splitContainer1 = new SplitContainer();
            NodeList = new TreeView();
            menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)KeyValueSet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // menu
            // 
            menu.ImageScalingSize = new Size(20, 20);
            menu.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, KVNameBox });
            menu.Location = new Point(0, 0);
            menu.Name = "menu";
            menu.Padding = new Padding(7, 3, 0, 3);
            menu.Size = new Size(661, 33);
            menu.TabIndex = 0;
            menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, loadToolStripMenuItem, saveToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 27);
            fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(125, 26);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(125, 26);
            loadToolStripMenuItem.Text = "Load";
            loadToolStripMenuItem.Click += loadToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(125, 26);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { createRowFromKeyListToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(49, 27);
            editToolStripMenuItem.Text = "Edit";
            // 
            // createRowFromKeyListToolStripMenuItem
            // 
            createRowFromKeyListToolStripMenuItem.Name = "createRowFromKeyListToolStripMenuItem";
            createRowFromKeyListToolStripMenuItem.Size = new Size(258, 26);
            createRowFromKeyListToolStripMenuItem.Text = "Create Row from Key List";
            createRowFromKeyListToolStripMenuItem.Click += createRowFromKeyListToolStripMenuItem_Click;
            // 
            // KVNameBox
            // 
            KVNameBox.BorderStyle = BorderStyle.FixedSingle;
            KVNameBox.Name = "KVNameBox";
            KVNameBox.Size = new Size(114, 27);
            KVNameBox.TextChanged += KVNameBox_TextChanged;
            // 
            // KeyValueSet
            // 
            KeyValueSet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            KeyValueSet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            KeyValueSet.Columns.AddRange(new DataGridViewColumn[] { KeyColumn, ValueColumn });
            KeyValueSet.Dock = DockStyle.Fill;
            KeyValueSet.Location = new Point(0, 0);
            KeyValueSet.Margin = new Padding(3, 4, 3, 4);
            KeyValueSet.Name = "KeyValueSet";
            KeyValueSet.RowHeadersWidth = 51;
            KeyValueSet.RowTemplate.Height = 25;
            KeyValueSet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            KeyValueSet.Size = new Size(439, 474);
            KeyValueSet.TabIndex = 1;
            KeyValueSet.CellContentDoubleClick += KeyValueSet_CellContentDoubleClick;
            KeyValueSet.CellLeave += KeyValueSet_CellLeave;
            KeyValueSet.Leave += KeyValueSet_CellLeave;
            // 
            // KeyColumn
            // 
            KeyColumn.HeaderText = "Key";
            KeyColumn.MinimumWidth = 6;
            KeyColumn.Name = "KeyColumn";
            // 
            // ValueColumn
            // 
            ValueColumn.HeaderText = "Value";
            ValueColumn.MinimumWidth = 6;
            ValueColumn.Name = "ValueColumn";
            // 
            // openFileDialog1
            // 
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Text Files|*.txt|All Files|*.*";
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Text Files|*.txt|All Files|*.*";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 33);
            splitContainer1.Margin = new Padding(3, 4, 3, 4);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(NodeList);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(KeyValueSet);
            splitContainer1.Size = new Size(661, 474);
            splitContainer1.SplitterDistance = 217;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 2;
            // 
            // NodeList
            // 
            NodeList.Dock = DockStyle.Fill;
            NodeList.Location = new Point(0, 0);
            NodeList.Margin = new Padding(3, 4, 3, 4);
            NodeList.Name = "NodeList";
            NodeList.Size = new Size(217, 474);
            NodeList.TabIndex = 0;
            NodeList.AfterSelect += NodeList_AfterSelect;
            // 
            // FabricatorEditorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(661, 507);
            Controls.Add(splitContainer1);
            Controls.Add(menu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menu;
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(381, 196);
            Name = "FabricatorEditorForm";
            Text = "Fabricator";
            Load += FabricatorOtherForm_Load;
            menu.ResumeLayout(false);
            menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)KeyValueSet).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private DataGridView KeyValueSet;
        private DataGridViewTextBoxColumn KeyColumn;
        private DataGridViewTextBoxColumn ValueColumn;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem createRowFromKeyListToolStripMenuItem;
        private ToolStripTextBox KVNameBox;
        private SplitContainer splitContainer1;
        private TreeView NodeList;
    }
}