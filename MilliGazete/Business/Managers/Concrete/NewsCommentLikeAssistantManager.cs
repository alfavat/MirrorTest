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
    public class NewsCommentLikeAssistantManager : INewsCommentLikeAssistantService
    {
        private readonly INewsCommentLikeDal _newsCommentLikeDal;
        private readonly IMapper _mapper;
        public NewsCommentLikeAssistantManager(INewsCommentLikeDal NewsCommentLikeDal, IMapper mapper)
        {
            _newsCommentLikeDal = NewsCommentLikeDal;
            _mapper = mapper;
        }

        public async Task<NewsCommentLike> GetById(int newsCommentLikeId)
        {
            return await _newsCommentLikeDal.Get(p => p.Id == newsCommentLikeId && !p.NewsComment.Deleted);
        }

        public async Task Update(NewsCommentLike newsCommentLike)
        {
            await _newsCommentLikeDal.Update(newsCommentLike);
        }

        public async Task Delete(NewsCommentLike newsCommentLike)
        {
            await _newsCommentLikeDal.Delete(newsCommentLike);
        }

        public async Task Add(NewsCommentLike newsCommentLike)
        {
            await _newsCommentLikeDal.Add(newsCommentLike);
        }

        public async Task<List<NewsCommentLikeDto>> GetList()
        {
            var list = _newsCommentLikeDal.GetList(p => !p.NewsComment.Deleted).Include(f => f.NewsComment).Include(f => f.User);
            return await _mapper.ProjectTo<NewsCommentLikeDto>(list).ToListAsync();
        }

        public async Task AddOrUpdate(int newsCommentId, int userId)
        {
            await _newsCommentLikeDal.AddOrUpdate(newsCommentId, userId);
        }
    }
}
