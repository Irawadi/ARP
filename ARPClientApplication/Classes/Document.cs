using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPClientApplication.ARPGenerated
{
    //Класс для сериализации документа (rtf)
    [Serializable]
    public class Document
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public Document()
        {

        }
    }
}
