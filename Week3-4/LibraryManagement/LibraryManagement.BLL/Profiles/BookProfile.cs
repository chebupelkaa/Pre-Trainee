using AutoMapper;
using LibraryManagement.BLL.DTOs;
using LibraryManagement.DAL.Entities;

namespace LibraryManagement.BLL.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
        }
    }
}
