using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : MainController
    {
        private IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int fileId)
        {
            return GetResponse(await _fileService.GetById(fileId));
        }

        [HttpPost("upload")]
        [RequestFormLimits(MultipartBodyLengthLimit = 524288000)]
        [RequestSizeLimit(524288000)]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            return GetResponse(await _fileService.Upload(file));
        }

        [HttpPost("uploadwithfiletypeentityids")]
        [RequestFormLimits(MultipartBodyLengthLimit = 524288000)]
        [RequestSizeLimit(524288000)]
        public async Task<IActionResult> UploadWithFileTypeEntityIds(IFormFile file)
        {
            var fileTypeEntityIdList = Request.Headers["fileTypeEntityIdList"].ToString().Split(",").ToList().ConvertAll(int.Parse);
            return GetResponse(await _fileService.UploadWithFileTypeEntityIds(new FileUploadWithEntityIdsDto
            {
                File = file,
                FileTypeEntityIdList = fileTypeEntityIdList
            }));
        }

        [HttpPost("deletefilebyid")]
        public async Task<IActionResult> DeleteFileById(int fileId)
        {
            return GetResponse(await _fileService.Delete(fileId));
        }

        [HttpPost("deletefilesbyid")]
        public async Task<IActionResult> DeleteFilesById(List<int> ids)
        {
            return GetResponse(await _fileService.DeleteFilesById(ids));
        }
    }
}