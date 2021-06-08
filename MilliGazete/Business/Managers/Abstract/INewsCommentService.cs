using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsCommentService
    {
        Task<IDataResult<NewsCommentDto>> GetById(int newsCommentId);
        IDataResult<List<UserNewsCommentDto>> GetByNewsId(int newsId, int limit, int page, out int total);
        Task<IResult> Update(NewsCommentUpdateDto newsCommentUpdateDto);
        Task<IResult> Add(NewsCommentAddDto newsCommentAddDto);
        Task<IResult> Delete(int newsCommentId);
        Task<IResult> DeleteUserCommentById(int newsCommentId);
        IDataResult<List<NewsCommentDto>> GetListByPaging(NewsCommentPagingDto pagingDto, out int total);
        IDataResult<List<NewsCommentDto>> GetUserCommentListByPaging(NewsCommentPagingDto pagingDto, out int total);
        Task<IResult> ChangeApprovedStatus(ChangeApprovedStatusDto dto);
    }
}
