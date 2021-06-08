using DataAccess.Base;
using Entity.Abstract;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UnitTest.DAL
{
    public class TestBaseRespository<T> where T : class, IEntity, new()
    {
        public static readonly int dataCount = 12;
        public IEntityRepository<T> _baseRepository;
        List<T> _list;
        public TestBaseRespository(List<T> list)
        {
            _list = list;
            _baseRepository = MockBaseRepositoryDal();
        }

        public IEntityRepository<T> MockBaseRepositoryDal()
        {
            var dal = new Mock<IEntityRepository<T>>();

            dal.Setup(r => r.Get(It.IsAny<Expression<Func<T, bool>>>()))
                .Returns(new Func<Expression<Func<T, bool>>, T>((filter)
                 => _list.AsQueryable().FirstOrDefault(filter)));

            dal.Setup(r => r.GetList(It.IsAny<Expression<Func<T, bool>>>()))
                .Returns(new Func<Expression<Func<T, bool>>, IQueryable<T>>((filter)
                 => filter == null ? _list.AsQueryable() : _list.AsQueryable().Where(filter)));

            dal.Setup(r => r.Add(It.IsAny<T>()))
                .Callback(new Action<T>(content
                => _list.Add(content)
                ));

            dal.Setup(r => r.Delete(It.IsAny<T>()))
                .Callback(new Action<T>(content
                => _list.Remove(content)
                ));

            dal.Setup(r => r.Update(It.IsAny<T>()))
                .Callback(new Action<T>(content
                =>
                {
                    var oldContent = _list.Find(f => f == content);
                    if (oldContent != null)
                        _list.Remove(content);
                    _list.Add(content);
                }
                ));

            dal.Setup(r => r.RemoveRange(It.IsAny<List<T>>()))
                .Callback(new Action<IList<T>>(T =>
                T.ToList().ForEach(f =>
                {
                    _list.Remove(f);
                })
                ));

            return dal.Object;
        }
    }
}
