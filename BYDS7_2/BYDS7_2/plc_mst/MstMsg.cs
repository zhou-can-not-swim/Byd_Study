using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BYDS7_2.plc_mst
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class MstMsg
    {
        public MstMsg(MstMsg msg)
        {
            GeneralCmdWord = msg.GeneralCmdWord;
            预留1 = msg.预留1;
        }
        public MstMsg() { }

        public const int DB_INDEX = 11;
        public const int DB_OFFSET = 0;

        /// <summary>
        /// 相互确认的信息
        /// </summary>
        public MstFlags_GeneralCmdWord GeneralCmdWord;

        public byte 预留1;

        [Flags]
        public enum MstFlags_GeneralCmdWord : byte
        {
            None = 0,
            心跳请求 = 1 << 0,
            心跳响应 = 1 << 1,
            下发任务请求 = 1 << 2,
            完成任务确认 = 1 << 3,
            下发RFID校验请求 = 1 << 4,
            RFID校验结果确认 = 1 << 5,
            请求出库 = 1 << 6,
        }
    }
}
