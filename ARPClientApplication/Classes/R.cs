using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPClientApplication.Interface
{
    /// <summary>
    /// Ответ службы передачи данных
    /// </summary>
    [Serializable]
    public class R
    {
        /// <summary>
        /// Код результата
        /// </summary>
        public int Res { get; set; }
        /// <summary>
        /// Инструкции для выполнения
        /// </summary>
        public List<I> Instructions { get; set; }
        public R() { }
    }
}
