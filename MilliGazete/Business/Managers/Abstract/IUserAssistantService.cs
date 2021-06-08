using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IUserAssistantService
    {
        Task<User> GetByMailOrUserName(string mailOrUserName);
        Task<User> GetByUserId(int userId);
        Task<List<UserViewDto>> GetListView();
        Task UpdateLastOpenDate(User user);
        Task Add(User user);
        Task AddUserClaim(UserOperationClaim claim);
        Task Update(User user);
        List<int> GetPassiveUsers();
        List<UserViewDto> GetListByPaging(UserPagingDto pagingDto, out int total);
    }
}
