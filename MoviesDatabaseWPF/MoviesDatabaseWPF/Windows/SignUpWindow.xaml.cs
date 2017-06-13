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
using System.Windows.Shapes;

namespace MoviesDatabaseWPF
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private readonly MovieDatabaseContext db;

        public SignUpWindow(MovieDatabaseContext db)
        {
            this.db = db;
            InitializeComponent();
        }

        private void signUp_button_Click(object sender, RoutedEventArgs e)
        {
            var checker = new Checker(this.db);
            var pass = password.Password;
            var verifyPass = passwordVerify.Password;

            this.passErrorMessage.Text = string.Empty;
            this.userNameExist.Text = string.Empty;

            if (!pass.Equals(verifyPass))
            {
                this.passErrorMessage.Text = "Please verify your password";
                this.password.Password = string.Empty;
                this.passwordVerify.Password = string.Empty;
            }
            else if (pass.Length < 8)
            {
                this.passErrorMessage.Text = "The password must be atleast 8 characters";
                this.password.Password = string.Empty;
                this.passwordVerify.Password = string.Empty;
            }
            else
            {
                var existingUserName = checker.CheckUser(this.userName.Text);

                if (existingUserName != null)
                {
                    this.userNameExist.Text = "This user name is already taken. Please, choose another one!";
                    this.userName.Text = string.Empty;
                }
                else
                {
                    var newUser = new User
                    {
                        UserName = userName.Text,
                        ClearTextPassword = pass,
                        FullName = fullName.Text,
                        EmailAddress = email.Text
                    };

                    db.User.Add(newUser);
                    db.SaveChanges();
                    this.succesfullyCreatedUser.Text = "You have succesfully create your account";
                    this.backToSignIn.Visibility = System.Windows.Visibility.Visible;
                }
            }            
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passLenght = password.Password.Length;
            if (passLenght <= 0)
            {
                this.passBar.Value = 0;
                this.passBar.Foreground = new SolidColorBrush(Colors.DarkRed);
            }
            else if (passLenght > 0 && passLenght < 3)
            {
                this.passBar.Value = 25;
                this.passBar.Foreground = new SolidColorBrush(Colors.Red);
            }
            else if (passLenght > 2 && passLenght < 6)
            {
                this.passBar.Value = 50;
                this.passBar.Foreground = new SolidColorBrush(Colors.Yellow);
            }
            else if (passLenght > 5 && passLenght < 8)
            {
                this.passBar.Value = 75;
                this.passBar.Foreground = new SolidColorBrush(Colors.YellowGreen);
            }
            else
            {
                this.passBar.Value = 100;
                this.passBar.Foreground = new SolidColorBrush(Colors.Green);
            }
        }

        private void backToSignIn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
