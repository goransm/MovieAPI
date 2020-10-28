using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public ICollection<MovieCharacterListDto> Characters { get; set; }
        public int? FranchiseId { get; set; }
        public FranchiseListDto Franchise { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Picture { get; set; }
        public string Trailer { get; set; }
        public int? PreviousId { get; set; }
        public MovieListDto Previous { get; set; }
    }
}
