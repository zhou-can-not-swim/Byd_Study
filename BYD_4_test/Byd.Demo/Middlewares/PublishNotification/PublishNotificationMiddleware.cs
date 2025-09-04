using Byd.Demo.plc_mst;
using Byd.Demo.scan.Itminus.Middlewares;
using Byd.Demo.scan.work;
using Byd.Protocols.QHStocker;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Byd.Demo.Middlewares.PublishNotification
{
    public class PublishNotificationMiddleware : IWorkMiddleware<ScanContext>
    {
        private readonly IMediator _mediator;
        private IMemoryCache _objCache;


        /// <summary>
        /// 向web端发送初始化数据,所以一开始就有mst心跳
        /// </summary>
        /// <param name="mediator"></param>
        public PublishNotificationMiddleware(IMediator mediator, IMemoryCache objCache)
        {
            this._mediator = mediator;
            this._objCache = objCache;

        }

        public async Task InvokeAsync(ScanContext context, WorkDelegate<ScanContext> next)
        {
            try
            {
                //Console.WriteLine("PublishNotificationMiddleware-->");
                var beated = GeneralHelper.CheckPlcHeartBeatSynced(context.PlcInfo, context.MstMsg);

                await this._mediator.Publish(new ScanContextNotification(context, beated));

            }
            finally
            {
                await next(context);
            }
        }
    }
}
