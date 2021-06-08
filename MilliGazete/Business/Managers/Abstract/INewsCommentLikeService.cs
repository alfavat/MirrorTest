using Core.Utilities.Results;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsCommentLikeService
    {
        Task<IResult> AddOrDelete(int newsCommentId);
    }
}
