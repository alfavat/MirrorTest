using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class UserOperationClaimAssistantManager : IUserOperationClaimAssistantService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IMapper _mapper;
        public UserOperationClaimAssistantManager(IUserOperationClaimDal UserOperationClaimDal
            , IMapper mapper)
        {
            _userOperationClaimDal = UserOperationClaimDal;
            _mapper = mapper;
        }

        public async Task<List<UserOperationClaimViewDto>> GetByUserId(int userId)
        {
            var list = _userOperationClaimDal.GetList(f => f.UserId == userId).Include(f => f.OperationClaim);
            return await _mapper.ProjectTo<UserOperationClaimViewDto>(list).ToListAsync();
        }

        public async Task<List<UserOperationClaim>> GetClaimsByUserId(int userId)
        {
            return await _userOperationClaimDal.GetList(f => f.UserId == userId).Include(f => f.OperationClaim).ToListAsync();
        }

        public async Task<UserOperationClaim> GetById(int id)
        {
            return await _userOperationClaimDal.Get(f => f.Id == id);
        }

        public async Task Update(UserOperationClaimUpdateDto userOperationClaim)
        {
            var list = new List<UserOperationClaim>();
            userOperationClaim.OperationClaimIds.ForEach(c => list.Add(new UserOperationClaim
            {
                OperationClaimId = c,
                UserId = userOperationClaim.UserId
            }));
            await _userOperationClaimDal.AddRange(list);
        }

        public async Task Add(UserOperationClaim userOperationClaim)
        {
            await _userOperationClaimDal.Add(userOperationClaim);
        }
    }
}
