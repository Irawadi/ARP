using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ARPClientApplication.Interface;
using MaterialDesignThemes.Wpf;

namespace ARPClientApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Connectometer worker = null;
        public MainWindow()
        {
            InitializeComponent();
            worker = new Connectometer(ShowConnection);
            IEx.DelLviSelected = Lvi_Selected;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }
        private void Lvi_Selected(object sender, RoutedEventArgs e)
        {
            DockPanel dockPanel;
            ListViewItem listViewItem = (ListViewItem)sender;

            StackPanel stackPanel = (StackPanel)listViewItem.Content;
            TextBlock textBlock;
            foreach (UIElement item in stackPanel.Children)
            {
                if (item is TextBlock) { textBlock = (TextBlock)item; TextBlockFormName.Text = textBlock.Text; }
            }


            foreach (UIElement item in InterfaceStackPanel.Children)
            {
                if (item is DockPanel)
                {
                    dockPanel = (DockPanel)item;
                    if (dockPanel.Name == "DockPanel" + listViewItem.Name)
                    { dockPanel.Visibility = Visibility.Visible; }
                    else
                    { dockPanel.Visibility = Visibility.Collapsed; }
                }
            }
        }

        private void ShowConnection(ValueTuple<int, string> ustate)
        {
            switch (ustate.Item1)
            {
                case 1:
                    ApplicationImageDBOK.Visibility = Visibility.Visible;
                    ApplicationImageDBDown.Visibility = Visibility.Collapsed;
                    ApplicationTextBlockDB.Text = "";
                    break;
                default:
                    ApplicationImageDBOK.Visibility = Visibility.Collapsed;
                    ApplicationImageDBDown.Visibility = Visibility.Visible;
                    ApplicationTextBlockDB.Text = ustate.Item2;
                    break;
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            worker.CancelAsync();
        }

        private void ApplicationButtonInformation_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicationTextBlockUserName.Visibility == Visibility.Collapsed) { ApplicationTextBlockUserName.Visibility = Visibility.Visible; } else { ApplicationTextBlockUserName.Visibility = Visibility.Collapsed; }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }
        private void ApplicationButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            //ApplicationTextBlockMessage.Inlines.Clear();
            //ApplicationTextBlockMessage.Inlines.Add("Документ находится в ");
            //Hyperlink hyperlink = new Hyperlink() { NavigateUri = new Uri(@"file:///D:\1\Направления\Стратегия") };
            //hyperlink.Inlines.Add("папке.");
            //hyperlink.RequestNavigate += Hyperlink_RequestNavigate;
            //ApplicationTextBlockMessage.Inlines.Add(hyperlink);
            //ApplicationTextBlockMessage.Text = @"file:///D:\1\Направления\Стратегия";
        }
        //private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        //{
        //    System.Diagnostics.Process.Start(e.Uri.ToString());
        //}
        private void ButtonBell_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
                this.WindowState = System.Windows.WindowState.Normal;
            else
                this.WindowState = System.Windows.WindowState.Maximized;
        }

        private void ButtonPrintPreview_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ButtonPrintPreviewOff_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ButtonSaveDocument_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ButtonPrintDocument_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
