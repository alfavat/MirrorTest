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
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class NewspaperManager : INewspaperService
    {
        private readonly INewspaperAssistantService _newspaperAssistantService;
        private readonly IMapper _mapper;
        private readonly IUploadHelper _uploadHelper;
        private readonly IFileService _fileService;
        private readonly IBaseService _baseService;

        public NewspaperManager(INewspaperAssistantService newspaperAssistantService, IMapper mapper, IUploadHelper uploadHelper,
            IFileService fileService, IBaseService baseService)
        {
            _newspaperAssistantService = newspaperAssistantService;
            _mapper = mapper;
            _uploadHelper = uploadHelper;
            _fileService = fileService;
            _baseService = baseService;
        }

        [PerformanceAspect()]
        public async Task<IDataResult<List<NewspaperDto>>> GetTodayList()
        {
            return new SuccessDataResult<List<NewspaperDto>>(await _newspaperAssistantService.GetTodayList());
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<NewspaperDto>> GetByName(string name)
        {
            var data = await _newspaperAssistantService.GetViewByName(name);
            if (data == null)
            {
                return new ErrorDataResult<NewspaperDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<NewspaperDto>(data);
        }

        [SecuredOperation("Service")]
        [ValidationAspect(typeof(NewspaperAddDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("INewspaperService.Get")]
        public async Task<IResult> AddArray(List<NewspaperAddDto> list)
        {
            if (!list.HasValue())
                return new ErrorResult(Messages.EmptyParameter);
            foreach (var item in list)
            {
                try
                {
                    if (_uploadHelper.FileExists(item.Name) || item.MainImageUrl.StringIsNullOrEmpty())
                    {
                        continue;
                    }
                    var data = _mapper.Map<Newspaper>(item);
                    var mainFile = await AddFile(item.MainImageUrl, item.Name, false);
                    data.MainImageFileId = mainFile.Id;

                    if (item.ThumbnailUrl.StringNotNullOrEmpty())
                    {
                        var thumbnailFile = await AddFile(item.ThumbnailUrl, item.Name, true);
                        data.ThumbnailFileId = thumbnailFile.Id;
                    }
                    await _newspaperAssistantService.Add(data);
                }
                catch (System.Exception ec)
                {
                }
            }
            return new SuccessResult(Messages.Added);
        }

        private async Task<File> AddFile(string imageUrl, string title, bool isThumbnail)
        {
            var url = _uploadHelper.DownloadNewspaperImage(imageUrl, (isThumbnail ? "thumbnail_" : "main_") + title.ToEnglishStandardUrl());
            var file = new File
            {
                CreatedAt = System.DateTime.Now,
                FileName = url,
                FileType = System.IO.Path.GetExtension(url),
                UserId = _baseService.RequestUserId
            };
            await _fileService.Add(file);
            return file;
        }
    }
}
