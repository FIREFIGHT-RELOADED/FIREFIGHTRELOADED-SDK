namespace Fabricator
{
    partial class FabricatorMainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FabricatorMainForm));
            FileTypeList = new GroupBox();
            SpawnlistRadioButton = new RadioButton();
            OtherRadioButton = new RadioButton();
            CatalogRadioButton = new RadioButton();
            PlaylistRadioButton = new RadioButton();
            RewardRadioButton = new RadioButton();
            MapaddRadioButton = new RadioButton();
            LoadoutRadioButton = new RadioButton();
            OpenFileEditor = new Button();
            SelectionLabel = new Label();
            FileTypeList.SuspendLayout();
            SuspendLayout();
            // 
            // FileTypeList
            // 
            FileTypeList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            FileTypeList.Controls.Add(SpawnlistRadioButton);
            FileTypeList.Controls.Add(OtherRadioButton);
            FileTypeList.Controls.Add(CatalogRadioButton);
            FileTypeList.Controls.Add(PlaylistRadioButton);
            FileTypeList.Controls.Add(RewardRadioButton);
            FileTypeList.Controls.Add(MapaddRadioButton);
            FileTypeList.Controls.Add(LoadoutRadioButton);
            FileTypeList.Location = new Point(12, 12);
            FileTypeList.Name = "FileTypeList";
            FileTypeList.Size = new Size(339, 197);
            FileTypeList.TabIndex = 7;
            FileTypeList.TabStop = false;
            FileTypeList.Text = "Select a file type to edit.";
            // 
            // SpawnlistRadioButton
            // 
            SpawnlistRadioButton.AutoSize = true;
            SpawnlistRadioButton.Location = new Point(6, 22);
            SpawnlistRadioButton.Name = "SpawnlistRadioButton";
            SpawnlistRadioButton.Size = new Size(208, 19);
            SpawnlistRadioButton.TabIndex = 0;
            SpawnlistRadioButton.TabStop = true;
            SpawnlistRadioButton.Text = "Spawnlist (used for NPC spawners)";
            SpawnlistRadioButton.UseVisualStyleBackColor = true;
            SpawnlistRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // OtherRadioButton
            // 
            OtherRadioButton.AutoSize = true;
            OtherRadioButton.Location = new Point(6, 172);
            OtherRadioButton.Name = "OtherRadioButton";
            OtherRadioButton.Size = new Size(203, 19);
            OtherRadioButton.TabIndex = 6;
            OtherRadioButton.TabStop = true;
            OtherRadioButton.Text = "Other (Mapinfo, NPC presets, etc)";
            OtherRadioButton.UseVisualStyleBackColor = true;
            OtherRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // CatalogRadioButton
            // 
            CatalogRadioButton.AutoSize = true;
            CatalogRadioButton.Location = new Point(6, 47);
            CatalogRadioButton.Name = "CatalogRadioButton";
            CatalogRadioButton.Size = new Size(249, 19);
            CatalogRadioButton.TabIndex = 1;
            CatalogRadioButton.TabStop = true;
            CatalogRadioButton.Text = "Shop Catalog (Used for the in-game Shop)";
            CatalogRadioButton.UseVisualStyleBackColor = true;
            CatalogRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // PlaylistRadioButton
            // 
            PlaylistRadioButton.AutoSize = true;
            PlaylistRadioButton.Location = new Point(6, 147);
            PlaylistRadioButton.Name = "PlaylistRadioButton";
            PlaylistRadioButton.Size = new Size(311, 19);
            PlaylistRadioButton.TabIndex = 5;
            PlaylistRadioButton.TabStop = true;
            PlaylistRadioButton.Text = "Music Playlist (Played in game with the Music System)";
            PlaylistRadioButton.UseVisualStyleBackColor = true;
            PlaylistRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // RewardRadioButton
            // 
            RewardRadioButton.AutoSize = true;
            RewardRadioButton.Location = new Point(6, 72);
            RewardRadioButton.Name = "RewardRadioButton";
            RewardRadioButton.Size = new Size(283, 19);
            RewardRadioButton.TabIndex = 2;
            RewardRadioButton.TabStop = true;
            RewardRadioButton.Text = "Rewards (Given to the player upon level increase)";
            RewardRadioButton.UseVisualStyleBackColor = true;
            RewardRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // MapaddRadioButton
            // 
            MapaddRadioButton.AutoSize = true;
            MapaddRadioButton.Location = new Point(6, 122);
            MapaddRadioButton.Name = "MapaddRadioButton";
            MapaddRadioButton.Size = new Size(212, 19);
            MapaddRadioButton.TabIndex = 4;
            MapaddRadioButton.TabStop = true;
            MapaddRadioButton.Text = "MapAdd (Creates entities for maps)";
            MapaddRadioButton.UseVisualStyleBackColor = true;
            MapaddRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // LoadoutRadioButton
            // 
            LoadoutRadioButton.AutoSize = true;
            LoadoutRadioButton.Location = new Point(6, 97);
            LoadoutRadioButton.Name = "LoadoutRadioButton";
            LoadoutRadioButton.Size = new Size(236, 19);
            LoadoutRadioButton.TabIndex = 3;
            LoadoutRadioButton.TabStop = true;
            LoadoutRadioButton.Text = "Loadout (Adjusts player stats and items)";
            LoadoutRadioButton.UseVisualStyleBackColor = true;
            LoadoutRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // OpenFileEditor
            // 
            OpenFileEditor.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            OpenFileEditor.Location = new Point(12, 237);
            OpenFileEditor.Name = "OpenFileEditor";
            OpenFileEditor.Size = new Size(339, 23);
            OpenFileEditor.TabIndex = 8;
            OpenFileEditor.Text = "Open Editor";
            OpenFileEditor.UseVisualStyleBackColor = true;
            OpenFileEditor.Click += OpenFileEditor_Click;
            // 
            // SelectionLabel
            // 
            SelectionLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SelectionLabel.Location = new Point(12, 215);
            SelectionLabel.Name = "SelectionLabel";
            SelectionLabel.Size = new Size(339, 15);
            SelectionLabel.TabIndex = 9;
            SelectionLabel.Text = "Selected: [ItemHere]";
            // 
            // FabricatorMainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(363, 268);
            Controls.Add(SelectionLabel);
            Controls.Add(OpenFileEditor);
            Controls.Add(FileTypeList);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FabricatorMainForm";
            Text = "Fabricator";
            Load += FabricatorForm_Load;
            FileTypeList.ResumeLayout(false);
            FileTypeList.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private GroupBox FileTypeList;
        private RadioButton SpawnlistRadioButton;
        private RadioButton CatalogRadioButton;
        private RadioButton RewardRadioButton;
        private RadioButton LoadoutRadioButton;
        private RadioButton MapaddRadioButton;
        private RadioButton PlaylistRadioButton;
        private RadioButton OtherRadioButton;
        private Button OpenFileEditor;
        private Label SelectionLabel;
    }
}
