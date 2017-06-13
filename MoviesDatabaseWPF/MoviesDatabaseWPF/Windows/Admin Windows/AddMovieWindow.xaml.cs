using MoviesDatabase.Context;
using MoviesDatabaseWPF.Classes;
using System.Windows;
using MovieAdminsPostgreeDb.PostgreeDb.Context;

namespace MoviesDatabaseWPF.Windows.Add_to_DB_windows
{
    /// <summary>
    /// Interaction logic for AddMovieWindow.xaml
    /// </summary>
    public partial class AddMovieWindow : Window
    {
        private readonly MovieDatabaseContext movieDb;

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
                this.AddTitle.Text, this.AddCountries.Text, this.BoxOffice.Text,
                this.AddDration.Text, this.IMDBrating.Text, this.AddActors.Text, this.AddDirector.Text,
                this.AddGenres.Text, this.ReleaseDate.Text, this.AddPlot.Text);

            this.ErrorMessage.Visibility = Visibility.Visible;
        }
    }
}
