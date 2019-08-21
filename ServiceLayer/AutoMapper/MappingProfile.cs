using AutoMapper;
using DatabaseLayer.Entity;
using ServiceLayer.DTO;

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
        }
        private void MapToEntity()
        {
            CreateMap<StudentDTO, Student>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<StudyMaterialDTO, StudyMaterial>();
            CreateMap<StudyMaterialRatingDTO, StudyMaterialRating>();
            CreateMap<CourseDTO, Course>();
        }
    }
}
