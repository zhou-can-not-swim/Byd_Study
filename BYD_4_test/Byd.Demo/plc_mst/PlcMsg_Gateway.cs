using Byd.Demo.utils.attribute;
using System.Runtime.InteropServices;

namespace Byd.Protocols.QHStocker.Model
{

    /// <summary>
    /// plc一个库口的信号信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct PlcMsg_Gateway
    {
        /// <summary>
        /// plc 100.0
        /// </summary>
        public PlcMsg_GatewayFlags Plc信号灯;

        /// <summary>
        /// 检验结果plc 101
        /// </summary>
        public byte 检验结果;//0是初始化，1是校验通过，2是校验不通过
  
        /// <summary>
        /// 入库来料RFID 102
        /// </summary>
        [Endian(Endianness.BigEndian)]
        public short 入库来料RFID;
    }



    [Serializable]
    [Flags]
    public enum PlcMsg_GatewayFlags : byte
    {
        就位请求=1<<0,
        允许出库 = 1 << 1      
    }

}
