using AutoMapper;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Managers.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Helper.Abstract;
using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class AgencyNewsManager : IAgencyNewsService
    {
        private readonly IAgencyNewsAssistantService _agencyNewsAssistantService;
        private readonly IBaseService _baseService;
        private readonly IUploadHelper _uploadHelper;
        private readonly IMapper _mapper;
        private readonly INewsService _newsService;

        public AgencyNewsManager(IAgencyNewsAssistantService agencyNewsAssistantService, IBaseService baseService,
            IUploadHelper uploadHelper, IMapper mapper, INewsService newsService)
        {
            _agencyNewsAssistantService = agencyNewsAssistantService;
            _baseService = baseService;
            _uploadHelper = uploadHelper;
            _mapper = mapper;
            _newsService = newsService;
        }

        [SecuredOperation("NewsAgencyGet")]
        [PerformanceAspect()]
        public IDataResult<List<AgencyNewsViewDto>> GetListByPaging(NewsAgencyPagingDto pagingDto, out int total)
        {
            return new SuccessDataResult<List<AgencyNewsViewDto>>(_agencyNewsAssistantService.GetListByPaging(pagingDto, out total));
        }

        [SecuredOperation("NewsAgencyGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<AgencyNewsViewDto>>> GetList()
        {
            return new SuccessDataResult<List<AgencyNewsViewDto>>(await _agencyNewsAssistantService.GetList());
        }

        [SecuredOperation("NewsAgencyGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<AgencyNewsViewDto>> GetById(int id)
        {
            var agencyNews = await _agencyNewsAssistantService.GetById(id);
            if (agencyNews == null)
            {
                return new ErrorDataResult<AgencyNewsViewDto>(Messages.RecordNotFound);
            }
            var data = _mapper.Map<AgencyNewsViewDto>(agencyNews);
            return new SuccessDataResult<AgencyNewsViewDto>(data);
        }

        [SecuredOperation("NewsAgencyAdd")]
        [ValidationAspect(typeof(NewsAgencyValidator))]
        [CacheRemoveAspect("IAgencyNewsService.Get")]
        public async Task<IResult> AddArray(List<NewsAgencyAddDto> data)
        {
            var agencyId = (int)data.First().NewsAgencyEntityId;
            await _agencyNewsAssistantService.DeleteAllByAgencyId(agencyId);
            await _agencyNewsAssistantService.AddArray(data);
            return new SuccessResult(Messages.Added);
        }

        [SecuredOperation("NewsAgencyAdd")]
        [ValidationAspect(typeof(AgencyNewsCopyDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IAgencyNewsService.Get")]
        public async Task<IDataResult<int>> CopyNewsFromAgencyNews(AgencyNewsCopyDto agencyNewsDto)
        {
            var agencyNews = agencyNewsDto.Code.StringNotNullOrEmpty() ?
                await _agencyNewsAssistantService.GetByCode(agencyNewsDto.Code) :
                await _agencyNewsAssistantService.GetById(agencyNewsDto.AgencyNewsId);
            if (agencyNews == null)
            {
                return new ErrorDataResult<int>(Messages.RecordNotFound);
            }
            var fileList = new List<NewsFileAddDto>();
            var positionList = new List<NewsPositionAddDto>();

            if (agencyNewsDto.NewsPositionEntityIdList.HasValue())
            {
                positionList.AddRange(agencyNewsDto.NewsPositionEntityIdList.Select(id => new NewsPositionAddDto
                {
                    Order = 0,
                    PositionEntityId = id,
                    Value = true
                }));
            }

            if (agencyNewsDto.NewsFileList.HasValue())
            {
                foreach (var f in agencyNewsDto.NewsFileList)
                {
                    var agencyFile = await _agencyNewsAssistantService.GetAgencyNewsFileById(f.AgencyNewsFileId);

                    if (agencyFile != null && !agencyFile.FileId.HasValue && agencyFile.Link.StringNotNullOrEmpty() && agencyFile.FileType.StringNotNullOrEmpty())
                    {
                        string filePath = agencyFile.FileType.ToLower().Trim() == "image" ? _uploadHelper.DownloadNewsImage(agencyFile.Link) :
                                agencyFile.FileType.ToLower().Trim() == "video" ? _uploadHelper.DownloadNewsVideo(agencyFile.Link) : "";

                        if (filePath.StringNotNullOrEmpty())
                        {
                            foreach (var newsFileTypeEntityId in f.NewsFileTypeEntityIdList)
                            {
                                var file = new Entity.Models.File()
                                {
                                    UserId = _baseService.RequestUserId,
                                    FileName = filePath,
                                    FileType = filePath.GetFileExtension(),
                                    FileSizeKb = filePath.GetFileSize()
                                };
                                await _agencyNewsAssistantService.AddFile(file);
                                fileList.Add(new NewsFileAddDto
                                {
                                    Description = agencyFile.Description,
                                    FileId = file.Id,
                                    NewsFileTypeEntityId = newsFileTypeEntityId,
                                    Order = 0,
                                    Title = null,
                                    VideoCoverFileId = null
                                });
                            }
                        }
                    }
                }
            }

            var dto = _mapper.Map<NewsAddDto>(agencyNews);
            dto.NewsFileList = fileList;
            dto.NewsPositionList = positionList;
            dto.UserId = _baseService.RequestUserId;
            dto.NewsId = 0;
            dto.NewsPropertyList = await _agencyNewsAssistantService.GetNewsPropertyEntities();
            if (agencyNewsDto.NewsTypeEntityId.HasValue)
            {
                dto.NewsTypeEntityId = agencyNewsDto.NewsTypeEntityId;
            }

            return await _newsService.Add(dto);
        }
    }
}
