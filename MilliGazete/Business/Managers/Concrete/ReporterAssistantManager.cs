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
using System.Linq.Dynamic.Core;
namespace Business.Managers.Concrete
{
    public class ReporterAssistantManager : IReporterAssistantService
    {
        private readonly IReporterDal _reporterDal;
        private readonly IMapper _mapper;
        private readonly INewsDal _newsDal;
        private readonly IBaseService _baseService;

        public ReporterAssistantManager(IReporterDal ReporterDal, IMapper mapper, INewsDal newsDal, IBaseService baseService)
        {
            _reporterDal = ReporterDal;
            _mapper = mapper;
            _newsDal = newsDal;
            _baseService = baseService;
        }

        public async Task<Reporter> GetById(int reporterId)
        {
            return await _reporterDal.Get(p => p.Id == reporterId && !p.Deleted);
        }

        public async Task<ReporterDto> GetViewById(int reporterId)
        {
            var data = _reporterDal.GetList(p => p.Id == reporterId && !p.Deleted).Include(f => f.ProfileImage);
            return await _mapper.ProjectTo<ReporterDto>(data).FirstOrDefaultAsync();
        }
        public async Task<ReporterDto> GetViewByUrl(string url)
        {
            var data = _reporterDal.GetList(p => p.Url == url && !p.Deleted).Include(f => f.ProfileImage);
            return await _mapper.ProjectTo<ReporterDto>(data).FirstOrDefaultAsync();
        }

        public async Task Update(Reporter reporter)
        {
            await _reporterDal.Update(reporter);
        }

        public async Task Delete(Reporter reporter)
        {
            reporter.Deleted = true;
            await _reporterDal.Update(reporter);
        }

        public async Task<Reporter> Add(Reporter reporter)
        {
            await _reporterDal.Add(reporter);
            return reporter;
        }

        public async Task<List<ReporterDto>> GetList()
        {
            var list = _reporterDal.GetList(p => !p.Deleted).Include(f => f.ProfileImage);
            return await _mapper.ProjectTo<ReporterDto>(list).ToListAsync();
        }

        public async Task<List<NewsViewDto>> GetListByReporterId(int reporterId)
        {
            var languageId = (int)_baseService.UserLanguage;
            var list = _newsDal.GetActiveList().Where(p => p.ReporterId == reporterId && p.NewsCategories.Any(f => (languageId == 0 || f.Category.LanguageId == languageId)))
                .Include(f => f.NewsTags).ThenInclude(f => f.Tag)
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsAgencyEntity)
                .Include(f => f.Author).ThenInclude(f => f.PhotoFile)
                .Include(f => f.NewsTypeEntity)
                .Include(f => f.NewsFiles).ThenInclude(f => f.File)
                .Include(f => f.NewsFiles).ThenInclude(f => f.VideoCoverFile)
                .Include(f => f.NewsRelatedNewsNews).ThenInclude(f => f.RelatedNews).ThenInclude(f => f.NewsFiles).ThenInclude(f => f.File)
                .Include(f => f.NewsPositions).Include(f => f.NewsProperties)
                .AsQueryable();
            return await _mapper.ProjectTo<NewsViewDto>(list).ToListAsync();
        }

        public async Task<Tuple<List<NewsPagingViewDto>, int>> GetListByPagingViaUrl(ReporterNewsPagingDto pagingDto)
        {
            var reporter = await _reporterDal.Get(p => p.Url == pagingDto.Url && !p.Deleted);
            if (reporter == null)
                return null;
            var query = _newsDal.GetActiveList().Where(f => f.ReporterId == reporter.Id);
            var total = await query.CountAsync();
            var mapped = _mapper.ProjectTo<NewsPagingViewDto>(query);
            var data = await mapped.OrderBy(pagingDto.OrderBy).Skip((pagingDto.PageNumber - 1) * pagingDto.Limit.CheckLimit()).Take(pagingDto.Limit.CheckLimit()).ToListAsync();
            return new Tuple<List<NewsPagingViewDto>, int>(data, total);
        }
    }
}
