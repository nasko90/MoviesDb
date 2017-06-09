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
using MovieAdminsPostgreeDb.PostgreeDb.Context;
using MoviesDatabaseWPF.Windows;

namespace MoviesDatabaseWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string userNameErrorMessage = "Please enter a valid username! ";
        private const string passwordErrorMessage = "Incorrect password. Please try again!";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            bool isPassCorrect = false;
            var movieDatabase = new MovieDatabaseContext();

            if (this.LogAsAdminCheckBox.IsChecked == true)
            {
                var adminDb = new MovieAdminsContext();
                var checker = new Checker(adminDb);
                var admin = checker.CheckAdmin(this.userNameInput.Text);

                if (user != null)
                {
                    isPassCorrect = checker.IsAdminPassCorrect(passwordInput.Password.ToString(), admin);
                }

                if (user == null)
                {
                    this.userAndPassErrors.Text = userNameErrorMessage;
                    this.userNameInput.Clear();
                }
                else if (isPassCorrect)
                {
                    AdminWindow logedInWindow = new AdminWindow(movieDatabase, adminDb, admin);
                    logedInWindow.Show();
                    this.Close();
                }
                else
                {
                    this.userAndPassErrors.Text = passwordErrorMessage;
                    this.passwordInput.Clear();
                }
            }

            else
            {

                var checker = new Checker(movieDatabase);
                var user = checker.CheckUser(this.userNameInput.Text);

                if (user != null)
                {
                    isPassCorrect = checker.IsUserPassCorrect(passwordInput.Password.ToString(), user);
                }

                if (user == null)
                {
                    this.userAndPassErrors.Text = userNameErrorMessage;
                    this.userNameInput.Clear();
                }
                else if (isPassCorrect)
                {
                    LoggedInWindow logedInWindow = new LoggedInWindow(movieDatabase, user);
                    logedInWindow.Show();
                    this.Close();
                }
                else
                {
                    this.userAndPassErrors.Text = passwordErrorMessage;
                    this.passwordInput.Clear();
                }
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
