using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FR_SDK.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Window Logic
        public MainWindow()
        {
            InitializeComponent();
            MouseDown += Window_MouseDown;
        }

        private void Window_init(object sender, EventArgs e)
        {
#if STEAM
            //workshop uploader
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            SteamworksIntegration.InitSteam(GlobalVars.sdkAppID);

            try
            {
                //check if we're just launching it from the exe file.
                GenerateGameConfig();
            }
            catch (Exception)
            {
                try
                {
                    //we're probably launching via steam, which messes up the path!  This means we have to reconfigure EVERY FILE PATH. ew!
                    GlobalVars.RefreshPathsForSteam();
                    GenerateGameConfig();
                }
                catch (Exception ex)
                {
                    Hide();
                    GlobalVars.CreateMessageBox("An error has occurred when generating the game configuration: " + ex.Message);
                    Application.Current.Shutdown();
                }
            }
#else
            try
            {
                //check if we're just launching it from the exe file.
                GenerateGameConfig();
            }
            catch (Exception ex)
            {
                Hide();
                GlobalVars.CreateMessageBox("An error has occurred when generating the game configuration: " + ex.Message);
                Application.Current.Shutdown();
            }
#endif
            
            //no more vproject
            /*try
            {
                //Set VPROJECT to the mod dir
                Environment.SetEnvironmentVariable("VPROJECT",
                GlobalVars.moddir,
                EnvironmentVariableTarget.User);
                GlobalVars.CreateMessageBox("VPROJECT has been set to " + Environment.GetEnvironmentVariable("VPROJECT") + ". If you experience issues launching any of the SDK tools, try restarting your computer." );
            }
            catch (Exception ex)
            {
                Hide();
                GlobalVars.CreateMessageBox("An error has occurred when setting VPROJECT: " + ex.Message);
                Application.Current.Shutdown();
            }*/
        }

        private void window_closing(object sender, CancelEventArgs e)
        {
#if STEAM
            SteamworksIntegration.ShutdownSteam();
#endif
        }

        private void GenerateGameConfig()
        {
            if (!File.Exists(GlobalVars.gameconfig))
            {
                File.Create(GlobalVars.gameconfig).Dispose();
                File.WriteAllText(GlobalVars.gameconfig, KeyValueCreators.GenerateGameConfig().ToString());
            }
            else
            {
                File.Delete(GlobalVars.gameconfig);
                File.WriteAllText(GlobalVars.gameconfig, KeyValueCreators.GenerateGameConfig().ToString());
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
        #endregion

        #region Launcher Logic
        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void minmize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void hammer_DoubleClick(object sender, RoutedEventArgs e)
        {
            string msgboxname = "hammerbox";
            GlobalVars.CreateMessageBoxAppLaunch(msgboxname, "Starting Hammer...");
            var proc = Process.Start(GlobalVars.hammer, " -game \"" + GlobalVars.moddir + "\"");
            while (string.IsNullOrEmpty(proc.MainWindowTitle))
            {
                GlobalVars.WaitForProcess(proc, GlobalVars.DelayMiliseconds);
            }
            GlobalVars.CloseWindow(msgboxname);
        }

        private void model_DoubleClick(object sender, RoutedEventArgs e)
        {
            string msgboxname = "hlmvbox";
            GlobalVars.CreateMessageBoxAppLaunch(msgboxname, "Starting Model Viewer...");
            var proc = Process.Start(GlobalVars.hlmv, " -game \"" + GlobalVars.moddir + "\" -olddialogs");
            while (string.IsNullOrEmpty(proc.MainWindowTitle))
            {
                GlobalVars.WaitForProcess(proc, GlobalVars.DelayMiliseconds);
            }
            GlobalVars.CloseWindow(msgboxname);
        }

        private void faceposer_DoubleClick(object sender, RoutedEventArgs e)
        {
            string msgboxname = "facebox";
            GlobalVars.CreateMessageBoxAppLaunch(msgboxname, "Starting Face Poser...");
            var proc = Process.Start(GlobalVars.hlfaceposer, " -game \"" + GlobalVars.moddir + "\"");
            while (string.IsNullOrEmpty(proc.MainWindowTitle))
            {
                GlobalVars.WaitForProcess(proc, GlobalVars.DelayMiliseconds);
            }
            GlobalVars.CloseWindow(msgboxname);
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async void workshop_DoubleClick(object sender, MouseButtonEventArgs e)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
#if STEAM
            SteamworksIntegration.ShutdownSteam();
            await Task.Delay(GlobalVars.SteamRelaunchDelayMiliseconds);

            WorkshopUploader workshopUploader = new WorkshopUploader();
            var dialogResult = workshopUploader.ShowDialog();

            await Task.Delay(GlobalVars.SteamRelaunchDelayMiliseconds);
            SteamworksIntegration.InitSteam(GlobalVars.sdkAppID);
#else
            GlobalVars.CreateMessageBox("This application is not available in the non-steam version of the FIREFIGHT RELOADED SDK.");
#endif
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async void launchmod_DoubleClick(object sender, RoutedEventArgs e)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            string msgboxname = "gamebox";
            GlobalVars.CreateMessageBoxAppLaunch(msgboxname, "Launching FIREFIGHT RELOADED...");
#if STEAM
            SteamworksIntegration.ShutdownSteam();
            await Task.Delay(GlobalVars.SteamRelaunchDelayMiliseconds);
            SteamworksIntegration.InitSteam(GlobalVars.gameAppID);
#endif
            var proc = Process.Start(GlobalVars.gameexe, " -steam -game firefightreloaded");
            while (string.IsNullOrEmpty(proc.MainWindowTitle))
            {
                GlobalVars.WaitForProcess(proc, GlobalVars.DelayMiliseconds);
            }

#if STEAM
            SteamworksIntegration.ShutdownSteam();
            await Task.Delay(GlobalVars.SteamRelaunchDelayMiliseconds);
            SteamworksIntegration.InitSteam(GlobalVars.sdkAppID);
#endif
            GlobalVars.CloseWindow(msgboxname);
        }
        #endregion
    }
}
