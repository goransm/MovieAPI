using AutoMapper;
using MovieAPI.DTOs;
using MovieAPI.Models;

namespace MovieAPI.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<Movie, MovieListDto>().ReverseMap();
            CreateMap<Movie, PostMovieDto>().ReverseMap();
        }
    }
}
