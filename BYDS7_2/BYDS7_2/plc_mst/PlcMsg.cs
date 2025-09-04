using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BYDS7_2.plc_mst
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class PlcMsg
    {
        public const int DB_INDEX = 13;
        public const int DB_OFFSET = 0;


        /// <summary>
        /// 相互确认的信息
        /// </summary>
        public PlcFlags_GeneralCmdWord GeneralCmdWord;

        [Flags]
        public enum PlcFlags_GeneralCmdWord : byte
        {
            None = 0,
            心跳响应 = 1 << 0,
            心跳请求 = 1 << 1,
            下发任务确认 = 1 << 2,
            完成任务请求 = 1 << 3,
            下发RFID校验确认 = 1 << 4,
            RFID校验结果请求 = 1 << 5,
            请求出库确认 = 1 << 6

        }

        public byte test1;


    }
}