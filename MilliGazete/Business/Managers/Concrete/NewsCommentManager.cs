using AutoMapper;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Helpers.Abstract;
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
    public class NewsCommentManager : INewsCommentService
    {
        private readonly INewsCommentAssistantService _newsCommentAssistantService;
        private readonly IMapper _mapper;
        private readonly INewsAssistantService _newsAssistantService;
        private readonly IBaseService _baseService;
        private readonly INewsCommentsHelper _newsCommentsHelper;

        public NewsCommentManager(INewsCommentAssistantService newsCommentAssistantService, IMapper mapper,
            INewsAssistantService newsAssistantService, IBaseService baseService, INewsCommentsHelper newsCommentsHelper)
        {
            _newsCommentAssistantService = newsCommentAssistantService;
            _mapper = mapper;
            _newsAssistantService = newsAssistantService;
            _baseService = baseService;
            _newsCommentsHelper = newsCommentsHelper;
        }

        [SecuredOperation("NewsCommentGet")]
        [PerformanceAspect()]
        public IDataResult<List<NewsCommentDto>> GetListByPaging(NewsCommentPagingDto pagingDto, out int total)
        {
            return new SuccessDataResult<List<NewsCommentDto>>(_newsCommentAssistantService.GetListByPaging(pagingDto, out total));
        }


        [SecuredOperation()]
        [PerformanceAspect()]
        public IDataResult<List<NewsCommentDto>> GetUserCommentListByPaging(NewsCommentPagingDto pagingDto, out int total)
        {
            pagingDto.UserId = _baseService.RequestUserId;
            var data = _newsCommentAssistantService.GetListByPaging(pagingDto, out total);
            return new SuccessDataResult<List<NewsCommentDto>>(data);
        }

        [SecuredOperation("NewsCommentGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<NewsCommentDto>> GetById(int newsCommentId)
        {
            var NewsComment = await _newsCommentAssistantService.GetById(newsCommentId);
            if (NewsComment == null)
            {
                return new ErrorDataResult<NewsCommentDto>(Messages.RecordNotFound);
            }
            var data = _mapper.Map<NewsCommentDto>(NewsComment);
            return new SuccessDataResult<NewsCommentDto>(data);
        }

        [SecuredOperation()]
        [ValidationAspect(typeof(NewsCommentUpdateDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("INewsCommentService.Get")]
        public async Task<IResult> Update(NewsCommentUpdateDto newsCommentUpdateDto)
        {
            var data = await _newsCommentAssistantService.GetById(newsCommentUpdateDto.Id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            if (data.UserId != _baseService.RequestUserId)
            {
                return new ErrorResult(Messages.AuthorizationDenied);
            }
            var cto = _mapper.Map(newsCommentUpdateDto, data);
            await _newsCommentAssistantService.Update(cto);
            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation()]
        [ValidationAspect(typeof(NewsCommentAddDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("INewsCommentService.Get")]
        public async Task<IResult> Add(NewsCommentAddDto dto)
        {
            var news = await _newsAssistantService.GetById(dto.NewsId);
            if (news == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            var data = _mapper.Map<NewsComment>(dto);
            data.UserId = _baseService.RequestUserId;
            await _newsCommentAssistantService.Add(data);
            return new SuccessResult(Messages.Added);
        }

        [SecuredOperation("NewsCommentDelete")]
        [LogAspect()]
        [CacheRemoveAspect("INewsCommentService.Get")]
        public async Task<IResult> Delete(int newsCommentId)
        {
            var newsComment = await _newsCommentAssistantService.GetById(newsCommentId);
            if (newsComment == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            newsComment.Deleted = true;
            await _newsCommentAssistantService.Update(newsComment);
            return new SuccessResult(Messages.Deleted);
        }

        [SecuredOperation()]
        [LogAspect()]
        [CacheRemoveAspect("INewsCommentService.Get")]
        public async Task<IResult> DeleteUserCommentById(int newsCommentId)
        {
            var data = await _newsCommentAssistantService.GetById(newsCommentId);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            if (data.UserId != _baseService.RequestUserId)
            {
                return new ErrorResult(Messages.AuthorizationDenied);
            }
            data.Deleted = true;
            await _newsCommentAssistantService.Update(data);
            return new SuccessResult(Messages.Deleted);
        }

        [SecuredOperation("NewsCommentUpdate")]
        [LogAspect()]
        [CacheRemoveAspect("INewsCommentService.Get")]
        public async Task<IResult> ChangeApprovedStatus(ChangeApprovedStatusDto dto)
        {
            var data = await _newsCommentAssistantService.GetById(dto.Id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            data.Approved = dto.Approved;
            await _newsCommentAssistantService.Update(data);
            return new SuccessResult(Messages.Updated);
        }

        [PerformanceAspect()]
        public IDataResult<List<UserNewsCommentDto>> GetByNewsId(int newsId, int limit, int page, out int total)
        {
            var data = _newsCommentAssistantService.GetByNewsId(newsId, limit, page, out total);
            data = _newsCommentsHelper.GetLikeStatus(data, _baseService.RequestUserId);
            return new SuccessDataResult<List<UserNewsCommentDto>>(data);
        }
    }
}
