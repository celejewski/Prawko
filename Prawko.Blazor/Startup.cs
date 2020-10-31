using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Prawko.Blazor.Configs;
using Prawko.Blazor.Middlewares;
using Prawko.Blazor.Services;
using Prawko.Core.Managers.Providers;

namespace Prawko.Blazor
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
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<GuidProvider, GuidProvider>();
            services.AddSingleton<MediaInfoLocalFileProvider>();
            services.AddTransient<IProgressStorage, LocalProgressStorageUsingGuid>();
            services.AddTransient<QuestionAccessor>();
            services.Configure<DirectoryOptions>(Configuration.GetSection("Directories"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseMiddleware<SetGuidMiddleware>();
            app.UseStaticFiles();

            var directoryOptions = new DirectoryOptions();
            Configuration.GetSection("Directories")
                .Bind(directoryOptions);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(directoryOptions.MediaAbsoluteDirectory),
                RequestPath = directoryOptions.MediaRelativeDirectory
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
