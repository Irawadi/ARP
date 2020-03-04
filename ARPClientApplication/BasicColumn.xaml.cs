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
    /// Логика взаимодействия для BasicColumn.xaml
    /// </summary>
    public partial class BasicColumn : UserControl
    {
        public void Populate(Graphica.PopulationInfoBSB p)
        {
            bool showdl = (p.DataLabels == "Yes");
            aX.Title = p.AxisXTitle; aY.Title = p.AxisYTitle;
            SeriesCollection = new SeriesCollection();
            foreach (Graphica.StackedColumnSeries s in p.SeriesCollection)
            {
                ColumnSeries ColumnSeries = new ColumnSeries(); StackedColumnSeries stackedColumnSeries = new StackedColumnSeries();
                switch (p.Type)
                {
                    case "StackedColumn":
                        stackedColumnSeries = new StackedColumnSeries() { DataLabels = showdl, Title = s.Title, Values = new ChartValues<double>() };
                        foreach (double value in s.Values) { stackedColumnSeries.Values.Add(value); }
                        SeriesCollection.Add(stackedColumnSeries);
                        break;
                    case "Column":
                        ColumnSeries = new ColumnSeries() { DataLabels = showdl, Title = s.Title, Values = new ChartValues<double>() };
                        foreach (double value in s.Values) { ColumnSeries.Values.Add(value); }
                        SeriesCollection.Add(ColumnSeries);
                        break;
                    default:
                        ColumnSeries = new ColumnSeries() { DataLabels = showdl, Title = s.Title, Values = new ChartValues<double>() };
                        foreach (double value in s.Values) { ColumnSeries.Values.Add(value); }
                        SeriesCollection.Add(ColumnSeries);
                        break;
                }
            }
            Formatter = value => value + p.FormatterString;
            Labels = p.Labels.ToArray();
            DataContext = this;
        }
        public BasicColumn(Graphica.PopulationInfoBSB p)
        {
            InitializeComponent();
            Populate(p);
        }
        public BasicColumn()
        {
            InitializeComponent();
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}