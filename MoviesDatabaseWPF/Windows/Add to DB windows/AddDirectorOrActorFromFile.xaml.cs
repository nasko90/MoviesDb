using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using MoviesDatabase.Converters;
using MoviesDatabase.ModelParsers;
using MoviesDatabase.Context;

namespace MoviesDatabaseWPF.Windows.Add_to_DB_windows
{
    /// <summary>
    /// Interaction logic for AddDirectorOrActorFromFile.xaml
    /// </summary>
    public partial class AddDirectorOrActorFromFile : Window
    {
        public AddDirectorOrActorFromFile()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var movieDb = new MovieDatabaseContext();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".json";
            dlg.Filter = "Json Files (*.json)|*.json";
            var result = dlg.ShowDialog();

            var parser = new Parser();
            var messageBoxText = new StringBuilder();


            if (result == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    var parsedPersons = parser.ParsePersons(dlg.FileName);
                   
                    if (this.Actors.IsChecked == true)
                    {
                        messageBoxText.AppendLine("The following actors were added to Database");
                        var actorConv = new ActorConverter(movieDb);
                        foreach (var actor in parsedPersons)
                        {
                            if (!movieDb.Actors.Any(x => x.Name.Equals(actor.Name)))
                            {
                                messageBoxText.Append(actor.Name + ", ");
                            }
                            actorConv.AddOrUpdateActorInfo(actor);                          
                        }
                    }
                    else
                    {
                        var directorConv = new DirectorConverter(movieDb);
                        foreach (var director in parsedPersons)
                        {
                            if (!movieDb.Directors.Any(x => x.Name.Equals(director.Name)))
                            {
                                messageBoxText.Append(director.Name + ", ");
                            }

                            directorConv.AddOrUpdateDirectorInfo(director);
                        }
                    }
                    System.Windows.MessageBox.Show(messageBoxText.ToString());
                }
                catch (Exception exception)
                {
                    messageBoxText.AppendLine("Erorr in adding actors/directors file! ");
                }    

                this.Close();               
            }               
        }
    }
}
