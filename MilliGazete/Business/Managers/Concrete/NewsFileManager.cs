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
    public class NewsFileManager : INewsFileService
    {
        private readonly INewsFileAssistantService _newsFileAssistantService;
        private readonly IMapper _mapper;

        public NewsFileManager(INewsFileAssistantService newsFileAssistantService, IMapper mapper)
        {
            _newsFileAssistantService = newsFileAssistantService;
            _mapper = mapper;
        }

        [SecuredOperation("NewsFileGet")]
        [PerformanceAspect()]
        public IDataResult<List<NewsFileDto>> GetListByPaging(NewsFilePagingDto pagingDto, out int total)
        {
            return new SuccessDataResult<List<NewsFileDto>>(_newsFileAssistantService.GetListByPaging(pagingDto, out total));
        }

        [SecuredOperation("NewsFileGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<NewsFileDto>> GetById(int newsFileId)
        {
            var data = await _newsFileAssistantService.GetViewById(newsFileId);
            if (data == null)
            {
                return new ErrorDataResult<NewsFileDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<NewsFileDto>(data);
        }
    }
}
