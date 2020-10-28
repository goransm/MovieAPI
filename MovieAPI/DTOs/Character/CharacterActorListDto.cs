using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.DTOs
{
    public class CharacterActorListDto
    {
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public int ActorId { get; set; }
        public string ActorName { get; set; }
        public string Picture { get; set; }
    }
}
