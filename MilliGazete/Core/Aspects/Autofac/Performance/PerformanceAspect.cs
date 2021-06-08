using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;
        private LoggerServiceBase _loggerServiceBase;
        private IHttpContextAccessor _httpContextAccessor;

        public PerformanceAspect(int interval = 5)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
            _loggerServiceBase = new LoggerServiceBase();
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine($"Performance:{invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
                _loggerServiceBase.Warn(GetLogDetail(invocation, _stopwatch.Elapsed.TotalSeconds));
                _loggerServiceBase.Dispose();
            }
            _stopwatch.Reset();
        }

        private PerformanceLogDetail GetLogDetail(IInvocation invocation, double totalSeconds)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter()
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i]?.Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i]?.GetType().Name
                });
            }
            var logDetail = new PerformanceLogDetail()
            {
                ClassName = invocation.TargetType.Name,
                MethodName = invocation.Method.Name,
                LogParameters = logParameters,
                UserId = _httpContextAccessor.HttpContext.User.GetClaimValue(ClaimTypes.NameIdentifier).ToInt32(),
                DateTime = DateTime.Now.ToString(),
                TotalSeconds = totalSeconds
            };

            GlobalContext.Properties["className"] = logDetail.ClassName;
            GlobalContext.Properties["methodName"] = logDetail.MethodName;

            return logDetail;
        }
    }
}
