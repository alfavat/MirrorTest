using ServerService.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
namespace ServerService.Helper
{
    public class InstanceFactory
    {
        public static T GetInstance<T>()
        {
            return new DependencyInjectionConfigure().getServiceProvider.GetService<T>();
        }
    }
}
