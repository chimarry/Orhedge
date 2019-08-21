using Autofac;
using Autofac.Extensions.DependencyInjection;
using DatabaseLayer;
using DatabaseLayer.Entity;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.DTO;
using ServiceLayer.Students.Helpers;
using ServiceLayer.Students.Interfaces;
using ServiceLayer.Students.Services;
using System;

namespace Orhedge.IoC
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceProvider Configure(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterType<OrhedgeContext>();

            builder.RegisterType<StudentService>().As<IStudentService>();
            builder.RegisterType<StudyMaterialService>().As<IStudyMaterialService>();
            builder.RegisterType<CourseService>().As<ICourseService>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<ServiceLayer.ErrorHandling.ErrorHandler>().As<ServiceLayer.ErrorHandling.IErrorHandler>();
            builder.RegisterGeneric(typeof(ServiceExecutor<,>)).As(typeof(IServicesExecutor<,>));
            var conteiner = builder.Build();
            return conteiner.Resolve<IServiceProvider>();
        }
    }
}
