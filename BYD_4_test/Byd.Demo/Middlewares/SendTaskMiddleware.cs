using Byd.Demo.scan.Itminus.Middlewares;
using Byd.Demo.scan.work;

namespace Byd.Protocols.QHStocker.Middlewares
{
    /// <summary>
    /// 发送任务扫描stockTask表
    /// </summary>
    public class SendTaskMiddleware : IWorkMiddleware<ScanContext>
    {

        public async Task InvokeAsync(ScanContext context, WorkDelegate<ScanContext> next)
        {
            try
            {
                //Console.WriteLine("SendTaskMiddleware");
            }
            finally
            {
                await next(context);
            }
        }
    }
}
