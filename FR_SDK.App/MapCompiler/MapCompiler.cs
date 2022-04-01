using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace MapCompiler
{
    public partial class MapCompiler : Form
    {
        DispatcherTimer oneShot = new DispatcherTimer();
        GlobalVars.ProcessController processController;
        string VMFFile;

        public MapCompiler()
        {
            InitializeComponent();
            oneShot.Interval = new TimeSpan(0, 0, 0, 0, GlobalVars.DelayMiliseconds);
            oneShot.Tick += new EventHandler(oneShot_Tick);
            processController = new GlobalVars.ProcessController();
        }

        private void MapCompiler_Load(object sender, EventArgs e)
        {
            oneShot.Start();
        }

        void MapCompiler_FormClosed(object sender, FormClosedEventArgs e)
        {
            processController.KillAllActiveProcesses();
        }

        void oneShot_Tick(object sender, EventArgs e)
        {
            oneShot.Stop();
            LoadMap();
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

            ConsoleWrite("Loading next map...");
            ConsoleWrite("Exit the file explorer if you do not wish to compile any more maps.");
            LoadMap();
        }

        private void LoadMap()
        {
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
                VMFFile = openFileDialog1.FileName;
                ConsoleWrite("Compiling map " + Path.GetFileName(VMFFile) + "...");

                BackgroundWorker bgWorker;
                bgWorker = new BackgroundWorker();
                bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
                bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
                bgWorker.RunWorkerAsync();
            }
            else
            {
                Close();
            }
        }

        private Task CompileMap(string VMFFile)
        {
            ConsoleWrite(GlobalVars.moddir);

            ConsoleWrite("Loading VBSP...");
            Process vbsp = processController.LaunchApp(GlobalVars.vbsp, "-game \"" + GlobalVars.moddir + "\" \"" + VMFFile + "\"");
            int code1 = BeginToolLoad(vbsp);
            ConsoleWrite("VBSP ended with code: " + code1);

            ConsoleWrite("Loading VVIS...");
            Process vvis = processController.LaunchApp(GlobalVars.vvis, "-game \"" + GlobalVars.moddir + "\" \"" + VMFFile + "\"");
            int code2 = BeginToolLoad(vvis);
            ConsoleWrite("VVIS ended with code: " + code2);

            ConsoleWrite("Loading VRAD...");
            string mapPath = Path.GetDirectoryName(VMFFile) + "\\" + Path.GetFileNameWithoutExtension(VMFFile) + ".bsp";
            Process vrad = processController.LaunchApp(GlobalVars.vrad, "-game \"" + GlobalVars.moddir + "\" -both \"" + mapPath + "\"");
            int code3 = BeginToolLoad(vrad);
            ConsoleWrite("VRAD ended with code: " + code3);

            return Task.CompletedTask;
        }

        private int BeginToolLoad(Process proc)
        {
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.ErrorDataReceived += (s, e) => ConsoleWrite(e.Data);
            proc.OutputDataReceived += (s, e) => ConsoleWrite(e.Data);
            proc.EnableRaisingEvents = true;
            proc.Start();
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
            proc.WaitForExit();

            return proc.ExitCode;
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
