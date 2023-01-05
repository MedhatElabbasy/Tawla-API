using Tawala.Infrastructure.Common.Interfaces;
using Tawala.Infrastructure.Identity;
using Tawala.Infrastructure.Persistence;
using Tawala.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;
using Tawala.Domain.Entities.Identity;
using Tawala.Infrastructure.Repository;

namespace Tawala.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            //{
            //    services.AddDbContext<ApplicationDbContext>(options =>
            //        options.UseInMemoryDatabase("TawalaDb"));
            //}
            //else
            //{
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                    , b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                    ));
            //}

            //services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            services
                .AddIdentity<AppUser, AppIdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();




            //services.AddIdentity<AppUser, IdentityRole>(config =>
            //{
            //    config.SignIn.RequireConfirmedEmail = true;
            //    config.Tokens.EmailConfirmationTokenProvider = "emailConfirmation";
            //    config.Tokens.PasswordResetTokenProvider = "passwordReset";
            //    config.Tokens.ChangePhoneNumberTokenProvider = "ChangePhoneNumberTokenProvider";
            //    config.Password.RequiredLength = 0;
            //    config.Password.RequiredUniqueChars = 0;
            //    config.Password.RequireLowercase = false;
            //    config.Password.RequireUppercase = false;
            //    config.Password.RequireDigit = false;
            //    config.Password.RequireNonAlphanumeric = false;
            // //   config.User.RequireUniqueEmail = true;
            //    //config.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+'#!/^%{}*";
            //})
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //     .AddTokenProvider<CustomEmailConfirmationTokenProvider<AppUser>>("ChangePhoneNumberTokenProvider")
            //     .AddTokenProvider<CustomEmailConfirmationTokenProvider<AppUser>>("PhoneCode")

            //    .AddDefaultTokenProviders();


            //services.AddTransient<EmailConfirmationTokenProviderOptions>();
            //services.AddTransient<Phonetest>();





            var jwtSettings = configuration.GetSection("MyConfig:JwtSettings");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value)),
                    ClockSkew = TimeSpan.FromMinutes(3)
                };
            });

            //services.AddAuthentication()
            //    .AddIdentityServerJwt();

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            //});
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IDateTime, DateTimeService>(); 
            //adding efcore repository
            services.AddScoped(typeof(IRepository<>), typeof(EFCoreRepository<>));
            services.AddScoped(typeof(IRepositoryBase<>), typeof(EFRepositoryBase<>));
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddScoped<MyConfigService>();

            return services;
        }
    }

    public class CustomEmailConfirmationTokenProvider<TUser>
                                       : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public CustomEmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider,
            IOptions<EmailConfirmationTokenProviderOptions> options,
            ILogger<DataProtectorTokenProvider<TUser>> logger)
                                              : base(dataProtectionProvider, options, logger)
        {

        }
    }
    public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public EmailConfirmationTokenProviderOptions()
        {
            Name = "EmailDataProtectorTokenProvider";
            TokenLifespan = TimeSpan.FromSeconds(10);

        }
    }

    public class Phonetest : DataProtectionTokenProviderOptions
    {
        public Phonetest()
        {
            Name = "PhoneCode";
            TokenLifespan = TimeSpan.FromSeconds(10);

        }
    }
}