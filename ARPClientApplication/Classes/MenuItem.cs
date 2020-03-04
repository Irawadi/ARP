using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPClientApplication.Interface
{
    /// <summary>
    /// Форма Интерфейса
    /// </summary>
    [Serializable]
    public class MenuItem
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Значок
        /// </summary>
        public string PackIconKind { get; set; }
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// XAML-содержимое
        /// </summary>
        public string Interface { get; set; }
        public MenuItem() { }
    }
    /// <summary>
    /// Класс коллекции форм
    /// </summary>
    [Serializable]
    public class MIC
    {
        public List<MenuItem> MenuItems { get; set; }
        public MIC() { }
    }
}
