using Byd.Demo.scan.Itminus.Middlewares;
using Byd.Demo.scan.work;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Byd.Protocols.QHStocker.Middlewares
{
    /// <summary>
    /// 请求发送出库任务,要校验RFID,
    /// 接收到校验结果
    /// </summary>
    public class RequestTaskMiddleware : IWorkMiddleware<ScanContext>
    {

        public async Task InvokeAsync(ScanContext context, WorkDelegate<ScanContext> next)
        {
            try
            {
                //Console.WriteLine("RequestTaskMiddleware");
            }
            finally
            {
                await next(context);
            }
        }
    }
}
