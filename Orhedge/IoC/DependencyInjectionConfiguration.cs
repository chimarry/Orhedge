using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Orhedge.AutoMapper;
using ServiceLayer.Common.Interfaces;
using ServiceLayer.Common.Services;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using ServiceLayer.Services.Forum;
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
            builder.RegisterType<StudyMaterialService>().InstancePerLifetimeScope().As<IStudyMaterialService>();
            builder.RegisterType<CourseService>().InstancePerLifetimeScope().As<ICourseService>();
            builder.RegisterType<CategoryService>().InstancePerLifetimeScope().As<ICategoryService>();
            builder.RegisterType<ServiceLayer.ErrorHandling.ErrorHandler>().InstancePerLifetimeScope().As<ServiceLayer.ErrorHandling.IErrorHandler>();
            builder.RegisterGeneric(typeof(ServiceExecutor<,>)).InstancePerLifetimeScope().As(typeof(IServicesExecutor<,>));
            builder.RegisterType<RegistrationService>().InstancePerLifetimeScope().As<IRegistrationService>();
            builder.RegisterType<EmailSenderService>().InstancePerLifetimeScope().As<IEmailSenderService>();
            builder.RegisterType<StudentManagmentService>().InstancePerLifetimeScope().As<IStudentManagmentService>();
            builder.RegisterType<AuthenticationService>().InstancePerLifetimeScope().As<IAuthenticationService>();
            builder.RegisterType<AnswerRatingService>().InstancePerLifetimeScope().As<IAnswerRatingService>();
            builder.RegisterType<AnswerService>().InstancePerLifetimeScope().As<IAnswerService>();
            builder.RegisterType<CommentService>().InstancePerLifetimeScope().As<ICommentService>();
            builder.RegisterType<DiscussionPostService>().InstancePerLifetimeScope().As<IDiscussionPostService>();
            builder.RegisterType<ForumCategoryService>().InstancePerLifetimeScope().As<IForumCategoryService>();
            builder.RegisterType<QuestionService>().InstancePerLifetimeScope().As<IQuestionService>();
            builder.RegisterType<TopicRatingService>().InstancePerLifetimeScope().As<ITopicRatingService>();
            builder.RegisterType<TopicService>().InstancePerLifetimeScope().As<ITopicService>();
            builder.RegisterType<ForumManagmentService>().InstancePerLifetimeScope().As<IForumManagmentService>();

            // IMapper is thread safe, hence we register it as singleton
            builder.Register(ctx => MappingConfiguration.CreateMapping());
            IContainer container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }
    }
}
