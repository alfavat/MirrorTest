using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface ITagAssistantService
    {
        Task<Tag> GetById(int tagId);
        Task Update(Tag tag);
        Task Delete(Tag tag);
        Task<List<TagDto>> GetList();
        List<TagDto> GetListByPaging(TagPagingDto pagingDto, out int total);
        Task<List<TagDto>> SearchByTagName(string tagName);
        Task Add(Tag tag);
        Task<Tag> GetByTagNameOrUrl(string tagName, string url);
    }
}
