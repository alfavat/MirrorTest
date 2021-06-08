using DataAccess.Base;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserCategoryRelationDal : IEntityRepository<UserCategoryRelation>
    {
        Task AddRange(List<UserCategoryRelation> userCategoryRelations);
    }
}
