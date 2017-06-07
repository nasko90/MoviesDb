﻿using MoviesDatabase.Context;
using MoviesDatabase.Models.Models;
using System;
using System.Linq;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using MoviesDatabase.ModelParsers;
using MoviesDatabase.Converters;
using MovieDatabase.PostgreeDatabase;
using MovieDatabase.PostgreeDatabase.Models;

namespace MoviesDatabase
{
    public class Program
    {
        public static void Main(string[] args)
        {

            /*
            var path = "Movie.json";
            var actorsPath = "Actors.json";
            var parser = new Parser();
            var movieDb = new MovieDatabaseContext();

            var parsedMovies = parser.ParseMovies(path);
            var parsedActors = parser.ParsePersons(actorsPath);
            var actorConverter = new ActorConverter(movieDb);
            var movieConverter = new MovieConverter(movieDb);

            var directorConverter = new DirectorConverter(movieDb);
            var parsedDirectors = parser.ParsePersons("Directors.json");


            foreach (var director in parsedDirectors)
            {
                directorConverter.UpdateDirectorInfo(director);
            }           

            var db = new MovieDatabaseContext();
            var user = new User();
            user.UserName = "nasko";
            user.ClearTextPassword = "parola";
            user.FullName = "Atanas Drenovichki";
            user.EmailAddress = "nasko@abv.bg";

            db.User.Add(user);
            db.SaveChanges();
            */
           
        }
    }
}
