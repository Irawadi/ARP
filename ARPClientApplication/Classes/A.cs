using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ARPClientApplication.ARPMessageEAV
{
    /// <summary>
    /// Сообщение о действии - изменение переменной (текстовый ввод, изменение выбора списка и т.д.)
    /// </summary>
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
    /// <summary>
    /// Сообщение о действии - нажатии кнопки, двойном щелчке по списку и т.д.
    /// </summary>
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
    /// <summary>
    /// Сообщение о действии - генерации документов
    /// </summary>
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