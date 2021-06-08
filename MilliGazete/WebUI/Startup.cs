using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using WebUI.Helper;
using WebUI.Models;
using WebUI.Repository.Abstract;
using WebUI.Services;

namespace WebUI
{

    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                //options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter());
                //options.JsonSerializerOptions.Converters.Add(new DateTimeToStringConverter());
                //options.JsonSerializerOptions.Converters.Add(new NullableDateTimeToStringConverter());
            });

            services.AddControllersWithViews(options => options.Filters.Add(typeof(ActionFilter)));

            CultureInfo[] supportedCultures = new[] { new CultureInfo("tr") };
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("tr");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider>
        {
           // new QueryStringRequestCultureProvider(),
            new CookieRequestCultureProvider()
        };
            });

            //var origins = Configuration.GetSection("Origins").Get<string[]>();
            //services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            //{
            //    builder
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .AllowCredentials()
            //    .WithOrigins(origins);
            //}));

            //services.AddSignalR(hubOptions =>
            //{
            //    hubOptions.EnableDetailedErrors = true;
            //    //hubOptions.ClientTimeoutInterval = TimeSpan.FromHours(6);
            //    //hubOptions.HandshakeTimeout = TimeSpan.FromMinutes(30);
            //}).AddJsonProtocol(options =>
            //{
            //    options.PayloadSerializerOptions.WriteIndented = false;
            //});
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            //Karakter Türkçe Kodlamasý Ýçin:
            services.AddSingleton(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA }));
            //services.AddSingleton<LocalizationService>();

            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.Scan(scan => scan
              .FromAssemblyOf<IMainPageRepository>()
              .AddClasses()
              .AsImplementedInterfaces()
              .WithScopedLifetime());
            services.Scan(scan => scan
             .FromAssemblyOf<ITokenHelper>()
             .AddClasses()
             .AsImplementedInterfaces()
             .WithScopedLifetime());

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMainPageRepository mainPageRepository, IOptionRepository optionRepository) //, IUserService userService
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //app.UseHsts();
                //app.UseHttpsRedirection();
            }

            //app.UseErrorWrappingMiddleware();

            //app.UseCors("CorsPolicy");

            app.UseStaticFiles();

            app.UseRouting();

            //app.UseMiddleware<WebSocketsMiddleware>();

            app.UseMiddleware<MiddlewareRedirect>();

            app.UseAuthentication();

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });




            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context => await context.Response.WriteAsync("Have A Nice Day :)"));
                //endpoints.MapHub<MilliGazeteHub>("/MilliGazeteHub");
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //userService.FillPassiveUserList();
            LayoutModel.LoadLayoutData(Configuration, mainPageRepository, optionRepository);


        }
    }



}
