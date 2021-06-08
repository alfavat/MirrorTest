using Entity.Models;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IFileAssistantService
    {
        Task<File> GetById(int fileId);
        Task Update(File file);
        Task Add(File file);
    }
}
