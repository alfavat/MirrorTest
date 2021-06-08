using AutoMapper;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Managers.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IArticleAssistantService _articleAssistantService;
        private readonly IMapper _mapper;

        public ArticleManager(IArticleAssistantService articleAssistantService, IMapper mapper)
        {
            _articleAssistantService = articleAssistantService;
            _mapper = mapper;
        }

        [SecuredOperation("ArticleGet")]
        [PerformanceAspect()]
        public IDataResult<List<ArticleDto>> GetListByPaging(ArticlePagingDto pagingDto, out int total)
        {
            return new SuccessDataResult<List<ArticleDto>>(_articleAssistantService.GetListByPaging(pagingDto, out total));
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<ArticleDto>>> GetLastWeekMostViewedArticles(int count)
        {
            return new SuccessDataResult<List<ArticleDto>>(await _articleAssistantService.GetLastWeekMostViewedArticles(count));
        }

        //[SecuredOperation("ArticleGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<ArticleDto>> GetByUrl(string url)
        {
            var article = await _articleAssistantService.GetByUrl(url);
            if (article == null)
            {
                return new ErrorDataResult<ArticleDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<ArticleDto>(article);
        }

        //[SecuredOperation("ArticleGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<ArticleDto>>> GetListByAuthorId(int authorId)
        {
            return new SuccessDataResult<List<ArticleDto>>(await _articleAssistantService.GetListByAuthorId(authorId));
        }

        [SecuredOperation("ArticleGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<ArticleDto>> GetById(int id)
        {
            var article = await _articleAssistantService.GetById(id);
            if (article == null)
            {
                return new ErrorDataResult<ArticleDto>(Messages.RecordNotFound);
            }
            var data = _mapper.Map<ArticleDto>(article);
            return new SuccessDataResult<ArticleDto>(data);
        }

        [SecuredOperation("ArticleUpdate")]
        [ValidationAspect(typeof(ArticleUpdateDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IArticleService.Get")]
        public async Task<IResult> Update(ArticleUpdateDto dto)
        {
            var article = await _articleAssistantService.GetById(dto.Id);
            if (article == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            var cto = _mapper.Map(dto, article);
            await _articleAssistantService.Update(cto);
            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation("ArticleAdd")]
        [ValidationAspect(typeof(ArticleAddDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IArticleService.Get")]
        public async Task<IResult> Add(ArticleAddDto dto)
        {
            var article = _mapper.Map<Article>(dto);
            await _articleAssistantService.Add(article);
            return new SuccessResult(Messages.Added);
        }

        [SecuredOperation("ArticleDelete")]
        [LogAspect()]
        [CacheRemoveAspect("IArticleService.Get")]
        public async Task<IResult> Delete(int id)
        {
            var article = await _articleAssistantService.GetById(id);
            if (article == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            article.Deleted = true;
            await _articleAssistantService.Update(article);
            return new SuccessResult(Messages.Deleted);
        }

        [SecuredOperation("ArticleUpdate")]
        [LogAspect()]
        [CacheRemoveAspect("IArticleService.Get")]
        public async Task<IResult> ChangeApprovedStatus(ChangeApprovedStatusDto dto)
        {
            var data = await _articleAssistantService.GetById(dto.Id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            data.Approved = dto.Approved;
            await _articleAssistantService.Update(data);
            return new SuccessResult(Messages.Updated);
        }
    }
}
