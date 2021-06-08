using Core.Utilities.Results;

namespace WebMobileUI.Repository.Abstract
{
    public interface INewsHitRepository
    {
        IResult Add(int newsId, int newsHitComeFromEntityId);
    }
}
