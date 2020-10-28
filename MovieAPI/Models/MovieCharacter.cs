using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Models
{
    public class MovieCharacter
    {
        public int ActorId { get; set; }
        public Actor Actor { get; set; }

        public int CharacterId { get; set; }
        public Character Character { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        [Url]
        public string Picture { get; set; }
    }
}
