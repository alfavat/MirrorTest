using Entity.Dtos;
using Entity.Models;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IOptionAssistantService
    {
        Task<Option> Get();
        Task<OptionDto> GetView();
        Task Update(Option option);
    }
}
