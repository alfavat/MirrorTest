using AutoMapper;
using Business.Constants;
using Business.Managers.Abstract;
using Business.Managers.Concrete;
using Entity.Dtos;
using Entity.Enums;
using System.Linq;
using System.Threading.Tasks;
using UnitTest.DAL;
using UnitTest.Extra;
using Xunit;
namespace UnitTest.BLL
{
    public class TestNewsHitService : TestNewsHitDal
    {
        private readonly IMapper _mapper;
        #region setup
        private readonly INewsHitService _newsHitService;
        public TestNewsHitService()
        {
            _mapper = new TestAutoMapper()._mapper;
            _newsHitService = new NewsHitManager(
                new NewsHitAssistantManager(newsHitDal, _mapper), _mapper, new TestBaseService()._baseService);
        }

        #endregion

        #region methods



        [Fact(DisplayName = "GetList")]
        [Trait("NewsHit", "Get")]
        public async Task ServiceShouldReturnNewsHitList()
        {
            // arrange
            // act
            var result = await _newsHitService.GetList();
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Count, db.NewsHit.Count());
        }

        [Fact(DisplayName = "Add")]
        [Trait("NewsHit", "Add")]
        public async Task ServiceShouldAddNewNewsCommentToList()
        {
            // arrange
            var hit = new NewsHitAddDto()
            {
                NewsHitComeFromEntityId = (int)NewsHitEntities.SocialMediaRedirect,
                NewsId = 1
            };
            // act
            var result = await _newsHitService.AddWithDetail(hit);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Added);
            var d = await _newsHitService.GetList();
            Assert.Contains(d.Data, f => f.NewsId == hit.NewsId && f.NewsHitComeFromEntityId == hit.NewsHitComeFromEntityId);
        }

        #endregion
    }
}
