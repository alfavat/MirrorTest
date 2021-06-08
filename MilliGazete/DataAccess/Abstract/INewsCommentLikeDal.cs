using DataAccess.Base;
using Entity.Models;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface INewsCommentLikeDal : IEntityRepository<NewsCommentLike>
    {
        Task AddOrUpdate(int newsCommentId , int userId);
    }
}
