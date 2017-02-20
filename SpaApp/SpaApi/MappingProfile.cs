using AutoMapper;
using SpaData.Models;
using SpaApi.ViewModels;

namespace SpaApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mappings between entities and viewModels
            CreateMap<Person, PersonViewModel>();
            CreateMap<PersonViewModel, Person>();
        }
    }
}
