using ServerService.Abstract;
using ServerService.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ServerService.DependencyInjection
{
    public class DependencyInjectionConfigure
    {
        private readonly IServiceProvider _serviceProvider;

        public DependencyInjectionConfigure()
        {
            var services = new ServiceCollection();
            services.AddScoped<IAaNewsService, AaNewsManager>();
            services.AddScoped<IIhaNewsService, IhaNewsManager>();
            services.AddScoped<IDhaNewsService, DhaNewsManager>();
            services.AddScoped<IForeksService, ForeksManager>();
            services.AddScoped<ISiteMapService, SiteMapManager>();
            _serviceProvider = services.BuildServiceProvider();
        }
        public IServiceProvider getServiceProvider
        {
            get => _serviceProvider;
        }
    }
}
