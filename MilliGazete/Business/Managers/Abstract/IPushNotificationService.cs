using Core.Utilities.Results;
using Entity.Dtos;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IPushNotificationService
    {
        Task<IResult> NewsAdded(NewsViewDto dto);
        Task<IResult> TestNewsAdded(NewsViewDto dto, string segment);
    }
}
