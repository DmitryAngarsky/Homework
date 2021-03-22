using AutoMapper;
using Domain;
using Domain.DTO;

namespace Application
{
    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            CreateMap<Author, AuthorModel>();
            CreateMap<AuthorModel, Author>();
            CreateMap<Author, AuthorBooks>();
        }
    }
}