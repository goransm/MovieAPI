using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MovieAPI.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MovieAPI.DTOs;
using Microsoft.AspNetCore.Http;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorsController : ControllerBase
    {
        private readonly MovieDbContext _context;
        private readonly IMapper _mapper;

        public ActorsController(MovieDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorListDto>>> GetActors()
        {
            return await _context.Actors.Select(a => _mapper.Map<Actor, ActorListDto>(a)).ToListAsync();
        }

        [HttpGet("{id}/characters")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CharacterListDto>>> GetActorCharacters(int id)
        {
            var characters = await _context.MovieCharacters
                .Where(mc => mc.ActorId == id)
                .Include(mc => mc.Character)
                .Select(mc => _mapper.Map<Character, CharacterListDto>(mc.Character))
                .Distinct()
                .ToListAsync();

            if(characters.Count < 1)
            {
                return NotFound();
            }

            return characters;
        }

        [HttpGet("{id}/movies")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MovieListDto>>> GetActorMovies(int id)
        {
            var movies = await _context.MovieCharacters
                .Where(mc => mc.ActorId == id)
                .Include(mc => mc.Movie)
                .Select(mc => _mapper.Map<Movie, MovieListDto>(mc.Movie))
                .Distinct()
                .ToListAsync();

            if(movies.Count < 1)
            {
                return NotFound();
            }
            return movies;
        }
    }
}
