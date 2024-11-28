namespace Fabricator
{
    partial class FabricatorCollectionEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FabricatorCollectionEditor));
            KeyValueSet = new DataGridView();
            KeyColumn = new DataGridViewTextBoxColumn();
            ValueColumn = new DataGridViewTextBoxColumn();
            menuStrip1 = new MenuStrip();
            createRowFromKeyListToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)KeyValueSet).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // KeyValueSet
            // 
            KeyValueSet.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            KeyValueSet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            KeyValueSet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            KeyValueSet.Columns.AddRange(new DataGridViewColumn[] { KeyColumn, ValueColumn });
            KeyValueSet.Location = new Point(0, 24);
            KeyValueSet.MultiSelect = false;
            KeyValueSet.Name = "KeyValueSet";
            KeyValueSet.RowHeadersWidth = 51;
            KeyValueSet.RowTemplate.Height = 25;
            KeyValueSet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            KeyValueSet.Size = new Size(519, 378);
            KeyValueSet.TabIndex = 1;
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
            // menuStrip1
            // 
            menuStrip1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            menuStrip1.Dock = DockStyle.None;
            menuStrip1.Items.AddRange(new ToolStripItem[] { createRowFromKeyListToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(159, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // createRowFromKeyListToolStripMenuItem
            // 
            createRowFromKeyListToolStripMenuItem.Name = "createRowFromKeyListToolStripMenuItem";
            createRowFromKeyListToolStripMenuItem.Size = new Size(151, 20);
            createRowFromKeyListToolStripMenuItem.Text = "Create Row from Key List";
            createRowFromKeyListToolStripMenuItem.Click += createRowFromKeyListToolStripMenuItem_Click;
            // 
            // FabricatorCollectionEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(519, 402);
            Controls.Add(KeyValueSet);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(335, 157);
            Name = "FabricatorCollectionEditor";
            Text = "Edit Collection";
            FormClosing += FabricatorCollectionEditor_FormClosing;
            Load += FabricatorCollectionEditor_Load;
            ((System.ComponentModel.ISupportInitialize)KeyValueSet).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView KeyValueSet;
        private DataGridViewTextBoxColumn KeyColumn;
        private DataGridViewTextBoxColumn ValueColumn;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem createRowFromKeyListToolStripMenuItem;
    }
}