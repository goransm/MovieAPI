using System;
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
    public class MoviesController : ControllerBase
    {
        private readonly MovieDbContext _context;
        private readonly IMapper _mapper;

        public MoviesController(MovieDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieListDto>>> GetMovies()
        {
            return await _context.Movies.Select(m => _mapper.Map<Movie, MovieListDto>(m)).ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.Where(m => m.Id == id).Include(m => m.Franchise).Include(m => m.Previous).SingleOrDefaultAsync();

            if(movie == null)
            {
                return NotFound();
            }

            MovieDto movieDto = _mapper.Map<MovieDto>(movie);
            Movie mv = _mapper.Map<Movie>(movieDto);

            return Ok(mv);
        }

        [HttpGet("{id}/characters")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CharacterActorListDto>>> GetCharacters(int id)
        {
            var characters = await _context.MovieCharacters
                .Where(mc => mc.MovieId == id)
                .Include(mcDto => mcDto.Actor)
                .Include(mcDto => mcDto.Character)
                .Select(mc => _mapper.Map<MovieCharacter, MovieCharacterDto>(mc))
                .Select(mcDto => _mapper.Map<MovieCharacterDto, CharacterActorListDto>(mcDto))
                .ToListAsync();
            if (characters == null)
            {
                return NotFound();
            }
            return characters;
                                    
        }


        [HttpPost]
        public async Task<ActionResult<Movie>> CreateMovie(PostMovieDto movie)
        {
            Movie mv = _mapper.Map<Movie>(movie);
            _context.Movies.Add(mv);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = mv.Id }, mv);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateMovie(int id, Movie movie)
        {
            if(id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            } 
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if(movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return Ok(movie);
        }


        private bool MovieExists(int id)
        {
            return _context.Movies.Any(m => m.Id == id);
        }
    }
}
