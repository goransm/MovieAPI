using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.DTOs
{
    public class MovieCharacterCreateDto
    {
        public int ActorId { get; set; }
        public int CharacterId { get; set; }
        public int MovieId { get; set; }

        public string Picture { get; set; }
    }
}
