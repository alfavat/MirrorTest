using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IContactAssistantService
    {
        Task<Contact> GetById(int contactId);
        Task<List<ContactDto>> GetList();
        Task Add(Contact subscription);
        List<ContactDto> GetListByPaging(ContactPagingDto pagingDto, out int total);
        Task<ContactDto> GetViewById(int contactId);
    }
}
