using Business.Managers.Abstract;
using Core.Utilities.Helper.Abstract;
using DataAccess.Abstract;
using Entity.Models;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class FileAssistantManager : IFileAssistantService
    {
        private readonly IFileDal _fileDal;
        private readonly IUploadHelper _uploadHelper;
        private readonly IBaseService _baseService;
        public FileAssistantManager(IFileDal fileDal, IUploadHelper uploadHelper, IBaseService baseService)
        {
            _fileDal = fileDal;
            _uploadHelper = uploadHelper;
            _baseService = baseService;
        }

        public async Task<File> GetById(int fileId)
        {
            return await _fileDal.Get(f => f.Id == fileId && !f.Deleted);
        }

        public async Task Update(File file)
        {
            await _fileDal.Update(file);
        }

        public async Task Add(File file)
        {
            await _fileDal.Add(file);
        }

        public async Task<int> CopyFile(int fileId)
        {
            var file = await _fileDal.Get(prop => prop.Id == fileId);
            double fileSize;
            var newPath = _uploadHelper.CopyFileToDownloadsFolder(_baseService.RequestUserId + "_", file.FileName, out fileSize);

            var newFile = new File()
            {
                UserId = _baseService.RequestUserId,
                FileName = newPath,
                FileType = file.FileType,
                FileSizeKb = file.FileSizeKb,
            };
            await Add(newFile);
            return newFile.Id;
        }
    }
}
