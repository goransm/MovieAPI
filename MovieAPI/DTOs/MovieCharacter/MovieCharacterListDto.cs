using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.DTOs
{
    public class MovieCharacterListDto
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; }
        public string CharacterName { get; set; }
        public string ActorName { get; set; }

        public string Picture { get; set; }
    }
}
