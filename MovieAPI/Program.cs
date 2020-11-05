using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieAPI.Models;

namespace MovieAPI
{
    public enum Gender
    {
        Male,
        Female,
        None
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            SeedDb();
            CreateHostBuilder(args).Build().Run();
        }

        private static void SeedDb()
        {
            using(var context = new MovieDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Actors
                var harrison = new Actor() { FirstName = "Harrison", LastName = "Ford", BirthDate = new DateTime(1942, 07, 13), Gender = Gender.Male, Picture = "https://secure-journal.hautehorlogerie.org/wp-content/uploads/2020/03/Harrison-Ford-b.jpg" };
                var daisy = new Actor() { FirstName = "Daisy", LastName = "Ridley", BirthDate = new DateTime(), Gender = Gender.Female };
                var rutger = new Actor() { FirstName = "Rutger", LastName = "Hauer", BirthDate = new DateTime(1944, 01, 23), Gender = Gender.Male, Picture = "", BirthPlace = "Utrecht" };

                context.Actors.Add(harrison);
                context.Actors.Add(daisy);
                context.Actors.Add(rutger);

                // Characters
                var solo = new Character() { FullName = "Han Solo", Gender = Gender.Male};
                var batty = new Character() {FullName = "Roy Batty", Gender = Gender.Male };
                var deckard = new Character() { FullName = "Rick Deckard", Gender = Gender.Male };
                var warren = new Character() { FullName = "Frank Warren", Gender = Gender.Male };
                var rey = new Character() { FullName = "Rey", Gender = Gender.Female };

                context.Characters.Add(solo);
                context.Characters.Add(batty);
                context.Characters.Add(deckard);
                context.Characters.Add(warren);
                context.Characters.Add(rey);

                // Franchises
                var starwars = new Franchise() { Name = "Star Wars" };

                context.Franchises.Add(starwars);


                // Movies
                var bladerunner = new Movie() { Title = "Blade Runner", Director = "Ridley Scott", Genre = "Science Fiction", ReleaseYear = 1982 };
                var newHope = new Movie() { Title = "A New Hope", Director = "George Lucas", Genre = "Action, Adventure, Fantasy", ReleaseYear = 1977, Franchise = starwars };
                var wedlock = new Movie() { Title = "Wedlock", Director = "Lewis Teague", Genre = "Action, Crime, Drama", ReleaseYear = 1991 };
                var force = new Movie() { Title = "The Force Awakens", Director = "J.J. Abrams", Genre = "Action, Adventure, Science Fiction", ReleaseYear = 2015, Franchise = starwars };

                context.Movies.Add(bladerunner);
                context.Movies.Add(newHope);
                context.Movies.Add(wedlock);
                context.Movies.Add(force);

                // MovieCharacters
                var hanford = new MovieCharacter() { Actor = harrison, Character = solo, Movie = newHope };
                var hanford2 = new MovieCharacter() { Actor = harrison, Character = solo, Movie = force };
                var dairey = new MovieCharacter() { Actor = daisy, Character = rey, Movie = force };
                var harrisonDeckard = new MovieCharacter() { Actor = harrison, Character = deckard, Movie = bladerunner };
                var rutgerBatty = new MovieCharacter() { Actor = rutger, Character = batty, Movie = bladerunner };
                var rutwar = new MovieCharacter() { Actor = rutger, Character = warren, Movie = wedlock };

                context.MovieCharacters.Add(hanford);
                context.MovieCharacters.Add(dairey);
                context.MovieCharacters.Add(rutwar);
                context.MovieCharacters.Add(hanford2);
                context.MovieCharacters.Add(harrisonDeckard);
                context.MovieCharacters.Add(rutgerBatty);

                context.SaveChanges();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
