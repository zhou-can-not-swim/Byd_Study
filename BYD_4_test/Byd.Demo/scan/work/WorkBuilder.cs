using Byd.Demo.scan.Itminus.Middlewares;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Byd.Demo.scan.work
{
    public class WorkBuilder<TWorkContext> where TWorkContext : IWorkContext
    {
        private List<Func<WorkDelegate<TWorkContext>, WorkDelegate<TWorkContext>>> _middlewares = new List<Func<WorkDelegate<TWorkContext>, WorkDelegate<TWorkContext>>>();

        public WorkBuilder<TWorkContext> Use(Func<WorkDelegate<TWorkContext>, WorkDelegate<TWorkContext>> mw)
        {
            _middlewares.Add(mw);
            return this;
        }

        public WorkBuilder<TWorkContext> Use(Func<TWorkContext, Func<Task>, Task> mw)
        {
            return Use((next) => async delegate (TWorkContext context)
            {
                Func<Task> arg = () => next(context);
                await mw(context, arg);
            });
        }

        //将conetext给到下一个管道
        public WorkBuilder<TWorkContext> Use<TMiddleware>() where TMiddleware : IWorkMiddleware<TWorkContext>
        {
            return Use((next) => async delegate (TWorkContext context)
            {
                TMiddleware requiredService = context.ServiceProvider.GetRequiredService<TMiddleware>();
                if (requiredService == null)
                {
                    throw new NullReferenceException("无法获取" + typeof(TMiddleware).FullName + "实例!");
                }

                await requiredService.InvokeAsync(context, next);
            });
        }

        public WorkBuilder<TWorkContext> Run(Func<TWorkContext, Task> mw)
        {
            return Use((next) => async delegate (TWorkContext context)
            {
                await mw(context);
            });
        }

        public WorkBuilder<TWorkContext> MapWhen(Func<TWorkContext, Task<bool>> predicate, Func<TWorkContext, Task> mw)
        {
            return Use((next) => async delegate (TWorkContext context)
            {
                if (await predicate(context))
                {
                    await mw(context);
                }
            });
        }

        public WorkBuilder<TWorkContext> MapWhen(Func<TWorkContext, Task<bool>> predicate, Func<WorkDelegate<TWorkContext>, WorkDelegate<TWorkContext>> mw)
        {
            return Use((next) => async delegate (TWorkContext context)
            {
                if (await predicate(context))
                {
                    await mw(next)(context);
                }
            });
        }

        public WorkBuilder<TWorkContext> UseWhen(Func<TWorkContext, Task<bool>> predicate, Func<TWorkContext, Func<Task>, Task> mw)
        {
            return Use((next) => async delegate (TWorkContext context)
            {
                if (await predicate(context))
                {
                    Func<Task> arg = () => next(context);
                    await mw(context, arg);
                }
                else
                {
                    await next(context);
                }
            });
        }

        public WorkBuilder<TWorkContext> UseWhen(Func<TWorkContext, Task<bool>> predicate, Func<WorkDelegate<TWorkContext>, WorkDelegate<TWorkContext>> mw)
        {
            return Use((next) => async delegate (TWorkContext context)
            {
                if (await predicate(context))
                {
                    await mw(next)(context);
                }
                else
                {
                    await next(context);
                }
            });
        }

        public WorkDelegate<TWorkContext> Build()
        {
            WorkDelegate<TWorkContext> workDelegate = (context) => Task.CompletedTask;
            _middlewares.Reverse();
            foreach (Func<WorkDelegate<TWorkContext>, WorkDelegate<TWorkContext>> middleware in _middlewares)
            {
                workDelegate = middleware(workDelegate);
            }

            return workDelegate;
        }
    }
}
