using AutoMapper;
using LibraryManagement.BLL.DTOs;
using LibraryManagement.DAL.Entities;

namespace LibraryManagement.BLL.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile() 
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Author, AuthorWithBooksDTO>().ReverseMap();
        }
    }
}
