using AutoMapper;
using MovieAPI.DTOs;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Profiles
{
    public class MovieCharacterProfile : Profile
    {
        public MovieCharacterProfile()
        {
            CreateMap<MovieCharacter, MovieCharacterDto>().ReverseMap();
            CreateMap<MovieCharacter, CharacterListDto>().ReverseMap();
            CreateMap<MovieCharacterDto, CharacterActorListDto>().ReverseMap();
        }
    }
}
