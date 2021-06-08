using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Managers.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class NewsCommentLikeManager : INewsCommentLikeService
    {
        private readonly INewsCommentLikeAssistantService _newsCommentLikeAssistantService;
        private readonly IBaseService _baseService;

        public NewsCommentLikeManager(INewsCommentLikeAssistantService NewsCommentLikeAssistantService, IBaseService baseService)
        {
            _newsCommentLikeAssistantService = NewsCommentLikeAssistantService;
            _baseService = baseService;
        }

        [SecuredOperation()]
        [ValidationAspect(typeof(NewsCommentLikeAddDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("INewsCommentLikeService.Get")]
        public async Task<IResult> AddOrDelete(int newsCommentId)
        {
            await _newsCommentLikeAssistantService.AddOrUpdate(newsCommentId, _baseService.RequestUserId);
            return new SuccessResult(Messages.Updated);
        }
    }
}
