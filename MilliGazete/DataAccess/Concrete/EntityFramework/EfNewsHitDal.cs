using DataAccess.Abstract;
using DataAccess.Base;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Dtos;
using Entity.Models;
using System;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfNewsHitDal : EfEntityRepositoryBase<NewsHit>, INewsHitDal
    {
        public EfNewsHitDal(MilliGazeteDbContext milligazeteDb) : base(milligazeteDb)
        {
        }

        public async Task AddWithDetail(NewsHitDetailAddDto dto)
        {
            using (var trans = Db.Database.BeginTransaction())
            {
                try
                {
                    Db.NewsHits.Add(new NewsHit
                    {
                        CreatedAt = DateTime.Now,
                        NewsHitComeFromEntityId = dto.NewsHitComeFromEntityId,
                        NewsId = dto.NewsId
                    });
                    Db.NewsHitDetails.Add(new NewsHitDetail
                    {
                        CreatedAt = DateTime.Now,
                        NewsHitComeFromEntityId = dto.NewsHitComeFromEntityId,
                        NewsId = dto.NewsId,
                        IpAddress = dto.IpAddress,
                        UserId = dto.UserId
                    });
                    Db.SaveChanges();
                    await trans.CommitAsync();
                }
                catch (Exception ec)
                {
                    await trans.RollbackAsync();
                    throw ec;
                }
            }
        }
    }
}
