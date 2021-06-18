using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class NewsHitDetailAssistantManager : INewsHitDetailAssistantService
    {
        private readonly INewsHitDetailDal _newsHitDetailDal;
        private readonly IEntityDal _entityDal;
        private readonly IMapper _mapper;
        public NewsHitDetailAssistantManager(INewsHitDetailDal newsHitDetailDal, IEntityDal entityDal,IMapper mapper)
        {
            _newsHitDetailDal = newsHitDetailDal;
            _mapper = mapper;
            _entityDal = entityDal;
        }

        public async Task Add(NewsHitDetail data)
        {
            await _newsHitDetailDal.Add(data);
        }

        public async Task<List<NewsHitDetailDto>> GetLastNewHitDetails(int minutes)
        {
            var dt = DateTime.Now.AddMinutes(-1 * minutes);
            var data = _newsHitDetailDal.GetList()
                .Where(f => f.CreatedAt >= dt)
                .Include(f => f.News)
                .Include(f => f.User).AsQueryable();
            return await _mapper.ProjectTo<NewsHitDetailDto>(data).ToListAsync();
        }

        public async Task<List<NewsHitDetailDto>> GetList()
        {
            return await _mapper.ProjectTo<NewsHitDetailDto>(_newsHitDetailDal.GetList()).ToListAsync();
        }

        public async Task<List<NewsHitDetailDto>> GetListByNewsId(int newsId)
        {
            return await _mapper.ProjectTo<NewsHitDetailDto>(_newsHitDetailDal.GetList(f => f.NewsId == newsId)).ToListAsync();
        }

        public async Task<List<NewsHitCountGroupDto>> GetListHitGroupByNewsId(int newsId)
        {
            var result = from newsHitDetailTable in _newsHitDetailDal.GetList()
                         where newsHitDetailTable.NewsId == newsId
                         group newsHitDetailTable by newsHitDetailTable.NewsHitComeFromEntityId into hitGroup
                         select new NewsHitCountGroupDto()
                         {
                             NewsHitComeFromEntityId = hitGroup.Key,
                             Count = hitGroup.Count(),
                             NewsHitEntityName = ""
                         };
            result = from resultRecords in result
                     join entityTable in _entityDal.GetList()
                     on resultRecords.NewsHitComeFromEntityId equals entityTable.Id
                     select new NewsHitCountGroupDto()
                     {
                         NewsHitComeFromEntityId = resultRecords.NewsHitComeFromEntityId,
                         Count = resultRecords.Count,
                         NewsHitEntityName = entityTable.EntityName
                     };

            return await result.ToListAsync();
        }
    }
}