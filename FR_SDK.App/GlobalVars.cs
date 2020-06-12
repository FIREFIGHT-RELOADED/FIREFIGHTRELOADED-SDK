using Steamworks;
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
        public static string gamedir = Directory.GetParent(Environment.CurrentDirectory).ToString();
        public static string moddir = gamedir + @"\firefightreloaded";
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
        #endregion
        #region Important files
        public static string fgd = moddir + @"\firefightreloaded.fgd";
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
        public static void RefreshPathsForSteam()
        {
            #region Refresh Basic Directories
            gamedir = Directory.GetParent(Environment.CurrentDirectory).ToString() + @"\FIREFIGHT RELOADED";
            moddir = gamedir + @"\firefightreloaded";
            bindir = gamedir + @"\bin";
            mapdir = moddir + @"\maps";
            mapsrcdir = moddir + @"\mapsrc";
            gameexe = gamedir + @"\fr.exe";
            #endregion
            #region Refresh SDK Tools
            hlmv = bindir + @"\hlmv.exe";
            hlfaceposer = bindir + @"\hlfaceposer.exe";
            hammer = bindir + @"\hammer.exe";
            vbsp = bindir + @"\vbsp.exe";
            vvis = bindir + @"\vvis.exe";
            vrad = bindir + @"\vrad.exe";
            #endregion
            #region Refresh Important files
            fgd = moddir + @"\firefightreloaded.fgd";
            gameconfig = bindir + @"\GameConfig.txt";
            #endregion
        }

        public static void CreateMessageBox(string text)
        {
            CustomMessageBox box = new CustomMessageBox(text);
            box.Show();
        }

        public static void CreateMessageBox(string name, string text)
        {
            CustomMessageBox box = new CustomMessageBox(name, text);
            box.Show();
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
