using Byd.Demo.test;
using System.Collections.Concurrent;

namespace BYD_4_test.plc.service
{
    public class S7PlcOptsBuilder
    {
        private ConcurrentDictionary<string, S7PlcOptItem> _maps;

        public S7PlcOptsBuilder()
        {
            _maps = new ConcurrentDictionary<string, S7PlcOptItem>();
        }

        public S7PlcOptsBuilder AddPlcItem(string name, S7PlcOptItem item)
        {
            if (!_maps.TryAdd(name, item))
            {
                throw new Exception("添加" + name + "失败！");
            }

            return this;
        }

        internal ConcurrentDictionary<string, S7PlcOptItem> BuildMaps()
        {
            return _maps;
        }
    }
}
