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
            createToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)KeyValueSet).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // KeyValueSet
            // 
            KeyValueSet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            KeyValueSet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            KeyValueSet.Columns.AddRange(new DataGridViewColumn[] { KeyColumn, ValueColumn });
            KeyValueSet.Dock = DockStyle.Fill;
            KeyValueSet.Location = new Point(0, 28);
            KeyValueSet.Margin = new Padding(3, 4, 3, 4);
            KeyValueSet.Name = "KeyValueSet";
            KeyValueSet.RowHeadersWidth = 51;
            KeyValueSet.RowTemplate.Height = 25;
            KeyValueSet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            KeyValueSet.Size = new Size(578, 492);
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
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { createToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(578, 28);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // createToolStripMenuItem
            // 
            createToolStripMenuItem.Name = "createToolStripMenuItem";
            createToolStripMenuItem.Size = new Size(189, 24);
            createToolStripMenuItem.Text = "Create Row from Key List";
            createToolStripMenuItem.Click += createToolStripMenuItem_Click;
            // 
            // FabricatorCollectionEditor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(578, 520);
            Controls.Add(KeyValueSet);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(381, 196);
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
        private ToolStripMenuItem createToolStripMenuItem;
    }
}