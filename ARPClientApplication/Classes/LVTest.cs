using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPClientApplication.Interface
{
    /// <summary>
    /// Класс строки списка 
    /// </summary>
    [Serializable]
    public class LVTest
    {
        public string T1 { get; set; }
        public string T2 { get; set; }
        public string T3 { get; set; }
        public string T4 { get; set; }
        public string T5 { get; set; }
        public string T6 { get; set; }
        public string T7 { get; set; }
        public string T8 { get; set; }
        public string T9 { get; set; }
        public string T10 { get; set; }
        public LVTest() { }
        public LVTest(string input)
        {
            T1 = "";
            T2 = "";
            T3 = "";
            T4 = "";
            T5 = "";
            T6 = "";
            T7 = "";
            T8 = "";
            T9 = "";
            T10 = "";
            string[] words = input.Split(new char[] { '|' });
            for (int i = 0; i < words.Length - 1; i++)
            {
                switch (i)
                {
                    case 0: T1 = words[i]; break;
                    case 1: T2 = words[i]; break;
                    case 2: T3 = words[i]; break;
                    case 3: T4 = words[i]; break;
                    case 4: T5 = words[i]; break;
                    case 5: T6 = words[i]; break;
                    case 6: T7 = words[i]; break;
                    case 7: T8 = words[i]; break;
                    case 8: T9 = words[i]; break;
                    case 9: T10 = words[i]; break;
                    default:
                        break;
                }
            }
        }
    }
    /// <summary>
    /// Класс содержимого списка
    /// </summary>
    [Serializable]
    public class ListViewContent
    {
        public List<LVTest> lVTests { get; set; }
        public ListViewContent(string input)
        {
            lVTests = new List<LVTest>();
            string[] words = input.Split(new char[] { '`' });
            for (int i = 0; i < words.Length - 1; i++)
            {
                try { if (words[i] != "") { lVTests.Add(new LVTest(words[i])); } } catch (Exception) { }

            }
        }
    }
}
