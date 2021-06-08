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
using System.Security.Claims;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        LoggerServiceBase _loggerServiceBase;
        public LogAspect()
        {
            _loggerServiceBase = new LoggerServiceBase();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _loggerServiceBase.Info(GetLogDetail(invocation));
            _loggerServiceBase.Dispose();
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            IHttpContextAccessor _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                if (invocation.Arguments[i] == null) continue;
                logParameters.Add(new LogParameter()
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }
            var logDetail = new LogDetail()
            {
                ClassName = invocation.TargetType.Name,
                MethodName = invocation.Method.Name,
                LogParameters = logParameters,
                UserId = _httpContextAccessor.HttpContext.User.GetClaimValue(ClaimTypes.NameIdentifier).ToInt32(),
                DateTime = DateTime.Now.ToString()
            };

            GlobalContext.Properties["className"] = logDetail.ClassName;
            GlobalContext.Properties["methodName"] = logDetail.MethodName;

            return logDetail;
        }


    }
}
