using Byd.Demo;
using Byd.Protocols;
using BYD_4_test.plc.service;

namespace BYD_4_test.plc
{

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Plc相关服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddPlcServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddS7PlcOptions(config);//将appsettings.json中的plc配置项添加到services中
            services.AddSingleton<PlcMgr>(); //创建一个单例PlcMgr，主要用来管理所有plc的读写操作
            services.AddPlcServicesForLine5_QHStocker();//添加所有plc服务的中间件-->跳转到QHStoker中的ServiceCollectionExtensions方法
            return services;
        }
        public static IServiceCollection AddScanOpts(this IServiceCollection services, IConfiguration canOptsConfig)
        {
            services.Configure<ScanOpts>(canOptsConfig); //将配置绑定到对象(plc设备) 在其他地方就可以通过IOptions<ScanOpts>来访问这些配置
            return services;
        }

    }
}
