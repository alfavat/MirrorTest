using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IAuthorAssistantService
    {
        Task<Author> GetById(int authorId);
        Task<AuthorDto> GetViewById(int id);
        Task Update(Author author);
        Task Delete(Author author);
        Task<List<AuthorDto>> GetList();
        Task Add(Author author);
        Task<Author> CheckAuthor(AuthorAddDto authorAddDto);
        Task<AuthorWithDetailsDto> GetViewByName(string nameSurename);
        Task<AuthorWithDetailsDto> GetViewByUrl(string url);
    }
}
