using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.DTOs;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly MovieDbContext _context;
        private readonly IMapper _mapper;

        public CharactersController(MovieDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterListDto>>> GetCharacters()
        {
            return await _context.Characters.Select(c => _mapper.Map<Character, CharacterListDto>(c)).ToListAsync();
        }

        [HttpGet("{id}")]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CharacterDto>> GetCharacter(int id)
        {
            var character = await _context.Characters.Where(c => c.Id == id).SingleOrDefaultAsync();
            if(character == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CharacterDto>(character));
        }

        [HttpGet("{id}/actors")]
        public async Task<ActionResult<IEnumerable<ActorListDto>>> GetCharacterActors(int id)
        {
            var actors = await _context.MovieCharacters
                .Where(mc => mc.CharacterId == id)
                .Include(mc => mc.Actor)
                .Select(mc => _mapper.Map<Actor, ActorListDto>(mc.Actor))
                .Distinct()
                .ToListAsync();

            if(actors.Count < 1)
            {
                return NotFound();
            }

            return actors;
        }


    }
}
