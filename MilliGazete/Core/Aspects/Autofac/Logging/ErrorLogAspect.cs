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
    public class ErrorLogAspect : MethodInterception
    {
        LoggerServiceBase _loggerServiceBase;
        public ErrorLogAspect()
        {
            _loggerServiceBase = new LoggerServiceBase();
        }

        protected override void OnException(IInvocation invocation, Exception ex)
        {
            _loggerServiceBase.Error(GetErrorLogDetail(invocation, ex));
            _loggerServiceBase.Dispose();
        }

        private ErrorLogDetail GetErrorLogDetail(IInvocation invocation, Exception ex)
        {
            List<ErrorLogParameter> listErrors = new List<ErrorLogParameter>();
            while (ex != null)
            {
                listErrors.Add(new ErrorLogParameter()
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    HelpLink = ex.HelpLink,
                    HResult = ex.HResult
                });
                ex = ex.InnerException;
            }

            var logDetail = GetLogDetail(invocation);

            GlobalContext.Properties["className"] = logDetail.ClassName;
            GlobalContext.Properties["methodName"] = logDetail.MethodName;

            return new ErrorLogDetail()
            {
                UserId = logDetail.UserId,
                MethodName = logDetail.MethodName,
                LogParameters = logDetail.LogParameters,
                DateTime = logDetail.DateTime,
                ErrorDetails = listErrors
            };
        }
        private LogDetail GetLogDetail(IInvocation invocation)
        {
            IHttpContextAccessor _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                if (invocation.Arguments[i] != null)
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
                UserId = _httpContextAccessor.HttpContext == null || _httpContextAccessor.HttpContext.User == null ||
                _httpContextAccessor.HttpContext.User.GetClaimValue(ClaimTypes.NameIdentifier) == null ? (int?)null :
                _httpContextAccessor.HttpContext.User.GetClaimValue(ClaimTypes.NameIdentifier).ToInt32(),
                DateTime = DateTime.Now.ToString()
            };
            return logDetail;
        }


    }
}
