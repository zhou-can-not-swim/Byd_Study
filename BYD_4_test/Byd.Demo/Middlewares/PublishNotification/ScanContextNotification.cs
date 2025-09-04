using Byd.Protocols.QHStocker;
using MediatR;

namespace Byd.Demo.Middlewares.PublishNotification
{

    /// <summary>
    /// 这个类中包含 扫描上下文 plc心跳和 plc心跳同步时间等信息。
    /// </summary>
    public class ScanContextNotification : INotification
    {
        public ScanContextNotification(ScanContext ctx, bool plcBeatSynced)
        {
            this.Context = ctx ?? throw new System.ArgumentNullException(nameof(ctx));
            this.PlcHeartBeated = plcBeatSynced;
            if (plcBeatSynced)
            {
                this.PlcHeartBeatedAt = ctx.CreatedAt;
            }
        }

        public ScanContext Context { get; }

        /// <summary>
        /// PLC心跳是否同步？
        /// </summary>
        public bool PlcHeartBeated { get; }
        /// <summary>
        /// PLC心跳同步时间？
        /// </summary>
        public DateTime PlcHeartBeatedAt { get; }
    }
}
