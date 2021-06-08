using DataAccess.Base;
using Entity.Dtos;
using Entity.Models;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface INewsHitDal : IEntityRepository<NewsHit>
    {
        Task AddWithDetail(NewsHitDetailAddDto dto);
    }
}
