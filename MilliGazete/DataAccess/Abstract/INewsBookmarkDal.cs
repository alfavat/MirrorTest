using DataAccess.Base;
using Entity.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface INewsBookmarkDal : IEntityRepository<NewsBookmark>
    {
        IQueryable<NewsBookmark> GetNewsBookmarks(Expression<Func<NewsBookmark, bool>> filter);
    }
}
