using MoviesDatabase.Context;
using MoviesDatabase.Models.Models;
using MoviesDatabaseWPF.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MoviesDatabaseWPF.Windows.Add_to_DB_windows
{
    /// <summary>
    /// Interaction logic for AddMovieWindow.xaml
    /// </summary>
    public partial class AddMovieWindow : Window
    {
        private MovieDatabaseContext movieDb;

        public AddMovieWindow(MovieDatabaseContext movieDb)
        {
            InitializeComponent();
            this.movieDb = movieDb;
        }

        private void AddMovie_Click(object sender, RoutedEventArgs e)
        {

            this.ErrorMessage.Visibility = Visibility.Hidden;

            var title = this.Title.Text;
            var databaseUpdater = new DatabaseUpdater(this.movieDb);

            this.ErrorMessage.Text = databaseUpdater.AddMovieToDatabaseManually(
                this.Title.Text, this.Countries.Text, this.BoxOffice.Text,
                this.Duration.Text, this.IMDBrating.Text, this.Actors.Text, this.Director.Text,
                this.Genres.Text, this.ReleaseDate.Text, this.Plot.Text);
            this.ErrorMessage.Visibility = Visibility.Visible;
        }
    }
}
