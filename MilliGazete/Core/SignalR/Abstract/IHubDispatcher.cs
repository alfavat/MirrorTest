using System.Threading.Tasks;

namespace Core.SignalR.Abstract
{
    public interface IHubDispatcher
    {
        Task LiveOrdersUpdated(int companyId);
    }
}
