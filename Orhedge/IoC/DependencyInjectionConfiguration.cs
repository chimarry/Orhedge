using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Orhedge.AutoMapper;
using ServiceLayer.Common.Interfaces;
using ServiceLayer.Common.Services;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.Services.Student;
using System;

namespace Orhedge.IoC
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceProvider Configure(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterType<StudentService>().InstancePerLifetimeScope().As<IStudentService>();
            builder.RegisterType<LocalDocumentService>().InstancePerLifetimeScope().As<IDocumentService>();
            builder.RegisterType<StudyMaterialService>().InstancePerLifetimeScope().As<IStudyMaterialService>();
            builder.RegisterType<CourseService>().InstancePerLifetimeScope().As<ICourseService>();
            builder.RegisterType<CategoryService>().InstancePerLifetimeScope().As<ICategoryService>();
            builder.RegisterType<ServiceLayer.ErrorHandling.ErrorHandler>().InstancePerLifetimeScope().As<ServiceLayer.ErrorHandling.IErrorHandler>();
            builder.RegisterGeneric(typeof(ServiceExecutor<,>)).InstancePerLifetimeScope().As(typeof(IServicesExecutor<,>));
            builder.RegisterType<RegistrationService>().InstancePerLifetimeScope().As<IRegistrationService>();
            builder.RegisterType<EmailSenderService>().InstancePerLifetimeScope().As<IEmailSenderService>();
            builder.RegisterType<StudentManagmentService>().InstancePerLifetimeScope().As<IStudentManagmentService>();
            builder.RegisterType<AuthenticationService>().InstancePerLifetimeScope().As<IAuthenticationService>();
            builder.RegisterType<StudyMaterialMenagementService>().InstancePerLifetimeScope().As<IStudyMaterialManagementService>();
            builder.RegisterType<ChatMessageService>().InstancePerLifetimeScope().As<IChatMessageService>();
            builder.RegisterType<ProfileImageService>().InstancePerLifetimeScope().As<IProfileImageService>();
            builder.RegisterType<CourseCategoryManagementService>().InstancePerLifetimeScope().As<ICourseCategoryManagementService>();

            // IMapper is thread safe, hence we register it as singleton
            builder.Register(ctx => MappingConfiguration.CreateMapping());
            IContainer container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }
    }
}
