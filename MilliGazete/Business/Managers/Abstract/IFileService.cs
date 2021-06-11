using Core.Utilities.Results;
using Entity.Dtos;
using Entity.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IFileService
    {
        Task<IDataResult<File>> Upload(IFormFile file);
        Task<IDataResult<File>> GetById(int fileId);
        Task<IResult> Delete(int fileId);
        Task<IResult> DeleteFilesById(List<int> ids);
        Task<IDataResult<List<FileUploadResponseDto>>> UploadWithFileTypeEntityIds(FileUploadWithEntityIdsDto dto);
        Task<IDataResult<File>> UploadBase64(TempUploadDto file);
    }
}
