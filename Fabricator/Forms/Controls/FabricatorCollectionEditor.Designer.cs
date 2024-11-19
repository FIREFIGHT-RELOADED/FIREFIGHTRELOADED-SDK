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
            ((System.ComponentModel.ISupportInitialize)KeyValueSet).BeginInit();
            SuspendLayout();
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
            KeyValueSet.Size = new Size(506, 390);
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
            // FabricatorCollectionEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(506, 390);
            Controls.Add(KeyValueSet);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(335, 157);
            Name = "FabricatorCollectionEditor";
            Text = "Edit Collection";
            FormClosing += FabricatorCollectionEditor_FormClosing;
            Load += FabricatorCollectionEditor_Load;
            ((System.ComponentModel.ISupportInitialize)KeyValueSet).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView KeyValueSet;
        private DataGridViewTextBoxColumn KeyColumn;
        private DataGridViewTextBoxColumn ValueColumn;
    }
}