using AutoMapper;
using MovieAPI.DTOs;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MovieAPI.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterDto>().ForMember(cDto => cDto.Name, opt => opt.MapFrom(character => $"{character.FullName} "));
            CreateMap<CharacterDto, Character>().ForMember(c => c.FullName, opt => opt.MapFrom(characterDto => $"{characterDto.Name} "));
            CreateMap<Character, CharacterListDto>().ForMember(clDto => clDto.Name, opt => opt.MapFrom(character => $"{character.FullName} ")).ReverseMap();
        }
    }
}