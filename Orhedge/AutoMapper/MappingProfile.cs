using AutoMapper;
using Orhedge.ViewModels;
using ServiceLayer.Models;

namespace Orhedge.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, RegisterData>();
            CreateMap<LoginViewModel, LoginRequest>();
        }
    }
}
