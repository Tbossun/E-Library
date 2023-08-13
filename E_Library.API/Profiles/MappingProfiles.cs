﻿using AutoMapper;
using E_Library.Data.DTOS.Request;
using E_Library.Data.Models;

namespace E_Library.API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<ApplicationUser, SignUpDto>().ReverseMap();
            CreateMap<Book, AddBookDto>().ReverseMap();
        }
    }
}