using Byd.Demo.plc_mst;
using Byd.Demo.scan.work;
using Newtonsoft.Json;

namespace Byd.Protocols.QHStocker
{

    /// <summary>
    /// 上下文,包含了很多的东西，包括
    /// 依赖的服务
    /// plc信息
    /// mst信息
    /// 创建时间
    /// 还可以通过pedding写入待修改的plcMst部分
    /// </summary>
    public class ScanContext : IWorkContext
    {
        public ScanContext(IServiceProvider sp, PlcMsg plcmsg, MstMsg mstmsg, DateTime createdAt)
        {
            this.ServiceProvider = sp;
            this.PlcMsg = plcmsg;
            this.PlcInfo = new PlcInfo(plcmsg);

            this.MstMsg = mstmsg;
            this.MstInfo = new MstInfo(mstmsg);
            this.Pending = new MstMsg(mstmsg);
            this.CreatedAt = createdAt;
        }

        /// <summary>
        /// 只读属性
        /// </summary>
        [JsonIgnore]
        public PlcMsg PlcMsg { get; }
        /// <summary>
        /// 只读属性
        /// </summary>
        public PlcInfo PlcInfo { get; }
        /// <summary>
        /// 只读属性
        /// </summary>
        public MstInfo MstInfo { get; }

        /// <summary>
        /// 只读属性（获取plcMst部分的值）
        /// </summary>
        public MstMsg MstMsg { get; }

        /// <summary>
        /// 只读属性（这个是上位机需要修改plcMst部分的值）
        /// 在FlushPendingMiddleware 里在Pending的值写给Plc
        /// </summary>
        [JsonIgnore]
        public MstMsg Pending { get; }

        /// <summary>
        /// 只读属性，在programe.cs中注册的服务都可以在ServiceProvider中获取
        /// </summary>
        [JsonIgnore]
        public IServiceProvider ServiceProvider { get; }

        public DateTime CreatedAt {get;}
    }

}
