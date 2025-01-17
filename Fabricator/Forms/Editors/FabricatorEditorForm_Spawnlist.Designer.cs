namespace Fabricator
{
    partial class FabricatorEditorForm_Spawnlist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FabricatorEditorForm_Spawnlist));
            menu = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            createRowFromKeyListToolStripMenuItem = new ToolStripMenuItem();
            addNodeToolStripMenuItem = new ToolStripMenuItem();
            duplicateNodeToolStripMenuItem = new ToolStripMenuItem();
            deleteNodeToolStripMenuItem = new ToolStripMenuItem();
            moveNodeUpToolStripMenuItem = new ToolStripMenuItem();
            moveNodeDownToolStripMenuItem = new ToolStripMenuItem();
            moveNodeToTopToolStripMenuItem = new ToolStripMenuItem();
            moveNodeToBottomToolStripMenuItem = new ToolStripMenuItem();
            editSettingsToolStripMenuItem = new ToolStripMenuItem();
            KeyValueSet = new DataGridView();
            KeyColumn = new DataGridViewTextBoxColumn();
            ValueColumn = new DataGridViewTextBoxColumn();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            splitContainer1 = new SplitContainer();
            NodeList = new TreeView();
            toolStripSeparator1 = new ToolStripSeparator();
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
            menu.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem });
            menu.Location = new Point(0, 0);
            menu.Name = "menu";
            menu.Size = new Size(578, 24);
            menu.TabIndex = 0;
            menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, loadToolStripMenuItem, saveToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(100, 22);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(100, 22);
            loadToolStripMenuItem.Text = "Load";
            loadToolStripMenuItem.Click += loadToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(100, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { createRowFromKeyListToolStripMenuItem, addNodeToolStripMenuItem, duplicateNodeToolStripMenuItem, deleteNodeToolStripMenuItem, moveNodeUpToolStripMenuItem, moveNodeDownToolStripMenuItem, moveNodeToTopToolStripMenuItem, moveNodeToBottomToolStripMenuItem, toolStripSeparator1, editSettingsToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // createRowFromKeyListToolStripMenuItem
            // 
            createRowFromKeyListToolStripMenuItem.Name = "createRowFromKeyListToolStripMenuItem";
            createRowFromKeyListToolStripMenuItem.Size = new Size(206, 22);
            createRowFromKeyListToolStripMenuItem.Text = "Create Row from Key List";
            createRowFromKeyListToolStripMenuItem.Click += createRowFromKeyListToolStripMenuItem_Click;
            // 
            // addNodeToolStripMenuItem
            // 
            addNodeToolStripMenuItem.Name = "addNodeToolStripMenuItem";
            addNodeToolStripMenuItem.Size = new Size(206, 22);
            addNodeToolStripMenuItem.Text = "Add Node";
            addNodeToolStripMenuItem.Click += addNodeToolStripMenuItem_Click;
            // 
            // duplicateNodeToolStripMenuItem
            // 
            duplicateNodeToolStripMenuItem.Name = "duplicateNodeToolStripMenuItem";
            duplicateNodeToolStripMenuItem.Size = new Size(206, 22);
            duplicateNodeToolStripMenuItem.Text = "Duplicate Node";
            duplicateNodeToolStripMenuItem.Click += duplicateNodeToolStripMenuItem_Click;
            // 
            // deleteNodeToolStripMenuItem
            // 
            deleteNodeToolStripMenuItem.Name = "deleteNodeToolStripMenuItem";
            deleteNodeToolStripMenuItem.Size = new Size(206, 22);
            deleteNodeToolStripMenuItem.Text = "Delete Node";
            deleteNodeToolStripMenuItem.Click += deleteNodeToolStripMenuItem_Click;
            // 
            // moveNodeUpToolStripMenuItem
            // 
            moveNodeUpToolStripMenuItem.Name = "moveNodeUpToolStripMenuItem";
            moveNodeUpToolStripMenuItem.Size = new Size(206, 22);
            moveNodeUpToolStripMenuItem.Text = "Move Node Up";
            moveNodeUpToolStripMenuItem.Click += moveNodeUpToolStripMenuItem_Click;
            // 
            // moveNodeDownToolStripMenuItem
            // 
            moveNodeDownToolStripMenuItem.Name = "moveNodeDownToolStripMenuItem";
            moveNodeDownToolStripMenuItem.Size = new Size(206, 22);
            moveNodeDownToolStripMenuItem.Text = "Move Node Down";
            moveNodeDownToolStripMenuItem.Click += moveNodeDownToolStripMenuItem_Click;
            // 
            // moveNodeToTopToolStripMenuItem
            // 
            moveNodeToTopToolStripMenuItem.Name = "moveNodeToTopToolStripMenuItem";
            moveNodeToTopToolStripMenuItem.Size = new Size(206, 22);
            moveNodeToTopToolStripMenuItem.Text = "Move Node to Top";
            moveNodeToTopToolStripMenuItem.Click += moveNodeToTopToolStripMenuItem_Click;
            // 
            // moveNodeToBottomToolStripMenuItem
            // 
            moveNodeToBottomToolStripMenuItem.Name = "moveNodeToBottomToolStripMenuItem";
            moveNodeToBottomToolStripMenuItem.Size = new Size(206, 22);
            moveNodeToBottomToolStripMenuItem.Text = "Move Node to Bottom";
            moveNodeToBottomToolStripMenuItem.Click += moveNodeToBottomToolStripMenuItem_Click;
            // 
            // editSettingsToolStripMenuItem
            // 
            editSettingsToolStripMenuItem.Name = "editSettingsToolStripMenuItem";
            editSettingsToolStripMenuItem.Size = new Size(206, 22);
            editSettingsToolStripMenuItem.Text = "Edit Settings";
            editSettingsToolStripMenuItem.Click += editSettingsToolStripMenuItem_Click;
            // 
            // KeyValueSet
            // 
            KeyValueSet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            KeyValueSet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            KeyValueSet.Columns.AddRange(new DataGridViewColumn[] { KeyColumn, ValueColumn });
            KeyValueSet.Dock = DockStyle.Fill;
            KeyValueSet.Location = new Point(0, 0);
            KeyValueSet.MultiSelect = false;
            KeyValueSet.Name = "KeyValueSet";
            KeyValueSet.RowHeadersWidth = 51;
            KeyValueSet.RowTemplate.Height = 25;
            KeyValueSet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            KeyValueSet.Size = new Size(385, 356);
            KeyValueSet.TabIndex = 1;
            KeyValueSet.CellMouseDoubleClick += KeyValueSet_CellMouseDoubleClick;
            KeyValueSet.CellValidated += KeyValueSet_CellLeave;
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
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(NodeList);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(KeyValueSet);
            splitContainer1.Size = new Size(578, 356);
            splitContainer1.SplitterDistance = 189;
            splitContainer1.TabIndex = 2;
            // 
            // NodeList
            // 
            NodeList.Dock = DockStyle.Fill;
            NodeList.Location = new Point(0, 0);
            NodeList.Name = "NodeList";
            NodeList.Size = new Size(189, 356);
            NodeList.TabIndex = 0;
            NodeList.AfterSelect += NodeList_AfterSelect;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(203, 6);
            // 
            // FabricatorEditorForm_Spawnlist
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(578, 380);
            Controls.Add(splitContainer1);
            Controls.Add(menu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menu;
            MinimumSize = new Size(335, 157);
            Name = "FabricatorEditorForm_Spawnlist";
            Text = "Fabricator - Spawnlist";
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
        private SplitContainer splitContainer1;
        private TreeView NodeList;
        private ToolStripMenuItem addNodeToolStripMenuItem;
        private ToolStripMenuItem deleteNodeToolStripMenuItem;
        private ToolStripMenuItem editSettingsToolStripMenuItem;
        private ToolStripMenuItem moveNodeUpToolStripMenuItem;
        private ToolStripMenuItem moveNodeDownToolStripMenuItem;
        private ToolStripMenuItem duplicateNodeToolStripMenuItem;
        private ToolStripMenuItem moveNodeToTopToolStripMenuItem;
        private ToolStripMenuItem moveNodeToBottomToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
    }
}