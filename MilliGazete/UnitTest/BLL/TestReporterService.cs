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
    public class TestReporterService : TestReporterDal
    {

        #region setup
        private readonly IReporterService _reporterService;
        public TestReporterService()
        {
            var _mapper = new TestAutoMapper()._mapper;
            _reporterService = new ReporterManager(new ReporterAssistantManager(reporterDal, _mapper), _mapper);
        }
        #endregion

        #region methods

        [Fact(DisplayName = "Add")]
        [Trait("Reporter", "Add")]
        public async Task ServiceShouldAddNewReporterToList()
        {
            // arrange
            var reporter = new ReporterAddDto()
            {
                FullName = "Test add reporter",
                ProfileImageId = 1
            };
            // act
            var result = await _reporterService.Add(reporter);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            var newReporter = db.Reporters.FirstOrDefault(f => f.FullName == reporter.FullName);
            Assert.NotNull(newReporter);
            Assert.Equal(result.Message, Messages.Added);

        }

        [Fact(DisplayName = "Update")]
        [Trait("Reporter", "Edit")]
        public async Task ServiceShouldUpdateReporter()
        {
            // arrange
            var reporter = db.Reporters.FirstOrDefault(f => !f.Deleted);
            var dto = new ReporterUpdateDto
            {
                Id = reporter.Id,
                FullName = "Test update",
                ProfileImageId = 2
            };
            // act
            var result = await _reporterService.Update(dto);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Message, Messages.Updated);
            var updatedReporter = db.Reporters.FirstOrDefault(f => f.Id == reporter.Id);
            Assert.Equal(updatedReporter.FullName, dto.FullName);
            Assert.Equal(updatedReporter.ProfileImageId, dto.ProfileImageId);
        }


        #endregion
    }
}
