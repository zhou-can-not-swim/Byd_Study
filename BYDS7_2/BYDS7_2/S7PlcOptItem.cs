using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYDS7_2
{
    public class S7PlcOptItem
    {
        public string IpAddr { get; set; } = "127.0.0.1";


        public short Rack { get; set; }

        public short Slot { get; set; } = 1;

    }
}
