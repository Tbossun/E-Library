using AutoMapper;
using E_Library.Core.Services.Implementations;
using E_Library.Data.DTOS.Request;
using E_Library.Data.DTOS.Response;
using E_Library.Data.Models;

namespace E_Library.API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<ApplicationUser, SignUpDto>().ReverseMap();
            CreateMap<Book, AddBookDto>().ReverseMap();
            CreateMap<Book, UpdateBookDto>().ReverseMap();
            CreateMap<Category, CreateCategory>().ReverseMap();
            CreateMap<Category, categoryDto>().ReverseMap();
            CreateMap<Category, GetCategory>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();               
        }
    }
}
