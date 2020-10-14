using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieAPI.Models
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieCharacter> MovieCharacters { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=PC7414\\SQLEXPRESS;Initial Catalog=MovieDb;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieCharacter>().HasKey(mc => new { mc.CharacterId, mc.MovieId });
        }

    }
}
