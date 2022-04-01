using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace FR_SDK.App
{
    public partial class MapCompiler : Form
    {
        DispatcherTimer oneShot = new DispatcherTimer();
        string VMFFile;

        public MapCompiler()
        {
            InitializeComponent();
            oneShot.Interval = new TimeSpan(0, 0, 0, 0, GlobalVars.DelayMiliseconds);
            oneShot.Tick += new EventHandler(oneShot_Tick);
        }

        private void MapCompiler_Load(object sender, EventArgs e)
        {
            oneShot.Start();
        }

        void MapCompiler_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("vbsp"))
            {
                process.Kill();
            }

            foreach (var process in Process.GetProcessesByName("vvis"))
            {
                process.Kill();
            }

            foreach (var process in Process.GetProcessesByName("vrad"))
            {
                process.Kill();
            }
        }

        void oneShot_Tick(object sender, EventArgs e)
        {
            oneShot.Stop();

            OpenFileDialog openFileDialog1 = new OpenFileDialog
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
                foreach (string file in openFileDialog1.FileNames)
                {
                    ConsoleWrite("Compiling map " + Path.GetFileName(file) + "...");
                    VMFFile = file;

                    BackgroundWorker bgWorker;
                    bgWorker = new BackgroundWorker();
                    bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
                    bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
                    bgWorker.RunWorkerAsync();
                }

                ConsoleWrite("Map compile for files finished.");
            }
            else
            {
                Close();
            }
        }

        async void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            await CompileMap(VMFFile);
        }

        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ConsoleWrite("ERROR: " + e.Error.Message);
            }
            else
            {
                ConsoleWrite("File " + VMFFile + " finished.");
            }
        }

        private async Task CompileMap(string VMFFile)
        {
            ConsoleWrite(GlobalVars.moddir);

            ConsoleWrite("Loading VBSP...");
            Process vbsp = GlobalVars.LaunchApp(GlobalVars.vbsp, "-game \"" + GlobalVars.moddir + "\" \"" + VMFFile + "\"");
            int code1 = await BeginToolLoad(vbsp);
            ConsoleWrite("VBSP ended with code: " + code1);

            ConsoleWrite("Loading VVIS...");
            Process vvis = GlobalVars.LaunchApp(GlobalVars.vvis, "-game \"" + GlobalVars.moddir + "\" \"" + VMFFile + "\"");
            int code2 = await BeginToolLoad(vvis);
            ConsoleWrite("VVIS ended with code: " + code2);

            ConsoleWrite("Loading VRAD...");
            string mapPath = Path.GetDirectoryName(VMFFile) + "\\" + Path.GetFileNameWithoutExtension(VMFFile) + ".bsp";
            Process vrad = GlobalVars.LaunchApp(GlobalVars.vrad, "-game \"" + GlobalVars.moddir + "\" -both \"" + mapPath + "\"");
            int code3 = await BeginToolLoad(vrad);
            ConsoleWrite("VRAD ended with code: " + code3);
        }

        private Task<int> BeginToolLoad(Process proc)
        {
            var tcs = new TaskCompletionSource<int>();

            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.ErrorDataReceived += (s, e) => ConsoleWrite(e.Data);
            proc.OutputDataReceived += (s, e) => ConsoleWrite(e.Data);
            proc.Exited += (s, ea) => tcs.SetResult(proc.ExitCode);
            proc.EnableRaisingEvents = true;
            proc.Start();
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
            proc.WaitForExit();

            return tcs.Task;
        }

        void ConsoleWrite(string text)
        {
            try
            {
                if (consoleBox != null)
                {
                    consoleBox.AppendText(text + Environment.NewLine);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
