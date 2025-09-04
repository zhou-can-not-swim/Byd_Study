using Byd.Demo.scan.Itminus.Middlewares;
using Byd.Demo.scan.work;
using Microsoft.AspNetCore.Http;

namespace Byd.Protocols.QHStocker.Middlewares
{
    /// <summary>
    /// 完成任务
    /// </summary>
    public class FinishedTaskMiddleware : IWorkMiddleware<ScanContext>
    {

        public async Task InvokeAsync(ScanContext context, WorkDelegate<ScanContext> next)
        {
            try
            {
                //Console.WriteLine("FinishedTaskMiddleware");
            }
            finally
            {
                await next(context);
            }
        }
    }
}
