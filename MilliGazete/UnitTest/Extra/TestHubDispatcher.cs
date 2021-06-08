using Core.SignalR.Abstract;
using Moq;

namespace UnitTest.Extra
{
    public class TestHubDispatcher
    {
        public IHubDispatcher _hubDispatcher;
        public TestHubDispatcher()
        {
            _hubDispatcher = MockHubDispatcher();
        }

        IHubDispatcher MockHubDispatcher()
        {
            var mockHub = new Mock<IHubDispatcher>();

            return mockHub.Object;
        }
    }
}
