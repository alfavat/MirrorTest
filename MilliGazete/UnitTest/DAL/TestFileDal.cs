using DataAccess.Abstract;
using Entity.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UnitTest.DAL
{
    public class TestFileDal : TestBaseRespository<File>
    {
        static List<File> GetFileTableData()
        {
            var list = new List<File>();
            for (int i = 1; i <= dataCount; i++)
            {
                list.Add(new File()
                {
                    Id = i,
                    FileName = "File Name " + i,
                    FileSizeKb = i * 1024,
                    FileType = i % 2 == 0 ? "image" : "video"
                });
            }
            return list;
        }
        public static List<File> _list = GetFileTableData();
        public readonly IFileDal _fileDal;
        public TestFileDal() : base(_list)
        {
            _fileDal = MockFileDal();
        }

        IFileDal MockFileDal()
        {
            var dal = new Mock<IFileDal>();

            dal.Setup(f => f.GetList(It.IsAny<Expression<Func<File, bool>>>()))
               .Returns(new Func<Expression<Func<File, bool>>, List<File>>((filter)
                         => _list.AsQueryable().Where(filter).ToList()
                    ));

            dal.Setup(f => f.Get(It.IsAny<Expression<Func<File, bool>>>()))
               .Returns(new Func<Expression<Func<File, bool>>, File>((filter)
                         => _list.AsQueryable().FirstOrDefault(filter)
                    ));

            dal.Setup(f => f.Delete(It.IsAny<File>()))
             .Callback(new Action<File>((file) =>
             {
                 _list.Remove(file);
             }
                  ));

            return dal.Object;
        }
    }
}
