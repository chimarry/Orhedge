using AutoMapper;
using Microsoft.AspNetCore.Http;
using Orhedge.Helpers;
using Orhedge.ViewModels;
using Orhedge.ViewModels.CourseCategory;
using Orhedge.ViewModels.Student;
using Orhedge.ViewModels.StudyMaterial;
using Orhedge.ViewModels.TechnicalSupport;
using ServiceLayer.DTO;
using ServiceLayer.DTO.Materials;
using ServiceLayer.DTO.Student;
using ServiceLayer.Models;
using ServiceLayer.Students.Shared;
using System;
using System.Collections.Generic;

namespace Orhedge.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapStudyMaterials();
            MapStudents();
            MapTechnicalSupport();
            MapCourseCategories();
        }

        private void MapCourseCategories()
        {
            CreateMap<DetailedCourseCategoryDTO, DetailedCourseViewModel>().ForMember(dest => dest.Name, opts => opts.MapFrom(x => x.Course.Name))
                                                                          .ForMember(dest => dest.CourseId, opts => opts.MapFrom(x => x.Course.CourseId))
                                                                          .ForMember(dest => dest.Categories, src => src.MapFrom(x => x.Categories));
        }

        private void MapTechnicalSupport()
        {
            CreateMap<ChatMessageDTO, ChatMessageViewModel>();
        }

        public void MapStudyMaterials()
        {
            CreateMap<CourseDTO, IndexCourseViewModel>();
            CreateMap<DetailedSemesterDTO, SemesterViewModel>().ForMember(dest => dest.Courses, conf => conf.MapFrom(src => src.Courses)).ReverseMap();
            CreateMap<CategoryDTO, CategoryViewModel>();
            CreateMap<CourseCategoryDTO, CourseCategoryViewModel>()
                .ForMember(dest => dest.CourseId, opts => opts.MapFrom(courseCat => courseCat.Course.CourseId))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(courseCat => courseCat.Course.Name))
                .ForMember(dest => dest.Categories, opts => opts.MapFrom(courseCat => courseCat.Categories));
            CreateMap<DetailedStudyMaterialDTO, StudyMaterialViewModel>().ForMember(dest => dest.GivenRating, conf => { conf.MapFrom(src => src.GivenRating); conf.NullSubstitute(0); });
            CreateMap<EditStudyMaterialViewModel, StudyMaterialDTO>();
            CreateMap<IFormFile, BasicFileInfo>().ConvertUsing<FormFileToBasicFileInfoConverter>();
        }

        private void MapStudents()
        {
            CreateMap<RegisterFormViewModel, RegisterFormDTO>()
               .ForMember(dest => dest.Index, opts => opts.MapFrom(src => src.IndexNumber));
            CreateMap<RegisterViewModel, RegisterUserDTO>();
            CreateMap<LoginViewModel, LoginRequest>();
            CreateMap<StudentDTO, EditProfileViewModel>()
                .ForMember(dest => dest.Photo, opts => opts.Ignore())
                .ForMember(dest => dest.PhotoVersion, opts => opts.Condition(s => s.Photo != null));
            CreateMap<EditProfileViewModel, ProfileUpdateDTO>()
                .ForMember(dest => dest.Photo, opts =>
                {
                    opts.MapFrom(src => new FormFile(src.Photo));
                    opts.Condition(src => src.Photo != null);
                });

            CreateMap<ChangePasswordViewModel, UpdatePasswordDTO>();
            CreateMap<StudentDTO, ViewModels.Admin.StudentViewModel>().ForMember(dest => dest.PhotoVersion, opts => opts.Condition(src => src.Photo != null)).ReverseMap();
            CreateMap<ViewModels.Admin.EditStudentViewModel, StudentDTO>().ReverseMap();
        }
    }
}
