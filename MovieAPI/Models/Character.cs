using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Models
{
    public class Character
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Alias { get; set; }
        public Gender Gender { get; set; }
        [Url]
        public string Picture { get; set; }
    }
}
