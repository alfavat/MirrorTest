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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class NewsManager : INewsService
    {
        private readonly INewsAssistantService _newsAssistantService;
        private readonly IMapper _mapper;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly INewsPositionAssistantService _newsPositionAssistantService;
        private readonly INewsHelper _newsHelper;

        public NewsManager(INewsAssistantService newsAssistantService, INewsPositionAssistantService newsPositionAssistantService,
            INewsHelper newsHelper, IMapper mapper, IPushNotificationService pushNotificationService)
        {
            _newsAssistantService = newsAssistantService;
            _newsPositionAssistantService = newsPositionAssistantService;
            _newsHelper = newsHelper;
            _mapper = mapper;
            _pushNotificationService = pushNotificationService;
        }

        [SecuredOperation("NewsGet")]
        [PerformanceAspect()]
        public IDataResult<List<NewsPagingViewDto>> GetListByPaging(NewsPagingDto pagingDto, out int total)
        {
            var list = _newsAssistantService.GetListByPaging(pagingDto).Result;
            var data = _newsHelper.ShortenDescription(list.Item1);
            total = list.Item2;
            return new SuccessDataResult<List<NewsPagingViewDto>>(data);
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<NewsSiteMapDto>>> GetListForSiteMap()
        {
            return new SuccessDataResult<List<NewsSiteMapDto>>(await _newsAssistantService.GetListForSiteMap());
        }

        [SecuredOperation("NewsGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<NewsHistoryDto>>> GetHistoryByNewsId(int newsId)
        {
            var data = await _newsAssistantService.GetById(newsId);
            if (data == null)
            {
                return new ErrorDataResult<List<NewsHistoryDto>>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<List<NewsHistoryDto>>(await _newsAssistantService.GetListByHistoryNo(data.HistoryNo ?? 0));
        }

        [SecuredOperation("NewsGet")]
        [PerformanceAspect()]
        public async Task<IDataResult<NewsViewDto>> GetViewById(int newsId)
        {
            var data = await _newsAssistantService.GetViewById(newsId);
            if (data == null)
            {
                return new ErrorDataResult<NewsViewDto>(Messages.RecordNotFound);
            }
            data.NewsFileList = _newsHelper.OrderEntities(data.NewsFileList);
            data.Url = data.Url.Replace("-" + data.HistoryNo.ToString(), "");
            var res = _newsHelper.FixUrl(data);
            return new SuccessDataResult<NewsViewDto>(res);
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<ArticleDto>> GetArticleByUrl(string url)
        {
            var data = await _newsAssistantService.GetArticleByUrl(url);
            if (data == null)
            {
                return new ErrorDataResult<ArticleDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<ArticleDto>(data);
        }

        [PerformanceAspect()]
        public async Task<IDataResult<NewsViewDto>> GetViewByUrl(string url)
        {
            var data = await _newsAssistantService.GetViewByUrl(url);
            if (data == null)
            {
                return new ErrorDataResult<NewsViewDto>(Messages.RecordNotFound);
            }
            data.NewsFileList = _newsHelper.OrderEntities(data.NewsFileList);
            data.Url = data.Url.Replace("-" + data.HistoryNo.ToString(), "");
            return new SuccessDataResult<NewsViewDto>(data);
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<ArticleDto>>> GetLastWeekMostViewedArticles(int limit)
        {
            return new SuccessDataResult<List<ArticleDto>>(await _newsAssistantService.GetLastWeekMostViewedArticles(limit));
        }


        [SecuredOperation("NewsAdd")]
        [ValidationAspect(typeof(NewsAddDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("INewsPositionService.Get")]
        [CacheRemoveAspect("INewsService.Get")]
        [CacheRemoveAspect("IMainPageService.Get")]
        [CacheRemoveAspect("ICategoryPageService.Get")]
        [CacheRemoveAspect("INewsDetailPageService.Get")]
        public async Task<IDataResult<int>> Add(NewsAddDto newsAddDto)
        {
            if (newsAddDto.PushNotification)
            {
                if (!newsAddDto.Active || newsAddDto.IsDraft)
                {
                    return new ErrorDataResult<int>(0, Messages.PushNotificationActiveDraftError);
                }
                if (newsAddDto.PublishDate.StringIsNullOrEmpty() && newsAddDto.PublishTime.StringIsNullOrEmpty())
                {
                    return new ErrorDataResult<int>(0, Messages.PushNotificationPublishDateError);
                }
                var publishDateTime = DateTime.Parse(newsAddDto.PublishDate).Date.Add(TimeSpan.Parse(newsAddDto.PublishTime));
                if (publishDateTime > DateTime.Now.AddMinutes(5))
                {
                    return new ErrorDataResult<int>(0, Messages.PushNotificationPublishDateError);
                }
            }


            int addUserId = 0;
            int historyNo;
            if (newsAddDto.NewsId > 0) // update => use the old HistoryNo
            {
                var data = await _newsAssistantService.GetById(newsAddDto.NewsId);
                if (data == null)
                {
                    return new ErrorDataResult<int>(0, Messages.RecordNotFound);
                }
                historyNo = data.HistoryNo.HasValue ? data.HistoryNo.Value : await _newsAssistantService.GetMaxHistoryNo() + 1;
                addUserId = data.AddUserId;
            }
            else // add => get max HistoryNo and add 1
            {
                historyNo = await _newsAssistantService.GetMaxHistoryNo() + 1;
            }

            var newsId = await _newsAssistantService.Add(newsAddDto, addUserId, historyNo);

            //if (newsAddDto.NewsId == 0 && newsAddDto.Active && !newsAddDto.IsDraft && newsAddDto.NewsPositionList.HasValue())
            //{
            //    if (newsAddDto.NewsPositionList.Any(f => f.PositionEntityId == (int)Entity.Enums.NewsPositionEntities.MainHeadingNews) &&
            //        !newsAddDto.NewsPositionList.Any(f => f.PositionEntityId == (int)Entity.Enums.NewsPositionEntities.MainPageNews))
            //    {
            //        await _newsPositionAssistantService.MoveLastMainHeadingNewsToMainPageNewsPosition();
            //    }
            //}

            if (newsAddDto.PushNotification)
            {
                var f = await _newsAssistantService.GetViewById(newsId);
                await _pushNotificationService.NewsAdded(f);
            }

            return new SuccessDataResult<int>(newsId, Messages.Added);
        }

        [SecuredOperation("NewsDelete")]
        [LogAspect()]
        [CacheRemoveAspect("INewsPositionService.Get")]
        [CacheRemoveAspect("INewsService.Get")]
        [CacheRemoveAspect("IMainPageService.Get")]
        [CacheRemoveAspect("ICategoryPageService.Get")]
        [CacheRemoveAspect("INewsDetailPageService.Get")]
        public async Task<IResult> Delete(int newsId)
        {
            var data = await _newsAssistantService.GetById(newsId);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            data.Deleted = true;
            await _newsAssistantService.Update(data);

            await _newsPositionAssistantService.ReOrderNewsPositionOrdersByNewsId(data.Id);

            return new SuccessResult(Messages.Deleted);
        }

        [SecuredOperation("NewsUpdate")]
        [LogAspect()]
        [CacheRemoveAspect("INewsPositionService.Get")]
        [CacheRemoveAspect("INewsService.Get")]
        [CacheRemoveAspect("IMainPageService.Get")]
        [CacheRemoveAspect("ICategoryPageService.Get")]
        [CacheRemoveAspect("INewsDetailPageService.Get")]
        public async Task<IResult> ChangeActiveStatus(ChangeActiveStatusDto changeActiveStatusDto)
        {
            var data = await _newsAssistantService.GetById(changeActiveStatusDto.Id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            data.Active = changeActiveStatusDto.Active;
            await _newsPositionAssistantService.ReOrderNewsPositionOrdersByNewsId(data.Id);
            await _newsAssistantService.Update(data);
            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation("NewsUpdate")]
        [LogAspect()]
        [CacheRemoveAspect("INewsPositionService.Get")]
        [CacheRemoveAspect("INewsService.Get")]
        [CacheRemoveAspect("IMainPageService.Get")]
        [CacheRemoveAspect("ICategoryPageService.Get")]
        [CacheRemoveAspect("INewsDetailPageService.Get")]
        public async Task<IResult> ChangeIsDraftStatus(ChangeIsDraftStatusDto dto)
        {
            var data = await _newsAssistantService.GetById(dto.Id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            data.IsDraft = dto.IsDraft;
            await _newsAssistantService.Update(data);
            await _newsPositionAssistantService.ReOrderNewsPositionOrdersByNewsId(data.Id);
            return new SuccessResult(Messages.Updated);
        }

        //[SecuredOperation("NewsUpdate")]
        [LogAspect()]
        [CacheRemoveAspect("INewsPositionService.Get")]
        [CacheRemoveAspect("INewsService.Get")]
        [CacheRemoveAspect("IMainPageService.Get")]
        [CacheRemoveAspect("ICategoryPageService.Get")]
        [CacheRemoveAspect("INewsDetailPageService.Get")]
        public async Task<IResult> IncreaseShareCount(int newsId)
        {
            var data = await _newsAssistantService.GetById(newsId);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            await _newsAssistantService.IncreaseShareCount(newsId);
            return new SuccessResult(Messages.Updated);
        }

        public async Task<IDataResult<List<NewsViewDto>>> GetListByAuthorId(int authorId)
        {
            return new SuccessDataResult<List<NewsViewDto>>(await _newsAssistantService.GetListByAuthorId(authorId));
        }

        public async Task<IDataResult<List<NewsViewDto>>> GetListByReporterId(int reporterId)
        {
            return new SuccessDataResult<List<NewsViewDto>>(await _newsAssistantService.GetListByReporterId(reporterId));
        }
    }
}
