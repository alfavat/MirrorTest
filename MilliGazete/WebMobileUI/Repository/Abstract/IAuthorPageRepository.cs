using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;

namespace WebMobileUI.Repository.Abstract
{
    public interface IAuthorPageRepository : IUIBaseRepository
    {
        IDataResult<List<AuthorDto>> GetAuthorList();
        IDataResult<AuthorWithDetailsDto> GetAuthorByName(string nameSurename);
    }
}
