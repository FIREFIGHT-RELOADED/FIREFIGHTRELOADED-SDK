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
            try
            {
                //check if we're just launching it from the exe file.
                GenerateGameConfig();
            }
            catch (Exception)
            {
                try
                {
                    GenerateGameConfig();
                }
                catch (Exception ex)
                {
                    Hide();
                    GlobalVars.CreateMessageBox("An error has occurred when generating the game configuration: " + ex.Message);
                    Application.Current.Shutdown();
                }
            }
        }

        private void window_closing(object sender, CancelEventArgs e)
        {
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
            var proc = GlobalVars.LaunchApp(GlobalVars.hammer, " -game \"" + GlobalVars.moddir + "\"");
            try
            {
                while (string.IsNullOrEmpty(proc.MainWindowTitle))
                {
                    GlobalVars.WaitForProcess(proc, GlobalVars.DelayMiliseconds);
                }

                GlobalVars.CloseWindow(msgboxname);
            }
            catch (Exception ex)
            {
                GlobalVars.CloseWindow(msgboxname);
                GlobalVars.CreateMessageBox("An error has occurred when launching the application: " + ex.Message);
            }
        }

        private void model_DoubleClick(object sender, RoutedEventArgs e)
        {
            string msgboxname = "hlmvbox";
            GlobalVars.CreateMessageBoxAppLaunch(msgboxname, "Starting Model Viewer...");
            var proc = GlobalVars.LaunchApp(GlobalVars.hlmv, " -game \"" + GlobalVars.moddir + "\" -olddialogs");
            try
            {
                while (string.IsNullOrEmpty(proc.MainWindowTitle))
                {
                    GlobalVars.WaitForProcess(proc, GlobalVars.DelayMiliseconds);
                }

                GlobalVars.CloseWindow(msgboxname);
            }
            catch (Exception ex)
            {
                GlobalVars.CloseWindow(msgboxname);
                GlobalVars.CreateMessageBox("An error has occurred when launching the application: " + ex.Message);
            }
        }

        private void faceposer_DoubleClick(object sender, RoutedEventArgs e)
        {
            string msgboxname = "facebox";
            GlobalVars.CreateMessageBoxAppLaunch(msgboxname, "Starting Face Poser...");
            var proc = GlobalVars.LaunchApp(GlobalVars.hlfaceposer, " -game \"" + GlobalVars.moddir + "\"");
            try
            {
                while (string.IsNullOrEmpty(proc.MainWindowTitle))
                {
                    GlobalVars.WaitForProcess(proc, GlobalVars.DelayMiliseconds);
                }

                GlobalVars.CloseWindow(msgboxname);
            }
            catch (Exception ex)
            {
                GlobalVars.CloseWindow(msgboxname);
                GlobalVars.CreateMessageBox("An error has occurred when launching the application: " + ex.Message);
            }
        }

        private void workshop_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            string msgboxname = "workbox";
            GlobalVars.CreateMessageBoxAppLaunch(msgboxname, "Starting Workshop Uploader...");
            var proc = GlobalVars.LaunchApp(GlobalVars.workshop, "");
            try
            {
                while (string.IsNullOrEmpty(proc.MainWindowTitle))
                {
                    GlobalVars.WaitForProcess(proc, GlobalVars.DelayMiliseconds);
                }

                GlobalVars.CloseWindow(msgboxname);
            }
            catch (Exception ex)
            {
                GlobalVars.CloseWindow(msgboxname);
                GlobalVars.CreateMessageBox("An error has occurred when launching the application: " + ex.Message);
            }
        }
        #endregion
    }
}
