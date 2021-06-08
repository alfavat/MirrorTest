using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsCommentLikeAssistantService
    {
        Task<NewsCommentLike> GetById(int newsCommentLikeId);
        Task Update(NewsCommentLike newsCommentLike);
        Task Delete(NewsCommentLike newsCommentLike);
        Task<List<NewsCommentLikeDto>> GetList();
        Task Add(NewsCommentLike newsCommentLike);
        Task AddOrUpdate(int newsCommentId,int userId);
    }
}
