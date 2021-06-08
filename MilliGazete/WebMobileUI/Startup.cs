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
using WebMobileUI.Helper;
using WebMobileUI.Models;
using WebMobileUI.Repository.Abstract;
using WebMobileUI.Services;

namespace WebMobileUI
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
            services.AddLocalization(config => { config.ResourcesPath = "Resources"; });

            services.AddControllers().AddJsonOptions(options =>
            {
                // options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter());
                // options.JsonSerializerOptions.Converters.Add(new DateTimeToStringConverter());
                //options.JsonSerializerOptions.Converters.Add(new NullableDateTimeToStringConverter());
            });

            //var sqlConnectionString = Configuration["DevConnectionString"];
            //services.AddDbContext<MilliGazeteDbContext>(options => options.UseNpgsql(sqlConnectionString));

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

            //var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidIssuer = tokenOptions.Issuer,
            //            ValidAudience = tokenOptions.Audience,
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
            //            ClockSkew = TimeSpan.Zero
            //        };
            //    });

            //services.AddDependencyResolvers(new ICoreModule[]
            //{
            //    new CoreModule()
            //});
            //var profiles = typeof(AutoMapperConfiguration).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
            //foreach (var profile in profiles)
            //{
            //    services.AddAutoMapper(c => c.AddProfile(Activator.CreateInstance(profile) as Profile));
            //}
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            //Karakter Türkçe Kodlamasý Ýçin:
            services.AddSingleton(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA }));

            services.AddRazorPages().AddRazorRuntimeCompilation(); ;

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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMainPageRepository mainPageRepository, IOptionRepository optionRepository)//IUserService userService)
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

            app.UseAuthorization();

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
