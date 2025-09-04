using Byd.Protocols.QHStocker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Byd.Demo.plc_mst
{
    //plc的一个库口信息
    public class PlcInfo_Gateway
    {
        private readonly PlcMsg_Gateway _msg;

        public PlcInfo_Gateway(PlcMsg_Gateway msg)
        {
            this._msg = msg;
        }


        public bool StandByReq => this._msg.Plc信号灯.HasFlag(PlcMsg_GatewayFlags.就位请求);
        public bool OutStatus => this._msg.Plc信号灯.HasFlag(PlcMsg_GatewayFlags.允许出库);
        public int RequestTaskResult => this._msg.检验结果;
        public string EntryRFID => this._msg.入库来料RFID.ToString();
    }
}
