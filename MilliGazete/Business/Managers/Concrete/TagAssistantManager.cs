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
    public class TagAssistantManager : ITagAssistantService
    {
        private readonly ITagDal _tagDal;
        private readonly IMapper _mapper;
        public TagAssistantManager(ITagDal TagDal, IMapper mapper)
        {
            _tagDal = TagDal;
            _mapper = mapper;
        }

        public async Task<Tag> GetById(int tagId)
        {
            return await _tagDal.Get(p => p.Id == tagId && !p.Deleted);
        }

        public List<TagDto> GetListByPaging(TagPagingDto pagingDto, out int total)
        {
            var list = _tagDal.GetList(f => !f.Deleted);
            var query = _mapper.ProjectTo<TagDto>(list);

            if (pagingDto.Query.StringNotNullOrEmpty())
                query = query.Where(f => f.TagName.Contains(pagingDto.Query) || f.Url.Contains(pagingDto.Query));

            if (pagingDto.Active.HasValue)
                query = query.Where(f => f.Active == pagingDto.Active.Value);

            if (pagingDto.FromCreatedAt.HasValue && pagingDto.ToCreatedAt.HasValue)
                query = query.Where(f => f.CreatedAt >= pagingDto.FromCreatedAt.Value && f.CreatedAt <= pagingDto.ToCreatedAt.Value);

            total = query.Count();
            var data = query.OrderBy(pagingDto.OrderBy).Skip((pagingDto.PageNumber - 1) * pagingDto.Limit.CheckLimit()).Take(pagingDto.Limit.CheckLimit());
            return data.ToList();
        }

        public async Task<List<TagDto>> SearchByTagName(string tagName)
        {
            var list = _tagDal.GetList(f => !f.Deleted);
            var query = _mapper.ProjectTo<TagDto>(list);

            if (tagName.StringNotNullOrEmpty())
                query = query.Where(f => f.TagName.StartsWith(tagName));
            else
                query = query.OrderByDescending(r => r.CreatedAt).Take(20);

            return await query.ToListAsync();
        }

        public async Task<Tag> GetByTagNameOrUrl(string tagName, string url)
        {
            return await _tagDal.Get(f => !f.Deleted && (f.TagName.ToLower() == tagName.ToLower() || f.Url.ToLower() == url.ToLower()));
        }

        public async Task Update(Tag tag)
        {
            await _tagDal.Update(tag);
        }

        public async Task Delete(Tag tag)
        {
            await _tagDal.Delete(tag);
        }

        public async Task Add(Tag tag)
        {
            await _tagDal.Add(tag);
        }

        public async Task<List<TagDto>> GetList()
        {
            var list = _tagDal.GetList(p => !p.Deleted);
            return await _mapper.ProjectTo<TagDto>(list).ToListAsync();
        }
    }
}
