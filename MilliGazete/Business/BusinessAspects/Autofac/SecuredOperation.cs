using Business.Constants;
using Business.Managers.Abstract;
using Castle.DynamicProxy;
using Core.Exceptions;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Authentication;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;
        private IBaseService _baseService;
        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _baseService = ServiceTool.ServiceProvider.GetService<IBaseService>();
        }
        public SecuredOperation()
        {
            _roles = new string[] { "" };
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            if (roleClaims == null || roleClaims.Count == 0)
            {
                throw new AuthenticationException(Messages.AuthenticationDenied);
            }
            if (_baseService != null && _baseService.IsCurrentUserPassive)
            {
                throw new AuthenticationException(Messages.UserIsNotActive);
            }
            //if (roleClaims.Contains("Admin") || roleClaims.Contains("admin"))
            //{
            //    return;
            //}
            if (_roles == null || _roles.Length == 0 || (_roles.Length == 1 && _roles[0].Length <= 1))
            {
                return;
            }
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new AuthorizationException(Messages.AuthorizationDenied);
        }
    }
}
