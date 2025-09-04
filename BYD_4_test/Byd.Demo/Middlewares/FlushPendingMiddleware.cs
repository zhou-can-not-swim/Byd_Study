using Byd.Demo.scan.Itminus.Middlewares;
using Byd.Demo.scan.work;
using Microsoft.AspNetCore.Http;

namespace Byd.Protocols.QHStocker.Middlewares
{
    public class FlushPendingMiddleware : IWorkMiddleware<ScanContext>
    {

        public async Task InvokeAsync(ScanContext context, WorkDelegate<ScanContext> next)
        {
            try
            {
                //Console.WriteLine("FlushPendingMiddleware");
            }
            finally
            {
                await next(context);
            }
        }
    }

}
