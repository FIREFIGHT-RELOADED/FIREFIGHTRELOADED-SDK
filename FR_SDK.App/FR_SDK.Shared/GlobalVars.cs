//#define PREENDGAME

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Windows;
#if LAUNCHER
using FR_SDK.App;
#endif

public static class GlobalVars
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
    public static string mapcomp = sdkdir + @"\FR_MapCompiler.exe";
    #endregion
#region Important files
#if PREENDGAME
    public static string fgd = sdkdir + @"\fgd\firefightreloaded.fgd";
#else
    public static string fgd = bindir + @"\firefightreloaded.fgd";
#endif
    public static string gameconfig = bindir + @"\GameConfig.txt";
#endregion
#region GameConfig Settings
    public static string DefaultSolidEntity = "func_detail";
    public static string DefaultPointEntity = "info_player_start";
    public static float DefaultTextureScale = 0.250000F;
    public static int DefaultLightmapScale = 16;
    public static string CordonTexture = "tools/toolsnodraw";
    public static int MaterialExcludeCount = 0;
    public static int SDKVersion = 5;
#endregion
#region Other Values
    public static int DelayMiliseconds = 100;
#endregion
#region Global Methods
    public class ProcessController
    {
        public static List<Process> ProcessList;

        public ProcessController()
        {
            ProcessList = new List<Process>();
        }

#if LAUNCHER
        public Process LaunchApp(string exePath, string exeArgs)
#else
        public Process LaunchApp(string exePath, string exeArgs, string formAppName = "")
#endif
        {
            try
            {
                Process pr = new Process();
                pr.StartInfo.FileName = exePath;
                pr.StartInfo.Arguments = exeArgs;
                pr.Exited += ProcessExited;
                ProcessList.Add(pr);
                return pr;
            }
            catch (Exception ex)
            {
#if LAUNCHER
                CreateMessageBox("An error has occurred when launching the application: " + ex.Message);
#elif CONSOLEAPP
                Console.WriteLine("An error has occurred when launching the application: " + ex.Message);
#else
                string app = !string.IsNullOrWhiteSpace(formAppName) ? formAppName : "FIREFIGHT RELOADED SDK";

                System.Windows.Forms.MessageBox.Show("An error has occurred when launching the application: " + ex.Message,
                    app + " - Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
#endif
            }

            return null;
        }

        public virtual void ProcessExitedExt() { }

        private void ProcessExited(object sender, EventArgs e)
        {
            try
            {
                Process proc = (Process)sender;

                if (proc != null)
                {
                    ProcessList.Remove(proc);
                }
            }
            catch (Exception)
            {

            }

            ProcessExitedExt();
        }

        //https://stackoverflow.com/questions/30249873/process-kill-doesnt-seem-to-kill-the-process
        private static void KillProcessAndChildrens(int pid)
        {
            ManagementObjectSearcher processSearcher = new ManagementObjectSearcher
              ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection processCollection = processSearcher.Get();

            // We must kill child processes first!
            if (processCollection != null)
            {
                foreach (ManagementObject mo in processCollection)
                {
                    KillProcessAndChildrens(Convert.ToInt32(mo["ProcessID"])); //kill child processes(also kills childrens of childrens etc.)
                }
            }

            // Then kill parents.
            try
            {
                Process proc = Process.GetProcessById(pid);
                if (!proc.HasExited) proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }

        public virtual void KillAllActiveProcesses()
        {
            foreach (Process proc in ProcessList)
            {
                KillProcessAndChildrens(proc.Id);

                if (proc != null)
                {
                    ProcessList.Remove(proc);
                }
            }
        }
    }

#if STEAM
    public class ProcessControllerSteam : ProcessController
    {
        public bool AppOverridesSteam = false;
        public bool ShuttingDown = false;

        public ProcessControllerSteam() : base()
        {
        }

        public override void ProcessExitedExt()
        {
            FixSteam();
        }

        public override void KillAllActiveProcesses()
        {
            base.KillAllActiveProcesses();
            FixSteam();
        }

        private void FixSteam()
        {
            if (AppOverridesSteam && !ShuttingDown)
            {
                Task.Delay(SteamworksIntegration.SteamRelaunchDelayMiliseconds);
                SteamworksIntegration.InitSteam(SteamworksIntegration.sdkAppID);
                AppOverridesSteam = false;
            }
        }
    }
#endif

#if LAUNCHER
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
#endif

    public static void WaitForProcess(Process proc, int miliseconds)
    {
        Task.Delay(miliseconds);
        proc.Refresh();
    }
#endregion
}
