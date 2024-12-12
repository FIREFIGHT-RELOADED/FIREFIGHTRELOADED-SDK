
namespace WorkshopUploader
{
    partial class WorkshopUploader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkshopUploader));
            ItemName = new System.Windows.Forms.Label();
            ItemNameBox = new System.Windows.Forms.TextBox();
            ItemImageBox = new System.Windows.Forms.PictureBox();
            ItemImage = new System.Windows.Forms.Label();
            ItemDescBox = new System.Windows.Forms.RichTextBox();
            ItemDesc = new System.Windows.Forms.Label();
            ItemImageBrowse = new System.Windows.Forms.Button();
            ItemPathBox = new System.Windows.Forms.TextBox();
            ItemPath = new System.Windows.Forms.Label();
            BrowseFolder = new System.Windows.Forms.Button();
            ItemID = new System.Windows.Forms.Label();
            ItemIDBox = new System.Windows.Forms.TextBox();
            LoadItem = new System.Windows.Forms.Button();
            ItemChangesBox = new System.Windows.Forms.RichTextBox();
            ItemChanges = new System.Windows.Forms.Label();
            UploadItem = new System.Windows.Forms.Button();
            progressBar1 = new System.Windows.Forms.ProgressBar();
            ItemEditingBox = new System.Windows.Forms.CheckBox();
            tagsBox = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)ItemImageBox).BeginInit();
            SuspendLayout();
            // 
            // ItemName
            // 
            ItemName.AutoSize = true;
            ItemName.Location = new System.Drawing.Point(168, 5);
            ItemName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            ItemName.Name = "ItemName";
            ItemName.Size = new System.Drawing.Size(66, 15);
            ItemName.TabIndex = 0;
            ItemName.Text = "Item Name";
            // 
            // ItemNameBox
            // 
            ItemNameBox.Location = new System.Drawing.Point(18, 25);
            ItemNameBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ItemNameBox.Name = "ItemNameBox";
            ItemNameBox.Size = new System.Drawing.Size(374, 23);
            ItemNameBox.TabIndex = 1;
            ItemNameBox.TextChanged += ItemNameBox_TextChanged;
            // 
            // ItemImageBox
            // 
            ItemImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            ItemImageBox.Location = new System.Drawing.Point(13, 294);
            ItemImageBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ItemImageBox.Name = "ItemImageBox";
            ItemImageBox.Size = new System.Drawing.Size(166, 165);
            ItemImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            ItemImageBox.TabIndex = 2;
            ItemImageBox.TabStop = false;
            // 
            // ItemImage
            // 
            ItemImage.AutoSize = true;
            ItemImage.Location = new System.Drawing.Point(58, 271);
            ItemImage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            ItemImage.Name = "ItemImage";
            ItemImage.Size = new System.Drawing.Size(75, 15);
            ItemImage.TabIndex = 3;
            ItemImage.Text = "Item Preview";
            // 
            // ItemDescBox
            // 
            ItemDescBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            ItemDescBox.Location = new System.Drawing.Point(13, 72);
            ItemDescBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ItemDescBox.Name = "ItemDescBox";
            ItemDescBox.Size = new System.Drawing.Size(378, 192);
            ItemDescBox.TabIndex = 5;
            ItemDescBox.Text = "";
            ItemDescBox.TextChanged += ItemDescBox_TextChanged;
            // 
            // ItemDesc
            // 
            ItemDesc.AutoSize = true;
            ItemDesc.Location = new System.Drawing.Point(153, 53);
            ItemDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            ItemDesc.Name = "ItemDesc";
            ItemDesc.Size = new System.Drawing.Size(94, 15);
            ItemDesc.TabIndex = 4;
            ItemDesc.Text = "Item Description";
            // 
            // ItemImageBrowse
            // 
            ItemImageBrowse.Location = new System.Drawing.Point(13, 466);
            ItemImageBrowse.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ItemImageBrowse.Name = "ItemImageBrowse";
            ItemImageBrowse.Size = new System.Drawing.Size(167, 27);
            ItemImageBrowse.TabIndex = 6;
            ItemImageBrowse.Text = "Browse Image...";
            ItemImageBrowse.UseVisualStyleBackColor = true;
            ItemImageBrowse.Click += ItemImageBrowse_Click;
            // 
            // ItemPathBox
            // 
            ItemPathBox.Location = new System.Drawing.Point(187, 436);
            ItemPathBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ItemPathBox.Name = "ItemPathBox";
            ItemPathBox.ReadOnly = true;
            ItemPathBox.Size = new System.Drawing.Size(200, 23);
            ItemPathBox.TabIndex = 7;
            // 
            // ItemPath
            // 
            ItemPath.AutoSize = true;
            ItemPath.Location = new System.Drawing.Point(233, 418);
            ItemPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            ItemPath.Name = "ItemPath";
            ItemPath.Size = new System.Drawing.Size(113, 15);
            ItemPath.TabIndex = 8;
            ItemPath.Text = "Item Content Folder";
            // 
            // BrowseFolder
            // 
            BrowseFolder.Location = new System.Drawing.Point(187, 466);
            BrowseFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BrowseFolder.Name = "BrowseFolder";
            BrowseFolder.Size = new System.Drawing.Size(201, 27);
            BrowseFolder.TabIndex = 9;
            BrowseFolder.Text = "Browse Content Folder...";
            BrowseFolder.UseVisualStyleBackColor = true;
            BrowseFolder.Click += BrowseFolder_Click;
            // 
            // ItemID
            // 
            ItemID.AutoSize = true;
            ItemID.Location = new System.Drawing.Point(537, 5);
            ItemID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            ItemID.Name = "ItemID";
            ItemID.Size = new System.Drawing.Size(116, 15);
            ItemID.TabIndex = 11;
            ItemID.Text = "ID of the item to edit";
            // 
            // ItemIDBox
            // 
            ItemIDBox.Location = new System.Drawing.Point(489, 25);
            ItemIDBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ItemIDBox.Name = "ItemIDBox";
            ItemIDBox.Size = new System.Drawing.Size(135, 23);
            ItemIDBox.TabIndex = 12;
            ItemIDBox.TextChanged += ItemIDBox_TextChanged;
            // 
            // LoadItem
            // 
            LoadItem.Location = new System.Drawing.Point(631, 23);
            LoadItem.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            LoadItem.Name = "LoadItem";
            LoadItem.Size = new System.Drawing.Size(85, 27);
            LoadItem.TabIndex = 13;
            LoadItem.Text = "Load Item";
            LoadItem.UseVisualStyleBackColor = true;
            LoadItem.Click += LoadItem_Click;
            // 
            // ItemChangesBox
            // 
            ItemChangesBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            ItemChangesBox.Enabled = false;
            ItemChangesBox.Location = new System.Drawing.Point(410, 72);
            ItemChangesBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ItemChangesBox.Name = "ItemChangesBox";
            ItemChangesBox.Size = new System.Drawing.Size(375, 512);
            ItemChangesBox.TabIndex = 15;
            ItemChangesBox.Text = "";
            ItemChangesBox.TextChanged += ItemChangesBox_TextChanged;
            // 
            // ItemChanges
            // 
            ItemChanges.AutoSize = true;
            ItemChanges.Location = new System.Drawing.Point(551, 53);
            ItemChanges.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            ItemChanges.Name = "ItemChanges";
            ItemChanges.Size = new System.Drawing.Size(80, 15);
            ItemChanges.TabIndex = 14;
            ItemChanges.Text = "Item Changes";
            // 
            // UploadItem
            // 
            UploadItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            UploadItem.Location = new System.Drawing.Point(13, 500);
            UploadItem.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            UploadItem.Name = "UploadItem";
            UploadItem.Size = new System.Drawing.Size(374, 51);
            UploadItem.TabIndex = 16;
            UploadItem.Text = "UPLOAD ITEM";
            UploadItem.UseVisualStyleBackColor = true;
            UploadItem.Click += UploadItem_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new System.Drawing.Point(13, 557);
            progressBar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(374, 27);
            progressBar1.TabIndex = 17;
            // 
            // ItemEditingBox
            // 
            ItemEditingBox.AutoSize = true;
            ItemEditingBox.Location = new System.Drawing.Point(18, 3);
            ItemEditingBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ItemEditingBox.Name = "ItemEditingBox";
            ItemEditingBox.Size = new System.Drawing.Size(73, 19);
            ItemEditingBox.TabIndex = 18;
            ItemEditingBox.Text = "Edit Item";
            ItemEditingBox.UseVisualStyleBackColor = true;
            ItemEditingBox.CheckedChanged += ItemEditingBox_CheckedChanged;
            // 
            // tagsBox
            // 
            tagsBox.FormattingEnabled = true;
            tagsBox.Items.AddRange(new object[] { "Weapon", "Works with Store Menu", "Works with Reward System", "Spawner Spawnlist", "Skin", "Sound", "Player Voice", "Overhaul", "Gamemode", "Character", "Map", "Music" });
            tagsBox.Location = new System.Drawing.Point(187, 271);
            tagsBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tagsBox.Name = "tagsBox";
            tagsBox.Size = new System.Drawing.Size(205, 130);
            tagsBox.TabIndex = 19;
            // 
            // WorkshopUploader
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ControlLightLight;
            ClientSize = new System.Drawing.Size(811, 593);
            Controls.Add(tagsBox);
            Controls.Add(ItemEditingBox);
            Controls.Add(progressBar1);
            Controls.Add(UploadItem);
            Controls.Add(ItemChangesBox);
            Controls.Add(ItemChanges);
            Controls.Add(LoadItem);
            Controls.Add(ItemIDBox);
            Controls.Add(ItemID);
            Controls.Add(BrowseFolder);
            Controls.Add(ItemPath);
            Controls.Add(ItemPathBox);
            Controls.Add(ItemImageBrowse);
            Controls.Add(ItemDescBox);
            Controls.Add(ItemDesc);
            Controls.Add(ItemImage);
            Controls.Add(ItemImageBox);
            Controls.Add(ItemNameBox);
            Controls.Add(ItemName);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "WorkshopUploader";
            Text = "Workshop Uploader";
            FormClosed += WorkshopUploader_FormClosed;
            Load += WorkshopUploader_Load;
            ((System.ComponentModel.ISupportInitialize)ItemImageBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label ItemName;
        private System.Windows.Forms.TextBox ItemNameBox;
        private System.Windows.Forms.PictureBox ItemImageBox;
        private System.Windows.Forms.Label ItemImage;
        private System.Windows.Forms.RichTextBox ItemDescBox;
        private System.Windows.Forms.Label ItemDesc;
        private System.Windows.Forms.Button ItemImageBrowse;
        private System.Windows.Forms.TextBox ItemPathBox;
        private System.Windows.Forms.Label ItemPath;
        private System.Windows.Forms.Button BrowseFolder;
        private System.Windows.Forms.Label ItemID;
        private System.Windows.Forms.TextBox ItemIDBox;
        private System.Windows.Forms.Button LoadItem;
        private System.Windows.Forms.RichTextBox ItemChangesBox;
        private System.Windows.Forms.Label ItemChanges;
        private System.Windows.Forms.Button UploadItem;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox ItemEditingBox;
        private System.Windows.Forms.CheckedListBox tagsBox;
    }
}