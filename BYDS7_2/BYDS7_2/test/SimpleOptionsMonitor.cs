using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYDS7_2.test
{
    public class SimpleOptionsMonitor<T> : IOptionsMonitor<T>
    {
        private readonly T _options;

        public SimpleOptionsMonitor(T options)
        {
            _options = options;
        }

        public T Get(string name) => _options;

        public IDisposable OnChange(Action<T, string> listener) => null;

        public T CurrentValue => _options;
    }
}
