using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ARPClientApplication.ARPMessageEAV
{
    [Serializable]
    public class A
    {
        public string S { get; set; }
        public string T { get; set; }
        public string N { get; set; }
        public string V { get; set; }
        public A()
        {
            S = Properties.Settings.Default.Session;
            T = "Запись переменной";
        }
    }
}
namespace ARPClientApplication.ARPMessageEAA
{
    [Serializable]
    public class A
    {
        public string S { get; set; }
        public string T { get; set; }
        public string N { get; set; }
        public string E { get; set; }
        public A()
        {
            S = Properties.Settings.Default.Session;
            T = "Д";
        }
    }
}
namespace ARPClientApplication.ARPMessageEAG
{
    [Serializable]
    public class A
    {
        public string S { get; set; }
        public string T { get; set; }
        public string N { get; set; }
        public string V { get; set; }
        public A()
        {
            S = Properties.Settings.Default.Session;
            T = "Генерация документов";
        }
    }
}