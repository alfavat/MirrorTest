using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IArticleService
    {
        Task<IDataResult<ArticleDto>> GetById(int articleId);
        Task<IResult> Update(ArticleUpdateDto articleUpdateDto);
        Task<IResult> Add(ArticleAddDto articleAddDto);
        Task<IResult> Delete(int articleId);
        IDataResult<List<ArticleDto>> GetListByPaging(ArticlePagingDto pagingDto, out int total);
        Task<IResult> ChangeApprovedStatus(ChangeApprovedStatusDto dto);
        Task<IDataResult<List<ArticleDto>>> GetListByAuthorId(int authorId);
        Task<IDataResult<ArticleDto>> GetByUrl(string url);
        Task<IDataResult<List<ArticleDto>>> GetLastWeekMostViewedArticles(int count);
    }
}
