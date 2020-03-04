using ASA.Communication;
using ASA.Cryptography;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ARPClientApplication
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly Connectometer worker = null;
        public LoginWindow()
        {
            InitializeComponent();
            worker = new Connectometer(ShowConnection);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            worker.CancelAsync();
        }
        private void ShowConnection(ValueTuple<int, string> ustate)
        {
            switch (ustate.Item1)
            {
                case 1:
                    LoginImageDBOK.Visibility = Visibility.Visible;
                    LoginImageDBDown.Visibility = Visibility.Collapsed;
                    if (LoginTextBlockMessage.Text.Length > 8) { if (LoginTextBlockMessage.Text.Substring(0, 9) != "Проверьте") { LoginTextBlockMessage.Text = ""; } }
                    if (LoginTextBlockMessage.Text=="Nothing") { LoginTextBlockMessage.Text = ""; } 
                    break;
                default:
                    LoginImageDBOK.Visibility = Visibility.Collapsed;
                    LoginImageDBDown.Visibility = Visibility.Visible;
                    LoginTextBlockMessage.Text = ustate.Item2;
                    break;
            }
        }
        #region ARPEventHandlers
        public void ARPButtonClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            LoginTextBlockMessage.Text = b.Content.ToString();
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ARPDispatcher.AssignEHButtonClick(this, ARPDispatcherRealizer.ARPButtonClick);
            Interface.IEx.window = this;
            //LoginButtonEnter.Click += LoginButtonEnter_Click;
            ARPDispatcher.AssignEHTextBoxTextChanged(this, ARPDispatcherRealizer.ARPTextBoxTextChanged);
            ARPDispatcher.AssignEHPasswordBoxPasswordChanged(this, ARPDispatcherRealizer.ARPPasswordBoxPasswordChanged);
            //ПОДУМАТЬ ПО ОБНУЛЕНИЮ ЛОГИНА И ПАРОЛЯ ПРИ ВХОДЕ
            LoginTextBoxLogin.Text = "1"; LoginTextBoxLogin.Text = "";
            LoginPasswordBoxPassword.Password = "1"; LoginPasswordBoxPassword.Password = "";
        }

        private void LoginButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void LoginPasswordBoxPassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButtonEnter.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void LoginButtonSetARPServiceAddress_Click(object sender, RoutedEventArgs e)
        {
            string s = $"http://{LoginTextBoxARPServiceAddress.Text}:9700/ARPService";
            Properties.Settings.Default.ARPServiceAddress = s;
            Properties.Settings.Default.Save();
            LoginStackPanelARPServiceAddress.Visibility = Visibility.Collapsed;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F12)
            {
                if (LoginStackPanelARPServiceAddress.Visibility == Visibility.Visible)
                { LoginStackPanelARPServiceAddress.Visibility = Visibility.Collapsed; }
                else
                { LoginStackPanelARPServiceAddress.Visibility = Visibility.Visible; }
            }
        }
    }
}