using AutoMapper;
using LibraryManagement.API.Models;
using LibraryManagement.BLL.DTOs;

namespace LibraryManagement.API.Profiles
{
    public class BookModelProfile:Profile
    {
        public BookModelProfile() 
        {
            CreateMap<BookModel, BookDTO>().ReverseMap();
        }
    }
}
