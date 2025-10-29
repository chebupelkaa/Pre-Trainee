using AutoMapper;
using LibraryManagement.API.Models;
using LibraryManagement.BLL.DTOs;

namespace LibraryManagement.API.Profiles
{
    public class AuthorModelProfile:Profile
    {
        public AuthorModelProfile()
        {
            CreateMap<AuthorModel, AuthorDTO>().ReverseMap();
        }
    }
}
