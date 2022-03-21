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
        private bool keepTempText = false;

        #region Window Logic
        public CustomMessageBox(string name, string content, bool bkeepTempText = false)
        {
            Construct(name, content, bkeepTempText);
        }

        public CustomMessageBox(string content, bool bkeepTempText = false)
        {
            Construct("MessageBox", content, bkeepTempText);
        }

        public CustomMessageBox()
        {
            Construct("MessageBox", "", true);
        }

        private void Construct(string name, string content, bool bkeepTempText)
        {
            Name = name;
            text = content;
            keepTempText = bkeepTempText;
            InitializeComponent();
            MouseDown += Window_MouseDown;
        }

        private void Window_init(object sender, EventArgs e)
        {
            if (!keepTempText)
            {
                content.Text = text;
            }
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
