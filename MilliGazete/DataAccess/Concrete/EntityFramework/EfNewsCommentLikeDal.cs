using DataAccess.Abstract;
using DataAccess.Base;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfNewsCommentLikeDal : EfEntityRepositoryBase<NewsCommentLike>, INewsCommentLikeDal
    {
        public EfNewsCommentLikeDal(MilliGazeteDbContext milligazeteDb) : base(milligazeteDb)
        {
        }

        public async Task AddOrUpdate(int newsCommentId, int userId)
        {
            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    var like = Db.NewsCommentLikes.FirstOrDefault(f => f.NewsCommentId == newsCommentId && f.UserId == userId);
                    if (like != null)
                    {
                        Db.NewsCommentLikes.Remove(like);
                    }
                    else
                    {
                        Db.NewsCommentLikes.Add(new NewsCommentLike
                        {
                            UserId = userId,
                            NewsCommentId = newsCommentId,
                            CreatedAt = DateTime.Now,
                            IsLike = true
                        });
                    }
                    Db.SaveChanges();
                    var comment = Db.NewsComments.FirstOrDefault(f => f.Id == newsCommentId && !f.Deleted);
                    if (comment != null)
                    {
                        comment.TotalLikeCount = Db.NewsCommentLikes.Count(f => f.NewsCommentId == newsCommentId);
                        Db.SaveChanges();
                    }
                    await transaction.CommitAsync();
                }
                catch (Exception ec)
                {
                    await transaction.RollbackAsync();
                    throw ec;
                }
            }
        }
    }
}
