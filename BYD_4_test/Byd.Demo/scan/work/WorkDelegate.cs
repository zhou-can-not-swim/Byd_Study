using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Byd.Demo.scan
{
    using Byd.Demo.scan.work;
    using System.Threading.Tasks;

    namespace Itminus.Middlewares
    {
        public delegate Task WorkDelegate<TWorkContext>(TWorkContext context) where TWorkContext : IWorkContext;
    }
}
