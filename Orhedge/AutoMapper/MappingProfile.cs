using AutoMapper;
using Orhedge.ViewModels;
using Orhedge.ViewModels.Forum;
using ServiceLayer.DTO;
using ServiceLayer.DTO.Forum;
using ServiceLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace Orhedge.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, RegisterData>();
            CreateMap<LoginViewModel, LoginRequest>();
            CreateMap<TopicListDTO, TopicSelectionViewModel[]>()
                .ConvertUsing((topicList, ctx) =>
                {
                    List<TopicSelectionViewModel> result = new List<TopicSelectionViewModel>();

                    TopicDTO[] topics = topicList.Topics;
                    StudentDTO[] authors = topicList.Authors;
                    int[] postCounts = topicList.PostCounts;

                    for(int i = 0; i < topics.Length; ++i)
                    {

                        TopicSelectionViewModel topicVm = new TopicSelectionViewModel
                        {
                            Author = new AuthorViewModel
                            {
                                Id = authors[i].StudentId,
                                Name = authors[i].Username
                            },
                            Title = topics[i].Title,
                            PostCount = postCounts[i],
                            LastPost = topics[i].LastPost
                        };

                        result.Add(topicVm);
                    }

                    return result.ToArray();
                });
        }
    }
}
