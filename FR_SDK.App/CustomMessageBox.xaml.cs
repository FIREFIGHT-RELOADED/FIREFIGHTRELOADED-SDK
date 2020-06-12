using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace FR_SDK.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        private string text = "";

        #region Window Logic
        public CustomMessageBox(string name, string content)
        {
            Construct(name, content);
        }

        public CustomMessageBox(string content)
        {
            Construct("MessageBox", content);
        }

        private void Construct(string name, string content)
        {
            Name = name;
            text = content;
            InitializeComponent();
            SizeToContent = SizeToContent.Height;
            MouseDown += Window_MouseDown;
        }

        private void Window_init(object sender, EventArgs e)
        {
            content.Text = text;
        }

        private void window_closing(object sender, CancelEventArgs e)
        {
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
        #endregion
    }
}
