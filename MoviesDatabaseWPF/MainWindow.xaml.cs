using MoviesDatabase.Context;
using MoviesDatabase.Models.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using MoviesDatabaseWPF.Windows;

namespace MoviesDatabaseWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            var db = new MovieDatabaseContext();
            var checker = new Checker(db);
            var user = checker.CheckUser(this.userNameInput.Text);
            bool isPassCorrect = false;

            if (user != null)
            {
              isPassCorrect   = checker.IsPassCorrect(passwordInput.Password.ToString(), user);
            }

            if (user == null)
            {
                this.userAndPassErrors.Text = "Please enter a valid username! ";
                this.userNameInput.Clear();
            }
            else if (isPassCorrect)
            {
                LoggedInWindow logedInWindow = new LoggedInWindow(db, user);
                logedInWindow.Show();
                this.Close();
            }
            else
            {
                this.userAndPassErrors.Text = "Incorrect password. Please try again!";
                this.passwordInput.Clear();
            }            
        }

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {
            var db = new MovieDatabaseContext();
            SignUpWindow signUpWindow = new SignUpWindow(db);
            signUpWindow.Show();
            this.Close();
        }
    }
}
