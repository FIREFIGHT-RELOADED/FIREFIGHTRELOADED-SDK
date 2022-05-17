using Steamworks;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkshopUploader
{
    public partial class WorkshopUploader : Form
    {
        #region Private Variables
        private UGCItem item;
        private int uploadAttempts = 0;
        private int maxUploadAttempts = 5;
        private bool EditMode = false;
        #endregion

        #region Constructor
        public WorkshopUploader()
        {
            InitializeComponent();
        }
        #endregion

        #region Window Events
        private void WorkshopUploader_Load(object sender, EventArgs e)
        {
            SteamworksIntegration.InitSteam(SteamworksIntegration.gameAppID);
            Reset();
            Width = 357;
            CenterToScreen();

            try
            {
                if (!SteamClient.IsLoggedOn)
                {
                    MessageBox.Show("The Workshop Uploader cannot load because Steam is offline. Please load Steam in Online Mode, relaunch the SDK, then try again.",
                        "Workshop Uploader - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }
            catch(Exception)
            {
                MessageBox.Show("The Workshop Uploader cannot load because Steam is offline. Please load Steam in Online Mode, relaunch the SDK, then try again.",
                        "Workshop Uploader - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        void WorkshopUploader_FormClosed(object sender, FormClosedEventArgs e)
        {
            SteamworksIntegration.ShutdownSteam();
        }

        private async void LoadItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ItemIDBox.Text))
            {
                MessageBox.Show("Please specify an Item ID.",
                    "Workshop Uploader - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            item = await LoadWorkshopItemInfo(Convert.ToUInt64(ItemIDBox.Text));

            if (item.ItemName.Equals("OFFLINE" + ItemIDBox.Text))
            {
                MessageBox.Show("The item cannot be loaded by the Workshop Uploader because Steam is offline. Please load Steam in Online Mode, relaunch the SDK, then try again.", 
                    "Workshop Uploader - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (item.ItemName.Equals("ERROR" + ItemIDBox.Text) )
            {
                MessageBox.Show("The item cannot be loaded by the Workshop Uploader because it doesn't exist.", 
                    "Workshop Uploader - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Reset();
                return;
            }

            if (item.ItemName.Equals("NOTOWNER" + ItemIDBox.Text))
            {
                MessageBox.Show("The item cannot be loaded by the Workshop Uploader because you do not own it. Please input the item ID of an item you own.", 
                    "Workshop Uploader - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Reset();
                return;
            }

            UGCItem itemOld = item;
            item = new UGCItem();

            ItemNameBox.Text = itemOld.ItemName;
            ItemDescBox.Text = itemOld.ItemDesc;

            foreach (string tag in itemOld.ItemTags)
            {
                for (int i = 0; i < tagsBox.Items.Count; i++)
                {
                    if (tagsBox.Items[i].ToString().Equals(tag, StringComparison.InvariantCultureIgnoreCase))
                    {
                        tagsBox.SetItemChecked(i, true);
                    }
                }
            }

            var request = WebRequest.Create(itemOld.ItemPreviewImage);

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                ItemImageBox.Image = Image.FromStream(stream);
            }

            ItemChangesBox.Enabled = true;
        }

        private void ItemNameBox_TextChanged(object sender, EventArgs e)
        {
            item.ItemName = ItemNameBox.Text;
        }

        private void ItemDescBox_TextChanged(object sender, EventArgs e)
        {
            item.ItemDesc = ItemDescBox.Text;
        }

        private void ItemImageBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Browse Images",
                Filter = "JPG Images|*.jpg;*.jpeg",
                FilterIndex = 1,
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(openFileDialog.FileName))
                {
                    Image img = Image.FromFile(openFileDialog.FileName);
                    ItemImageBox.Image = img;
                    item.ItemPreviewImage = openFileDialog.FileName;
                }
            }
        }

        private void BrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Browse for item content";

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                item.ItemContentPath = folderBrowserDialog.SelectedPath;
                ItemPathBox.Text = item.ItemContentPath;
            }
        }

        private void ItemChangesBox_TextChanged(object sender, EventArgs e)
        {
            item.ItemChanges = ItemChangesBox.Text;
        }

        private void UploadItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ItemIDBox.Text))
            {
                PublishToWorkshop(item, Convert.ToUInt64(ItemIDBox.Text));
            }
            else
            {
                PublishToWorkshop(item);
            }
        }

        private void ItemEditingBox_CheckedChanged(object sender, EventArgs e)
        {
            EditMode = ItemEditingBox.Checked;

            if (EditMode)
            {
                Width = 711;
            }
            else
            {
                Width = 357;
            }
        }
        #endregion

        #region Functions
        public void Reset()
        {
            ItemIDBox.Text = "";
            ItemNameBox.Text = "";
            ItemDescBox.Text = "";
            ItemImageBox.Image = null;
            ItemChangesBox.Enabled = false;
            ItemChangesBox.Text = "";
            item = new UGCItem();
        }

        public async Task<UGCItem> LoadWorkshopItemInfo(ulong id)
        {
            try
            {
                if (!SteamClient.IsLoggedOn)
                {
                    UGCItem item = new UGCItem();
                    item.ItemName = "OFFLINE" + id.ToString();
                    return item;
                }

                Steamworks.Ugc.Item itemInfo = (Steamworks.Ugc.Item)await SteamUGC.QueryFileAsync(id);
                if (itemInfo.Owner.Id.Equals(SteamClient.SteamId))
                {
                    UGCItem item = new UGCItem();
                    item.ItemName = itemInfo.Title;
                    item.ItemDesc = itemInfo.Description;
                    item.ItemPreviewImage = itemInfo.PreviewImageUrl;
                    item.ItemTags = itemInfo.Tags;

                    return item;
                }
                else
                {
                    UGCItem item = new UGCItem();
                    item.ItemName = "NOTOWNER" + id.ToString();
                    return item;
                }
            }
            catch (Exception)
            {
                UGCItem item = new UGCItem();
                item.ItemName = "ERROR" + id.ToString();
                return item;
            }
        }

        private static void ItemMissingError(string name)
        {
            MessageBox.Show("Your item is missing " + name + ". Please correct this on the Workshop Uploader.",
                    "Workshop Uploader - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public async void PublishToWorkshop(UGCItem item, ulong id = 0)
        {
            try
            {
                if (!SteamClient.IsLoggedOn)
                {
                    MessageBox.Show("The item cannot be loaded by the Workshop Uploader because Steam is offline. Please load Steam in Online Mode, relaunch the SDK, then try again.",
                    "Workshop Uploader - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The Workshop Uploader cannot load because Steam is offline. Please load Steam in Online Mode, relaunch the SDK, then try again.",
                        "Workshop Uploader - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            Steamworks.Ugc.Editor file;

            if (EditMode)
            {
                Steamworks.Ugc.Item itemInfo = (Steamworks.Ugc.Item)await SteamUGC.QueryFileAsync(id);

                if (!itemInfo.Owner.Id.Equals(SteamClient.SteamId))
                {
                    MessageBox.Show("The item cannot be edited by the Workshop Uploader because you do not own it. Please input the item ID of an item you own.",
                        "Workshop Uploader - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Reset();
                    return;
                }

                file = new Steamworks.Ugc.Editor(itemInfo.Id);
            }
            else
            {
                file = Steamworks.Ugc.Editor.NewCommunityFile;
            }

            if (!string.IsNullOrWhiteSpace(item.ItemName))
            {
                file.WithTitle(item.ItemName);
            }
            else
            {
                if (!EditMode)
                {
                    ItemMissingError("a name");
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(item.ItemContentPath))
            {
                file.WithContent(item.ItemContentPath);
            }
            else
            {
                if (!EditMode)
                {
                    ItemMissingError("the content path");
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(item.ItemPreviewImage))
            {
                file.WithPreviewFile(item.ItemPreviewImage);
            }
            else
            {
                if (!EditMode)
                {
                    ItemMissingError("the preview image");
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(item.ItemDesc))
            {
                file.WithDescription(item.ItemDesc);
            }

            if (!EditMode)
            {
                file.WithPrivateVisibility();
            }

            if (EditMode)
            {
                if (!string.IsNullOrWhiteSpace(item.ItemChanges))
                {
                    file.WithChangeLog(item.ItemChanges);
                }
            }

            foreach (object itemChecked in tagsBox.CheckedItems)
            {
                file.WithTag(itemChecked.ToString());
            }

            var result = await file.SubmitAsync(new UploadProgress(progressBar1));

            if (result.Success)
            {
                MessageBox.Show("'" + item.ItemName + "' has been successfuly uploaded!" + 
                    (!EditMode ? " Your item has been set to \"Hidden\" so you can make any further edits before you publish. " : " ") + 
                    "Your item will now open up in an external web browser.",
                    "Workshop Uploader - Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start("https://steamcommunity.com/sharedfiles/filedetails/?id=" + result.FileId.Value);
            }
            else
            {
                if (result.Result == Result.Timeout)
                {
                    if (uploadAttempts < maxUploadAttempts)
                    {
                        uploadAttempts++;
                        MessageBox.Show("The item upload timed out. The Workshop Uploader will attempt to reupload the item again. Remaining tries: " + (maxUploadAttempts - uploadAttempts),
                            "Workshop Uploader - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        progressBar1.Value = 0;
                        PublishToWorkshop(item, id);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("The item upload timed out. The Workshop Uploader is unable to upload your item at this time.",
                            "Workshop Uploader - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("An error has occurred when uploading '" + item.ItemName + "': " + result.Result.ToString(),
                        "Workshop Uploader - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            progressBar1.Value = 0;
            uploadAttempts = 0;
        }
    #endregion
    }

    #region UploadProgress
    class UploadProgress : IProgress<float>
    {
        float Value = 0;
        ProgressBar bar = null;

        public UploadProgress(ProgressBar progressBar)
        {
            bar = progressBar;
        }

        public void Report(float value)
        {
            if (Value >= value) return;
            Value = value;

            int ActualValue = Convert.ToInt32(value * 100);
            bar.Value = ActualValue;
        }
    }
    #endregion

    #region UGCItem
    public class UGCItem
    {
        public string ItemName { get; set; }
        public string ItemDesc { get; set; }
        public string ItemContentPath { get; set; }
        public string ItemPreviewImage { get; set; }
        public string ItemChanges { get; set; }
        public string[] ItemTags { get; set; }
    }
    #endregion
}
