using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface ISubscriptionService
    {
        Task<IDataResult<SubscriptionDto>> GetById(int subscriptionId);
        Task<IResult> Add(SubscriptionAddDto subscriptionAddDto);
        Task<IDataResult<List<SubscriptionDto>>> GetList();
        IDataResult<List<SubscriptionDto>> GetListByPaging(SubscriptionPagingDto pagingDto, out int total);
    }
}
