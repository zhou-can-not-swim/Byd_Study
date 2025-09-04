using Byd.Protocols.QHStocker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Byd.Demo.plc_mst
{
    //管理自动化仓储系统中的标志位（flags）和位置信息
    public class MstFlagsGeneralBuilder
    {
        //上位机 在页面可能看到
        private MstFlags_GeneralCmdWord _mstFlags;

        public MstFlagsGeneralBuilder(MstFlags_GeneralCmdWord mstFlags)
        {
            this._mstFlags = mstFlags;
        }

        /// <summary>
        /// helper
        /// </summary>
        /// <param name="bitIndicator"></param>
        /// <param name="onoff">ture的时候置1，false置0</param>
        /// <returns></returns>
        /// 
        //对标志位进行 01操作
        private MstFlagsGeneralBuilder SetOnOff(MstFlags_GeneralCmdWord bitIndicator, bool onoff)
        {
            this._mstFlags = onoff ?
                this._mstFlags | bitIndicator :
                this._mstFlags & ~bitIndicator;
            return this;
        }

        /// <summary>
        /// 构建上料标志
        /// </summary>
        /// <returns></returns>
        public MstFlags_GeneralCmdWord Build() => this._mstFlags;


        public MstFlagsGeneralBuilder 下发任务请求(bool onff)
        {
            this.SetOnOff(MstFlags_GeneralCmdWord.下发任务请求, onff);
            return this;
        }
        public MstFlagsGeneralBuilder 完成任务确认(bool onff)
        {
            this.SetOnOff(MstFlags_GeneralCmdWord.完成任务确认, onff);
            return this;
        }
        public MstFlagsGeneralBuilder 下发RFID校验请求(bool onff)
        {
            this.SetOnOff(MstFlags_GeneralCmdWord.下发RFID校验请求, onff);
            return this;
        }
        public MstFlagsGeneralBuilder RFID校验结果确认(bool onff)
        {
            this.SetOnOff(MstFlags_GeneralCmdWord.RFID校验结果确认, onff);
            return this;
        }

        public MstFlagsGeneralBuilder 下发请求出库(bool onff)
        {
            this.SetOnOff(MstFlags_GeneralCmdWord.请求出库, onff);
            return this;
        }

        /// <summary>
        /// 设置心跳确认位开/关
        /// </summary>
        /// <param name="onoff"></param>
        /// <returns></returns>
        public MstFlagsGeneralBuilder 响应PLC心跳请求(bool onoff) => this.SetOnOff(MstFlags_GeneralCmdWord.心跳响应, onoff);
        /// <summary>
        /// 设置心跳请求位开/关
        /// </summary>
        /// <param name="onoff"></param>
        /// <returns></returns>
        public MstFlagsGeneralBuilder 下发心跳请求(bool onoff) => this.SetOnOff(MstFlags_GeneralCmdWord.心跳请求, onoff);

    }


    public class MstFlagsGatewayBuilder
    {
        private MstMsg_GatewayFlags _mstGateway;

        public MstFlagsGatewayBuilder(MstMsg_GatewayFlags mstGateway)
        {
            this._mstGateway = mstGateway;
        }
        /// <summary>
        /// 构建命令字
        /// </summary>
        /// <returns></returns>
        public MstMsg_GatewayFlags Build() => this._mstGateway;

        /// <summary>
        /// helper
        /// </summary>
        /// <param name="bitIndicator"></param>
        /// <param name="onoff">ture的时候置1，false置0</param>
        /// <returns></returns>
        private MstFlagsGatewayBuilder SetOnOff(MstMsg_GatewayFlags bitIndicator, bool onoff)
        {
            this._mstGateway = onoff ?
                this._mstGateway | bitIndicator :
                this._mstGateway & ~bitIndicator;
            return this;
        }

        public MstFlagsGatewayBuilder 入口就位确认(bool onoff)
        {

            this.SetOnOff(MstMsg_GatewayFlags.就位确认, onoff);
            return this;
        }

    }

    /// <summary>
    /// 地板库入库口
    /// </summary>
    public enum 地板_EntryLocation
    {
        EC010_A工位入口 = 1,
        EC010_B工位入口 = 2,

    }

    /// <summary>
    /// 地板库出库口
    /// </summary>
    public enum 地板_OutLocation
    {
        EC010_A工位出口 = 32,
        EC010_B工位出口 = 33,
    }
}

