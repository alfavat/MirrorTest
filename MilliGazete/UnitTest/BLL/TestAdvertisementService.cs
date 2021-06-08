using Business.Constants;
using Business.Managers.Abstract;
using Business.Managers.Concrete;
using Entity.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnitTest.DAL;
using UnitTest.Extra;
using Xunit;
namespace UnitTest.BLL
{
    public class TestAdvertisementService : TestAdvertisementDal
    {

        #region setup
        private readonly IAdvertisementService _advertisementService;
        public TestAdvertisementService()
        {
            var _mapper = new TestAutoMapper()._mapper;
            _advertisementService = new AdvertisementManager(new AdvertisementAssistantManager(advertisementDal, _mapper), _mapper);
        }
        #endregion

        //[Fact(DisplayName = "test")]
        //[Trait("Advertisement", "Get")]
        //public void testuploadvideo()
        //{
        //    // arrange
        //    // act
        //    var result = new UploadHelper().DownloadNewsVideo(@"https://abonerss.iha.com.tr/download.ashx?type=video&param1=IRO_UJaHURWfYRSTYBa3A9GrgFGLQ5S_UVaXUJaLI5S_YFSTIpijA5iHMdWDUNafUBW3YJWPYdG3I1aLUVGD");
        //    // assert
        //    Assert.NotNull(result);
        //}


        #region methods
        [Fact(DisplayName = "GetActiveList")]
        [Trait("Advertisement", "Get")]
        public async Task ServiceShouldReturnActiveAdvertisementListAsync()
        {
            // arrange
            // act
            var result = await _advertisementService.GetActiveList();
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.True(result.Data.Count <= db.Advertisement.Count(f => f.Active && !f.Deleted));
        }

        [Fact(DisplayName = "GetList")]
        [Trait("Advertisement", "Get")]
        public async Task ServiceShouldReturnAdvertisementListAsync()
        {
            // arrange
            // act
            var result = await _advertisementService.GetList();
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Count, db.Advertisement.Count(f => !f.Deleted));
        }

        [Theory(DisplayName = "GetById")]
        [Trait("Advertisement", "Get")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task ServiceShouldReturnAdvertisementByIdAsync(int id)
        {
            // arrange
            // act
            var result = await _advertisementService.GetById(id);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Key, db.Advertisement.FirstOrDefault(f => f.Id == id & !f.Deleted).Key);
        }
        [Theory(DisplayName = "GetByIdError")]
        [Trait("Advertisement", "Get")]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public async Task ServiceShouldReturnErrorForNotValidAdvertisementIdAsync(int id)
        {
            // arrange
            // act
            var result = await _advertisementService.GetById(id);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal(result.Message, Messages.RecordNotFound);
        }

        [Fact(DisplayName = "Add")]
        [Trait("Advertisement", "Add")]
        public async Task ServiceShouldAddNewAdvertisementToListAsync()
        {
            // arrange
            var Advertisement = new AdvertisementAddDto()
            {
                Key = "add key",
                Description = "test desc",
                Width = 50,
                Height = 25,
                GoogleId = "add gid"
            };
            // act
            var result = await _advertisementService.Add(Advertisement);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            var newAdvertisement = db.Advertisement.FirstOrDefault(f => f.Key == Advertisement.Key);
            Assert.NotNull(newAdvertisement);
            Assert.Equal(newAdvertisement.CreatedAt.Date, DateTime.Now.Date);
            Assert.Equal(result.Message, Messages.Added);

        }

        [Fact(DisplayName = "ChangeStatus")]
        [Trait("Advertisement", "Edit")]
        public async Task ServiceShouldChangeAdvertisementStatusAsync()
        {
            // arrange
            var Advertisement = db.Advertisement.FirstOrDefault();
            var status = new ChangeActiveStatusDto()
            {
                Active = !Advertisement.Active,
                Id = Advertisement.Id
            };
            // act
            var result = await _advertisementService.ChangeActiveStatus(status);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            Assert.Equal(status.Active, db.Advertisement.FirstOrDefault(f => f.Id == Advertisement.Id).Active);
        }
        [Fact(DisplayName = "ChangeStatusError")]
        [Trait("Advertisement", "Edit")]
        public async Task ServiceShouldNotChangeAdvertisementStatusIfAdvertisementIdIsWrongAsync()
        {
            // arrange
            var status = new ChangeActiveStatusDto()
            {
                Active = true,
                Id = -1
            };
            // act
            var result = await _advertisementService.ChangeActiveStatus(status);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal(result.Message, Messages.RecordNotFound);
        }

        [Fact(DisplayName = "Update")]
        [Trait("Advertisement", "Edit")]
        public async Task ServiceShouldUpdateAdvertisementAsync()
        {
            // arrange
            var Advertisement = db.Advertisement.FirstOrDefault(f => !f.Deleted);
            var dto = new AdvertisementUpdateDto
            {
                Description = "Edited desc",
                Active = !Advertisement.Active,
                Id = Advertisement.Id,
                GoogleId = "edited gid",
                Key = "adited key",
                Height = 10,
                Width = 30
            };
            // act
            var result = await _advertisementService.Update(dto);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            var updatedAdvertisement = db.Advertisement.FirstOrDefault(f => f.Id == Advertisement.Id);
            Assert.Equal(updatedAdvertisement.Active, dto.Active);
            Assert.Equal(updatedAdvertisement.Key, dto.Key);
            Assert.Equal(updatedAdvertisement.GoogleId, dto.GoogleId);
        }


        #endregion
    }
}
