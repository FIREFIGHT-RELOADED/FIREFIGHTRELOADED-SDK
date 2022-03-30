using Steamworks;
using System;
using System.Windows.Forms;

namespace WorkshopUploader
{
    public static class SteamworksIntegration
    {
        public static int SteamRelaunchDelayMiliseconds = 500;
        public static AppId sdkAppID = 494770;
        public static AppId gameAppID = 397680;

        #region Functions
        public static void InitSteam(AppId id)
        {
            try
            {
                if (!SteamClient.IsValid)
                {
                    SteamClient.Init(id, true);
                }
            }
            catch (Exception ex)
            {
                string messageBoxMessage = "An error has occurred when initializing Steam for the SDK: " + ex.Message;
                MessageBox.Show(messageBoxMessage, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public static void ShutdownSteam()
        {
            try
            {
                if (SteamClient.IsValid)
                {
                    SteamClient.Shutdown();
                }
            }
            catch (Exception ex)
            {
                string messageBoxMessage = "An error has occurred when stopping Steam for the SDK: " + ex.Message;
                MessageBox.Show(messageBoxMessage, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}