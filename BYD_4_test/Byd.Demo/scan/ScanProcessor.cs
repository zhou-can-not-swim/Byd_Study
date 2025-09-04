using Byd.Demo.Middlewares.PublishNotification;
using Byd.Demo.scan.Itminus.Middlewares;
using Byd.Demo.scan.work;
using Byd.Protocols.QHStocker.Middlewares;


namespace Byd.Protocols.QHStocker
{

    /// <summary>
    /// 处理器
    /// </summary>
    /// 

    public class ScanProcessor
    {
        private WorkDelegate<ScanContext> BuildContainer()
        {
            var container = new WorkBuilder<ScanContext>()
               .Use<PublishNotificationMiddleware>()
              .Use<EntryArrivedMiddleware>()//就位请求             
                                            //.Use<RequestTaskMiddleware>()//请求Rfid校验
                  .Use<RequestOutTaskMiddleware>()//请求出库
             .Use<SendTaskMiddleware>()//发送任务
              .Use<FinishedTaskMiddleware>()//任务完成
                .Use<HeartBeatMiddleware>()//心跳交互
              .Use<FlushPendingMiddleware>()//写plc
              .Build();

            return container;
        }

        public async Task HandleAsync(ScanContext ctx)
        {
            var workcontainer = this.BuildContainer();
            await workcontainer.Invoke(ctx);
        }
    }

}
