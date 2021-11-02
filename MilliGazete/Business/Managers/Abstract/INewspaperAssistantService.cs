using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewspaperAssistantService
    {
        Task<Newspaper> GetById(int id);
        Task<NewspaperDto> GetViewById(int id);
        Task Update(Newspaper item);
        Task Delete(Newspaper item);
        Task<List<NewspaperDto>> GetTodayList();
        Task Add(Newspaper item);
        Task<NewspaperDto> GetViewByName(string name);
    }
}
