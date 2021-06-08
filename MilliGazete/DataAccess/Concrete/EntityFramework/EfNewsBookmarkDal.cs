using DataAccess.Abstract;
using DataAccess.Base;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfNewsBookmarkDal : EfEntityRepositoryBase<NewsBookmark>, INewsBookmarkDal
    {
        public EfNewsBookmarkDal(MilliGazeteDbContext milligazeteDb) : base(milligazeteDb)
        {
        }

        public IQueryable<NewsBookmark> GetNewsBookmarks(Expression<Func<NewsBookmark, bool>> filter)
        {
            return Db.NewsBookmark.Where(filter);
        }
    }
}
