using DataAccess.Base;
using Entity.Models;

namespace DataAccess.Abstract
{
    public interface IArticleDal : IEntityRepository<Article>
    {
    }
}
