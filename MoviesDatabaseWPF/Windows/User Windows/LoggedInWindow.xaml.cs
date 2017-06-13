﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Forms;
using MoviesDatabase.Context;
using MoviesDatabase.Models.Models;
using MoviesDatabaseWPF.Classes;
using MoviesDatabaseWPF.ViewModelObjects;
using MoviesDatabaseWPF.Windows.Admin_Windows;

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
            var selectedMovies = new List<ViewModelMovie>();
            var listOfMoviesThatExist = new StringBuilder();
            this.allreadyAddedMovieToUserErrorText.Visibility = Visibility.Hidden;

            if (filteredMovies.SelectedItems.Count == 0)
            {
                searchError.Text = "No movie selected!";
            }
            else
            {
                foreach (var movie in filteredMovies.SelectedItems)
                {                   
                    selectedMovies.Add((ViewModelMovie)movie);
                }

                foreach (var movie in selectedMovies)
                {
                    var userMovies = new Movie_User();
                    userMovies.User = this.currentUser;
                    userMovies.Movie = ConvertFromVeiwModelMovietoMovie(movie);
                    bool isMovieExisting = CheckIfUserAllreadyHaveThisMovie(userMovies);

                    if (isMovieExisting)
                    {
                        listOfMoviesThatExist.AppendLine(movie.Title);
                    }
                    else
                    {
                        db.Movies_Users.Add(userMovies);
                        db.SaveChanges();
                    }                   
                }

                if (listOfMoviesThatExist.Length > 0)
                {
                    this.allreadyAddedMovieToUserErrorText.Visibility = Visibility.Visible;
                    this.allreadyAddedMovieToUserErrorText.Text = 
                        $"This movies already exist in the user collection: {Environment.NewLine}{listOfMoviesThatExist.ToString()}";
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
            var userMovies = ViewModelMovie.GetUserMovies(this.db, this.currentUser.Id);
            var collectionWindow = new CollectionsWindow(userMovies.ToList());
            collectionWindow.Show();

        }       

        private bool CheckIfUserAllreadyHaveThisMovie(Movie_User movieUser)
        {
            bool isExisting = db.Movies_Users.Any(x => x.User.Id == movieUser.User.Id && x.Movie.Id == movieUser.Movie.Id);
            return isExisting;
        }

        private void ExportMoviecollectionToPdf_Click(object sender, RoutedEventArgs e)
        {
            var userMovies = from movie in this.db.Movies.AsEnumerable()
                join movieUser in this.db.Movies_Users on movie.Id equals movieUser.MovieId
                join user in this.db.User on movieUser.UserId equals user.Id
                where user.Id == this.currentUser.Id
                select (new 
                {
                    Title = movie.Title,
                    Duration = movie.Duration + " minutes",
                    Genres = string.Join(", ", movie.Genres.Select(x => string.Concat(x.Name))),
                    Director = movie.Director.Name,
                    Actors = string.Join(", ", movie.Actors.Select(x => string.Concat(x.Name))),
                    ReleaseDate = movie.ReleaseDate.ToString("yyyy"),
                    ImdbRating = movie.ImdbRating,
                });

            var dataGridView = new GridView();
            dataGridView.DataSource = userMovies.ToList();
            dataGridView.DataBind();
            var pdfExporter = new PdfExoirter();
            pdfExporter.ExportGridToPdf(dataGridView, this.currentUser);
        }
    }
}

