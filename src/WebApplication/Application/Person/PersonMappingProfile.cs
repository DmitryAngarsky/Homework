using AutoMapper;
using Domain;
using Domain.DTO;

namespace Application
{
    public class PersonMappingProfile : Profile
    {
        public PersonMappingProfile()
        {
            CreateMap<Person, PersonModel>();
            CreateMap<PersonModel, Person>();
            CreateMap<Person, PersonBooks>();
        }
    }
}