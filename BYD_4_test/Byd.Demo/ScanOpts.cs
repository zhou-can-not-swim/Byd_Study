using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Byd.Demo
{
    public class StationOptionItem
    {
        public bool Enabled { get; set; }
    }

    public class ScanOpts
    {
        public ScanOpts()
        {
            地板_堆垛机 = new StationOptionItem();
            侧围_堆垛机 = new StationOptionItem();
        }

        //初始化了两个 StationOptionItem
        public StationOptionItem 地板_堆垛机 { get; set; }
        public StationOptionItem 侧围_堆垛机 { get; set; }
    }
}
