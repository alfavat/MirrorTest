using AutoMapper;
using Business.AutoMapper;
using Business.Managers.Abstract;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Hubs;
using Core.Middlewares;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encyption;
using Core.Utilities.Security.Jwt;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using WebAPI.Helper;

namespace WebAPI
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
                options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter());
                options.JsonSerializerOptions.Converters.Add(new DateTimeToStringConverter());
                options.JsonSerializerOptions.Converters.Add(new NullableDateTimeToStringConverter());
            });

            var sqlConnectionString = Configuration["DevConnectionString"];
            services.AddDbContext<MilliGazeteDbContext>(options => options.UseNpgsql(sqlConnectionString));

            var origins = Configuration.GetSection("Origins").Get<string[]>();
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins(origins);
            }));

            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
                //hubOptions.ClientTimeoutInterval = TimeSpan.FromHours(6);
                //hubOptions.HandshakeTimeout = TimeSpan.FromMinutes(30);
            }).AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.WriteIndented = false;
            });

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule()
            });
            EntityServiceTool.Create(services);
            var profiles = typeof(AutoMapperConfiguration).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
            foreach (var profile in profiles)
            {
                services.AddAutoMapper(c => c.AddProfile(Activator.CreateInstance(profile) as Profile));
            }

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IUserService userService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseErrorWrappingMiddleware();

            app.UseCors("CorsPolicy");

            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("tr"),
            };

            app.UseRequestLocalization(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("tr-TR");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers["Access-Control-Allow-Origin"] = "*";
                },
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                RequestPath = "/Resources"
            });

            app.UseRouting();

            app.UseMiddleware<WebSocketsMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => await context.Response.WriteAsync("Have A Nice Day :)"));
                endpoints.MapControllers();
                endpoints.MapHub<MilliGazeteHub>("/MilliGazeteHub");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
                c.DocExpansion(DocExpansion.None);
            });

            userService.FillPassiveUserList();
        }
    }
}