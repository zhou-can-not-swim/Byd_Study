using Byd.Demo.test;

namespace BYD_4_test.plc.service
{
    public static class S7PlcServiceCollectionExtensions
    {
        public static IServiceCollection AddS7PlcOptions(this IServiceCollection services, Action<S7PlcOptsBuilder> configure)
        {
            S7PlcOptsBuilder s7PlcOptsBuilder = new S7PlcOptsBuilder();
            configure?.Invoke(s7PlcOptsBuilder);
            foreach (KeyValuePair<string, S7PlcOptItem> mapping in s7PlcOptsBuilder.BuildMaps())
            {
                services.AddOptions<S7PlcOptItem>(mapping.Key).Configure(delegate (S7PlcOptItem item)
                {
                    item.IpAddr = mapping.Value.IpAddr;
                    item.Rack = mapping.Value.Rack;
                    item.Slot = mapping.Value.Slot;
                });
            }

            return services;
        }


        //遍历配置中的所有子节（每个子节代表一个PLC的配置）
        //为每个PLC配置创建一个命名选项（使用子节的Key作为名称）
        //将配置节绑定到S7PlcOptItem类型的选项上
        public static IServiceCollection AddS7PlcOptions(this IServiceCollection services, IConfiguration plcsConfig)
        {
            foreach (IConfigurationSection child in plcsConfig.GetChildren())
            {
                services.AddOptions<S7PlcOptItem>(child.Key).Bind(child);
            }

            return services;
        }
    }
}
