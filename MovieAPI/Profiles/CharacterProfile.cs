using AutoMapper;
using MovieAPI.DTOs;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterDto>().ForMember(cDto => cDto.Name, opt => opt.MapFrom(character => $"{character.FullName} ")).ReverseMap();
            CreateMap<Character, CharacterListDto>().ForMember(clDto => clDto.Name, opt => opt.MapFrom(character => $"{character.FullName} ")).ReverseMap();
        }
    }
}
