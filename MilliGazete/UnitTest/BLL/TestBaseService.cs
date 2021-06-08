using Business.Managers.Abstract;
using Moq;

namespace UnitTest.BLL
{
    public class TestBaseService
    {
        public IBaseService _baseService;
        bool isEmployee { get; set; }
        bool isCustomer { get; set; }
        public TestBaseService(bool? isEmployee = null, bool? isCustomer = null)
        {
            if (isEmployee.HasValue)
                this.isEmployee = isEmployee.Value;
            if (isCustomer.HasValue)
                this.isCustomer = isCustomer.Value;
            _baseService = MockBaseService();
        }

        private IBaseService MockBaseService()
        {
            var service = new Mock<IBaseService>();

            service.Setup(f => f.IsEmployee).Returns(isEmployee);
            service.Setup(f => f.RequestUserId).Returns(1);
            service.Setup(f => f.RequestUserId).Returns(1);

            return service.Object;
        }
    }
}
