using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface ISubscriptionAssistantService
    {
        Task<Subscription> GetById(int subscriptionId);
        Task<List<SubscriptionDto>> GetList();
        Task Add(Subscription subscription);
        List<SubscriptionDto> GetListByPaging(SubscriptionPagingDto pagingDto, out int total);
        Task<SubscriptionDto> GetViewById(int subscriptionId);
    }
}
