using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsHubShared.Service;
using NewsHubShared.ViewModels;
using System;

namespace NewsHubShared
{
    public class Startup
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static void Init()
        {
            ServiceProvider = new HostBuilder()
                .ConfigureServices(ConfigureServices)
                .Build().Services;
        }

        private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            services.AddTransient<INewsApiClientService, NewsApiClientService>();
            services.AddTransient<NewsViewModel>();
            services.AddTransient<AdvancedSearchViewModel>();
        }
    }
}