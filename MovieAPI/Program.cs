using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
            //SeedDb();
            CreateHostBuilder(args).Build().Run();
        }

        private static void SeedDb()
        {
            using(var context = new MovieDbContext())
            {
                var grant = context.Actors.Find(1);
                var flash = context.Characters.Find(1);

                var arrowVerse = context.Franchises.Find(1);// new Franchise() { Name = "Arrowverse" };
                var flashs01e01 = new Movie() { Title = "Pilot", Director = "David Nutter", Franchise = arrowVerse, Genre = "superhero", ReleaseYear = 2014 };
                var flashs01e02 = new Movie() { Title = "Fastest Man Alive", Director = "David Nutter", Franchise = arrowVerse, Genre = "superhero", ReleaseYear = 2014, Previous = flashs01e01 };
                var s01e01character = new MovieCharacter() { Character = flash, Movie = flashs01e01, Actor = grant };
                var s01e02character = new MovieCharacter() { Character = flash, Movie = flashs01e02, Actor = grant };

                //context.Actors.Add(grant);
                //context.Characters.Add(flash);
                //context.Franchises.Add(arrowVerse);
                context.Movies.Add(flashs01e01);
                context.Movies.Add(flashs01e02);
                context.MovieCharacters.Add(s01e01character);
                context.MovieCharacters.Add(s01e02character);


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
