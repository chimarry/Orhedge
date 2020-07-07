using AutoMapper;
using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using System;

namespace ServiceLayer.AutoMapper
{
    /// <summary>
    /// When creating MappingProfile instance, all maps are created.
    /// </summary>
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ChatMessage, ChatMessageDTO>().ReverseMap();
            CreateMap<StudentDTO, Student>().ForAllOtherMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Student, Student>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Student, StudentDTO>();
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<StudyMaterialDTO, StudyMaterial>()
                .ForMember(dest => dest.UploadDate, conf =>
                {
                    conf.PreCondition(src => src.UploadDate != default);
                    conf.MapFrom(src => src.UploadDate);
                })
                .ForMember(dest => dest.StudentId, conf =>
                  {
                      conf.PreCondition(src => src.StudentId != default);
                      conf.MapFrom(src => src.StudentId);
                  })
                .ForMember(dest => dest.TotalRating, conf => conf.Ignore())
                .ForAllOtherMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;
            CreateMap<StudyMaterial, StudyMaterial>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<StudyMaterial, StudyMaterialDTO>();
            CreateMap<StudyMaterialRating, StudyMaterialRatingDTO>().ReverseMap();
            CreateMap<Registration, RegistrationDTO>().ReverseMap();
            CreateMap<RegisterFormDTO, RegistrationDTO>();
            CreateMap<RegisterUserDTO, StudentDTO>();
            CreateMap<RegistrationDTO, StudentDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName));
            CreateMap<RegisterRootDTO, StudentDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName));
        }
    }
}
