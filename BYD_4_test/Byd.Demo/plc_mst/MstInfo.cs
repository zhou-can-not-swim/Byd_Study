using Byd.Demo.Common;
using static Byd.Demo.plc_mst.MstMsg;

namespace Byd.Demo.plc_mst
{
    public class MstInfo
    {
        private readonly MstMsg msg;

        /// 封装一个PlsMsg
        internal MstInfo(MstMsg msg)
        {
            this.msg = msg;
        }

        public bool heartBeatReq => msg.GeneralCmdWord.HasFlag(MstFlags_GeneralCmdWord.心跳请求);
        public bool heartBeatAck => msg.GeneralCmdWord.HasFlag(MstFlags_GeneralCmdWord.心跳响应);

        public bool SendTaskReq => msg.GeneralCmdWord.HasFlag(MstFlags_GeneralCmdWord.下发任务请求);

        public bool FinishTaskAck => msg.GeneralCmdWord.HasFlag(MstFlags_GeneralCmdWord.完成任务确认);
        public bool RequsetTaskReq => msg.GeneralCmdWord.HasFlag(MstFlags_GeneralCmdWord.下发RFID校验请求);
        public bool RequsetTaskResultAck => msg.GeneralCmdWord.HasFlag(MstFlags_GeneralCmdWord.RFID校验结果确认);
        public bool RequsetOutTaskReq => msg.GeneralCmdWord.HasFlag(MstFlags_GeneralCmdWord.请求出库);


        /// <summary>
        /// 任务号 mst2
        /// </summary>
        public int TaskNo => (int)msg.Core.TaskNo;
        /// <summary>
        /// 命令代码 mst4
        /// </summary>
        public PLCTaskType TaskType => msg.Core.TaskType;

        /// <summary>
        /// 源排号 mst6
        /// </summary>
        public int StartLine => (int)msg.Core.StartLine;
        /// <summary>
        /// 源层号 mst8
        /// </summary>
        public int StartFloor => (int)msg.Core.StartFloor;
        /// <summary>
        /// 源列号 mst10
        /// </summary>
        public int StartColumn => (int)msg.Core.StartColumn;

        /// <summary>
        /// 目的排号 mst12
        /// </summary>
        public int EndLine => (int)msg.Core.EndLine;
        /// <summary>
        /// 目的层号 mst14
        /// </summary>
        public int EndFloor => (int)msg.Core.EndFloor;
        /// <summary>
        /// 目的列号 mst16
        /// </summary>
        public int EndColumn => (int)msg.Core.EndColumn;
        /// <summary>
        /// 校验号 mst18
        /// </summary>
        public int VerificationCode => (int)msg.Core.VerificationCode;

        /// <summary>
        /// 下发任务RIFD mst22
        /// </summary>
        public short TaskRFID => msg.Core.TaskRFID;



    }
}
