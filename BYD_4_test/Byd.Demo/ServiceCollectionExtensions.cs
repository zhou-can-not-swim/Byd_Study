using Byd.Demo.Middlewares.PublishNotification;
using Byd.Protocols.QHStocker;
using Byd.Protocols.QHStocker.Middlewares;
using Microsoft.Extensions.DependencyInjection;

namespace BYD_4_test.plc
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// PLC相关服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddPlcServicesForLine5_QHStocker(this IServiceCollection services)
        {
            // background services & plc processor
            services.AddHostedService<PlcHostedService>();
            services.AddSingleton<ScanProcessor>();

            #region 中间件
            services.AddScoped<PublishNotificationMiddleware>();
            services.AddScoped<EntryArrivedMiddleware>();
            //services.AddScoped<RequestTaskMiddleware>();
            services.AddScoped<RequestOutTaskMiddleware>();
            services.AddScoped<SendTaskMiddleware>();
            services.AddScoped<FinishedTaskMiddleware>();
            services.AddScoped<HeartBeatMiddleware>();
            services.AddScoped<FlushPendingMiddleware>();
            #endregion

            return services;
        }
    }
}
