using AutoMapper;
using Business.Managers.Abstract;
using Business.Managers.Concrete;
using System.Linq;
using System.Threading.Tasks;
using UnitTest.DAL;
using UnitTest.Extra;
using Xunit;
namespace UnitTest.BLL
{
    public class TestOperationClaimService : TestOperationClaimDal
    {
        private readonly IMapper _mapper;
        #region setup
        private readonly IOperationClaimService _operationClaimService;
        public TestOperationClaimService()
        {
            _mapper = new TestAutoMapper()._mapper;
            _operationClaimService = new OperationClaimManager(
                new OperationClaimAssistantManager(operationClaimDal,_mapper), _mapper);
        }

        #endregion

        #region methods



        [Fact(DisplayName = "GetList")]
        [Trait("OperationClaim", "Get")]
        public async Task ServiceShouldReturnOperationClaimListAsync()
        {
            // arrange
            // act
            var result = await _operationClaimService.GetList();
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Count, db.OperationClaim.Count());
        }


        #endregion
    }
}
