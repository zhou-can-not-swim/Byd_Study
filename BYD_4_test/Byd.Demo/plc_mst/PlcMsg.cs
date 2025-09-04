using Byd.Demo.Common;
using Byd.Demo.utils.attribute;
using Byd.Protocols.QHStocker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Byd.Demo.plc_mst
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

        public byte 预留1;


        [Endian(Endianness.BigEndian)]
        public StockerStatus 堆垛机状态;

        [Endian(Endianness.BigEndian)]
        public StockerTrip 行程;

        [Endian(Endianness.BigEndian)]
        public StockerCargo 堆垛机是否载货;

        [Endian(Endianness.BigEndian)]
        public StockerAction 动作;

        [Endian(Endianness.BigEndian)]
        public ushort 堆垛机当前层号;

        [Endian(Endianness.BigEndian)]
        public ushort 堆垛机当前排号;

        [Endian(Endianness.BigEndian)]
        public ushort 堆垛机当前列号;

        [Endian(Endianness.BigEndian)]
        public ushort 执行包号;

        [Endian(Endianness.BigEndian)]
        public ushort 完成包号;

        [Endian(Endianness.BigEndian)]
        public ushort 下发任务校验结果;

        /// <summary>
        /// 报警不要，去掉
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 78)]
        public byte[] 预留2;

        /// <summary>
        /// 库口只有2个，A库口和B库口
        /// </summary>
        public PlcMsg_Gateway EC010_A库口; [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 16)] public byte[] 预留EC010_A库口;
        public PlcMsg_Gateway EC010_B库口; [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 16)] public byte[] 预留EC010_B库口;

    }


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
}