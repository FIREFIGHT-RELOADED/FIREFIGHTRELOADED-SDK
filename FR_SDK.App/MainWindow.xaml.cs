﻿using FR_SDK.Core;
using KVLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
        ProcessControllerSteam processController;

        public MainWindow()
        {
            InitializeComponent();
            MouseDown += Window_MouseDown;
            processController = new ProcessControllerSteam();
        }

        private void Window_init(object sender, EventArgs e)
        {
            SteamworksIntegration.InitSteam(SteamworksIntegration.sdkAppID);

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
                    CreateMessageBox("An error has occurred when generating the game configuration: " + ex.Message);
                    Application.Current.Shutdown();
                }
            }
        }

        private void window_closing(object sender, CancelEventArgs e)
        {
            processController.ShuttingDown = true;
            processController.KillAllActiveProcesses();
            SteamworksIntegration.ShutdownSteam();
        }

        private void GenerateGameConfig()
        {
            string[] args = Environment.GetCommandLineArgs();

            foreach (string arg in args)
            {
                if (arg.Contains("preendgame"))
                {
                    GlobalVars.fgd = GlobalVars.sdkdir + @"\pre_endgame_fgd\firefightreloaded.fgd";
                    break;
                }
            }

            if (!File.Exists(GlobalVars.gameconfig))
            {
                File.Create(GlobalVars.gameconfig).Dispose();
                File.WriteAllText(GlobalVars.gameconfig, KeyValueCreators.GenerateGameConfig().ToString());
            }
            else
            {
                string gameConfigText = File.ReadAllText(GlobalVars.gameconfig);
                KeyValue gameConfigValues = KVParser.ParseKeyValueText(gameConfigText);
                KeyValue FGDPath = gameConfigValues["Games"]["FIREFIGHTRELOADED"]["Hammer"];
                string FixedString = GlobalVars.fgd.Replace(" ", "");
                string FGDFilePath = FGDPath["GameData0"].GetString();

                if (!FGDFilePath.Equals(FixedString))
                {
                    File.Delete(GlobalVars.gameconfig);
                    File.WriteAllText(GlobalVars.gameconfig, KeyValueCreators.GenerateGameConfig().ToString());
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

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
            CreateMessageBoxAppLaunch(msgboxname, "Starting Hammer...");
            var proc = processController.LaunchApp(GlobalVars.hammer, " -game \"" + GlobalVars.moddir + "\"");
            proc.StartInfo.WorkingDirectory = GlobalVars.bindir;
            proc.Start();
            try
            {
                while (string.IsNullOrEmpty(proc.MainWindowTitle))
                {
                    GlobalVars.WaitForProcess(proc, GlobalVars.DelayMiliseconds);
                }

                CloseWindow(msgboxname);
            }
            catch (Exception ex)
            {
                CloseWindow(msgboxname);
                CreateMessageBox("An error has occurred when launching the application: " + ex.Message);
            }
        }

        private void model_DoubleClick(object sender, RoutedEventArgs e)
        {
            string msgboxname = "hlmvbox";
            CreateMessageBoxAppLaunch(msgboxname, "Starting Model Viewer...");
            var proc = processController.LaunchApp(GlobalVars.hlmv, " -game \"" + GlobalVars.moddir + "\" -olddialogs");
            proc.StartInfo.WorkingDirectory = GlobalVars.bindir;
            proc.Start();
            try
            {
                while (string.IsNullOrEmpty(proc.MainWindowTitle))
                {
                    GlobalVars.WaitForProcess(proc, GlobalVars.DelayMiliseconds);
                }

                CloseWindow(msgboxname);
            }
            catch (Exception ex)
            {
                CloseWindow(msgboxname);
                CreateMessageBox("An error has occurred when launching the application: " + ex.Message);
            }
        }

        private void faceposer_DoubleClick(object sender, RoutedEventArgs e)
        {
            string msgboxname = "facebox";
            CreateMessageBoxAppLaunch(msgboxname, "Starting Face Poser...");
            var proc = processController.LaunchApp(GlobalVars.hlfaceposer, " -game \"" + GlobalVars.moddir + "\"");
            proc.StartInfo.WorkingDirectory = GlobalVars.bindir;
            proc.Start();
            try
            {
                while (string.IsNullOrEmpty(proc.MainWindowTitle))
                {
                    GlobalVars.WaitForProcess(proc, GlobalVars.DelayMiliseconds);
                }

                CloseWindow(msgboxname);
            }
            catch (Exception ex)
            {
                CloseWindow(msgboxname);
                CreateMessageBox("An error has occurred when launching the application: " + ex.Message);
            }
        }

        private void workshop_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            string msgboxname = "workbox";
            CreateMessageBoxAppLaunch(msgboxname, "Starting Workshop Uploader...");
            var proc = processController.LaunchApp(GlobalVars.workshop, "");
            processController.AppOverridesSteam = true;
            proc.Start();
            try
            {
                while (string.IsNullOrEmpty(proc.MainWindowTitle))
                {
                    GlobalVars.WaitForProcess(proc, GlobalVars.DelayMiliseconds);
                }

                CloseWindow(msgboxname);
            }
            catch (Exception ex)
            {
                CloseWindow(msgboxname);
                CreateMessageBox("An error has occurred when launching the application: " + ex.Message);
            }
        }

        private void mapcomp_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            string msgboxname = "mapbox";
            CreateMessageBoxAppLaunch(msgboxname, "Starting Map Compiler...");
            var proc = processController.LaunchApp(GlobalVars.mapcomp, "");
            proc.Start();
            Task.Delay(2500);
            CloseWindow(msgboxname);
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
    }
}
