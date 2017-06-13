using MoviesDatabase.Context;
using MoviesDatabase.Models.Models;
using MoviesDatabaseWPF.ViewModelObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoviesDatabaseWPF.ViewModelObjects
{
    public class ViewModelMovie : IViewable
    {
        public ViewModelMovie(Movie movie)
        {
            Id = movie.Id;
            Title = movie.Title;
            Duration = movie.Duration + " minutes";
            Genres = string.Join(", ", movie.Genres.Select(x => string.Concat(x.Name)));
            Director = movie.Director.Name;
            Actors = string.Join(", ", movie.Actors.Select(x => string.Concat(x.Name)));
            ReleaseDate = movie.ReleaseDate.ToString("MMMM dd, yyyy");
            Countries = string.Join(", ", movie.Countries.Select(x => string.Concat(x.Name)));
            BoxOffice = movie.BoxOffice.ToString("# ###");
            ImdbRating = movie.ImdbRating;
            Plot = movie.Plot;
        }
        public ViewModelMovie()
        {

        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Duration { get; set; }
        public string Genres { get; set; }

        public string Director{ get; set; }
        public string Actors { get; set; }

        public string ReleaseDate { get; set; }

        public string Countries { get; set; }

        public string BoxOffice { get; set; }

        public double ImdbRating { get; set; }

        public string Plot { get; set; }

      

        public static IEnumerable<ViewModelMovie> ConvertMoviesToVeiwModelMovies(IEnumerable<Movie> movies)
        {
            var viewModelMovies = new ObservableCollection<ViewModelMovie>();
            
            foreach (var movie in movies)
            {
                viewModelMovies.Add(new ViewModelMovie(movie));
            }

            return viewModelMovies;
        }

        public static IEnumerable<ViewModelMovie> GetViewModelMovies(MovieDatabaseContext movieDatabase)
        {
            var listOfMovies = movieDatabase.Movies.AsEnumerable().Select(movie => new ViewModelMovie
            {
                Id = movie.Id,
                Title = movie.Title,
                Duration = movie.Duration + " minutes",
                Genres = string.Join(", ", movie.Genres.Select(x => string.Concat(x.Name))),
                Director = movie.Director.Name,
                Actors = string.Join(", ", movie.Actors.Select(x => string.Concat(x.Name))),
                ReleaseDate = movie.ReleaseDate.ToString("MMMM dd, yyyy"),
                Countries = string.Join(", ", movie.Countries.Select(x => string.Concat(x.Name))),
                BoxOffice = movie.BoxOffice.ToString("# ###"),
                ImdbRating = movie.ImdbRating,
                Plot = movie.Plot
            });

            return  listOfMovies;
        }

        public static IEnumerable<ViewModelMovie> GetUserMovies(MovieDatabaseContext movieDatabase, int userId)
        {
            var listOfMovies = from movie in movieDatabase.Movies.AsEnumerable() 
                               join movieUser in movieDatabase.Movies_Users on movie.Id equals movieUser.MovieId
                               join user in movieDatabase.User on movieUser.UserId equals user.Id
                               where user.Id == userId                              
                               select  ( new ViewModelMovie
            {
                Id = movie.Id,
                Title = movie.Title,
                Duration = movie.Duration + " minutes",
                Genres = string.Join(", ", movie.Genres.Select(x => string.Concat(x.Name))),
                Director = movie.Director.Name,
                Actors = string.Join(", ", movie.Actors.Select(x => string.Concat(x.Name))),
                ReleaseDate = movie.ReleaseDate.ToString("MMMM dd, yyyy"),
                Countries = string.Join(", ", movie.Countries.Select(x => string.Concat(x.Name))),
                BoxOffice = movie.BoxOffice.ToString("# ###"),
                ImdbRating = movie.ImdbRating,
                Plot = movie.Plot
            });

            return listOfMovies;
        }
    }    
}
