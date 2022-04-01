
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FR_SDK.App
{
    class GlobalVars
    {
        #region Basic Directories
        private static string SetCorrectDir()
        {
            string dir = Directory.GetParent(Environment.CurrentDirectory).ToString();
            if (!dir.Contains(@"\FIREFIGHT RELOADED"))
            {
                dir = Directory.GetParent(Environment.CurrentDirectory).ToString() + @"\FIREFIGHT RELOADED";
            }

            return dir;
        }

        public static string gamedir = SetCorrectDir();
        public static string moddir = gamedir + @"\firefightreloaded";
        public static string sdkdir = gamedir + @"\sdk"; //LAZY AS FUCK.
        public static string bindir = gamedir + @"\bin";
        public static string mapdir = moddir + @"\maps";
        public static string mapsrcdir = moddir + @"\mapsrc";
        public static string gameexe = gamedir + @"\fr.exe";
        #endregion
        #region SDK Tools
        public static string hlmv = bindir + @"\hlmv.exe";
        public static string hlfaceposer = bindir + @"\hlfaceposer.exe";
        public static string hammer = bindir + @"\hammer.exe";
        public static string vbsp = bindir + @"\vbsp.exe";
        public static string vvis = bindir + @"\vvis.exe";
        public static string vrad = bindir + @"\vrad.exe";
        public static string workshop = sdkdir + @"\FR_WorkshopUploader.exe";
        #endregion
        #region Important files
        public static string fgd = bindir + @"\firefightreloaded.fgd";
        public static string gameconfig = bindir + @"\GameConfig.txt";
        #endregion
        #region GameConfig Settings
        public static string DefaultSolidEntity = "func_detail";
        public static string DefaultPointEntity = "info_player_start";
        public static float DefaultTextureScale = 0.250000F;
        public static int DefaultLightmapScale = 16;
        public static string CordonTexture = "BLACK";
        public static int MaterialExcludeCount = 0;
        public static int SDKVersion = 5;
        #endregion
        #region Other Values
        public static int DelayMiliseconds = 100;
        #endregion
        #region Global Methods
        public static Process LaunchApp(string exePath, string exeArgs, bool form = false, string formAppName = "")
        {
            try
            {
                Process pr = new Process();
                pr.StartInfo.FileName = exePath;
                pr.StartInfo.Arguments = exeArgs;
                return pr;
            }
            catch(Exception ex)
            {
                if (form)
                {
                    string app = !string.IsNullOrWhiteSpace(formAppName) ? formAppName : "FIREFIGHT RELOADED SDK";

                    System.Windows.Forms.MessageBox.Show("An error has occurred when launching the application: " + ex.Message,
                        app + " - Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                else
                {
                    CreateMessageBox("An error has occurred when launching the application: " + ex.Message);
                }
            }

            return null;
        }

        public static void CreateMessageBox(string text)
        {
            CustomMessageBox box = new CustomMessageBox(text);
            box.ShowDialog();
        }

        public static void CreateMessageBoxAppLaunch(string name, string text)
        {
            CustomMessageBox box = new CustomMessageBox(name, text);
            box.Show();
        }

        public static void CreateTestMessageBox()
        {
            CustomMessageBox box = new CustomMessageBox();
            box.ShowDialog();
        }

        //credits to https://stackoverflow.com/questions/5363015/close-a-wpf-window-separately
        public static void CloseWindow(string name)
        {
            Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == name);
            win.Close();
        }

        public static async void Delay(int miliseconds)
        {
            await Task.Delay(miliseconds);
        }

        public static void WaitForProcess(Process proc, int miliseconds)
        {
            Delay(miliseconds);
            proc.Refresh();
        }
        #endregion
    }
}
