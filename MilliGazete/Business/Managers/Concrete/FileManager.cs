using AutoMapper;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Managers.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.File;
using Core.Utilities.Helper.Abstract;
using Core.Utilities.Results;
using Entity.Dtos;
using Entity.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class FileManager : IFileService
    {
        private readonly IFileAssistantService _fileAssistantService;
        private readonly IMapper _mapper;
        private readonly IFileHelper _fileHelper;
        private readonly IUploadHelper _uploadHelper;
        private readonly IBaseService _baseService;
        public FileManager(IFileAssistantService fileAssistantService, IMapper mapper, IFileHelper fileHelper, IUploadHelper uploadHelper, IBaseService baseService)
        {
            _fileAssistantService = fileAssistantService;
            _mapper = mapper;
            _fileHelper = fileHelper;
            _uploadHelper = uploadHelper;
            _baseService = baseService;
        }

        [SecuredOperation()]
        [PerformanceAspect()]
        [CacheAspect]
        public async Task<IDataResult<File>> GetById(int fileId)
        {
            return new SuccessDataResult<File>(await _fileAssistantService.GetById(fileId));
        }

        [SecuredOperation()]
        [LogAspect()]
        [CacheRemoveAspect("IFileService.Get")]
        public async Task<IDataResult<File>> Upload(IFormFile formFile)
        {
            if (!new UploadHelper().FileTypeControl(formFile.ContentType))
            {
                return new ErrorDataResult<File>(Messages.FileTypeError);
            }
            if (!new UploadHelper().FileSizeControl(formFile.ContentType, formFile.Length))
            {
                return new ErrorDataResult<File>(Messages.FileSizeLimitError);
            }
            var filePath = _uploadHelper.UploadFile(_baseService.RequestUserId.ToString() + "_", formFile);
            if (string.IsNullOrEmpty(filePath))
            {
                return new ErrorDataResult<File>(Messages.FileUploadError);
            }
            if (new UploadHelper().IsVideo(formFile.ContentType) && !_fileHelper.OptimizeVideo(filePath))
            {
                new UploadHelper().DeleteFile(filePath);
                return new ErrorDataResult<File>(Messages.FileUploadError);
            }
            File file = new File()
            {
                UserId = _baseService.RequestUserId,
                FileName = filePath,
                FileType = formFile.ContentType.GetFileContentType(),
                FileSizeKb = formFile.Length / 1024,
            };
            await _fileAssistantService.Add(file);
            return new SuccessDataResult<File>(file, Messages.FileUploadSuccess);
        }

        [SecuredOperation()]
        [LogAspect()]
        [CacheRemoveAspect("IFileService.Get")]
        public async Task<IResult> Delete(int fileId)
        {
            var file = await _fileAssistantService.GetById(fileId);
            if (file == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }

            if (!_baseService.IsEmployee && file.UserId != _baseService.RequestUserId)
            {
                return new ErrorResult(Messages.AuthorizationDenied);
            }

            file.Deleted = true;
            await _fileAssistantService.Update(file);
            new UploadHelper().DeleteFile(file.FileName);
            return new SuccessResult(Messages.Deleted);
        }

        [SecuredOperation()]
        [LogAspect()]
        [CacheRemoveAspect("IFileService.Get")]
        public async Task<IResult> DeleteFilesById(List<int> ids)
        {
            if (ids == null)
            {
                return new ErrorResult(Messages.EmptyParameter);
            }

            foreach (var fileId in ids)
            {
                var file = await _fileAssistantService.GetById(fileId);
                if (file != null && (_baseService.IsEmployee || file.UserId == _baseService.RequestUserId))
                {
                    file.Deleted = true;
                    await _fileAssistantService.Update(file);
                    new UploadHelper().DeleteFile(file.FileName);
                }
            }

            return new SuccessResult(Messages.Deleted);
        }

        [SecuredOperation()]
        [LogAspect()]
        [CacheRemoveAspect("IFileService.Get")]
        public async Task<IDataResult<List<FileUploadResponseDto>>> UploadWithFileTypeEntityIds(FileUploadWithEntityIdsDto dto)
        {
            var list = new List<FileUploadResponseDto>();
            foreach (var fileTypeEntityId in dto.FileTypeEntityIdList)
            {
                var result = await Upload(dto.File);
                if (result.Success && result.Data != null)
                {
                    var file = _mapper.Map<FileDto>(result.Data);
                    list.Add(new FileUploadResponseDto
                    {
                        NewsFileTypeEntityId = fileTypeEntityId,
                        File = file
                    });
                }
            }
            return new SuccessDataResult<List<FileUploadResponseDto>>(list, Messages.FileUploadSuccess);
        }

        // ==================================== //
        [SecuredOperation()]
        [LogAspect()]
        [CacheRemoveAspect("IFileService.Get")]
        public async Task<IDataResult<File>> UploadBase64(TempUploadDto temp)
        {
            var filePath = _uploadHelper.UploadFileBase64(_baseService.RequestUserId.ToString() + "_", temp.Base64Str, temp.FileType);
            if (filePath.StringIsNullOrEmpty())
            {
                return new ErrorDataResult<File>(Messages.FileUploadError);
            }
            if (new UploadHelper().IsVideo(temp.FileType) && !_fileHelper.OptimizeVideo(filePath))
            {
                new UploadHelper().DeleteFile(filePath);
                return new ErrorDataResult<File>(Messages.FileUploadError);
            }
            File file = new File()
            {
                UserId = _baseService.RequestUserId,
                FileName = filePath,
                FileType = temp.FileType,
                FileSizeKb = temp.FileLength / 1024,
            };
            await _fileAssistantService.Add(file);
            return new SuccessDataResult<File>(file, Messages.FileUploadSuccess);
        }
    }
}
