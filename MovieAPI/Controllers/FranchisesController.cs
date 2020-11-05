using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.DTOs;
using MovieAPI.Models;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FranchisesController : ControllerBase
    {
        private readonly MovieDbContext _context;
        private readonly IMapper _mapper;

        public FranchisesController(MovieDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Franchise
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseListDto>>> GetFranchises()
        {
            return await _context.Franchises.Select(f => _mapper.Map<FranchiseListDto>(f)).ToListAsync();
        }

        // GET: api/Franchise/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Franchise>> GetFranchise(int id)
        {
            var franchise = await _context.Franchises.Where(f => f.Id == id).Include(f => f.Movies).SingleOrDefaultAsync();

            if (franchise == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FranchiseDto>(franchise));
        }

        // GET: api/franchises/{id}/movies
        [HttpGet("{id}/movies")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MovieListDto>>> GetFranchiseMovies(int id)
        {
            var movies = await _context.Movies.Where(m => m.FranchiseId == id).Select(m => _mapper.Map<Movie, MovieListDto>(m)).ToListAsync();
            if(movies.Count < 1)
            {
                return NotFound();
            }
            return Ok(movies);
        }

        [HttpGet("{id}/characters")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CharacterListDto>>> GetFranchiseCharacters(int id)
        {
            var movieIds = await _context.Movies.Where(m => m.FranchiseId == id).Select(m => m.Id).ToArrayAsync();

            var characterIds = await _context.MovieCharacters
                .Where(mc => movieIds.Contains(mc.MovieId))
                .Include(mcDto => mcDto.Actor)
                .Include(mcDto => mcDto.Character)
                .Select(mc => _mapper.Map<MovieCharacter, MovieCharacterDto>(mc))
                .Select(mcDto => mcDto.Character.Id)
                .ToArrayAsync();
            if (characterIds.Length < 1)
            {
                return NotFound();
            }
            return await _context.Characters
                .Where(c => characterIds.Contains(c.Id))
                .Select(c => _mapper.Map<Character, CharacterListDto>(c))
                .Distinct()
                .ToListAsync();

        }

        // PUT: api/Franchise/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> EditFranchise(int id, Franchise franchise)
        {
            if (id != franchise.Id)
            {
                return BadRequest();
            }

            _context.Entry(franchise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FranchiseExists(id))
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

        // POST: api/Franchise
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Franchise>> PostFranchise(Franchise franchise)
        {
            // TODO: check for existing franchise with same name?
            _context.Franchises.Add(franchise);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFranchise), new { id = franchise.Id }, franchise);
        }

        // DELETE: api/Franchise/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Franchise>> DeleteFranchise(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            if (franchise == null)
            {
                return NotFound();
            }

            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();

            return Ok(franchise);
        }

        private bool FranchiseExists(int id)
        {
            return _context.Franchises.Any(e => e.Id == id);
        }
    }
}
