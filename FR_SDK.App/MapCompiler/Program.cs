using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace MapCompiler
{
    class Program
    {
        static DispatcherTimer oneShot = new DispatcherTimer();
        static GlobalVars.ProcessController processController;
        static string VMFFile;
        static bool appOn = true;
        static bool isMapCompiling = false;
        static OpenFileDialog openFileDialog1;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.Title = "Map Compiler";
            processController = new GlobalVars.ProcessController();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            while (appOn)
            {
                if (!isMapCompiling)
                {
                    LoadMap();
                }
            }
            return;
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            DialogCloser.Execute();
        }

        static async void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            await CompileMap(VMFFile);
        }

        static void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Console.WriteLine("ERROR: " + e.Error.Message);
            }
            else
            {
                Console.WriteLine("File " + VMFFile + " finished.");
            }

            Console.WriteLine("Loading next map...");
            Console.WriteLine("Exit the file explorer if you do not wish to compile any more maps.");
            isMapCompiling = false;
        }

        private static Task LoadMap()
        {
            isMapCompiling = true;

            openFileDialog1 = new OpenFileDialog
            {
                Title = "Browse VMF File",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "vmf",
                Filter = "Valve Map Format file (*.vmf)|*.vmf",
                FilterIndex = 1
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                VMFFile = openFileDialog1.FileName;
                Console.WriteLine("Compiling map " + Path.GetFileName(VMFFile) + "...");

                BackgroundWorker bgWorker;
                bgWorker = new BackgroundWorker();
                bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
                bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
                bgWorker.RunWorkerAsync();
            }
            else
            {
                appOn = false;
            }

            return Task.CompletedTask;
        }

        private static Task CompileMap(string VMFFile)
        {
            Console.WriteLine("Mod: " + GlobalVars.moddir);

            Console.WriteLine("Loading VBSP...");
            Process vbsp = processController.LaunchApp(GlobalVars.vbsp, "-game \"" + GlobalVars.moddir + "\" -low \"" + VMFFile + "\"");
            int code1 = BeginToolLoad(vbsp);
            Console.WriteLine("VBSP ended with code: " + code1);

            Console.WriteLine("Loading VVIS...");
            Process vvis = processController.LaunchApp(GlobalVars.vvis, "-game \"" + GlobalVars.moddir + "\" -low -fast \"" + VMFFile + "\"");
            int code2 = BeginToolLoad(vvis);
            Console.WriteLine("VVIS ended with code: " + code2);

            Console.WriteLine("Loading VRAD...");
            string mapPath = Path.GetDirectoryName(VMFFile) + "\\" + Path.GetFileNameWithoutExtension(VMFFile) + ".bsp";
            Process vrad = processController.LaunchApp(GlobalVars.vrad, "-game \"" + GlobalVars.moddir + "\" -low -both -StaticPropLighting -StaticPropPolys -bounce 2 -noextra \"" + mapPath + "\"");
            int code3 = BeginToolLoad(vrad);
            Console.WriteLine("VRAD ended with code: " + code3);

            return Task.CompletedTask;
        }

        private static int BeginToolLoad(Process proc)
        {
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.ErrorDataReceived += (s, e) => Console.WriteLine(e.Data);
            proc.OutputDataReceived += (s, e) => Console.WriteLine(e.Data);
            proc.EnableRaisingEvents = true;
            proc.Start();
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
            proc.WaitForExit();

            return proc.ExitCode;
        }
    }

    //https://stackoverflow.com/questions/12090691/closing-openfiledialog-savefiledialog
    static class DialogCloser
    {
        public static void Execute()
        {
            // Enumerate windows to find dialogs
            EnumThreadWndProc callback = new EnumThreadWndProc(checkWindow);
            EnumThreadWindows(GetCurrentThreadId(), callback, IntPtr.Zero);
            GC.KeepAlive(callback);
        }

        private static bool checkWindow(IntPtr hWnd, IntPtr lp)
        {
            // Checks if <hWnd> is a Windows dialog
            StringBuilder sb = new StringBuilder(260);
            GetClassName(hWnd, sb, sb.Capacity);
            if (sb.ToString() == "#32770")
            {
                // Close it by sending WM_CLOSE to the window
                SendMessage(hWnd, 0x0010, IntPtr.Zero, IntPtr.Zero);
            }
            return true;
        }

        // P/Invoke declarations
        private delegate bool EnumThreadWndProc(IntPtr hWnd, IntPtr lp);
        [DllImport("user32.dll")]
        private static extern bool EnumThreadWindows(int tid, EnumThreadWndProc callback, IntPtr lp);
        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();
        [DllImport("user32.dll")]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder buffer, int buflen);
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
    }
}
