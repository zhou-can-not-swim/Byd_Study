using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Byd.Demo.plc_mst
{
    public static class GeneralHelper
    {
        /// <summary>
        /// 检查plc和mst是否同步
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="mst"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool CheckPlcHeartBeatSynced(PlcInfo plc, MstMsg mst)
        {
            if (plc is null)
            {
                throw new ArgumentNullException(nameof(plc));
            }

            if (mst is null)
            {
                throw new ArgumentNullException(nameof(mst));
            }
            // mst当前心跳请求 检查当前枚举值是否包含指定的标志
            var mstreq = mst.GeneralCmdWord.HasFlag(MstFlags_GeneralCmdWord.心跳请求);
            // plc当前心跳响应
            var plcack = plc.heartBeatAck;

            //比较mst的心跳请求和plc的心跳响应是否一致
            return mstreq == plcack;
        }
    }
}
