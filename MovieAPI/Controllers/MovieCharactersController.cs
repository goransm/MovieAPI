using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.DTOs;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieCharactersController : ControllerBase
    {
        private readonly MovieDbContext _context;
        private readonly IMapper _mapper;

        public MovieCharactersController(MovieDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieCharacterDto>> GetMovieCharacters()
        {
            return await _context.MovieCharacters
                .Include(mc => mc.Actor)
                .Include(mc => mc.Character)
                .Include(mc => mc.Movie)
                .Select(mc => _mapper.Map<MovieCharacter, MovieCharacterDto>(mc))
                .ToListAsync();
        }

       [HttpPost]
       public async Task<IActionResult> CreateMovieCharacter(MovieCharacterCreateDto movieCharacter)
        {
            var mc = new MovieCharacter() { ActorId = movieCharacter.ActorId, CharacterId = movieCharacter.CharacterId, MovieId = movieCharacter.MovieId, Picture = movieCharacter.Picture};
            _context.MovieCharacters.Add(mc);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(movieCharacter);
            }
            catch (DbException)
            {
                if (MovieCharacterExists(mc.ActorId, mc.CharacterId, mc.MovieId))
                {
                    return Ok(movieCharacter);
                } else
                {
                    return BadRequest();
                }
            }

        }


       [HttpPut]
       public async Task<IActionResult> EditMovieCharacter(MovieCharacterCreateDto movieCharacter)
        {
            var mc = await _context.MovieCharacters.FindAsync( movieCharacter.MovieId,  movieCharacter.CharacterId, movieCharacter.ActorId );
            if(mc == null)
            {
                return NotFound();
            }
            mc.Picture = movieCharacter.Picture;
            _context.Entry(mc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DBConcurrencyException)
            {
                if (!MovieCharacterExists(mc.ActorId, mc.CharacterId, mc.MovieId))
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

       [HttpDelete]
       public async Task<ActionResult<MovieCharacter>> DeleteMovieCharacter(MovieCharacterCreateDto movieCharacter)
        {
            var mc = await _context.MovieCharacters.FindAsync(movieCharacter.MovieId, movieCharacter.CharacterId, movieCharacter.ActorId);

            if(mc == null)
            {
                return NotFound();
            }

            _context.MovieCharacters.Remove(mc);
            await _context.SaveChangesAsync();

            return Ok(mc);
        }

        private bool MovieCharacterExists(int actorId, int characterId, int movieId)
        {
            return _context.MovieCharacters.Any(mc => mc.ActorId == actorId && mc.CharacterId == characterId && mc.MovieId == movieId);
        }
    }
}
