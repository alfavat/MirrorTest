using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Managers.Abstract;
using Core.Utilities.Helper;
using Core.Utilities.Helper.Abstract;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IUserAssistantService _userAssistantService;
        private readonly ITokenHelper _tokenHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IUserPasswordRequestDal _userPasswordRequestDal;
        private readonly IConfiguration _configuration;
        private readonly IBaseService _baseService;
        private readonly IUserOperationClaimAssistantService _userOperationClaimAssistantService;

        public AuthManager(
            IUserService userService,
            IUserAssistantService userAssistantService,
            ITokenHelper tokenHelper,
            IMailHelper mailHelper,
            IUserPasswordRequestDal userPasswordRequestDal,
            IConfiguration configuration,
            IBaseService baseService,
            IUserOperationClaimAssistantService userOperationClaimAssistantService)
        {
            _userService = userService;
            _userAssistantService = userAssistantService;
            _tokenHelper = tokenHelper;
            _mailHelper = mailHelper;
            _userPasswordRequestDal = userPasswordRequestDal;
            _configuration = configuration;
            _baseService = baseService;
            _userOperationClaimAssistantService = userOperationClaimAssistantService;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = user.UserOperationClaims.Select(f => f.OperationClaim).ToList();
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AcccessTokenCreated);
        }

        public async Task<IDataResult<User>> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await _userAssistantService.GetByMailOrUserName(userForLoginDto.EmailOrUserName);

            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            if (!userToCheck.Active)
            {
                return new ErrorDataResult<User>(Messages.UserIsNotActive);
            }

            await _userAssistantService.UpdateLastOpenDate(userToCheck);
            return new SuccessDataResult<User>(userToCheck, Messages.SuccessForLogin);
        }

        [SecuredOperation()]
        public async Task<IDataResult<User>> GetAndCheckCurrentUser()
        {
            if (_baseService.RequestUserId == 0)
            {
                return new ErrorDataResult<User>(Messages.EmptyToken);
            }
            var userToCheck = await _userAssistantService.GetByUserId(_baseService.RequestUserId);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (!userToCheck.Active)
            {
                return new ErrorDataResult<User>(Messages.UserIsNotActive);
            }

            await _userAssistantService.UpdateLastOpenDate(userToCheck);
            var data = await _userOperationClaimAssistantService.GetClaimsByUserId(_baseService.RequestUserId);
            if (data.HasValue())
            {
                userToCheck.UserOperationClaims = data;
            }
            return new SuccessDataResult<User>(userToCheck, Messages.SuccessForLogin);
        }

        public async Task<IResult> UserExists(string emailOrUserName)
        {
            if (await _userAssistantService.GetByMailOrUserName(emailOrUserName) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public async Task<IResult> CreateAndSendResetPasswordLink(string userNameOrEmail, string callBackUrl)
        {
            var user = await _userAssistantService.GetByMailOrUserName(userNameOrEmail);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            if (user.Email.StringIsNullOrEmpty())
            {
                return new ErrorResult(Messages.EmailNotFound);
            }

            var token = Guid.NewGuid().ToString();

            var newReq = new UserPasswordRequest()
            {
                CreatedAt = DateTime.Now,
                Token = token,
                UserId = user.Id,
                ExpirationDate = DateTime.Now.AddDays(1)
            };
            await _userPasswordRequestDal.Add(newReq);
            //string url = _configuration.GetSection("EmailSettings").Get<EmailSettings>().Url;
            string subject = " Şifremi Unuttum";
            string link = callBackUrl + "?id=" + newReq.Id + "&token=" + token;
            string body = "<b>Şifrenizi sıfırlamak için lütfen aşağıdaki linke tıklayın. </b><br/><a href='" + link + "'>Şifremi Sıfırla</a>";

            if (_mailHelper.SendEmail(subject, body, user.Email.Split(",")))
                return new SuccessResult(Messages.MailSent);
            return new ErrorResult(Messages.MailError);
        }

        public async Task<IResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var request = await _userPasswordRequestDal.Get(f => f.Id == resetPasswordDto.Id && f.Token == resetPasswordDto.Token);
            if (request == null)
            {
                return new ErrorResult(Messages.NoRecordFound);
            }
            if (request.ExpirationDate < DateTime.Now)
            {
                return new ErrorResult(Messages.ExpirationDatePassed);
            }
            User user = await _userAssistantService.GetByUserId(request.UserId.Value);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            if (!user.Active)
            {
                return new ErrorResult(Messages.UserIsNotActive);
            }
            if (user.Email.StringIsNullOrEmpty())
            {
                return new ErrorResult(Messages.EmailNotFound);
            }
            await _userService.UpdatePassword(user, resetPasswordDto);

            request.ExpirationDate = DateTime.Now.AddDays(-1);
            await _userPasswordRequestDal.Update(request);
            new Thread(() =>
            {
                try
                {
                    string url = _configuration.GetSection("EmailSettings").Get<EmailSettings>().Url;
                    string subject = " Şifremi Sıfırla";
                    string body = "<b>Şifreniz başarıyla sıfırlandı. </b><br/><a href='" + url + "/login'>Giriş için buraya tıklayın</a>";
                    _mailHelper.SendEmail(subject, body, user.Email.Split(","));
                }
                catch (Exception)
                { }
            }).Start();

            return new SuccessResult(Messages.Updated);
        }
    }
}
