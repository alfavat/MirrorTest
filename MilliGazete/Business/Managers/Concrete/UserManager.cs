using AutoMapper;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Managers.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Entity.Dtos;
using Entity.Enums;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserAssistantService _userAssistantService;
        private readonly IMapper _mapper;
        private readonly IBaseService _baseService;

        public UserManager(
            IUserAssistantService userAssistantService,
            IMapper mapper,
            IBaseService baseService)
        {
            _userAssistantService = userAssistantService;
            _mapper = mapper;
            _baseService = baseService;
        }

        public void FillPassiveUserList()
        {
            List<int> users = _userAssistantService.GetPassiveUsers();
            _baseService.FillPassiveUserList(users);
        }

        [SecuredOperation("UserGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<UserViewDto>>> GetList()
        {
            return new SuccessDataResult<List<UserViewDto>>(await _userAssistantService.GetListView());
        }

        [SecuredOperation("UserGet")]
        [PerformanceAspect()]
        public IDataResult<List<UserViewDto>> GetListByPaging(UserPagingDto pagingDto, out int total)
        {
            return new SuccessDataResult<List<UserViewDto>>(_userAssistantService.GetListByPaging(pagingDto, out total));
        }

        [SecuredOperation("UserGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<UserViewDto>> GetById(int userId)
        {
            var user = await _userAssistantService.GetByUserId(userId);
            if (user == null)
            {
                return new ErrorDataResult<UserViewDto>(Messages.UserNotFound);
            }
            return new SuccessDataResult<UserViewDto>(_mapper.Map<UserViewDto>(user));
        }

        [SecuredOperation()]
        [PerformanceAspect()]
        public async Task<IDataResult<UserViewDto>> GetCurrentUser()
        {
            var user = await _userAssistantService.GetByUserId(_baseService.RequestUserId);
            if (user == null)
            {
                return new ErrorDataResult<UserViewDto>(Messages.UserNotFound);
            }
            return new SuccessDataResult<UserViewDto>(_mapper.Map<UserViewDto>(user));
        }

        [ValidationAspect(typeof(UserValidator))]
        [PerformanceAspect()]
        [TransactionScopeAspect()]
        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IResult> RegisterUser(RegisterDto registerDto)
        {
            var userExists = await _userAssistantService.GetByMailOrUserName(registerDto.Email);
            if (userExists != null)
            {
                return new ErrorDataResult<RegisterResultDto>(Messages.EmailAlreadyExists);
            }
            userExists = await _userAssistantService.GetByMailOrUserName(registerDto.UserName);
            if (userExists != null)
            {
                return new ErrorDataResult<RegisterResultDto>(Messages.UserNameAlreadyExists);
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);

            var user = _mapper.Map<User>(registerDto);
            user.IsEmployee = false;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _userAssistantService.Add(user);
            await _userAssistantService.AddUserClaim(new UserOperationClaim() { UserId = user.Id, OperationClaimId = (int)OperationClaims.UserGet });
            return new SuccessResult(Messages.UserRegistered);
        }

        [SecuredOperation("UserAdd")]
        [ValidationAspect(typeof(UserAddDtoValidator))]
        [PerformanceAspect()]
        [TransactionScopeAspect()]
        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IDataResult<int>> AddUser(UserAddDto userAddDto)
        {
            var userExists = await _userAssistantService.GetByMailOrUserName(userAddDto.Email);
            if (userExists != null)
            {
                return new ErrorDataResult<int>(-1, Messages.EmailAlreadyExists);
            }
            userExists = await _userAssistantService.GetByMailOrUserName(userAddDto.UserName);
            if (userExists != null)
            {
                return new ErrorDataResult<int>(-1, Messages.UserNameAlreadyExists);
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userAddDto.Password, out passwordHash, out passwordSalt);

            var user = _mapper.Map<User>(userAddDto);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _userAssistantService.Add(user);
            return new SuccessDataResult<int>(user.Id, Messages.UserRegistered);
        }

        [SecuredOperation("UserUpdate")]
        [ValidationAspect(typeof(UserUpdateValidator))]
        [PerformanceAspect()]
        [TransactionScopeAspect()]
        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IResult> UpdateUser(UserUpdateDto updateUserDto)
        {
            var user = await _userAssistantService.GetByUserId(updateUserDto.Id);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            var userExists = await _userAssistantService.GetByMailOrUserName(updateUserDto.Email);
            if (userExists != null && userExists.Id != updateUserDto.Id)
            {
                return new ErrorResult(Messages.EmailAlreadyExists);
            }
            userExists = await _userAssistantService.GetByMailOrUserName(updateUserDto.UserName);
            if (userExists != null && userExists.Id != updateUserDto.Id)
            {
                return new ErrorResult(Messages.UserNameAlreadyExists);
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(updateUserDto.Password, out passwordHash, out passwordSalt);

            var cto = _mapper.Map(updateUserDto, user);

            cto.PasswordHash = passwordHash;
            cto.PasswordSalt = passwordSalt;

            await _userAssistantService.Update(cto);
            _baseService.UpdatePassiveUserList(cto.Id, cto.Active);
            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation()]
        [ValidationAspect(typeof(CurrentUserUpdateValidator))]
        [PerformanceAspect()]
        [TransactionScopeAspect()]
        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IResult> UpdateCurrentUser(CurrentUserUpdateDto updateUserDto)
        {
            var user = await _userAssistantService.GetByUserId(_baseService.RequestUserId);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            var userExists = await _userAssistantService.GetByMailOrUserName(updateUserDto.Email);
            if (userExists != null && userExists.Id != _baseService.RequestUserId)
            {
                return new ErrorResult(Messages.EmailAlreadyExists);
            }
            userExists = await _userAssistantService.GetByMailOrUserName(updateUserDto.UserName);
            if (userExists != null && userExists.Id != _baseService.RequestUserId)
            {
                return new ErrorResult(Messages.UserNameAlreadyExists);
            }

            _mapper.Map(updateUserDto, user);
            await _userAssistantService.Update(user);
            _baseService.UpdatePassiveUserList(user.Id, user.Active);
            return new SuccessResult(Messages.Updated);
        }


        [SecuredOperation()]
        [LogAspect()]
        [PerformanceAspect()]
        [ValidationAspect(typeof(UserPasswordValidator))]
        public async Task<IResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = await _userAssistantService.GetByUserId(_baseService.RequestUserId);
            if (user == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (!user.Active)
            {
                return new ErrorDataResult<User>(Messages.UserIsNotActive);
            }
            byte[] passwordHash, passwordSalt;

            if (!HashingHelper.VerifyPasswordHash(changePasswordDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            HashingHelper.CreatePasswordHash(changePasswordDto.NewPassword, out passwordHash, out passwordSalt);

            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            await _userAssistantService.Update(user);
            return new SuccessResult(Messages.Updated);
        }

        [LogAspect()]
        [ValidationAspect(typeof(ResetPasswordValidator))]
        public async Task UpdatePassword(User user, ResetPasswordDto resetPasswordDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(resetPasswordDto.NewPassword, out passwordHash, out passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            await _userAssistantService.Update(user);
        }

        [SecuredOperation("UserDelete")]
        [LogAspect()]
        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IResult> Delete(int userId)
        {
            var user = await _userAssistantService.GetByUserId(userId);
            if (user == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            user.Deleted = true;
            await _userAssistantService.Update(user);
            _baseService.UpdatePassiveUserList(user.Id, false);
            return new SuccessResult(Messages.Deleted);
        }

        [SecuredOperation("UserUpdate")]
        [LogAspect()]
        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IResult> ChangeActiveStatus(ChangeActiveStatusDto changeActiveStatusDto)
        {
            var user = await _userAssistantService.GetByUserId(changeActiveStatusDto.Id);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            user.Active = changeActiveStatusDto.Active;
            await _userAssistantService.Update(user);
            _baseService.UpdatePassiveUserList(changeActiveStatusDto.Id, changeActiveStatusDto.Active);
            return new SuccessResult(Messages.Updated);
        }

    }
}
