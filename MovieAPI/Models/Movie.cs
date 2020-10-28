﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Models
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public ICollection<MovieCharacter> Characters { get; set; }
        public int? FranchiseId { get; set; }
        public Franchise Franchise { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        [Url]
        public string Picture { get; set; }
        [Url]
        public string Trailer { get; set; }
        public int? PreviousId { get; set; }
        public Movie Previous { get; set; }
    }
}
