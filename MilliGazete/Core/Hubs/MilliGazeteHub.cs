using Core.Extensions;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Hubs
{
    public class HubObject
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public bool isCompany { get; set; }
        public bool isTransporter { get; set; }
        public string FullName { get; set; }
        public string ConnectionId { get; set; }
        public DateTime ConnectedTime { get; set; }
    }
    public class MilliGazeteHub : Hub
    {
        private static ConcurrentDictionary<int, HubObject> _Connections = new ConcurrentDictionary<int, HubObject>();

        public static List<string> HubErrors = new List<string>();

        public static List<HubObject> GetConnectionList()
        {
            return _Connections.Select(u => u.Value).ToList();
        }
        public static List<string> GetConnectionIdListByCompanyId(int companyId)
        {
            return _Connections.Where(u => u.Value.CompanyId == companyId).Select(u => u.Value.ConnectionId).ToList();
        }
        public static List<string> GetConnectionIdListByUserId(int userId)
        {
            return _Connections.Where(u => u.Value.UserId == userId).Select(u => u.Value.ConnectionId).ToList();
        }

        public override async Task OnConnectedAsync()
        {
            try
            {

                var roles = Context.User.GetClaimValue(ClaimTypes.Role);
                bool isCompany = string.IsNullOrEmpty(roles) ? false : roles.Contains("Company");
                bool isTransporter = string.IsNullOrEmpty(roles) ? false : roles.Contains("Transporter");

                if (Context?.User?.Claims == null) return;
                HubObject hubObject = new HubObject()
                {
                    UserId = Context.User.GetClaimValue(ClaimTypes.NameIdentifier).ToInt32(),
                    CompanyId = Context.User.GetClaimValue("companyid").ToInt32(),
                    isCompany = isCompany,
                    isTransporter = isTransporter,
                    FullName = Context.User.GetClaimValue(ClaimTypes.Name),
                    ConnectionId = Context.ConnectionId,
                    ConnectedTime = DateTime.Now,
                };
                if (hubObject.UserId == 0) return;
                _Connections.AddOrUpdate(hubObject.UserId, hubObject, (oldValue, newValue) => hubObject);
                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                HubErrors.Add(ex.Message + ", " + ex.InnerException != null ? ex.InnerException.Message : "");
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                if (_Connections == null || !_Connections.Any() || Context == null) return;
                var connection = _Connections.FirstOrDefault(u => u.Value.ConnectionId == Context.ConnectionId);
                if (connection.Value == null) return;
                _Connections.TryRemove(connection.Key, out _);
                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception ex)
            {
                HubErrors.Add(ex.Message + ", " + ex.InnerException != null ? ex.InnerException.Message : "");
                if (exception != null) HubErrors.Add(exception.Message);
            }
        }

    }
}
