using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using ARPClientApplication.Graphica;
using MaterialDesignThemes.Wpf;
using System.Windows.Media.Effects;
using System.Xml.Serialization;

namespace ARPClientApplication
{
    /// <summary>
    /// Логика взаимодействия для TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public List<Interface.I> ins;
        public int n = 0;
        public TestWindow()
        {
            InitializeComponent();
            //CultureInfo cultureInfo = new CultureInfo("ru-RU");
            ////DatePickerExpenditureDate.Language = XmlLanguage.GetLanguage(System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag); 
            //DatePickerExpenditureDate.Language = XmlLanguage.GetLanguage(cultureInfo.IetfLanguageTag);
            //ins = new List<Interface.I>()
            //{
            //    new Interface.I() { N = "RadioButtonGroupTypePaymentMeasurement", A = "ClickRadioButton", V="Разовый" },
            //    new Interface.I() { N = "RadioButtonGroupTypePaymentMeasurement", A = "ClickRadioButton", V="По площади" },
            //    new Interface.I() { N = "RadioButtonGroupTypePaymentMeasurement", A = "ClickRadioButton", V="По использованию" }
            //};
        }

        private void ButtonTest_Click(object sender, RoutedEventArgs e)
        {
            Interface.IEx.ExecuteInstruction(ins[n]);
            if (n == 2) { n = 0; } else { n = n + 1; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Basic_Stacked_Bar uCBSB = new Basic_Stacked_Bar()
            ////{
            ////    SeriesCollection = new SeriesCollection
            ////    {
            ////    new StackedColumnSeries
            ////    {
            ////        Values = new ChartValues<double> { 4, 5, 6, 8 },
            ////        StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
            ////        DataLabels = true
            ////    },
            ////    new StackedColumnSeries
            ////    {
            ////        Values = new ChartValues<double> { 2, 5, 6, 7 },
            ////        StackMode = StackMode.Values,
            ////        DataLabels = true
            ////    }
            ////    }
            ////}
            //;
            //uCBSB.Height = 400;
            //TestStackPanel.Children.Add(uCBSB);
            //Interface.IEx.window = this;
            //DatePickerExpenditureDate.DisplayDate = Convert.ToDateTime("19.03.2019");
        }

        private void ButtonCounterpartDelete_Click(object sender, RoutedEventArgs e)
        {
            //DatePickerExpenditureDate.SelectedDate = null;
            //DatePickerExpenditureDate.DisplayDate = Convert.ToDateTime("19.03.2019");
        }
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            PopulationInfoBSB bSB = new PopulationInfoBSB()
            {
                AxisXTitle = "Годы",
                AxisYTitle = "",
                Labels = new List<string>() { "2016", "2017", "2018" },
                Type = "Column",
                FormatterString = "руб.",
                SeriesCollection = new List<Graphica.StackedColumnSeries>()
                {
                    new Graphica.StackedColumnSeries(){ Title = "Доходы", Values = new List<double>(){ 547000, 611500, 712000 } },
                    new Graphica.StackedColumnSeries(){ Title = "Расходы", Values = new List<double>(){ 523000, 615000, 589300 } }
                }
            };
            XmlSerializer formatter = new XmlSerializer(typeof(PopulationInfoBSB));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(@"C:\Users\Людмила\Documents\PopulationInfoBSB.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, bSB);
            }
            BasicColumn column = new BasicColumn(bSB);
            foreach (Label label in FindVisualChildren<Label>(this).Where(l => l.Name.StartsWith("LabelBasicColumn")))
            {
                label.Content = new BasicColumn(bSB) { Height = label.Height - 15, Width = label.Width - 15 };
                label.Effect = new DropShadowEffect
                {
                    Color = new Color { A = 255, R = 0, G = 0, B = 0},
                    Direction = 320,
                    ShadowDepth = 5,
                    Opacity = 0.5,
                    BlurRadius = 15
                };
            }
        }

        private void ButtonPaymentDelete_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = "RichText Files (*.rtf)|*.rtf|All files (*.*)|*.*";

            //if (ofd.ShowDialog() == true)
            //{
            //    TextRange doc = new TextRange(docBox.Document.ContentStart, docBox.Document.ContentEnd);
            //    using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open))
            //    {
            //        if (System.IO.Path.GetExtension(ofd.FileName).ToLower() == ".rtf")
            //            doc.Load(fs, DataFormats.Rtf);
            //        else if (System.IO.Path.GetExtension(ofd.FileName).ToLower() == ".txt")
            //            doc.Load(fs, DataFormats.Text);
            //        else
            //            doc.Load(fs, DataFormats.Xaml);
            //    }
            //}
        }

        private void ButtonTryFindVisualChildren_Click(object sender, RoutedEventArgs e)
        {
            //SetDatePickersLanguage(this);
            //foreach (Button b in FindVisualChildren<Button>(this))
            //{
            //    TB.Text += b.Name + "|";
            //}
        }
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        private static void SetDatePickersLanguage(DependencyObject depObj)
        {
            CultureInfo cultureInfo = new CultureInfo("ru-RU");
            foreach (DatePicker d in FindVisualChildren<DatePicker>(depObj)) { d.Language = XmlLanguage.GetLanguage(cultureInfo.IetfLanguageTag); }
        }
    }
}
