using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IContactService
    {
        Task<IDataResult<ContactDto>> GetById(int contactId);
        Task<IResult> Add(ContactAddDto contactAddDto);
        Task<IDataResult<List<ContactDto>>> GetList();
        IDataResult<List<ContactDto>> GetListByPaging(ContactPagingDto pagingDto, out int total);
    }
}
