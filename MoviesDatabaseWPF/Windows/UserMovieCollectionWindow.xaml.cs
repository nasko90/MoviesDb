using MoviesDatabase.Context;
using MoviesDatabase.Models.Models;
using MoviesDatabaseWPF.ViewModelObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Linq.Dynamic;
using System.Windows.Controls;

namespace MoviesDatabaseWPF.Windows
{
    /// <summary>
    /// Interaction logic for UserMovieCollectionWindow.xaml
    /// </summary>
    public partial class UserMovieCollectionWindow : Window
    {
        private readonly MovieDatabaseContext db;
        private readonly User currentUser;

        public UserMovieCollectionWindow(MovieDatabaseContext db, User currentUser)
        {
            this.db = db;
            this.currentUser = currentUser;
            InitializeComponent();

            var userMovies =
                    from movieInUserCollection in db.Movies_Users
                    join user in db.User on movieInUserCollection.UserId equals user.Id
                    where user.Id == this.currentUser.Id
                    select movieInUserCollection.Movie;

            this.UserMoviesModel = ViewModelMovie.ConvertMoviesToVeiwModelMovies(userMovies.ToList());
            this.userMovieColection.ItemsSource = this.UserMoviesModel;
        }

        public IEnumerable UserMoviesModel { get; set; }
        public int HeaderClicks { get; set; }

        private void titleColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            this.HeaderClicks++;
            var headerName = GetColumnHeaderName(0);

            if (this.HeaderClicks % 2 != 0)
            {
                var movieModels = ViewModelMovie.ConvertMoviesToVeiwModelMovies(OrderMovieAscending(headerName));
                this.userMovieColection.ItemsSource = movieModels;
            }
            else
            {
                var movieModels = ViewModelMovie.ConvertMoviesToVeiwModelMovies(OrderMovieDescending(headerName));
                this.userMovieColection.ItemsSource = movieModels;
            }
            
        }

        private IEnumerable<Movie> OrderMovieAscending(string orderBy)
        {
            var orderedByTitle =
                    from movieInUserCollection in db.Movies_Users
                    join user in db.User on movieInUserCollection.UserId equals user.Id
                    where user.Id == this.currentUser.Id
                    select movieInUserCollection.Movie;          

            return orderedByTitle.OrderBy(orderBy).ToList();

            
        }

        private IEnumerable<Movie> OrderMovieDescending(string orderBy)
        {
            var orderedByTitle =
                    from movieInUserCollection in db.Movies_Users
                    join user in db.User on movieInUserCollection.UserId equals user.Id
                    where user.Id == this.currentUser.Id
                    select movieInUserCollection.Movie;

            return orderedByTitle.OrderBy(orderBy + " Desc").ToList();
        }

        private string GetColumnHeaderName(int columnIndex)
        {
            var headerColumn = ((GridView)userMovieColection.View);
            var toRemove = headerColumn.Columns[columnIndex].Header.ToString().LastIndexOf(':');
            var headerName = headerColumn.Columns[columnIndex].Header.ToString().Substring(toRemove + 2);

            return headerName;
        }

        

        private void durationColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void boxOfficeColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            this.HeaderClicks++;
            var headerName = GetColumnHeaderName(5);

            if (this.HeaderClicks % 2 != 0)
            {
                var movieModels = ViewModelMovie.ConvertMoviesToVeiwModelMovies(OrderMovieAscending(headerName));
                this.userMovieColection.ItemsSource = movieModels;
            }
            else
            {
                var movieModels = ViewModelMovie.ConvertMoviesToVeiwModelMovies(OrderMovieDescending(headerName));
                this.userMovieColection.ItemsSource = movieModels;
            }
        }
    }
}
