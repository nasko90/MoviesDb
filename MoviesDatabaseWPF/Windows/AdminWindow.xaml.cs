using MoviesDatabase.Context;
using System;
using System.Collections.Generic;
using System.Linq;
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
using MovieAdminsPostgreeDb.PostgreeDb.Context;
using MovieDatabase.PostgreeDatabase.Models;

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
            
        }
    }
}
