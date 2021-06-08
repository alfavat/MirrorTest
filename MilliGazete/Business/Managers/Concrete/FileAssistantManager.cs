using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Models;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class FileAssistantManager : IFileAssistantService
    {
        private readonly IFileDal _fileDal;
        public FileAssistantManager(IFileDal fileDal)
        {
            _fileDal = fileDal;
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
    }
}
