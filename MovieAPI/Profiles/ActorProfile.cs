using AutoMapper;
using MovieAPI.DTOs;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Profiles
{
    public class ActorProfile : Profile
    {
        public ActorProfile()
        {
            CreateMap<Actor, ActorDto>().ReverseMap();
            CreateMap<Actor, ActorListDto>().ForMember(alDto => alDto.Name, opt => opt.MapFrom(actor => actor.FullName())).ReverseMap();
        }
    }
}
