using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPClientApplication.Interface
{
    [Serializable]
    public class R
    {
        public int Res { get; set; }
        public List<I> Instructions { get; set; }
        public R() { }
    }
}
