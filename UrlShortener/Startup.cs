using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Interfaces;
using LocalUrlStorageProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UrlShortener.Configurations;

namespace UrlShortener
{
    public class Startup
    {
        public const string ApplicationSettingsKey = "ApplicationConfiguration";

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddOptions();

            IConfigurationSection appSettings = Configuration.GetSection(ApplicationSettingsKey);
            services.Configure<ApplicationConfig>(appSettings);

            SetupStoredUrlProvider(services);
        }

        private void SetupStoredUrlProvider(IServiceCollection services)
        {
            string useLocal = Configuration[$"{ApplicationSettingsKey}:UseInMemoryUrlStorageProvider"];
            if (Convert.ToBoolean(useLocal))
            {
                services.AddSingleton<LocalUrlStoreSettings>(new LocalUrlStoreSettings() { Store = new Dictionary<string, Common.DataModels.StoredUrl>() });
                services.AddScoped<IUrlStorageProvider, LocalUrlStorageProvider.LocalUrlStorageProvider>();
                   
            }
            else
            {
                throw new NotImplementedException("Non-local url store is not implemented");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Register global exception Handling
            //must be registered before other middleware that can throw exceptions
            RegisterErrorHandling(app);

            app.UseMvc();
        }

        private static void RegisterErrorHandling(IApplicationBuilder app)
        {
            app.UseStatusCodePagesWithReExecute("/api/Error/{0}");
            app.UseExceptionHandler("/api/Error/500");
        }
    }
}
