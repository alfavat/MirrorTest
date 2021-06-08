using Core.Utilities.Results;
using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface ITagService
    {
        Task<IDataResult<Tag>> GetById(int tagId);
        Task<IDataResult<int>> Update(TagUpdateDto tagUpdateDto);
        Task<IDataResult<int>> Add(TagAddDto tagAddDto);
        Task<IResult> Delete(int tagId);
        Task<IDataResult<List<TagDto>>> GetList();
        IDataResult<List<TagDto>> GetListByPaging(TagPagingDto pagingDto, out int total);
        Task<IDataResult<List<TagDto>>> SearchByTagName(string tagName);
        Task<IResult> ChangeActiveStatus(ChangeActiveStatusDto changeActiveStatusDto);
    }
}
