using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class NewsHitAssistantManager : INewsHitAssistantService
    {
        private readonly INewsHitDal _newsHitDal;
        private readonly IMapper _mapper;
        public NewsHitAssistantManager(INewsHitDal newsHitDal, IMapper mapper)
        {
            _newsHitDal = newsHitDal;
            _mapper = mapper;
        }

        public async Task AddWithDetail(NewsHitDetailAddDto dto)
        {
            await _newsHitDal.AddWithDetail(dto);
        }

        public async Task<List<NewsHitDto>> GetList()
        {
            return await _mapper.ProjectTo<NewsHitDto>(_newsHitDal.GetList()).ToListAsync();
        }

        public async Task<List<NewsHitDto>> GetListByNewsId(int newsId)
        {
            return await _mapper.ProjectTo<NewsHitDto>(_newsHitDal.GetList(f => f.NewsId == newsId)).ToListAsync();
        }
    }
}
