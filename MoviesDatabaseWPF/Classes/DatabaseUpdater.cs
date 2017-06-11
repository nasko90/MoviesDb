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
        private readonly MovieDatabaseContext movieDb;

        public DatabaseUpdater(MovieDatabaseContext movieDb)
        {
            this.movieDb = movieDb;
        }

        public string AddMovieToDatabaseManually(string title, string countries, string boxOffice, string duration,
                                         string imdbRating, string actors, string director,
                                         string genres, string releaseDate, string plot)
        {
            var movieToBeAdded = new Movie();
            var logMessage = new StringBuilder();
            bool isMovieValid = true;
            var movieConverter = new MovieConverter(this.movieDb);

            if (this.movieDb.Movies.FirstOrDefault(x => x.Title.ToLower().Equals(title.ToLower())) == null)
            {
                movieToBeAdded.Title = title;
            }
            else
            {
                isMovieValid = false;
                logMessage.AppendLine("This movie already exist in the Database!");
            }

            if (ConvertBoxOffice(boxOffice) != long.MinValue)
            {
                movieToBeAdded.BoxOffice = ConvertBoxOffice(boxOffice);
            }
            else
            {
                isMovieValid = false;
                logMessage.AppendLine("Invalid Box Office value");
            }

            if (ConvertImdbRating(imdbRating) != double.MinValue)
            {
                movieToBeAdded.ImdbRating = ConvertImdbRating(imdbRating);
            }
            else
            {
                isMovieValid = false;
                logMessage.AppendLine("Invalid Imdb Rating value");
            }

            if (ConvertReleaseDate(releaseDate) != DateTime.MinValue)
            {
                movieToBeAdded.ReleaseDate = ConvertReleaseDate(releaseDate);
            }
            else
            {
                isMovieValid = false;
                logMessage.AppendLine("Invalid Release Date format!");
            }

            movieToBeAdded.Plot = plot;


            if (isMovieValid)
            {
                return "Succesfully added movie to the Database";
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
