using Business.Managers.Abstract;
using Core.Extensions;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Business.Managers.Concrete
{
    public class BaseManager : IBaseService
    {
        private IHttpContextAccessor _httpContextAccessor;
        public bool IsEmployee
        {
            get
            {
                _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
                return _httpContextAccessor.HttpContext.User.GetClaimValue("isemployee").ToBool();
            }
        }
        public int RequestUserId
        {
            get
            {
                _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
                var nameIdentifier = _httpContextAccessor?.HttpContext?.User?.GetClaimValue(ClaimTypes.NameIdentifier);
                return nameIdentifier.StringIsNullOrEmpty() ? 0 : nameIdentifier.ToInt32();
            }
        }

        public string UserIpAddress
        {
            get
            {
                _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
                return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
        }

        public static List<int> PassiveUsers = new List<int>();
        public void FillPassiveUserList(List<int> users)
        {
            try
            {
                PassiveUsers.AddRange(users);
            }
            catch (System.Exception ec)
            {
            }
        }

        public List<int> GetPassiveUserList => PassiveUsers;

        public void UpdatePassiveUserList(int userId, bool active)
        {
            try
            {
                if (!active) // add to passive list
                {
                    if (!PassiveUsers.Any(f => f == userId))
                    {
                        PassiveUsers.Add(userId);
                    }
                }
                else // remove from list
                {
                    if (PassiveUsers.Any(f => f == userId))
                    {
                        PassiveUsers.Remove(userId);
                    }
                }
            }
            catch (System.Exception ec)
            {
            }
        }
        public bool IsCurrentUserPassive
        {
            get
            {
                return PassiveUsers.Any(f => f == RequestUserId);
            }
        }
    }
}