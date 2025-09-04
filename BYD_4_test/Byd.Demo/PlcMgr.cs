using Microsoft.Extensions.DependencyInjection;
using System;

namespace Byd.Protocols
{
    /// <summary>
    /// Plc 控制器 —— 单例
    /// </summary>
    public class PlcMgr
    {
        public PlcMgr(IServiceProvider sp)
        {
            //使用ActivatorUtilities工具类动态创建一个QHStocker.PlcCtrl类型的实例      //解析依赖项    // 传递给PlcCtrl
            PlcName_QHStocker = ActivatorUtilities.CreateInstance<QHStocker.PlcCtrl>(sp, PlcNames.PlcName_QHStocker);
        }

        public QHStocker.PlcCtrl PlcName_QHStocker { get; }

    }
}