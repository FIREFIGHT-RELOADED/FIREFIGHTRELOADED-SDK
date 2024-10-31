
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Windows;

namespace FR_SDK.Core
{
    public static class GlobalVars
    {
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

        public static string hlmv = bindir + @"\hlmv.exe";
        public static string hlfaceposer = bindir + @"\hlfaceposer.exe";
        public static string hammer = bindir + @"\hammer.exe";
        public static string vbsp = bindir + @"\vbsp.exe";
        public static string vvis = bindir + @"\vvis.exe";
        public static string vrad = bindir + @"\vrad.exe";
        public static string workshop = sdkdir + @"\FR_WorkshopUploader.exe";
        public static string mapcomp = sdkdir + @"\FR_MapCompiler.exe";

        public static string fgd = bindir + @"\firefightreloaded.fgd";
        public static string gameconfig = bindir + @"\GameConfig.txt";

        public static string DefaultSolidEntity = "func_detail";
        public static string DefaultPointEntity = "info_player_start";
        public static float DefaultTextureScale = 0.250000F;
        public static int DefaultLightmapScale = 16;
        public static string CordonTexture = "tools/toolsnodraw";
        public static int MaterialExcludeCount = 0;
        public static int SDKVersion = 5;

        public static int DelayMiliseconds = 100;
        public static bool isPreEndgame = false;

        public static void WaitForProcess(Process proc, int miliseconds)
        {
            Task.Delay(miliseconds);
            proc.Refresh();
        }
    }
}
