using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PTI.Microservices.Library.Configuration;
using PTI.Microservices.Library.Interceptors;
using PTI.Microservices.Library.Services;
using PTI.Microservices.Library.Services.Specialized;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSample.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var rapidApiKey = Configuration.GetValue<string>("RapidAPIKey");
            GlobalPackageConfiguration.RapidApiKey = rapidApiKey;
            var azureMapsConfiguration =
                Configuration.GetSection($"AzureConfiguration:{nameof(AzureMapsConfiguration)}")
                .Get<AzureMapsConfiguration>();
            services.AddSingleton(azureMapsConfiguration);

            var azureSpeechConfiguration =
                Configuration.GetSection($"AzureConfiguration:{nameof(AzureSpeechConfiguration)}")
                .Get<AzureSpeechConfiguration>();
            services.AddSingleton(azureSpeechConfiguration);
            services.AddTransient<AzureSpeechService>();

            services.AddTransient<CustomHttpClientHandler>();
            services.AddTransient<CustomHttpClient>();
            services.AddTransient<AzureMapsService>();
            services.AddTransient<AudibleWeatherService>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler(options =>
                {
                    options.Run(async context =>
                    {
                        var errorFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                        await Task.Yield();
                    });
                });
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
