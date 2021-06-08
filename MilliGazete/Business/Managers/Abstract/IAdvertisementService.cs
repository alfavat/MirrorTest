using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IAdvertisementService
    {
        Task<IDataResult<AdvertisementDto>> GetById(int id);
        Task<IResult> Update(AdvertisementUpdateDto dto);
        Task<IResult> Add(AdvertisementAddDto dto);
        Task<IResult> Delete(int id);
        Task<IResult> ChangeActiveStatus(ChangeActiveStatusDto changeActiveStatusDto);
        Task<IDataResult<List<AdvertisementDto>>> GetList();
        Task<IDataResult<List<AdvertisementDto>>> GetActiveList();
    }
}
