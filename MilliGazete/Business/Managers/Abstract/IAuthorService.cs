using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IAuthorService
    {
        Task<IDataResult<AuthorDto>> GetById(int authorId);
        Task<IResult> Update(AuthorUpdateDto authorUpdateDto);
        Task<IResult> Add(AuthorAddDto authorAddDto);
        Task<IResult> Delete(int authorId);
        Task<IDataResult<List<AuthorDto>>> GetList();
        Task<IDataResult<AuthorWithDetailsDto>> GetByName(string nameSurename);
    }
}
