using MoviesDatabase;
using MoviesDatabase.Context;
using MoviesDatabase.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (ConvertReleaseDate(releaseDate).Year < FirstEverMovieReleaseYear)
            {
                isMovieValid = false;
                logMessage.AppendLine("Invalid Release Date format! ");
            }
            else
            {
                movieToBeAdded.ReleaseDate = ConvertReleaseDate(releaseDate);
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

        private DateTime ConvertReleaseDate(string releaseDate)
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


    }
}
