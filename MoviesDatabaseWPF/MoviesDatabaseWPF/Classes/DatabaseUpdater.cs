using MoviesDatabase;
using MoviesDatabase.Context;
using MoviesDatabase.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesDatabase.Models.Models.Enums;

namespace MoviesDatabaseWPF.Classes
{
    public class DatabaseUpdater
    {
        private const int FirstEverMovieReleaseYear = 1890;
        private readonly MovieDatabaseContext movieDb;

        public DatabaseUpdater(MovieDatabaseContext movieDb)
        {
            this.movieDb = movieDb;
        }

        public string AddMovieToDatabaseManually(string title, string countriesInput, string boxOffice, string duration,
                                         string imdbRating, string actorsAsString, string director,
                                         string genresInput, string releaseDate, string plot)
        {
            var movieToBeAdded = new Movie();
            var logMessage = new StringBuilder();
            var isMovieValid = true;
            var movieConverter = new MovieConverter(this.movieDb);

            var actors = MovieConverter.ConvertFromStringToIEnumerable(actorsAsString);
            foreach (var actor in actors)
            {
                movieToBeAdded.Actors.Add(movieConverter.AddOrUpdateActor(actor));
            }

            var countries = MovieConverter.ConvertFromStringToIEnumerable(countriesInput);
            foreach (var country in countries)
            {
                movieToBeAdded.Countries.Add(movieConverter.AddOrUpdateCountry(country));    
            }

            var genres = MovieConverter.ConvertFromStringToIEnumerable(genresInput);
            foreach (var genre in genres)
            {
                movieToBeAdded.Genres.Add(movieConverter.AddOrUpdateGenre(genre));
            }

            movieToBeAdded.Director = movieConverter.AddOrUpdateDirector(director);
            movieToBeAdded.Plot = plot;

            if (this.movieDb.Movies.FirstOrDefault(x => x.Title.ToLower().Equals(title.ToLower())) == null)
            {
                movieToBeAdded.Title = title;
            }
            else
            {
                isMovieValid = false;
                logMessage.AppendLine("This movie already exist in the Database! ");
            }

            if (ConvertDuration(duration) < 0)
            {
                isMovieValid = false;
                logMessage.AppendLine("Invalid Duration value! ");
            }
            else
            {
                movieToBeAdded.Duration = ConvertDuration(duration);               
            }

            if (ConvertBoxOffice(boxOffice) < 0)
            {
                isMovieValid = false;
                logMessage.AppendLine("Invalid Box Office value! ");

            }
            else
            {
                movieToBeAdded.BoxOffice = ConvertBoxOffice(boxOffice);
            }

            if (ConvertImdbRating(imdbRating) < 1 || ConvertImdbRating(imdbRating) > 10)
            {
                isMovieValid = false;
                logMessage.AppendLine("Invalid Imdb Rating value! ");
            }
            else
            {
                movieToBeAdded.ImdbRating = ConvertImdbRating(imdbRating);
            }

            if (ConvertDate(releaseDate).Year < FirstEverMovieReleaseYear)
            {
                isMovieValid = false;
                logMessage.AppendLine("Invalid Release Date format! ");
            }
            else
            {
                movieToBeAdded.ReleaseDate = ConvertDate(releaseDate);
            }

            if (isMovieValid)
            {
                this.movieDb.Movies.Add(movieToBeAdded);
                this.movieDb.SaveChanges();
                return "Succesfully added movie to the Database";
            }

            logMessage.AppendLine("No movie was added to the database!");
            return logMessage.ToString();
        }

        public string AddActorToDatabase(string name, string dateOfBirth, string nationality, string gender)
        {
            var logMessage = new StringBuilder();
            var isActorInValidFormat = true;

            if (this.movieDb.Actors.Any(x => x.Name.Equals(name)))
            {
                isActorInValidFormat = false;
                logMessage.AppendLine("The actor already exist in the Database");
            }

            var actor = new Actor {Name = name};

            Gender actorGender;
            if (Enum.TryParse(gender, out actorGender))
            {
                actor.Gender = actorGender;
            }
            else
            {
                isActorInValidFormat = false;
                logMessage.AppendLine("Invalid gender");
            }

            DateTime birhtDate;
            if (DateTime.TryParse(dateOfBirth,out birhtDate))
            {
                actor.DateOfBirth = birhtDate;
            }
            else
            {
                isActorInValidFormat = false;
                logMessage.AppendLine("Invalid Date format");
            }

            
            actor.Country = GetNationality(nationality);

            if (isActorInValidFormat)
            {
                this.movieDb.Actors.Add(actor);
                this.movieDb.SaveChanges();
                logMessage.AppendLine("The actor was successfully added to the Database");
            }

            return logMessage.ToString();
        }

        public string AddDirectorToDatabase(string name, string dateOfBirth, string nationality, string gender)
        {
            var logMessage = new StringBuilder();
            var isDirectorValid = true;

            if (this.movieDb.Directors.Any(x => x.Name.Equals(name)))
            {
                isDirectorValid = false;
                logMessage.AppendLine("The actor already exist in the Database");
            }

            var director = new Director() { Name = name };

            Gender actorGender;
            if (Enum.TryParse(gender, out actorGender))
            {
                director.Gender = actorGender;
            }
            else
            {
                isDirectorValid = false;
                logMessage.AppendLine("Invalid gender");
            }

            DateTime birhtDate;
            if (DateTime.TryParse(dateOfBirth, out birhtDate))
            {
                director.DateOfBirth = birhtDate;
            }
            else
            {
                isDirectorValid = false;
                logMessage.AppendLine("Invalid Date format");
            }


            director.Country = GetNationality(nationality);

            if (isDirectorValid)
            {
                this.movieDb.Directors.Add(director);
                this.movieDb.SaveChanges();
                logMessage.AppendLine("The director  was successfully added to the Database");
            }

            return logMessage.ToString();
        }

        private Double ConvertImdbRating(string rating)
        {
            double imdbRating;
            if (double.TryParse(rating, out imdbRating))
            {
                return imdbRating;
            }

            return double.MinValue;
        }

        private int ConvertDuration(string movieDuration)
        {
            int duration;
            if (int.TryParse(movieDuration, out duration))
            {
                return duration;
            }

            return int.MinValue;
        }

        private DateTime ConvertDate(string releaseDate)
        {
            DateTime date;
            if (DateTime.TryParse(releaseDate, out date))
            {
                return date;
            }

            return DateTime.MinValue;
        }

        private long ConvertBoxOffice(string boxOffice)
        {
            long revenue;
            if (long.TryParse(boxOffice, out revenue))
            {
                return revenue;
            }

            return long.MinValue;
        }

        private Country GetNationality(string nationality)
        {
            Country country = this.movieDb.Countries.FirstOrDefault(x => x.Name.Equals(nationality));
            if (country == null)
            {
                return new Country
                {
                    Name = nationality
                };
            }

            return country;
        }

       
            
    }
}
