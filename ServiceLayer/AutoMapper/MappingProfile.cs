using AutoMapper;
using DatabaseLayer.Entity;
using ServiceLayer.DTO;
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

            CreateMap<RegisterData, StudentDTO>()
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
        }
        private void MapToEntity()
        {
            CreateMap<StudentDTO, Student>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<StudyMaterialDTO, StudyMaterial>();
            CreateMap<StudyMaterialRatingDTO, StudyMaterialRating>();
            CreateMap<CourseDTO, Course>();
            CreateMap<RegistrationDTO, Registration>();
        }
    }
}
