using Byd.Protocols.QHStocker;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Byd.Demo.Hub
{
    public interface IProductionHub
    {
        //一个方法声明，表示客户端可以调用这个方法来发送消息
        Task receiveQHStockerMsg(ScanContext context);

    }


    public class ProductionHub : Hub<IProductionHub>
    {
        private readonly IServiceProvider _sp;

        public ProductionHub(IServiceProvider sp)
        {
            this._sp = sp;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("与客户端断开连接");
            return base.OnDisconnectedAsync(exception);
        }


    }
}
