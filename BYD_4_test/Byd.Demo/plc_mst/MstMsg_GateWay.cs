using Byd.Demo.utils.attribute;
using System;
using System.Runtime.InteropServices;

namespace Byd.Protocols.QHStocker.Model
{

    /// <summary>
    ///Msg一个库口的信号信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct MstMsg_GateWay
    {
        /// <summary>
        /// mst 100.0
        /// </summary>
        public MstMsg_GatewayFlags Mst信号灯;
        public byte 预留;
        
        /// <summary>
        ///  mst 102B
        /// </summary>
        [Endian(Endianness.BigEndian)]
        public short 请求出库RFID;
    }


    [Serializable]
    [Flags]
    public enum MstMsg_GatewayFlags : byte
    {
        就位确认 = 1 << 0
    }
}
