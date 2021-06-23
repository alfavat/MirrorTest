using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class NewsFileAssistantManager : INewsFileAssistantService
    {
        private readonly INewsFileDal _newsFileDal;
        private readonly IMapper _mapper;
        public NewsFileAssistantManager(INewsFileDal newsFileDal, IMapper mapper)
        {
            _newsFileDal = newsFileDal;
            _mapper = mapper;
        }
        public List<NewsFileDto> GetListByPaging(NewsFilePagingDto pagingDto, out int total)
        {
            var list = _newsFileDal.GetList(prop=>!prop.CameFromPool && 
                                                    prop.News.Active && 
                                                    !prop.News.Deleted && 
                                                    prop.News.Approved == true && 
                                                    prop.News.IsLastNews &&
                                                    !prop.News.IsDraft).Include(f => f.File).Include(prop=>prop.News).AsQueryable();

            if (pagingDto.Query.StringNotNullOrEmpty())
                list = list.Where(f => f.Title.Contains(pagingDto.Query) || f.Description.Contains(pagingDto.Query) || f.News.Title.Contains(pagingDto.Query));

            var query = _mapper.ProjectTo<NewsFileDto>(list);
            total = query.Count();
            var data = query.OrderBy(pagingDto.OrderBy).Skip((pagingDto.PageNumber - 1) * pagingDto.Limit.CheckLimit()).Take(pagingDto.Limit.CheckLimit());
            return data.ToList();
        }

        public async Task<NewsFile> GetById(int newsFileId)
        {
            return await _newsFileDal.Get(p => p.Id == newsFileId);
        }

        public async Task<NewsFileDto> GetViewById(int newsFileId)
        {
            var data = await _newsFileDal.GetList(p => p.Id == newsFileId).Include(f => f.File).FirstOrDefaultAsync();
            if (data != null)
            {
                return _mapper.Map<NewsFileDto>(data);
            }
            return null;
        }
    }
}
