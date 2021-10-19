using Microsoft.Extensions.DependencyInjection;
using System;

namespace Entity
{
    public static class EntityServiceTool
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
