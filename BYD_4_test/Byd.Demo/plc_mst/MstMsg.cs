using Byd.Demo.Common;
using Byd.Demo.utils.attribute;
using Byd.Protocols.QHStocker.Model;
using System.Runtime.InteropServices;

namespace Byd.Demo.plc_mst
{
    /// <summary>
    /// 放点表mst的内容，主要用于内存映射
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class MstMsg
    {
        public MstMsg(MstMsg msg)
        {
            GeneralCmdWord = msg.GeneralCmdWord;
            预留1 = msg.预留1;
            this.Core = msg.Core;
        }
        public MstMsg(){}

        public const int DB_INDEX = 11;
        public const int DB_OFFSET = 0;

        /// <summary>
        /// 相互确认的信息
        /// </summary>
        public MstFlags_GeneralCmdWord GeneralCmdWord;

        public byte 预留1;

        /// <summary>
        /// 下任务内容2B-22B
        /// </summary>
        public MstMsg_Core Core;

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 76)]
        public byte[] 预留2;

        /// <summary>
        /// 所有库口信息
        /// </summary>
        public MstMsgGateWay GateWay;



    }

    /// <summary>
    /// 用于前期的相互确认的信息，比如说心跳响应请求啥的
    /// </summary>
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

    /// <summary>
    /// 争对mst 2B -22B 的下发任务内容，比如下发任务包
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class MstMsg_Core
    {

        public MstMsg_Core(MstMsg_Core core)
        {
            this.TaskNo = core.TaskNo;
            this.TaskType = core.TaskType;
            this.StartLine = core.StartLine;
            this.StartFloor = core.StartFloor;
            this.StartColumn = core.StartColumn;
            this.EndLine = core.EndLine;
            this.EndFloor = core.EndFloor;
            this.EndColumn = core.EndColumn;
            this.VerificationCode = core.VerificationCode;
            this.TaskRFID = core.TaskRFID;
        }

        /// <summary>
        /// 下发任务包号 2 -- 22
        /// </summary>
        [Endian(Endianness.BigEndian)]
        public ushort TaskNo;
        /// <summary>
        /// 命令代码
        /// </summary>
        [Endian(Endianness.BigEndian)]
        public PLCTaskType TaskType;

        /// <summary>
        /// 源排号
        /// </summary>
        [Endian(Endianness.BigEndian)]
        public ushort StartLine;
        /// <summary>
        /// 源层号
        /// </summary>
        [Endian(Endianness.BigEndian)]
        public ushort StartFloor;
        /// <summary>
        /// 源列号
        /// </summary>
        [Endian(Endianness.BigEndian)]
        public ushort StartColumn;

        /// <summary>
        /// 目的排号
        /// </summary>
        [Endian(Endianness.BigEndian)]
        public ushort EndLine;
        /// <summary>
        /// 目的层号
        /// </summary>
        [Endian(Endianness.BigEndian)]
        public ushort EndFloor;
        /// <summary>
        /// 目的列号
        /// </summary>
        [Endian(Endianness.BigEndian)]
        public ushort EndColumn;
        /// <summary>
        /// 校验号
        /// </summary>
        [Endian(Endianness.BigEndian)]
        public int VerificationCode;
        /// <summary>
        ///下发任务rfid 
        /// </summary>
        [Endian(Endianness.BigEndian)]
        public short TaskRFID;



    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class MstMsgGateWay
    {
        public MstMsg_GateWay EC010_A库口; [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 16)] public byte[] 预留EC010_A库口;
        public MstMsg_GateWay EC010_B库口; [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 16)] public byte[] 预留EC010_B库口;
    }

}
