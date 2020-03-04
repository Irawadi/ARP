using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPClientApplication.Interface
{
    /// <summary>
    /// Класс Инструкции (Инструкция поступает от службы передачи данных)
    /// </summary>
    [Serializable]
    public class I
    {
        /// <summary>
        /// Имя элемента управления
        /// </summary>
        public string N { get; set; }
        /// <summary>
        /// Наименование действия
        /// </summary>
        public string A { get; set; }
        /// <summary>
        /// Значение - параметр действия
        /// </summary>
        public string V { get; set; }
        public I() { }
    }
}
