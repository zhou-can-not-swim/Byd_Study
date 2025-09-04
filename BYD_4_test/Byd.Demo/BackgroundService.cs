using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Byd.Protocols.QHStocker
{
    public class PlcHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _ssf;
        private readonly IMediator _mediator;
        private readonly PlcMgr _plcmgr;

        public PlcHostedService(IServiceScopeFactory ssf, 
                                IMediator mediator,
                                PlcMgr plcmgr//控制plc的读写操作
        )
        {
            _ssf = ssf;
            _mediator = mediator;
            _plcmgr = plcmgr;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var thread = new Thread(() => { _ = RunAsync(stoppingToken); });
            thread.Start();
            return Task.CompletedTask;//这一步完成打开网页
        }

        private async Task RunAsync(CancellationToken ct)//不停进入这一步扫描middleware
        {
            var DiscountConnectedCount = 0;
            var PlcStatus = "";
            //只要页面不关闭（或者更准确地说，只要托管该服务的应用程序没有停止或取消该服务）
            //这个循环就会持续运行。
            while (!ct.IsCancellationRequested)
            {
                try
                {
                    //确保plc已连接，如果未连接会抛异常，进行下一次尝试连接
                    await _plcmgr.PlcName_QHStocker.EnsureConnectedAsync();
                    DiscountConnectedCount = 0;

                    //读plc给上位机部分
                    var plcOp = await _plcmgr.PlcName_QHStocker.ReadPlcMsgAsync();//13

                    //读上位机写的部分
                    var mstOp = await _plcmgr.PlcName_QHStocker.ReadMstMsgAsync();//11

                    if (plcOp.IsError)
                    {
                        throw new Exception(plcOp.ErrorValue);
                    }
                    if (mstOp.IsError)
                    {
                        throw new Exception(plcOp.ErrorValue);
                    }
                    var dt = DateTime.Now;

                    var plcmsg = plcOp.ResultValue;//plc信息
                    var mstmsg = mstOp.ResultValue;//msg信息

                    using var scope = _ssf.CreateScope();
                    var serviceprovider = scope.ServiceProvider;
                    //获取处理器
                    var processor = serviceprovider.GetRequiredService<ScanProcessor>();
                    //构建上下文
                    var context = new ScanContext(serviceprovider, plcmsg, mstmsg, dt);


                    //处理器处理上下文
                    await processor.HandleAsync(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error-->BackgroundService-->",ex.Message);
                }
            }

        }
    }
}