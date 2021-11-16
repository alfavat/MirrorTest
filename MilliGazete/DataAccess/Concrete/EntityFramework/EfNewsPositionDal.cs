using DataAccess.Abstract;
using DataAccess.Base;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Dtos;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfNewsPositionDal : EfEntityRepositoryBase<NewsPosition>, INewsPositionDal
    {
        public EfNewsPositionDal(MilliGazeteDbContext milligazeteDb) : base(milligazeteDb)
        {
        }

        public async Task UpdateNewsPositions(List<NewsPositionUpdateDto> newsPositions)
        {
            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    var entityId = newsPositions.First().PositionEntityId;
                    Db.Database.ExecuteSqlRaw($"update public.news_position set \"order\"=0 where position_entity_id={entityId}");

                    newsPositions.ForEach(item =>
                    {
                        var position = Db.NewsPositions.FirstOrDefault(f => f.NewsId == item.NewsId && f.PositionEntityId == item.PositionEntityId);
                        if (position != null)
                        {
                            position.Order = item.Order;
                        }
                    });
                    Db.SaveChanges();

                    await transaction.CommitAsync();
                }
                catch (System.Exception ec)
                {
                    await transaction.RollbackAsync();
                    throw ec;
                }
            }
        }

        public async Task IncreaseNewsPositionOrdersByEntityId(int newsPositionEntityId)
        {
            var newsIds = await Db.News.AsNoTracking().Where(u => u.Active && !u.Deleted && u.Approved.Value && !u.IsDraft && u.IsLastNews).Select(u => u.Id).ToListAsync();
            var list = await Db.NewsPositions.Where(f => newsIds.Contains(f.NewsId) && f.PositionEntityId == newsPositionEntityId && f.Order > 0).OrderBy(f => f.Order).ToListAsync();
            if (list.HasValue())
            {
                list.ForEach(g => g.Order++);
                if (list.Count > 35) list.Last().Order = 0;
                var res = await Db.SaveChangesAsync();
            }
        }

        public async Task ReOrderNewsPositionOrdersByNewsId(int newsId)
        {
            var news = await Db.News.AsNoTracking().Where(u => u.Id == newsId).Include(f => f.NewsPositions).FirstOrDefaultAsync();
            //if (news.NewsPosition == null || !news.NewsPosition.Any())
            //{
            //    return;
            //}
            //var positionEntityIds = news.NewsPosition.Select(u => u.PositionEntityId);
            //var newsIds = Db.News.AsNoTracking().Where(u => u.Active && !u.Deleted && u.Id != newsId && u.Approved.Value && !u.IsDraft && u.IsLastNews).Select(u => u.Id);
            //foreach (var positionEntityId in positionEntityIds)
            //{
            //    var list = Db.NewsPositions.Where(f => newsIds.Contains(f.NewsId) && f.PositionEntityId == positionEntityId && f.Order > 0).OrderBy(f => f.Order).ToList();
            //    if (list.HasValue())
            //    {
            //        for (int i = 0; i < list.Count; i++)
            //        {
            //            if (i >= 50) list[i].Order = 0;
            //            else list[i].Order = i + 1;
            //        }
            //        Db.SaveChanges();
            //    }
            //}
        }

        public async Task MoveSixteenthNewsToMainPageNewsPosition()
        {
            var newsIds = await Db.News.AsNoTracking().Where(u => u.Active && !u.Deleted && u.Approved.Value && !u.IsDraft && u.IsLastNews).Select(u => u.Id).ToListAsync();
            var mainHeadingNewsPositions = await Db.NewsPositions.Where(f => newsIds.Contains(f.NewsId) && f.PositionEntityId == (int)Entity.Enums.NewsPositionEntities.MainHeadingNews && f.Order > 0)
                .OrderBy(f => f.Order).Take(16).ToListAsync();
            if (mainHeadingNewsPositions.HasValue() && mainHeadingNewsPositions.Count >= 16)
            {
                var mainPagePositions = await Db.NewsPositions.Where(f => newsIds.Contains(f.NewsId) && f.PositionEntityId == (int)Entity.Enums.NewsPositionEntities.MainPageNews && f.Order > 0).OrderBy(f => f.Order).ToListAsync();
                mainPagePositions.ForEach(g => g.Order++);

                var sixteenthNews = mainHeadingNewsPositions.Last();
                sixteenthNews.Order = 1;
                sixteenthNews.PositionEntityId = (int)Entity.Enums.NewsPositionEntities.MainPageNews;

                var r = await Db.SaveChangesAsync();
            }
        }
    }
}
