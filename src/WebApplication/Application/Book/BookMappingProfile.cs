using AutoMapper;
using Domain;
using Domain.DTO;

namespace Application
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookModel>();
            CreateMap<BookModel, Book>();
            CreateMap<Book, AuthorBook>();
        }
    }
}