using MoviesDatabase.Context;
using MoviesDatabase.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;



namespace MoviesDatabase

{

    public class MovieConverter
    {
        private readonly MovieDatabaseContext movieDb;

        public MovieConverter(MovieDatabaseContext movieDb)
        {
            this.movieDb = movieDb;
        }

        public void AddOrUpdateToDatabase(MovieParser parsedMovie)
        {

            var movieCheck = CheckForExistingMovie(parsedMovie.Title);
            if (movieCheck == null)
            {
                var movie = new Movie
                {
                    Title = parsedMovie.Title,
                    ImdbRating = parsedMovie.ImdbRating,
                    Plot = parsedMovie.Plot,
                    BoxOffice = ConvertFromStringToNum(parsedMovie.BoxOffice),
                    ReleaseDate = parsedMovie.ReleaseDate,
                    Duration = (int) ConvertFromStringToNum(parsedMovie.Duration),
                    Director = AddOrUpdateDirector(parsedMovie.Director.Split(',')[0])
                };

                var actors = ConvertFromStringToIEnumerable(parsedMovie.Actors);
                foreach (var actor in actors)
                {
                    movie.Actors.Add(AddOrUpdateActor(actor));
                }

                var countries = ConvertFromStringToIEnumerable(parsedMovie.Countries);
                foreach (var country in countries)
                {
                    movie.Countries.Add(AddOrUpdateCountry(country));
                }

                var genres = ConvertFromStringToIEnumerable(parsedMovie.Genres);
                foreach (var genre in genres)
                {
                    movie.Genres.Add(AddOrUpdateGenre(genre));
                }

                this.movieDb.Movies.Add(movie);
                this.movieDb.SaveChanges();
            }               
        }

        public Actor AddOrUpdateActor(string actorName)
        {
            var actor = this.movieDb.Actors.FirstOrDefault(x => x.Name.Equals(actorName));
            if (actor == null)
            {
                actor = new Actor { Name = actorName };
            }

            return actor;
        }

        private Movie CheckForExistingMovie(string movieTitle)
        {
            var movie = this.movieDb.Movies.FirstOrDefault(x => x.Title.Equals(movieTitle));            

            return movie;
        }

        public Director AddOrUpdateDirector(string directorName)
        {
            var director = this.movieDb.Directors.FirstOrDefault(x => x.Name.Equals(directorName));
            if (director == null)
            {
                director = new Director { Name = directorName };
            }

            return director;
        }

        public Country AddOrUpdateCountry( string countryName)
        {
            var country = this.movieDb.Countries.FirstOrDefault(x => x.Name.Equals(countryName));
            if (country == null)
            {
                country = new Country { Name = countryName };
            }

            return country;
        }

        public Genre AddOrUpdateGenre(string genreName)
        {
            var genre = this.movieDb.Genres.FirstOrDefault(x => x.Name.Equals(genreName));
            if (genre == null)
            {
                genre = new Genre { Name = genreName };
            }

            return genre;
        }

        public static IEnumerable<string> ConvertFromStringToIEnumerable(string stringToConvert)
        {        
            var actors = stringToConvert.Split(',').Select(a => a.Trim()).ToList(); ;
            return actors;
        }

        private long ConvertFromStringToNum(string boxOfficeString)
        {
            boxOfficeString = Regex.Replace(boxOfficeString, "[^0-9]", "");
            long boxOffice;

            long.TryParse(boxOfficeString, out boxOffice);

            return boxOffice;
        }
    }
}