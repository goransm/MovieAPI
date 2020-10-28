using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.DTOs
{
    public class MovieCharacterDto
    {
        public ActorListDto Actor { get; set; }

        public CharacterListDto Character { get; set; }

        public MovieListDto Movie { get; set; }
        public string Picture { get; set; }
    }
}
