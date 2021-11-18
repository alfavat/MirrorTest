using AutoMapper;
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
    public class CategoryAssistantManager : ICategoryAssistantService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IMapper _mapper;
        public CategoryAssistantManager(ICategoryDal categoryDal, IMapper mapper)
        {
            _categoryDal = categoryDal;
            _mapper = mapper;
        }
        public List<CategoryDto> GetListByPaging(CategoryPagingDto pagingDto, out int total)
        {
            var list = _categoryDal.GetList(f => !f.Deleted).Include(f => f.FeaturedImageFile).Include(prop => prop.Language);
            var query = _mapper.ProjectTo<CategoryDto>(list);

            if (pagingDto.Query.StringNotNullOrEmpty())
                query = query.Where(f => f.CategoryName.Contains(pagingDto.Query) || f.Url.Contains(pagingDto.Query) ||
                f.SeoDescription.Contains(pagingDto.Query) || f.SeoKeywords.Contains(pagingDto.Query) ||
                f.StyleCode.Contains(pagingDto.Query) || f.Symbol.Contains(pagingDto.Query) ||
                f.ParentCategoryName.Contains(pagingDto.Query));

            if (pagingDto.Active.HasValue)
                query = query.Where(f => f.Active == pagingDto.Active.Value);

            if (pagingDto.IsStatic.HasValue)
                query = query.Where(f => f.IsStatic == pagingDto.IsStatic.Value);

            if (pagingDto.ParentCategoryId.HasValue)
                query = query.Where(f => f.ParentCategoryId == pagingDto.ParentCategoryId.Value);

            if (pagingDto.FromCreatedAt.HasValue && pagingDto.ToCreatedAt.HasValue)
                query = query.Where(f => f.CreatedAt >= pagingDto.FromCreatedAt.Value && f.CreatedAt <= pagingDto.ToCreatedAt.Value);

            total = query.Count();
            var data = query.OrderBy(pagingDto.OrderBy).Skip((pagingDto.PageNumber - 1) * pagingDto.Limit.CheckLimit()).Take(pagingDto.Limit.CheckLimit());
            return data.ToList();
        }

        public async Task<Category> GetById(int categoryId)
        {
            return await _categoryDal.Get(p => p.Id == categoryId && !p.Deleted);
        }

        public async Task<Category> GetByUrl(string url)
        {
            return await _categoryDal.GetList(p => p.Url != null && p.Url.ToLower() == url.ToLower() && !p.Deleted)
                .Include(f => f.FeaturedImageFile)
                .FirstOrDefaultAsync();
        }

        public async Task Update(Category category)
        {
            await _categoryDal.Update(category);
        }

        public async Task Delete(Category category)
        {
            await _categoryDal.Delete(category);
        }

        public async Task Add(Category category)
        {
            await _categoryDal.Add(category);
        }

        public async Task<List<CategoryDto>> GetList()
        {
            var list = _categoryDal.GetList(p => !p.Deleted).Include(f => f.FeaturedImageFile).Include(prop => prop.Language);
            return await _mapper.ProjectTo<CategoryDto>(list).ToListAsync();
        }
        public async Task<List<CategoryDto>> GetActiveList()
        {
            var languageId = CommonHelper.CurrentLanguageId;
            var list = _categoryDal.GetList(p => !p.Deleted && p.Active && (languageId == (int)Languages.All || p.LanguageId == languageId)).Include(f => f.FeaturedImageFile);
            return await _mapper.ProjectTo<CategoryDto>(list).ToListAsync();
        }

        public async Task<CategoryDto> GetViewById(int categoryId)
        {
            var data = await _categoryDal.GetList(p => !p.Deleted && p.Id == categoryId).Include(f => f.FeaturedImageFile).FirstOrDefaultAsync();
            if (data != null)
            {
                return _mapper.Map<CategoryDto>(data);
            }
            return null;
        }
    }
}
