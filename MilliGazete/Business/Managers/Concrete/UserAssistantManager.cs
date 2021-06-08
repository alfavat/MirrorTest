using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class UserAssistantManager : IUserAssistantService
    {
        private readonly IUserDal _userDal;
        private readonly IMapper _mapper;

        public UserAssistantManager(IUserDal userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
        }
        public List<UserViewDto> GetListByPaging(UserPagingDto pagingDto, out int total)
        {
            var list = _userDal.GetList(f => !f.Deleted && f.Id != 53);
            var query = _mapper.ProjectTo<UserViewDto>(list);

            if (pagingDto.IsEmployee.HasValue)
                query = query.Where(f => f.IsEmployee == pagingDto.IsEmployee);

            if (pagingDto.Active.HasValue)
                query = query.Where(f => f.Active == pagingDto.Active);

            if (pagingDto.FromCreatedAt.HasValue && pagingDto.ToCreatedAt.HasValue)
                query = query.Where(f => f.CreatedAt >= pagingDto.FromCreatedAt.Value && f.CreatedAt <= pagingDto.ToCreatedAt.Value);


            if (pagingDto.Query.StringNotNullOrEmpty())
                query = query.Where(f => f.Email.Contains(pagingDto.Query) || f.FirstName.Contains(pagingDto.Query) ||
                f.LastName.Contains(pagingDto.Query) || f.UserName.Contains(pagingDto.Query));

            total = query.Count();
            var data = query.OrderBy(pagingDto.OrderBy).Skip((pagingDto.PageNumber - 1) * pagingDto.Limit.CheckLimit()).Take(pagingDto.Limit.CheckLimit());
            return data.ToList();
        }

        public async Task<User> GetByMailOrUserName(string mailOrUserName)
        {
            return await _userDal.GetList(u => (u.Email == mailOrUserName || u.UserName == mailOrUserName) && !u.Deleted)
            .Include(f => f.UserOperationClaim).ThenInclude(f => f.OperationClaim)
            .FirstOrDefaultAsync();
        }

        public async Task<User> GetByUserId(int userId)
        {
            return await _userDal.Get(u => u.Id == userId && !u.Deleted);
        }

        public List<int> GetPassiveUsers()
        {
            return _userDal.GetList(f => !f.Deleted && !f.Active).Select(f => f.Id).ToList();
        }

        public async Task<List<UserViewDto>> GetListView()
        {
            var list = _userDal.GetList(f => !f.Deleted).OrderBy(u => u.UserName);
            return await _mapper.ProjectTo<UserViewDto>(list).ToListAsync();
        }

        public async Task UpdateLastOpenDate(User user)
        {
            user.LastLoginDate = DateTime.Now;
            await _userDal.Update(user);
        }
        public async Task Update(User user)
        {
            await _userDal.Update(user);
        }

        public async Task Add(User user)
        {
            await _userDal.Add(user);
        }

        public async Task AddUserClaim(UserOperationClaim claim)
        {
            await _userDal.AddUserClaim(claim);
        }
    }
}
