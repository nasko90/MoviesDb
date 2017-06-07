using MoviesDatabase.Context;
using MoviesDatabase.Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabaseWPF.ViewModelObjects
{
    public class ViewModelMovie
    {
        public ViewModelMovie(Movie movie)
        {
            this.Id = movie.Id;
            this.Title = movie.Title;
            this.Duration = string.Format("{0} minutes", movie.Duration);
            this.BoxOffice = movie.BoxOffice.ToString("# ##0");
            this.Genres = string.Join(", ", movie.Genres.Select(x => string.Concat(x.Name)));
            this.Actors = string.Join(", ", movie.Actors.Select(x => string.Concat(x.Name)));
            this.Director = movie.Director.Name;
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public string Duration { get; set; }

        public string Genres { get; set; }

        public string Director { get; set; }

        public string Actors { get; set; }

        public string BoxOffice { get; set; }

        public static IEnumerable<ViewModelMovie> ConvertMoviesToVeiwModelMovies(IEnumerable<Movie> movies)
        {
            var viewModelMovies = new ObservableCollection<ViewModelMovie>();
            
            foreach (var movie in movies)
            {
                viewModelMovies.Add(new ViewModelMovie(movie));
            }

            return viewModelMovies;
        }
    }    
}
