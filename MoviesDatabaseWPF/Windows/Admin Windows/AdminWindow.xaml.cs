using MoviesDatabase.Context;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using MovieAdminsPostgreeDb.PostgreeDb.Context;
using MovieDatabase.PostgreeDatabase.Models;
using MoviesDatabase;
using MoviesDatabase.ModelParsers;
using MoviesDatabaseWPF.Windows.Add_to_DB_windows;
using MoviesDatabaseWPF.ViewModelObjects;
using MoviesDatabaseWPF.Windows.Admin_Windows;

namespace MoviesDatabaseWPF.Windows
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private readonly MovieDatabaseContext movieDatabase;
        private readonly MovieAdminsContext adminsDatabase;
        private readonly Admin currentAdmin;

        public AdminWindow(MovieDatabaseContext movieDatabase, MovieAdminsContext adminsDatabase, Admin admin)
        {
            this.movieDatabase = movieDatabase;
            this.adminsDatabase = adminsDatabase;
            this.currentAdmin = admin;

            InitializeComponent();

            this.AdminUserName.Text = this.currentAdmin.UserName;
            this.Permissions.Text = string.Join(", ", this.currentAdmin.Permissions.Select(x => string.Concat(x.Name)));
        }

        private void ShowMovieDbCollection_Click(object sender, RoutedEventArgs e)
        {         
            var listOfMovies = ViewModelMovie.GetViewModelMovies(this.movieDatabase);

            var collectionWindow = new CollectionsWindow(listOfMovies.ToList());
            collectionWindow.Show();
        }

        private void AddMovieManually_Click(object sender, RoutedEventArgs e)
        {
            var addMovieWindow = new AddMovieWindow(this.movieDatabase);
            addMovieWindow.Show();            
        }

        private void AddMovieFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".json";
            dlg.Filter = "Json Files (*.json)|*.json";
            var result = dlg.ShowDialog();

            var parser = new Parser();
            var movieConverter = new MovieConverter(this.movieDatabase);
            var messageBoxText = new StringBuilder();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Open document              
                try
                {
                    var parsedMovies = parser.ParseMovies(dlg.FileName);
                    messageBoxText.AppendLine("Films added in the database:");
                    foreach (var parsedMovie in parsedMovies)
                    {
                        if (!this.movieDatabase.Movies.Any(x => x.Title.Equals(parsedMovie.Title)))
                        {
                            messageBoxText.Append(parsedMovie.Title + ", ");
                        }

                        movieConverter.AddOrUpdateToDatabase(parsedMovie);
                    }
                }
                catch (Exception)
                {
                    messageBoxText.AppendLine("Erorr in parsing the JSON file, no movies were added! ");
                }

                System.Windows.MessageBox.Show(messageBoxText.ToString());
            }
        }

        private void AddActorOrDirectorFromFile_Click(object sender, RoutedEventArgs e)
        {            
            var addDirectorOrActorWindow = new AddDirectorOrActorFromFile();
            addDirectorOrActorWindow.Show();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void AddActorOrDirectorManualy_Click(object sender, RoutedEventArgs e)
        {
            var addActorOrDirectorManuallyWindow = new AddDirectorOrActorManuallyWindow(this.movieDatabase);
            addActorOrDirectorManuallyWindow.Show();
        }

        private void ShowActorsCollection_Click(object sender, RoutedEventArgs e)
        {          
            var listOfActors = this.movieDatabase.Actors.AsEnumerable().Select(person => new ViewModelPerson
            {
                Name = person.Name,
                DateOfBirth = person.DateOfBirth != null ? person.DateOfBirth.ToString() : "N/A",
                Nationality = person.Country != null ? person.Country.Name : "N/A",
                Gender = person.Gender.ToString()
            });

            var collectionWindow = new CollectionsWindow(listOfActors.ToList());
            collectionWindow.Show();
        }

        private void ShowDirectorCollection_Click(object sender, RoutedEventArgs e)
        {
            var listOfDirectors = movieDatabase.Directors.AsEnumerable().Select(direcotr => new ViewModelPerson
            {
                Name = direcotr.Name,
                DateOfBirth = direcotr.DateOfBirth != null ? direcotr.DateOfBirth.ToString() : "N/A",
                Nationality = direcotr.Country != null ? direcotr.Country.Name : "N/A",
                Gender = direcotr.Gender.ToString()
            });

            var collectionWindow = new CollectionsWindow(listOfDirectors.ToList());
            collectionWindow.Show();
        }    
    }
}
