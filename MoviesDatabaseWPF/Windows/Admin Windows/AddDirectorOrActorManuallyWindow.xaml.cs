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
using MoviesDatabase.Context;
using MoviesDatabaseWPF.Classes;

namespace MoviesDatabaseWPF.Windows.Add_to_DB_windows
{
    /// <summary>
    /// Interaction logic for AddDirectorOrActorManuallyWindow.xaml
    /// </summary>
    public partial class AddDirectorOrActorManuallyWindow : Window
    {
        private readonly MovieDatabaseContext movieDb;

        public AddDirectorOrActorManuallyWindow(MovieDatabaseContext movieDb)
        {
            InitializeComponent();
            this.movieDb = movieDb;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.LogMessage.Visibility = Visibility.Hidden;
            var dbUpdater = new DatabaseUpdater(this.movieDb);

            if (this.Actor.IsChecked == true)
            {
                this.LogMessage.Text = dbUpdater.AddActorToDatabase(this.Name.Text, this.DateOfBirth.Text,
                    this.Country.Text, this.Gender.Text);
            }
            else
            {
                this.LogMessage.Text = dbUpdater.AddDirectorToDatabase(this.Name.Text, this.DateOfBirth.Text,
                    this.Country.Text, this.Gender.Text);
            }
            this.LogMessage.Visibility = Visibility.Visible;
        }
    }
}
