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
    public class UserCategoryRelationAssistantManager : IUserCategoryRelationAssistantService
    {
        private readonly IUserCategoryRelationDal _userCategoryRelationDal;
        private readonly IMapper _mapper;
        public UserCategoryRelationAssistantManager(IUserCategoryRelationDal userCategoryRelationDal, IMapper mapper)
        {
            _userCategoryRelationDal = userCategoryRelationDal;
            _mapper = mapper;
        }

        public async Task Update(UserCategoryRelationUpdateDto dto)
        {
            var userCategoryRelations = new List<UserCategoryRelation>();
            dto.CategoryIds.ForEach(categoryId =>
            {
                userCategoryRelations.Add(new UserCategoryRelation
                {
                    CategoryId = categoryId,
                    UserId = dto.UserId
                });
            });
            await _userCategoryRelationDal.AddRange(userCategoryRelations);
        }

        public async Task<List<UserCategoryRelationViewDto>> GetByUserId(int userId)
        {
            var list = _userCategoryRelationDal.GetList(f => f.UserId == userId);
            return await _mapper.ProjectTo<UserCategoryRelationViewDto>(list).ToListAsync();
        }
    }
}
