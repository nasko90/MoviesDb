using MoviesDatabase.Context;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MovieAdminsPostgreeDb.PostgreeDb.Context;
using MovieDatabase.PostgreeDatabase.Models;
using MoviesDatabase;
using MoviesDatabase.ModelParsers;
using MoviesDatabaseWPF.Windows.Add_to_DB_windows;
using MessageBox = System.Windows.MessageBox;

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
            this.Permissions.Text = string.Join(", ", currentAdmin.Permissions.Select(x => string.Concat(x.Name)));
        }

        private void ShowMovieDbCollection_Click(object sender, RoutedEventArgs e)
        {

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
                catch (Exception exception)
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
    }
}
