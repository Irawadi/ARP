using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml;
using System.Xml.Serialization;
using ASA.Communication;
using ASA.Helpers;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using SVM.Documents.RTF;

namespace ARPClientApplication.Interface
{
    public static class IEx
    {
        public static string FileSavingPath { get; set; }
        public static RTFDocument rTFDocument { get; set; }
        public static RoutedEventHandler DelLviSelected { get; set; }
        public static System.Windows.Window window { get; set; }
        public static void ExecuteInstructions(R r)
        {
            foreach (I ins in r.Instructions)
            {
                ExecuteInstruction(ins);
            }
        }
        public static void ExecuteInstruction(I i)
        {
            switch (i.N)
            {
                case "Window":
                    NewWin();
                    break;
                default:
                    ExecNamedElementInstruction(i);
                    break;
            }
        }
        private static void NewWin()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            window.Close();
            window = mainWindow;
        }
        private static void ExecNamedElementInstruction(I ins)
        {
            switch (ins.A)
            {
                case "SetText":
                    ExecSetText(ins);
                    break;
                case "AddHyperLinkToSavedFile":
                    ExecAddHyperLinkToSavedFile(ins);
                    break;
                case "ClickRadioButton":
                    ExecClickRadioButton(ins);
                    break;
                case "SetImageBrushSource":
                    ExecSetImageBrushSource(ins);
                    break;
                case "PopulateMenuListView":
                    ExecPopulateMenuListView(ins);
                    break;
                case "PopulateListView":
                    ExecPopulateListView(ins);
                    break;
                case "TextBoxSetText":
                    ExecTextBoxSetText(ins);
                    break;
                case "ClearListView":
                    ExecClearListView(ins);
                    break;
                case "TextBoxSetEnable":
                    ExecTextBoxSetEnable(ins);
                    break;
                case "TextBoxSetDisable":
                    ExecTextBoxSetDisable(ins);
                    break;
                case "ButtonSetEnable":
                    ExecButtonSetEnable(ins);
                    break;
                case "ButtonSetDisable":
                    ExecButtonSetDisable(ins);
                    break;
                case "DatePickerSelectDate":
                    ExecDatePickerSelectDate(ins);
                    break;
                case "GenerateDocuments":
                    ExecGenerateDocuments(ins);
                    break;
                case "GenerateDocumentsNoFolderDelition":
                    ExecGenerateDocuments(ins, false);
                    break;
                case "ButtonSetVisibility":
                    ExecButtonSetVisibility(ins);
                    break;
                case "StackPanelSetVisibility":
                    ExecStackPanelSetVisibility(ins);
                    break;
                case "RichTextBoxSetVisibility":
                    ExecRichTextBoxSetVisibility(ins);
                    break;
                case "RichTextBoxSetDocument":
                    ExecRichTextBoxSetDocument(ins);
                    break;
                case "SetFileSavingPath":
                    ExecSetFileSavingPath(ins);
                    break;
                case "SaveRTFDocument":
                    ExecSaveRTFDocument(ins);
                    break;
                case "PrintRTFDocument":
                    ExecPrintRTFDocument(ins);
                    break;
                case "SaveRTFFile":
                    ExecSaveRTFFile(ins);
                    break;
                case "NestedInstructions":
                    ExecNestedInstructions(ins);
                    break;
                case "PopulateBar":
                    ExecPopulateBar(ins);
                    break;
                default:
                    break;
            }
        }
        #region SetText
        private static void ExecSetText(I ins)
        {
            TextBlock textBlock = (TextBlock)window.FindName(ins.N);
            textBlock.Text = ins.V;
        }
        #endregion
        #region AddHyperLink
        private static void ExecAddHyperLink(I ins)
        {
            TextBlock textBlock = (TextBlock)window.FindName(ins.N);
            Hyperlink hyperlink = new Hyperlink() { NavigateUri = new Uri(ins.V) };
            hyperlink.Inlines.Add(textBlock.Text);
            hyperlink.RequestNavigate += Hyperlink_RequestNavigate;
            textBlock.Inlines.Clear();
            textBlock.Inlines.Add(hyperlink);
        }
        private static void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e) { System.Diagnostics.Process.Start(e.Uri.ToString()); }
        #endregion        
        #region AddHyperLinkToSavedFile
        private static void ExecAddHyperLinkToSavedFile(I ins)
        {
            TextBlock textBlock = (TextBlock)window.FindName(ins.N);
            Hyperlink hyperlink = new Hyperlink() { NavigateUri = new Uri(FileSavingPath) };
            hyperlink.Inlines.Add(ins.V);
            hyperlink.RequestNavigate += Hyperlink_RequestNavigate;
            textBlock.Inlines.Clear();
            textBlock.Inlines.Add(hyperlink);
        }
        #endregion        
        #region ClickRadioButton
        private static void ExecClickRadioButton(I ins)
        {
            RadioButton radioButton = ARPDispatcher.FindRadioButtonByGroupNameAndContent(window, ins.N, ins.V);
            radioButton.RaiseEvent(new RoutedEventArgs(RadioButton.ClickEvent));
            radioButton.IsChecked = true;
        }
        #endregion
        #region SetImageBrushSource
        private static void ExecSetImageBrushSource(I ins)
        {
            ImageBrush imageBrush = (ImageBrush)window.FindName(ins.N);
            imageBrush.ImageSource = GetImageSourceFromValue(ins.V);
        }
        private static BitmapImage GetImageSourceFromValue(string hex)
        {
            byte[] a = Asesor.ToBytesFromSQLServerBinary(hex);
            using (Stream stream = new MemoryStream(a))
            {
                BitmapImage image = new BitmapImage();
                stream.Position = 0;
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }
        #endregion
        #region PopulateMenuListView
        private static void ExecPopulateMenuListView(I ins)
        {
            ListView listView = (ListView)window.FindName(ins.N);
            MIC mic;
            XmlSerializer formatter = new XmlSerializer(typeof(MIC));
            using (TextReader textReader = new StringReader(ins.V.Replace("[[[[[", "<").Replace("]]]]]", ">")))
            {
                mic = (MIC)formatter.Deserialize(textReader);
            }
            LoadMenu(mic, listView);
            AssignInterfaceActions(window);
        }
        private static void LoadMenu(MIC mic, ListView listView)
        {
            TextBlock textBlock; PackIcon packIcon; StackPanel stackPanel; ListViewItem listViewItem;
            StackPanel stack = (StackPanel)window.FindName("InterfaceStackPanel");
            DockPanel dockPanel; XmlReader xmlReader; StringReader stringReader;
            string foodForXAML;
            foreach (MenuItem menuItem in mic.MenuItems)
            {
                textBlock = new TextBlock()
                {
                    Text = menuItem.Caption,
                    Margin = new System.Windows.Thickness(10),
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 16,
                    Width = 190
                };
                packIcon = new PackIcon()
                {
                    Kind = (PackIconKind)Int32.Parse(menuItem.PackIconKind),
                    Width = 30,
                    Height = 30,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                    Margin = new System.Windows.Thickness(30, 5, 5, 5),
                    Foreground = new SolidColorBrush(Colors.White)
                };
                stackPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new System.Windows.Thickness(10, 0, 10, 0)
                };
                stackPanel.Children.Add(textBlock); stackPanel.Children.Add(packIcon);
                listViewItem = new ListViewItem()
                {
                    Name = menuItem.Name,
                    Height = 60,
                    Content = stackPanel
                };
                listViewItem.Selected += DelLviSelected;
                listView.Items.Add(listViewItem);
                foodForXAML = menuItem.Interface.Replace("{{{{{", "<").Replace("}}}}}", ">");
                try
                {
                    stringReader = new StringReader(foodForXAML);
                    xmlReader = XmlReader.Create(stringReader);
                    dockPanel = (DockPanel)XamlReader.Load(xmlReader);
                    dockPanel.Name = "DockPanel" + menuItem.Name;
                    stack.Children.Add(dockPanel);
                }
                catch (Exception) { }
            }
        }
        private static void AssignInterfaceActions(DependencyObject dependencyObject)
        {
            ARPDispatcher.AssignEHTextBoxTextChanged(dependencyObject, ARPDispatcherRealizer.ARPTextBoxTextChanged);
            ARPDispatcher.AssignEHButtonClick(dependencyObject, ARPDispatcherRealizer.ARPButtonClick);
            ARPDispatcher.AssignEHRadioButtonClick(dependencyObject, ARPDispatcherRealizer.ARPRadioButtonClick);
            ARPDispatcher.AssignEHListViewSelectionChanged(dependencyObject, ARPDispatcherRealizer.ARPListViewSelectionChanged);
            ARPDispatcher.AssignEHDatePickerSelectedDateChanged(dependencyObject, ARPDispatcherRealizer.ARPDatePickerSelectionChanged);
            ARPDispatcher.SetDatePickersLanguage(dependencyObject);
        }
        #endregion
        #region PopulateListView
        private static void ExecPopulateListView(I ins)
        {
            ListView listView = ARPDispatcher.FindListViewByName(window, ins.N);
            ListViewContent listViewContent = new ListViewContent(ins.V);
            listView.ItemsSource = null;
            try { listView.ItemsSource = listViewContent.lVTests; } catch (Exception) { }

        }
        #endregion
        #region TextBoxSetText
        private static void ExecTextBoxSetText(I ins)
        {
            TextBox textBox = ARPDispatcher.FindTextBoxByName(window, ins.N);
            textBox.Text = ins.V;
        }
        #endregion
        #region ClearListView
        private static void ExecClearListView(I ins)
        {
            ListView listView = ARPDispatcher.FindListViewByName(window, ins.N);
            listView.ItemsSource = null;
        }
        #endregion
        #region TextBoxSetEnable
        private static void ExecTextBoxSetEnable(I ins)
        {
            TextBox textBox = ARPDispatcher.FindTextBoxByName(window, ins.N);
            textBox.IsEnabled = true;
        }
        #endregion
        #region TextBoxSetDisable
        private static void ExecTextBoxSetDisable(I ins)
        {
            TextBox textBox = ARPDispatcher.FindTextBoxByName(window, ins.N);
            textBox.IsEnabled = false;
        }
        #endregion
        #region ButtonSetEnable
        private static void ExecButtonSetEnable(I ins)
        {
            Button button = ARPDispatcher.FindButtonByName(window, ins.N);
            button.IsEnabled = true;
        }
        #endregion
        #region ButtonSetDisable
        private static void ExecButtonSetDisable(I ins)
        {
            Button button = ARPDispatcher.FindButtonByName(window, ins.N);
            button.IsEnabled = false;
        }
        #endregion
        #region DatePickerSelectDate
        private static void ExecDatePickerSelectDate(I ins)
        {
            DatePicker datePicker = ARPDispatcher.FindDatePickerByName(window, ins.N);
            try { if (ins.V == "") { datePicker.SelectedDate = null; } else { datePicker.SelectedDate = Convert.ToDateTime(ins.V); } } catch (Exception) { }
        }
        #endregion
        #region GenerateDocuments
        private static void ExecGenerateDocuments(I ins, bool dirdelete = true)
        {
            try
            {
                //Get Array of Values
                string typeDocument = ins.N;
                string[] vals = ins.V.Split(new char[] { '|' });
                //DIRECTORY: Create and Delete
                string folderName = vals[0];
                DirectoryInfo directoryInfo = new DirectoryInfo(folderName);
                if (dirdelete)
                {
                    if (directoryInfo.Exists) { directoryInfo.Delete(true); }
                    directoryInfo.Create();
                }
                else
                {
                    if (!directoryInfo.Exists) { directoryInfo.Create(); }
                }
                //Generate Files Asyncronously
                BackgroundWorker worker = new BackgroundWorker() { WorkerSupportsCancellation = true, WorkerReportsProgress = true };
                worker.DoWork += Worker_GenerateDocuments;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerCompleted += DocumentGenerationCompleted;
                LVTest lv = new LVTest() { T1 = typeDocument, T2 = folderName, T3 = ins.V };
                worker.RunWorkerAsync(argument: lv);
            }
            catch (Exception e)
            {
                TextBlock textBlock = (TextBlock)window.FindName("ApplicationTextBlockMessage");
                textBlock.Text = e.Message;
            }
        }
        private static void Worker_GenerateDocuments(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            LVTest lv = (LVTest)e.Argument;
            string input = lv.T3;
            string[] vals = input.Split(new char[] { '|' });
            int progress; double n = (double)(vals.Length - 1) / 100;
            if (vals.Length > 1)
            {
                for (int i = 1; i < vals.Length - 1; i++)
                {
                    if (vals[i] != "")
                    {
                        lv.T4 = vals[i];
                        try { GenerateDocument(lv); }
                        catch (Exception) { }
                    }
                    progress = Convert.ToInt32(i / n);
                    worker.ReportProgress(progress);
                }
            }
            e.Result = "Генерация документов: готово.";
        }
        private static void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)window.FindName("ApplicationTextBlockMessage");
            textBlock.Text = "Генерация документов: выполнено " + e.ProgressPercentage.ToString() + "%...";
        }
        private static void DocumentGenerationCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)window.FindName("ApplicationTextBlockMessage");
            if (e.Error != null)
            {
                textBlock.Text = e.Error.Message;
            }
            else
            {
                textBlock.Text = e.Result.ToString();
            }
        }
        private static void GenerateDocument(LVTest lv)
        {
            string query;
            string result;
            ARPMessageEAG.A a = new ARPMessageEAG.A() { N = lv.T1, V = lv.T4 };
            XmlSerializer xsSubmit = new XmlSerializer(typeof(ARPMessageEAG.A));
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, a);
                    query = sww.ToString(); // Your XML
                }
            }
            try
            {
                using (ServiceSpeaker serviceSpeaker = new ServiceSpeaker(ServiceSpeaker.ConstructionMode.fromString, Properties.Settings.Default.ARPServiceAddress))
                {
                    result = serviceSpeaker.Do(query);
                }
            }
            catch (Exception)
            {
                result = "Nothing";
            }
            try
            {
                if (result != "Nothing")
                {
                    ARPGenerated.Document d;
                    XmlSerializer formatter = new XmlSerializer(typeof(ARPGenerated.Document));
                    using (TextReader textReader = new StringReader(result))
                    {
                        d = (ARPGenerated.Document)formatter.Deserialize(textReader);
                    }
                    string fullname = Path.Combine(lv.T2, d.Name);
                    DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(fullname));       //для имён файлов с датами
                    if (!directoryInfo.Exists) { directoryInfo.Create(); }
                    RTFDocument rd = new RTFDocument();
                    d.Content = rd.AddUnicodeToString(d.Content);
                    using (FileStream fstream = new FileStream(fullname, FileMode.OpenOrCreate))
                    {
                        // преобразуем строку в байты
                        byte[] array = Encoding.UTF8.GetBytes(d.Content);
                        // запись массива байтов в файл
                        fstream.Write(array, 0, array.Length);
                    }
                }
            }
            catch (Exception)
            { }
        }
        #endregion
        #region ButtonSetVisibility
        private static void ExecButtonSetVisibility(I ins)
        {
            Button button = ARPDispatcher.FindButtonByName(window, ins.N);
            switch (ins.V)
            {
                case "Collapsed": button.Visibility = Visibility.Collapsed; break;
                case "Visible": button.Visibility = Visibility.Visible; break;
            }
        }
        #endregion
        #region StackPanelSetVisibility
        private static void ExecStackPanelSetVisibility(I ins)
        {
            StackPanel stackPanel = ARPDispatcher.FindStackPanelByName(window, ins.N);
            switch (ins.V)
            {
                case "Collapsed": stackPanel.Visibility = Visibility.Collapsed; break;
                case "Visible": stackPanel.Visibility = Visibility.Visible; break;
            }
        }
        #endregion        
        #region RichTextBoxSetVisibility
        private static void ExecRichTextBoxSetVisibility(I ins)
        {
            RichTextBox richTextBox = ARPDispatcher.FindRichTextBoxByName(window, ins.N);
            switch (ins.V)
            {
                case "Collapsed": richTextBox.Visibility = Visibility.Collapsed; break;
                case "Visible": richTextBox.Visibility = Visibility.Visible; break;
            }
        }
        #endregion
        #region RichTextBoxSetDocument
        private static void ExecRichTextBoxSetDocument(I ins)
        {
            RichTextBox richTextBox = ARPDispatcher.FindRichTextBoxByName(window, ins.N);
            rTFDocument = new RTFDocument();
            string txt = rTFDocument.AddUnicodeToString(ins.V);
            rTFDocument.RichText = txt;
            TextRange doc = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            using (MemoryStream ms = new MemoryStream())
            {
                var writer = new StreamWriter(ms);
                writer.Write(txt);
                writer.Flush();
                ms.Position = 0;
                doc.Load(ms, DataFormats.Rtf);
            }
        }
        #endregion
        #region SetFileSavingPath
        private static void ExecSetFileSavingPath(I ins)
        {
            FileSavingPath = ins.V;
        }
        #endregion
        #region SaveRTFDocument
        private static void ExecSaveRTFDocument(I ins)
        {
            try
            {
                string folderName = Path.GetDirectoryName(FileSavingPath);
                DirectoryInfo directoryInfo = new DirectoryInfo(folderName);
                if (!directoryInfo.Exists) { directoryInfo.Create(); }
                using (FileStream fstream = new FileStream(FileSavingPath, FileMode.OpenOrCreate))
                {
                    // преобразуем строку в байты
                    byte[] array = Encoding.UTF8.GetBytes(rTFDocument.RichText);
                    // запись массива байтов в файл
                    fstream.Write(array, 0, array.Length);
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion
        #region PrintRTFDocument
        private static void ExecPrintRTFDocument(I ins)
        {
            RichTextBox richTextBox = ARPDispatcher.FindRichTextBoxByName(window, ins.N);
            try
            {
                PrintDialog pd = new PrintDialog();
                if ((pd.ShowDialog() == true))
                {
                    pd.PrintVisual(richTextBox as Visual, "printing as visual");
                }
            }
            catch (Exception) { }
        }
        #endregion
        #region SaveRTFFile
        private static void ExecSaveRTFFile(I ins)
        {
            try
            {
                rTFDocument = new RTFDocument();
                rTFDocument.RichText = rTFDocument.AddUnicodeToString(ins.V);
                string folderName = Path.GetDirectoryName(ins.N);
                DirectoryInfo directoryInfo = new DirectoryInfo(folderName);
                if (!directoryInfo.Exists) { directoryInfo.Create(); }
                using (FileStream fstream = new FileStream(ins.N, FileMode.OpenOrCreate))
                {
                    // преобразуем строку в байты
                    byte[] array = Encoding.UTF8.GetBytes(rTFDocument.RichText);
                    // запись массива байтов в файл
                    fstream.Write(array, 0, array.Length);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion
        #region NestedInstructions
        private static void ExecNestedInstructions(I ins)
        {
            ARPDispatcherRealizer.ExecuteInterfaceActions(ins.V.Replace("[", "<").Replace("]", ">"));
        }
        #endregion
        #region PopulateBar
        private static void ExecPopulateBar(I ins)
        {
            string s = (ins.V.Replace("[", "<").Replace("]", ">"));
            try
            {
                Graphica.PopulationInfoBSB infoBSB;
                XmlSerializer formatter = new XmlSerializer(typeof(Graphica.PopulationInfoBSB));
                using (TextReader textReader = new StringReader(s))
                {
                    infoBSB = (Graphica.PopulationInfoBSB)formatter.Deserialize(textReader);
                }
                Label label = ARPDispatcher.FindLabelByName(window, ins.N);
                label.Content = new BasicColumn(infoBSB) { Height = label.Height - 15, Width = label.Width - 15 };
                label.Effect = new DropShadowEffect
                {
                    Color = new System.Windows.Media.Color { A = 255, R = 0, G = 0, B = 0 },
                    Direction = 320,
                    ShadowDepth = 5,
                    Opacity = 0.5,
                    BlurRadius = 15
                };
            }
            catch (Exception) { }
        }
        #endregion        
    }
}
