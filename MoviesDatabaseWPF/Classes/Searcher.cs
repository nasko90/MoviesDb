using MoviesDatabase.Context;
using MoviesDatabase.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabaseWPF.Classes
{
    public class Searcher
    {
        private readonly MovieDatabaseContext db;

        public Searcher(MovieDatabaseContext db)
        {
            this.db = db;
        }

        public List<Movie> SearchByTitle(string movieTitle)
        {
            movieTitle = movieTitle.ToLower();
            List<Movie> movieCollection = new List<Movie>();
            foreach (var movie in this.db.Movies)
            {
                if (movie.Title.ToLower().Contains(movieTitle))
                {
                    movieCollection.Add(movie);
                }
            }

            return movieCollection;
        }

        public List<Movie> SearchByDirector(string directorName)
        {
            directorName = directorName.ToLower();
            List<Movie> movieCollection = new List<Movie>();
            foreach (var movie in this.db.Movies)
            {
                if (movie.Director.Name.ToLower().Contains(directorName))
                {
                    movieCollection.Add(movie);
                }
            }

            return movieCollection;
        }

        public List<Movie> SearchByGenre(string genre)
        {
            genre = genre.ToLower();
            List<Movie> movieCollection = new List<Movie>();
            foreach (var movie in this.db.Movies)
            {
                foreach (var genres in movie.Genres)
                {
                    if (genres.Name.ToLower().Contains(genre))
                    {
                        movieCollection.Add(movie);
                    }
                }
            }

            return movieCollection;
        }

        public List<Movie> SearchByActor(string actorName)
        {
            actorName = actorName.ToLower();
            List<Movie> movieCollection = new List<Movie>();
            foreach (var movie in this.db.Movies)
            {
                foreach (var actors in movie.Actors)
                {
                    if (actors.Name.ToLower().Contains(actorName))
                    {
                        movieCollection.Add(movie);
                    }
                }
            }

            return movieCollection;
        }
    }
}
