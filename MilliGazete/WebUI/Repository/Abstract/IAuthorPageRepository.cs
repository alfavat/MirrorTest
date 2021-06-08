using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Repository.Abstract
{
    public interface IAuthorPageRepository : IUIBaseRepository
    {
        Task<IDataResult<List<AuthorDto>>> GetAuthorList();
        Task<IDataResult<AuthorWithDetailsDto>> GetAuthorByName(string nameSurename);
    }
}
