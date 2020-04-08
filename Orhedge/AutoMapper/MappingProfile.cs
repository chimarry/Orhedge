using AutoMapper;
using Orhedge.Helpers;
using Orhedge.ViewModels;
using Orhedge.ViewModels.Forum;
using Orhedge.ViewModels.Student;
using Orhedge.ViewModels.StudyMaterial;
using ServiceLayer.DTO;
using ServiceLayer.DTO.Forum;
using ServiceLayer.DTO.Student;
using ServiceLayer.Models;
using System.Collections.Generic;

namespace Orhedge.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterFormViewModel, RegisterFormDTO>();
            CreateMap<RegisterViewModel, RegisterUserDTO>();
            CreateMap<LoginViewModel, LoginRequest>();
            CreateMap<StudentDTO, EditProfileViewModel>();
            CreateMap<EditProfileViewModel, ProfileUpdateDTO>()
                .ForMember(dest => dest.Photo, opts =>
                {
                    opts.MapFrom(src => new FormFile(src.Photo));
                    opts.Condition(src => src.Photo != null);
                });

            CreateMap<ChangePasswordViewModel, UpdatePasswordDTO>();
            CreateMap<StudentDTO, ViewModels.Admin.StudentViewModel>().ReverseMap();
            CreateMap<ViewModels.Admin.EditStudentViewModel, StudentDTO>().ReverseMap();
            CreateMap<CourseDTO, IndexCourseViewModel>();
            CreateMap<DetailedSemesterDTO, SemesterViewModel>().ForMember(dest => dest.Courses, conf => conf.MapFrom(src => src.Courses)).ReverseMap();
        }
        public void MapForum()
        {
            CreateMap<TopicListDTO, TopicSelectionViewModel[]>()
                .ConvertUsing((topicList, ctx) =>
                {
                    List<TopicSelectionViewModel> result = new List<TopicSelectionViewModel>();

                    TopicDTO[] topics = topicList.Topics;
                    StudentDTO[] authors = topicList.Authors;
                    int[] postCounts = topicList.PostCounts;

                    for (int i = 0; i < topics.Length; ++i)
                    {

                        TopicSelectionViewModel topicVm = new TopicSelectionViewModel
                        {
                            Author = new AuthorViewModel
                            {
                                Id = authors[i].StudentId,
                                Username = authors[i].Username
                            },
                            Title = topics[i].Title,
                            PostCount = postCounts[i],
                            LastPost = topics[i].LastPost
                        };

                        result.Add(topicVm);
                    }

                    return result.ToArray();
                });
            CreateMap<StudentDTO, AuthorViewModel>()
                .ConvertUsing((student, author) =>
                {
                    AuthorViewModel result = new AuthorViewModel
                    {
                        Id = student.StudentId,
                        Username = student.Username
                    };

                    return result;
                });
            CreateMap<DiscussionPostsDTO, DiscussionPostViewModel[]>()
                .ConvertUsing((discussionPosts, dpa) =>
                {
                    List<DiscussionPostViewModel> result = new List<DiscussionPostViewModel>();
                    DiscussionPostDTO[] posts = discussionPosts.DiscussionPosts;
                    StudentDTO[] authors = discussionPosts.Authors;

                    for (int i = 0; i < posts.Length; ++i)
                    {
                        DiscussionPostViewModel dpvm = new DiscussionPostViewModel
                        {
                            Content = posts[i].Content,
                            Created = posts[i].Created,
                            Edited = posts[i].Edited,
                            Author = new AuthorViewModel
                            {
                                Id = authors[i].StudentId,
                                Username = authors[i].Username
                            }
                        };
                        result.Add(dpvm);
                    }

                    return result.ToArray();
                });
        }
    }
}
