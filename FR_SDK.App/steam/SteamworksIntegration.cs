using Steamworks;
using System;

namespace FR_SDK.App
{
    public class SteamworksIntegration
    {
        #region Functions
        public static void InitSteam(AppId id, bool IsForm = false)
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

                if (IsForm)
                {
                    System.Windows.Forms.MessageBox.Show(messageBoxMessage, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                else
                {
                    GlobalVars.CreateMessageBox(messageBoxMessage);
                }
            }
        }

        public static void ShutdownSteam(bool IsForm = false)
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

                if (IsForm)
                {
                    System.Windows.Forms.MessageBox.Show(messageBoxMessage, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                else
                {
                    GlobalVars.CreateMessageBox(messageBoxMessage);
                }
            }
        }
        #endregion
    }
}