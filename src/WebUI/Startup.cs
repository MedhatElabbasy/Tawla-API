using Tawala.Application;
using Tawala.Domain.Common;
using Tawala.Infrastructure;
using Tawala.Infrastructure.Common.Interfaces;
using Tawala.Infrastructure.Persistence;
using Tawala.WebUI.Filters;
using Tawala.WebUI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using System.Text;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Identity;
using Tawala.Application.Common.RealTime.Hubs;
using Tawala.Application.Common.SendEmail;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Tawala.Application.Services.UsersService;
using Tawala.Domain.Entities.Identity;
using AutoMapper;
using System.Reflection;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.EntityFrameworkCore;
using System;

namespace Tawala.WebUI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add our Config object so it can be injected
            services.Configure<MyConfig>(Configuration.GetSection("MyConfig"));
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            services.AddInfrastructure(Configuration);
            services.AddApplication();
            // services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddCors();

            services.AddSignalR();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();


            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            services.AddControllersWithViews(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>());

            services.AddControllersWithViews(options =>
            options.Filters.Add<ApiGeneralFilter>());

            services.AddRazorPages();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddSwaggerDocument();
            services.AddSwaggerDocument(config =>
            {
                config.DocumentName = "OpenAPI 2";
                config.Title = "Tawala Api";
                config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
                config.AddSecurity("JWT Token", Enumerable.Empty<string>(),
                    new OpenApiSecurityScheme()
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Description = "Copy this into the value field: Bearer {token}"
                    }
                );
            });


            ////for fluent validation as filter
            //services.AddMvc(opt =>
            //{
            //    opt.Filters.Add(typeof(ValidatorActionFilter));
            //}).AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());



            services.AddControllers()
               .AddNewtonsoftJson(options =>
                       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);



            services.AddControllers().AddNewtonsoftJson(options =>
            {
                var dateConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter
                {
                    DateTimeFormat = "dd'-'MM'-'yyyy HH':'mm:ss"
                    // DateTimeFormat = "dd'-'MM'-'yyyy"
                };
                options.SerializerSettings.Converters.Add(dateConverter);
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });


            services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);



            services.AddDbContext<ApplicationDbContext>(options =>
              {

                  options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
              });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                app.UseHsts();
            }

            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseOpenApi();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/ChatHub");
            });
        }
    }
}
