using AutoMapper;
using Tawala.Application.Common.Attachments;
using Tawala.Application.Common.Behaviours;
using Tawala.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tawala.Application.Services.Common.IService;
using Tawala.Application.Common.UploadFilesService;
using Tawala.Application.Common.CodeEncrypt;
using Tawala.Application.Common.SendEmail;
using Takid.Application.Services.Common.Service;
using Tawala.Application.Services.UsersService;
using Tawala.Application.Common.NotificationService;
using Tawala.Application.Services.AuthService;

namespace Tawala.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            services.AddScoped<IFileConversion, FileConversion>();
            services.AddScoped<IEncryptDecryptService, EncryptDecryptService>();

            services.AddScoped<IMailService, MailService>();
            //-------------------helpers------------------

            services.AddScoped<IUploadFileService, UploadFileService>();
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<INotificationService, NotificationService>();

            //---------------User Service------------

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<JwtHandler>();

            return services;
        }
    }

}
