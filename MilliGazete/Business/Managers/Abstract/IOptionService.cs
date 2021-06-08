using Core.Utilities.Results;
using Entity.Dtos;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IOptionService
    {
        Task<IDataResult<OptionDto>> Get();
        Task<IResult> Update(OptionUpdateDto optionUpdateDto);
    }
}
