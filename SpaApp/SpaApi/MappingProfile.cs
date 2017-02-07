using AutoMapper;
using SpaData.Models;
using SpaDto.SpaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mappings between entities and dtos
            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>();
        }
    }
}
