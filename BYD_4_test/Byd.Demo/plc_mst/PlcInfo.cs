using Byd.Demo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Byd.Demo.plc_mst
{
    public class PlcInfo
    {
        private readonly PlcMsg msg;

        /// 封装一个PlsMsg
        internal PlcInfo(PlcMsg msg)
        {
            this.msg = msg;
        }

        public bool heartBeatAck => msg.GeneralCmdWord.HasFlag(PlcFlags_GeneralCmdWord.心跳响应);

        public bool heartBeatReq => msg.GeneralCmdWord.HasFlag(PlcFlags_GeneralCmdWord.心跳请求);

        public bool SendTaskAck => msg.GeneralCmdWord.HasFlag(PlcFlags_GeneralCmdWord.下发任务确认);

        public bool FinishTaskReq => msg.GeneralCmdWord.HasFlag(PlcFlags_GeneralCmdWord.完成任务请求);
        public bool RequestTaskAck => msg.GeneralCmdWord.HasFlag(PlcFlags_GeneralCmdWord.下发RFID校验确认);
        public bool RequestTaskResultReq => msg.GeneralCmdWord.HasFlag(PlcFlags_GeneralCmdWord.RFID校验结果请求);
        public bool RequestOutTaskAck => msg.GeneralCmdWord.HasFlag(PlcFlags_GeneralCmdWord.请求出库确认);


        public StockerStatus StorkerStatus => msg.堆垛机状态;
        public StockerTrip StockerTrip => msg.行程;
        public StockerCargo StockerCargo => msg.堆垛机是否载货;
        public StockerAction StockerAction => msg.动作;
        public int CurrentFloor => (int)msg.堆垛机当前层号;
        public int CurrentLine => (int)msg.堆垛机当前排号;
        public int CurrentColumn => (int)msg.堆垛机当前列号;
        public int DoTaskNo => (int)msg.执行包号;
        public int TaskNo => (int)msg.完成包号;
        public int VerificationCode => (int)msg.下发任务校验结果;

        public PlcInfo_Gateway EC010_A库口 => new PlcInfo_Gateway(msg.EC010_A库口);
        public PlcInfo_Gateway EC010_B库口 => new PlcInfo_Gateway(msg.EC010_B库口);

    }
}
