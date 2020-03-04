using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPClientApplication.Interface
{
    [Serializable]
    public class MenuItem
    {
        public string Name { get; set; }
        public string PackIconKind { get; set; }
        public string Caption { get; set; }
        public string Interface { get; set; }
        public MenuItem() { }
    }
    [Serializable]
    public class MIC
    {
        public List<MenuItem> MenuItems { get; set; }
        public MIC() { }
    }
}
