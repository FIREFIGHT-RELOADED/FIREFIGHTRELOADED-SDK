namespace Fabricator
{
    partial class FabricatorKeyvalueLoader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FabricatorKeyvalueLoader));
            AvailableKeys = new ListBox();
            SuspendLayout();
            // 
            // AvailableKeys
            // 
            AvailableKeys.Dock = DockStyle.Fill;
            AvailableKeys.FormattingEnabled = true;
            AvailableKeys.ItemHeight = 19;
            AvailableKeys.Location = new Point(0, 0);
            AvailableKeys.Margin = new Padding(4, 4, 4, 4);
            AvailableKeys.Name = "AvailableKeys";
            AvailableKeys.Size = new Size(606, 458);
            AvailableKeys.TabIndex = 0;
            AvailableKeys.SelectedIndexChanged += AvailableKeys_SelectedIndexChanged;
            // 
            // FabricatorKeyvalueLoader
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(606, 458);
            Controls.Add(AvailableKeys);
            Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 4, 4, 4);
            Name = "FabricatorKeyvalueLoader";
            Text = "Available Keys";
            ResumeLayout(false);
        }

        #endregion

        private ListBox AvailableKeys;
    }
}