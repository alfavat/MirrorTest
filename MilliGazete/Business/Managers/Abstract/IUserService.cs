using Core.Utilities.Results;
using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<List<UserViewDto>>> GetList();
        Task<IResult> RegisterUser(RegisterDto registerDto);
        Task<IResult> Delete(int userId);
        Task<IResult> ChangePassword(ChangePasswordDto changePasswordDto);
        Task UpdatePassword(User user, ResetPasswordDto resetPasswordDto);
        Task<IResult> UpdateUser(UserUpdateDto updateUserDto);
        Task<IResult> ChangeActiveStatus(ChangeActiveStatusDto changeActiveStatusDto);
        Task<IDataResult<UserViewDto>> GetById(int userId);
        void FillPassiveUserList();
        Task<IDataResult<UserViewDto>> GetCurrentUser();
        Task<IDataResult<int>> AddUser(UserAddDto userAddDto);
        IDataResult<List<UserViewDto>> GetListByPaging(UserPagingDto pagingDto, out int total);
        Task<IResult> UpdateCurrentUser(CurrentUserUpdateDto dto);
    }
}
