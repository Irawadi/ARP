using LiveCharts;
using LiveCharts.Wpf;
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

namespace ARPClientApplication
{
    /// <summary>
    /// Логика взаимодействия для UCBSB.xaml  ---=== UCBSB - User Control Basic Stacked Bar
    /// </summary>
    public partial class Basic_Stacked_Bar : UserControl
    {
        public Basic_Stacked_Bar(Graphica.PopulationInfoBSB p)
        {
            InitializeComponent();
            aX.Title = p.AxisXTitle; aY.Title = p.AxisYTitle;
            SeriesCollection = new SeriesCollection();
            foreach (Graphica.StackedColumnSeries s in p.SeriesCollection)
            {
                StackedColumnSeries stackedColumnSeries = new StackedColumnSeries() { DataLabels = true, Title = s.Title, Values = new ChartValues<double>() };
                foreach (double value in s.Values) { stackedColumnSeries.Values.Add(value); }
                SeriesCollection.Add(stackedColumnSeries);
            }
            Formatter = value => value + p.FormatterString;
            Labels = p.Labels.ToArray();
            DataContext = this;
        }
        public Basic_Stacked_Bar()
        {
            InitializeComponent();
            aX.Title = "Год"; aY.Title = "";
            SeriesCollection = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Values = new ChartValues<double> {4, 5, 6, 8},
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true,
                    Foreground = new SolidColorBrush(Colors.White),
                    Fill = new SolidColorBrush(Colors.DarkMagenta),
                    Title = "Фиолетовые"
                },
                new StackedColumnSeries
                {
                    Values = new ChartValues<double> {2, 5, 6, 7},
                    StackMode = StackMode.Values,
                    DataLabels = true
                }
            };

            //adding series updates and animates the chart
            SeriesCollection.Add(new StackedColumnSeries
            {
                Values = new ChartValues<double> { 6, 2, 7 },
                StackMode = StackMode.Values,
                DataLabels = true
            });

            //adding values also updates and animates
            SeriesCollection[2].Values.Add(4d);

            Labels = new[] { "2016", "2017", "2018", "2019" };
            Formatter = value => value + " тыс. руб.";

            DataContext = this;
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}
