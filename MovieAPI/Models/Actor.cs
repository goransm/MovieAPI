using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Models
{
    public class Actor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string OtherNames { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Biography { get; set; }
        [Url]
        public string Picture { get; set; }
        public ICollection<MovieCharacter> Roles { get; set; }
        public string FullName()
        {
            var middleName = OtherNames != null ? $" {OtherNames} " : " ";
            return $"{FirstName}{middleName}{LastName}";
        }

    }
}
