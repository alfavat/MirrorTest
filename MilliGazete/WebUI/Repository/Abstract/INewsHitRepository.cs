using Core.Utilities.Results;
using System.Threading.Tasks;

namespace WebUI.Repository.Abstract
{
    public interface INewsHitRepository
    {
        Task<IResult> Add(int newsId, int newsHitComeFromEntityId);
    }
}
