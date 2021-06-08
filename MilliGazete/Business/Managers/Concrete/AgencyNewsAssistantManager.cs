using AutoMapper;
using Business.Helpers.Abstract;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Enums;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class AgencyNewsAssistantManager : IAgencyNewsAssistantService
    {
        private readonly IAgencyNewsDal _agencyNewsDal;
        private readonly IMapper _mapper;
        private readonly IAgencyNewsHelper _agencyNewsHelper;
        private readonly IFileDal _fileDal;
        private readonly IEntityDal _entityDal;

        public AgencyNewsAssistantManager(IAgencyNewsDal AgencyNewsDal, IMapper mapper, IAgencyNewsHelper agencyNewsHelper, IFileDal fileDal, IEntityDal entityDal)
        {
            _agencyNewsDal = AgencyNewsDal;
            _mapper = mapper;
            _agencyNewsHelper = agencyNewsHelper;
            _fileDal = fileDal;
            _entityDal = entityDal;
        }
        public List<AgencyNewsViewDto> GetListByPaging(NewsAgencyPagingDto pagingDto, out int total)
        {
            var list = _agencyNewsDal.GetList();
            var query = _mapper.ProjectTo<AgencyNewsViewDto>(list);

            if (pagingDto.Query.StringNotNullOrEmpty())
                query = query.Where(f => f.Category.Contains(pagingDto.Query) || f.City.Contains(pagingDto.Query) ||
                f.Code.Contains(pagingDto.Query) || f.Country.Contains(pagingDto.Query) ||
                f.Description.Contains(pagingDto.Query) || f.ParentCategory.Contains(pagingDto.Query) ||
                f.Title.Contains(pagingDto.Query));


            if (pagingDto.NewsAgencyId.HasValue)
                query = query.Where(f => f.AgencyId == pagingDto.NewsAgencyId.Value);

            if (pagingDto.FromPublishedAt.HasValue && pagingDto.ToPublishedAt.HasValue)
                query = query.Where(f => f.PublishDate >= pagingDto.FromPublishedAt.Value && f.PublishDate <= pagingDto.ToPublishedAt.Value);

            total = query.Count();
            var data = query.OrderBy(pagingDto.OrderBy).Skip((pagingDto.PageNumber - 1) * pagingDto.Limit.CheckLimit()).Take(pagingDto.Limit.CheckLimit());
            return data.ToList();
        }

        public async Task<AgencyNews> GetById(int id)
        {
            return await _agencyNewsDal.Get(p => p.Id == id);
        }

        public async Task<AgencyNews> GetByCode(string code)
        {
            return await _agencyNewsDal.Get(p => p.Code == code);
        }

        public async Task Delete(AgencyNews agencyNews)
        {
            await _agencyNewsDal.Delete(agencyNews);
        }

        public async Task DeleteAllByAgencyId(int newsAgencyEntityId)
        {
            await _agencyNewsDal.DeleteAllByAgencyId(newsAgencyEntityId);
        }

        public async Task AddArray(List<NewsAgencyAddDto> data)
        {
            foreach (var news in data)
            {
                var agencyNews = _mapper.Map<AgencyNews>(news);
                var files = _agencyNewsHelper.GetFileList(news.Images, news.Videos);
                await _agencyNewsDal.AddWithFiles(agencyNews, files);
            }
        }

        public async Task<List<AgencyNewsViewDto>> GetList()
        {
            return await _mapper.ProjectTo<AgencyNewsViewDto>(_agencyNewsDal.GetList()).ToListAsync();
        }

        public async Task<AgencyNewsFile> GetAgencyNewsFileById(int agencyNewsFileId)
        {
            return await _agencyNewsDal.GetAgencyNewsFileById(agencyNewsFileId);
        }

        public async Task AddFile(File file)
        {
            await _fileDal.Add(file);
        }

        public async Task<List<NewsPropertyAddDto>> GetNewsPropertyEntities()
        {
            var groupId = (int)EntityGroupType.PropertyEntities;
            var entity = await _entityDal.GetList(f => f.EntityGroupId == groupId).Select(f => f.Id).ToListAsync();
            if (entity.HasValue())
            {
                return entity.Select(id => new NewsPropertyAddDto
                {
                    PropertyEntityId = id,
                    Value = false
                }).ToList();
            }
            return new List<NewsPropertyAddDto>();
        }
    }
}
