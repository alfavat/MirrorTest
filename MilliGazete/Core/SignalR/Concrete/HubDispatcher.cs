using Core.Hubs;
using Core.SignalR.Abstract;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SignalR.Concrete
{
    public class HubDispatcher : IHubDispatcher
    {
        private readonly IHubContext<MilliGazeteHub> _hubContext;
        public HubDispatcher(IHubContext<MilliGazeteHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public List<string> GetCompanyConnectionIds(int companyId)
        {
            var list = MilliGazeteHub.GetConnectionIdListByCompanyId(companyId);
            if (list != null && list.Any())
            {
                return list;
            }
            return null;
        }
        public async Task LiveOrdersUpdated(int companyId)
        {
            var connectionIds = GetCompanyConnectionIds(companyId);
            if (connectionIds != null)
                await _hubContext.Clients.Clients(connectionIds).SendAsync("LiveOrdersUpdated");
        }

    }
}