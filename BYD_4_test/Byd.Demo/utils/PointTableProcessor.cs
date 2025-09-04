using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Byd.Demo.utils
{
    public class PointTableProcessor
    {
        public class PointDefinition
        {
            public int ByteOffset { get; set; }
            public int BitOffset { get; set; }
            public string Name { get; set; }
            public string DataType { get; set; }
            public string Description { get; set; }
            public int Length { get; set; } = 1; // 默认长度为1
        }

        private static readonly List<PointDefinition> PointDefinitions = new List<PointDefinition>
    {
        // 控制字部分
        new PointDefinition { ByteOffset = 0, BitOffset = 0, Name = "心跳响应", DataType = "bit", Description = "控制字" },
        new PointDefinition { ByteOffset = 0, BitOffset = 1, Name = "心跳请求", DataType = "bit", Description = "控制字" },
        new PointDefinition { ByteOffset = 0, BitOffset = 2, Name = "下发任务确认", DataType = "bit" },
        new PointDefinition { ByteOffset = 0, BitOffset = 3, Name = "完成任务请求", DataType = "bit" },
        new PointDefinition { ByteOffset = 0, BitOffset = 4, Name = "下发RFID校验确认", DataType = "bit" },
        new PointDefinition { ByteOffset = 0, BitOffset = 5, Name = "RFID校验结果请求", DataType = "bit", Description = "PLC接收到线体校验结果后使用，当校验结果确认为1时，此时该点位置0，当置0的时候把校验结果内容清除" },
        new PointDefinition { ByteOffset = 0, BitOffset = 6, Name = "请求出库确认", DataType = "bit", Description = "为1的时候，请求出库为0，并且把请求的库口上的请求出库RFID清空" },
        new PointDefinition { ByteOffset = 0, BitOffset = 7, Name = "预留", DataType = "bit" },
        
        // 其他数据部分
        new PointDefinition { ByteOffset = 1, Name = "预留", DataType = "byte", Length = 1 },
        new PointDefinition { ByteOffset = 2, Name = "堆垛机的状态", DataType = "UInt", Description = "0:关 1:维修 2:手动 3:半自动 4:自动 5:联机（由上位机控制）" },
        new PointDefinition { ByteOffset = 4, Name = "行程", DataType = "UInt", Description = "0：无，1待机, 2是运行 ,3是异常（异常内容写在堆垛机故障代码里）" },
        new PointDefinition { ByteOffset = 6, Name = "堆垛机是否载货", DataType = "UInt", Description = "0：无，1表示无货, 2表示有货（上位机下发任务条件：联机+待机+无货+下发任务请求和确认都为0+下发任务校验结果为0）" },
        new PointDefinition { ByteOffset = 8, Name = "动作", DataType = "UInt", Description = "1前进，2后退，3上升，4下降，5左放货，6左取货。7右放货，8右取货" },
        new PointDefinition { ByteOffset = 10, Name = "堆垛机当前层号", DataType = "UInt" },
        new PointDefinition { ByteOffset = 12, Name = "堆垛机当前排号", DataType = "UInt" },
        new PointDefinition { ByteOffset = 14, Name = "堆垛机当前列号", DataType = "UInt" },
        new PointDefinition { ByteOffset = 16, Name = "执行包号", DataType = "UInt" },
        new PointDefinition { ByteOffset = 18, Name = "完成包号", DataType = "UInt" },
        new PointDefinition { ByteOffset = 20, Name = "下发任务校验结果", DataType = "UInt", Description = "校验失败需要堆垛机行程该为异常，0是表示初始化，1是正常，2不正常[当值为1，mst校验号不为0，下发任务包号不为0时，清除下发任务包号和校验号]" },
        new PointDefinition { ByteOffset = 22, Name = "报警1", DataType = "Word" },
        new PointDefinition { ByteOffset = 24, Name = "报警2", DataType = "Word" },
        new PointDefinition { ByteOffset = 26, Name = "报警3", DataType = "Word" },
        new PointDefinition { ByteOffset = 28, Name = "报警4", DataType = "Word" },
        new PointDefinition { ByteOffset = 30, Name = "报警5", DataType = "Word" },
        new PointDefinition { ByteOffset = 32, Name = "预留", DataType = "byte", Length = 68 },
        
        // 维修口部分
        new PointDefinition { ByteOffset = 100, BitOffset = 0, Name = "就位请求", DataType = "bit", Description = "维修口【就位请求=1 and 就位确认=0 and 入库来料RFID不为空 上位机才会做入库任务】" },
        new PointDefinition { ByteOffset = 100, BitOffset = 1, Name = "允许出库", DataType = "bit" },
        new PointDefinition { ByteOffset = 100, BitOffset = 2, Name = "预留", DataType = "bit", Length = 6 },
        new PointDefinition { ByteOffset = 101, Name = "检验结果", DataType = "byte", Description = "0是初始化，1是校验通过，2是校验不通过，当0.5的位置RFID校验结果请求由1变成0的时候清校验结果内容" },
        new PointDefinition { ByteOffset = 102, Name = "入库来料RFID", DataType = "int", Description = "与就位请求同时使用" },
        new PointDefinition { ByteOffset = 104, Name = "预留", DataType = "byte", Length = 16 },
        
        // BSR010_020部分
        new PointDefinition { ByteOffset = 120, BitOffset = 0, Name = "就位请求", DataType = "bit", Description = "BSR010_020" },
        new PointDefinition { ByteOffset = 120, BitOffset = 1, Name = "允许出库", DataType = "bit" },
        new PointDefinition { ByteOffset = 120, BitOffset = 2, Name = "预留", DataType = "bit", Length = 6 },
        new PointDefinition { ByteOffset = 121, Name = "检验结果", DataType = "byte" },
        new PointDefinition { ByteOffset = 122, Name = "入库来料RFID", DataType = "int" },
        new PointDefinition { ByteOffset = 124, Name = "预留", DataType = "byte", Length = 16 }
    };

        public static string ProcessBuffer(byte[] buffer)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var point in PointDefinitions)
            {
                try
                {
                    if (point.DataType == "bit")
                    {
                        // 处理位数据
                        if (point.ByteOffset < buffer.Length)
                        {
                            byte value = buffer[point.ByteOffset];
                            bool bitValue = ((value >> point.BitOffset) & 0x01) == 0x01;

                            sb.AppendLine($"字节偏移:{point.ByteOffset}, 位偏移:{point.BitOffset}, 名称:{point.Name}, 值:{bitValue}, 类型:{point.DataType}, 描述:{point.Description}");
                        }
                    }
                    else
                    {
                        // 处理字节数据
                        int dataSize = GetDataTypeSize(point.DataType);
                        if (point.ByteOffset + dataSize <= buffer.Length)
                        {
                            object value = ReadData(buffer, point.ByteOffset, point.DataType);

                            sb.AppendLine($"字节偏移:{point.ByteOffset}, 名称:{point.Name}, 值:{value}, 类型:{point.DataType}, 描述:{point.Description}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    sb.AppendLine($"处理{point.Name}时出错: {ex.Message}");
                }
            }

            return sb.ToString();
        }

        private static int GetDataTypeSize(string dataType)
        {
            switch (dataType.ToLower())
            {
                case "byte": return 1;
                case "word": return 2;
                case "uint": return 2;
                case "int": return 2;
                default: return 1;
            }
        }

        private static object ReadData(byte[] buffer, int offset, string dataType)
        {
            switch (dataType.ToLower())
            {
                case "byte":
                    return buffer[offset];
                case "word":
                case "uint":
                    return BitConverter.ToUInt16(buffer, offset);
                case "int":
                    return BitConverter.ToInt16(buffer, offset);
                default:
                    return buffer[offset];
            }
        }
    }
}
