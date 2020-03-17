using AutoMapper;
using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.DTO.Registration;
using ServiceLayer.Models;

namespace ServiceLayer.AutoMapper
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region EntityToDTO
            MapToDTO();
            #endregion

            #region DTOToEntity
            MapToEntity();
            #endregion

            CreateMap<RegisterFormDTO, RegistrationDTO>();
            CreateMap<RegisterUserDTO, StudentDTO>();
            CreateMap<RegistrationDTO, StudentDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName));
            CreateMap<RegisterRootDTO, StudentDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName));

            //CreateMap<Source,Destination>();
            // Additional mappings here...
        }
        private void MapToDTO()
        {
            CreateMap<Student, StudentDTO>();
            CreateMap<Course, CourseDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<StudyMaterial, StudyMaterialDTO>();
            CreateMap<StudyMaterialRating, StudyMaterialRatingDTO>();
            CreateMap<Registration, RegistrationDTO>();
            CreateMap<Answer, AnswerDTO>();
            CreateMap<AnswerRating, AnswerRatingDTO>();
            CreateMap<Discussion, DiscussionDTO>();
            CreateMap<DiscussionPost, DiscussionPostDTO>();
            CreateMap<ForumCategory, ForumCategoryDTO>();
            CreateMap<Question, QuestionDTO>();
            CreateMap<Topic, TopicDTO>();
            CreateMap<TopicRating, TopicRatingDTO>();
        }
        private void MapToEntity()
        {
            CreateMap<StudentDTO, Student>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<StudyMaterialDTO, StudyMaterial>();
            CreateMap<StudyMaterialRatingDTO, StudyMaterialRating>();
            CreateMap<CourseDTO, Course>();
            CreateMap<RegistrationDTO, Registration>();
            CreateMap<AnswerDTO, Answer>();
            CreateMap<AnswerRatingDTO, AnswerRating>();
            CreateMap<DiscussionDTO, Discussion>();
            CreateMap<DiscussionPostDTO, DiscussionPost>();
            CreateMap<ForumCategoryDTO, ForumCategory>();
            CreateMap<QuestionDTO, Question>();
            CreateMap<TopicDTO, Topic>();
            CreateMap<TopicRatingDTO, TopicRating>();
        }
    }
}
