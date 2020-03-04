using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPClientApplication.Graphica
{
    /// <summary>
    /// Класс для формирования графиков
    /// </summary>
    [Serializable]
    public class PopulationInfoBSB
    {
        public string AxisXTitle { get; set; }
        public string AxisYTitle { get; set; }
        public List<string> Labels { get; set; }
        public string FormatterString { get; set; }
        public List<StackedColumnSeries> SeriesCollection { get; set; }
        public string Type { get; set; }
        public string DataLabels { get; set; }
        public PopulationInfoBSB()
        {
            Labels = new List<string>();
            SeriesCollection = new List<StackedColumnSeries>();
        }
    }
    [Serializable]
    public class StackedColumnSeries
    {
        public string Title { get; set; }
        public List<double> Values { get; set; }
        public StackedColumnSeries()
        {
            Values = new List<double>();
        }
    }
}
