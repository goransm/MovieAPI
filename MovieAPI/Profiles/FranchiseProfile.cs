using AutoMapper;
using MovieAPI.DTOs;
using MovieAPI.Models;

namespace MovieAPI.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseDto>().ReverseMap();
            CreateMap<Franchise, FranchiseListDto>().ReverseMap();
        }
    }
}
