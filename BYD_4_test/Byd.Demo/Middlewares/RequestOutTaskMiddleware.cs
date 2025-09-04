using Byd.Demo.scan.Itminus.Middlewares;
using Byd.Demo.scan.work;
using Microsoft.AspNetCore.Http;

namespace Byd.Protocols.QHStocker.Middlewares
{
    /// <summary>
    /// 请求发送出库任务
    /// </summary>
    public class RequestOutTaskMiddleware : IWorkMiddleware<ScanContext>
    {
        public async Task InvokeAsync(ScanContext context, WorkDelegate<ScanContext> next)
        {
            try
            {
                //Console.WriteLine("RequestOutTaskMiddleware");
            }
            finally
            {
                await next(context);
            }
        }
    }
}
