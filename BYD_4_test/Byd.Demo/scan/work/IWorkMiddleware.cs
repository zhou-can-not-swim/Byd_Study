using Byd.Demo.scan.Itminus.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Byd.Demo.scan.work
{
    public interface IWorkMiddleware<TWorkConext> where TWorkConext : IWorkContext
    {
        Task InvokeAsync(TWorkConext context, WorkDelegate<TWorkConext> next);
    }
}
