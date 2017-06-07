using MoviesDatabase.Context;
using MoviesDatabase.Models.Models;
using MoviesDatabaseWPF.Classes;
using MoviesDatabaseWPF.ViewModelObjects;
using System.Collections.Generic;
using System.Windows;

namespace MoviesDatabaseWPF.Windows
{
    /// <summary>
    /// Interaction logic for LoggedInWindow.xaml
    /// </summary>
    public partial class LoggedInWindow : Window
    {
        private readonly MovieDatabaseContext db;
        private readonly User currentUser;

        public LoggedInWindow(MovieDatabaseContext db, User currentUser)
        {
            InitializeComponent();
            this.db = db;

            this.currentUser = currentUser;
            this.email.Text = currentUser.EmailAddress;
            this.userName.Text = currentUser.UserName;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.searchError.Text = string.Empty;
            var movies = new List<Movie>();
            filteredMovies.ItemsSource = null;

            var searcher = new Searcher(this.db);

            if (searchByTitle.IsChecked == true)
            {
                movies = searcher.SearchByTitle(this.search.Text);
                var viewModelMovies = ViewModelMovie.ConvertMoviesToVeiwModelMovies(movies);              

                if (movies.Count == 0)
                {
                    this.searchError.Text = "No film with this title was found! ";
                }
                else
                {
                    filteredMovies.ItemsSource = viewModelMovies;
                }

            }

            if (searchByGenre.IsChecked == true)
            {
                movies = searcher.SearchByGenre(this.search.Text);
                var viewModelMovies = ViewModelMovie.ConvertMoviesToVeiwModelMovies(movies);              

                if (movies.Count == 0)
                {
                    this.searchError.Text = "No film in this genre was found! ";
                }
                else
                {
                    filteredMovies.ItemsSource = viewModelMovies;
                }
            }

            if (searchByActor.IsChecked == true)
            {
                movies = searcher.SearchByActor(this.search.Text);
                var viewModelMovies = ViewModelMovie.ConvertMoviesToVeiwModelMovies(movies);               

                if (movies.Count == 0)
                {
                    this.searchError.Text = "No film with this actor was found! ";
                }
                else
                {
                    filteredMovies.ItemsSource = viewModelMovies;
                }
            }

            if (searchByDirector.IsChecked == true)
            {
                movies = searcher.SearchByDirector(this.search.Text);
                var viewModelMovies = ViewModelMovie.ConvertMoviesToVeiwModelMovies(movies);               

                if (movies.Count == 0)
                {
                    this.searchError.Text = "No film with this director was found! ";
                }
                else
                {
                    filteredMovies.ItemsSource = viewModelMovies;
                }
            }

        }

        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (filteredMovies.SelectedItems.Count == 0)
            {
                searchError.Text = "No movie selected!";
            }
            else
            {
                var selectedMovies = new List<ViewModelMovie>();

                foreach (var movie in filteredMovies.SelectedItems)
                {
                    selectedMovies.Add((ViewModelMovie)movie);
                }

                foreach (var movie in selectedMovies)
                {
                    var userMovies = new Movie_User();
                    userMovies.User = this.currentUser;
                    userMovies.Movie = ConvertFromVeiwModelMovietoMovie(movie);
                    db.Movies_Users.Add(userMovies);
                    db.SaveChanges();
                }
            }
        }

        private Movie ConvertFromVeiwModelMovietoMovie(ViewModelMovie modelMovie)
        {
            var movie = this.db.Movies.Find(modelMovie.Id);

            return movie;
        }

        private void userCollection_Click(object sender, RoutedEventArgs e)
        {
            UserMovieCollectionWindow userCollectionWindow = new UserMovieCollectionWindow(this.db, this.currentUser);
            userCollectionWindow.Show();
            this.Close();
        }
    }
}

