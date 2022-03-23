
namespace FR_SDK.App
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
            this.ItemName = new System.Windows.Forms.Label();
            this.ItemNameBox = new System.Windows.Forms.TextBox();
            this.ItemImageBox = new System.Windows.Forms.PictureBox();
            this.ItemImage = new System.Windows.Forms.Label();
            this.ItemDescBox = new System.Windows.Forms.TextBox();
            this.ItemDesc = new System.Windows.Forms.Label();
            this.ItemImageBrowse = new System.Windows.Forms.Button();
            this.ItemPathBox = new System.Windows.Forms.TextBox();
            this.ItemPath = new System.Windows.Forms.Label();
            this.BrowseFolder = new System.Windows.Forms.Button();
            this.ItemID = new System.Windows.Forms.Label();
            this.ItemIDBox = new System.Windows.Forms.TextBox();
            this.LoadItem = new System.Windows.Forms.Button();
            this.ItemChangesBox = new System.Windows.Forms.TextBox();
            this.ItemChanges = new System.Windows.Forms.Label();
            this.UploadItem = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.ItemEditingBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ItemImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ItemName
            // 
            this.ItemName.AutoSize = true;
            this.ItemName.Location = new System.Drawing.Point(69, 7);
            this.ItemName.Name = "ItemName";
            this.ItemName.Size = new System.Drawing.Size(58, 13);
            this.ItemName.TabIndex = 0;
            this.ItemName.Text = "Item Name";
            // 
            // ItemNameBox
            // 
            this.ItemNameBox.Location = new System.Drawing.Point(11, 23);
            this.ItemNameBox.Name = "ItemNameBox";
            this.ItemNameBox.Size = new System.Drawing.Size(172, 20);
            this.ItemNameBox.TabIndex = 1;
            this.ItemNameBox.TextChanged += new System.EventHandler(this.ItemNameBox_TextChanged);
            // 
            // ItemImageBox
            // 
            this.ItemImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ItemImageBox.Location = new System.Drawing.Point(205, 22);
            this.ItemImageBox.Name = "ItemImageBox";
            this.ItemImageBox.Size = new System.Drawing.Size(120, 120);
            this.ItemImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ItemImageBox.TabIndex = 2;
            this.ItemImageBox.TabStop = false;
            // 
            // ItemImage
            // 
            this.ItemImage.AutoSize = true;
            this.ItemImage.Location = new System.Drawing.Point(231, 6);
            this.ItemImage.Name = "ItemImage";
            this.ItemImage.Size = new System.Drawing.Size(68, 13);
            this.ItemImage.TabIndex = 3;
            this.ItemImage.Text = "Item Preview";
            // 
            // ItemDescBox
            // 
            this.ItemDescBox.Location = new System.Drawing.Point(11, 62);
            this.ItemDescBox.Multiline = true;
            this.ItemDescBox.Name = "ItemDescBox";
            this.ItemDescBox.Size = new System.Drawing.Size(172, 80);
            this.ItemDescBox.TabIndex = 5;
            this.ItemDescBox.TextChanged += new System.EventHandler(this.ItemDescBox_TextChanged);
            // 
            // ItemDesc
            // 
            this.ItemDesc.AutoSize = true;
            this.ItemDesc.Location = new System.Drawing.Point(61, 46);
            this.ItemDesc.Name = "ItemDesc";
            this.ItemDesc.Size = new System.Drawing.Size(83, 13);
            this.ItemDesc.TabIndex = 4;
            this.ItemDesc.Text = "Item Description";
            // 
            // ItemImageBrowse
            // 
            this.ItemImageBrowse.Location = new System.Drawing.Point(205, 148);
            this.ItemImageBrowse.Name = "ItemImageBrowse";
            this.ItemImageBrowse.Size = new System.Drawing.Size(120, 23);
            this.ItemImageBrowse.TabIndex = 6;
            this.ItemImageBrowse.Text = "Browse Image...";
            this.ItemImageBrowse.UseVisualStyleBackColor = true;
            this.ItemImageBrowse.Click += new System.EventHandler(this.ItemImageBrowse_Click);
            // 
            // ItemPathBox
            // 
            this.ItemPathBox.Location = new System.Drawing.Point(11, 191);
            this.ItemPathBox.Name = "ItemPathBox";
            this.ItemPathBox.ReadOnly = true;
            this.ItemPathBox.Size = new System.Drawing.Size(172, 20);
            this.ItemPathBox.TabIndex = 7;
            // 
            // ItemPath
            // 
            this.ItemPath.AutoSize = true;
            this.ItemPath.Location = new System.Drawing.Point(116, 175);
            this.ItemPath.Name = "ItemPath";
            this.ItemPath.Size = new System.Drawing.Size(99, 13);
            this.ItemPath.TabIndex = 8;
            this.ItemPath.Text = "Item Content Folder";
            // 
            // BrowseFolder
            // 
            this.BrowseFolder.Location = new System.Drawing.Point(189, 190);
            this.BrowseFolder.Name = "BrowseFolder";
            this.BrowseFolder.Size = new System.Drawing.Size(144, 23);
            this.BrowseFolder.TabIndex = 9;
            this.BrowseFolder.Text = "Browse Content Folder...";
            this.BrowseFolder.UseVisualStyleBackColor = true;
            this.BrowseFolder.Click += new System.EventHandler(this.BrowseFolder_Click);
            // 
            // ItemID
            // 
            this.ItemID.AutoSize = true;
            this.ItemID.Location = new System.Drawing.Point(460, 4);
            this.ItemID.Name = "ItemID";
            this.ItemID.Size = new System.Drawing.Size(102, 13);
            this.ItemID.TabIndex = 11;
            this.ItemID.Text = "ID of the item to edit";
            // 
            // ItemIDBox
            // 
            this.ItemIDBox.Location = new System.Drawing.Point(419, 22);
            this.ItemIDBox.Name = "ItemIDBox";
            this.ItemIDBox.Size = new System.Drawing.Size(116, 20);
            this.ItemIDBox.TabIndex = 12;
            // 
            // LoadItem
            // 
            this.LoadItem.Location = new System.Drawing.Point(541, 20);
            this.LoadItem.Name = "LoadItem";
            this.LoadItem.Size = new System.Drawing.Size(73, 23);
            this.LoadItem.TabIndex = 13;
            this.LoadItem.Text = "Load Item";
            this.LoadItem.UseVisualStyleBackColor = true;
            this.LoadItem.Click += new System.EventHandler(this.LoadItem_Click);
            // 
            // ItemChangesBox
            // 
            this.ItemChangesBox.Enabled = false;
            this.ItemChangesBox.Location = new System.Drawing.Point(351, 62);
            this.ItemChangesBox.Multiline = true;
            this.ItemChangesBox.Name = "ItemChangesBox";
            this.ItemChangesBox.Size = new System.Drawing.Size(322, 230);
            this.ItemChangesBox.TabIndex = 15;
            this.ItemChangesBox.TextChanged += new System.EventHandler(this.ItemChangesBox_TextChanged);
            // 
            // ItemChanges
            // 
            this.ItemChanges.AutoSize = true;
            this.ItemChanges.Location = new System.Drawing.Point(472, 46);
            this.ItemChanges.Name = "ItemChanges";
            this.ItemChanges.Size = new System.Drawing.Size(72, 13);
            this.ItemChanges.TabIndex = 14;
            this.ItemChanges.Text = "Item Changes";
            // 
            // UploadItem
            // 
            this.UploadItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UploadItem.Location = new System.Drawing.Point(11, 219);
            this.UploadItem.Name = "UploadItem";
            this.UploadItem.Size = new System.Drawing.Size(321, 44);
            this.UploadItem.TabIndex = 16;
            this.UploadItem.Text = "UPLOAD ITEM";
            this.UploadItem.UseVisualStyleBackColor = true;
            this.UploadItem.Click += new System.EventHandler(this.UploadItem_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(11, 269);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(321, 23);
            this.progressBar1.TabIndex = 17;
            // 
            // ItemEditingBox
            // 
            this.ItemEditingBox.AutoSize = true;
            this.ItemEditingBox.Location = new System.Drawing.Point(64, 151);
            this.ItemEditingBox.Name = "ItemEditingBox";
            this.ItemEditingBox.Size = new System.Drawing.Size(67, 17);
            this.ItemEditingBox.TabIndex = 18;
            this.ItemEditingBox.Text = "Edit Item";
            this.ItemEditingBox.UseVisualStyleBackColor = true;
            this.ItemEditingBox.CheckedChanged += new System.EventHandler(this.ItemEditingBox_CheckedChanged);
            // 
            // WorkshopUploader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(695, 301);
            this.Controls.Add(this.ItemEditingBox);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.UploadItem);
            this.Controls.Add(this.ItemChangesBox);
            this.Controls.Add(this.ItemChanges);
            this.Controls.Add(this.LoadItem);
            this.Controls.Add(this.ItemIDBox);
            this.Controls.Add(this.ItemID);
            this.Controls.Add(this.BrowseFolder);
            this.Controls.Add(this.ItemPath);
            this.Controls.Add(this.ItemPathBox);
            this.Controls.Add(this.ItemImageBrowse);
            this.Controls.Add(this.ItemDescBox);
            this.Controls.Add(this.ItemDesc);
            this.Controls.Add(this.ItemImage);
            this.Controls.Add(this.ItemImageBox);
            this.Controls.Add(this.ItemNameBox);
            this.Controls.Add(this.ItemName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WorkshopUploader";
            this.Text = "Workshop Uploader";
            this.Load += new System.EventHandler(this.WorkshopUploader_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WorkshopUploader_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.ItemImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ItemName;
        private System.Windows.Forms.TextBox ItemNameBox;
        private System.Windows.Forms.PictureBox ItemImageBox;
        private System.Windows.Forms.Label ItemImage;
        private System.Windows.Forms.TextBox ItemDescBox;
        private System.Windows.Forms.Label ItemDesc;
        private System.Windows.Forms.Button ItemImageBrowse;
        private System.Windows.Forms.TextBox ItemPathBox;
        private System.Windows.Forms.Label ItemPath;
        private System.Windows.Forms.Button BrowseFolder;
        private System.Windows.Forms.Label ItemID;
        private System.Windows.Forms.TextBox ItemIDBox;
        private System.Windows.Forms.Button LoadItem;
        private System.Windows.Forms.TextBox ItemChangesBox;
        private System.Windows.Forms.Label ItemChanges;
        private System.Windows.Forms.Button UploadItem;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox ItemEditingBox;
    }
}