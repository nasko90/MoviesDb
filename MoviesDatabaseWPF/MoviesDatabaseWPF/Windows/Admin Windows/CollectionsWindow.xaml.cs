using MoviesDatabaseWPF.ViewModelObjects;
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

namespace MoviesDatabaseWPF.Windows.Admin_Windows
{
    /// <summary>
    /// Interaction logic for CollectionsWindow.xaml
    /// </summary>
    public partial class CollectionsWindow : Window
    {
        public CollectionsWindow(IEnumerable<IViewable> collection)
        {
            InitializeComponent();
            ShowMovies.ItemsSource = collection;
        }
    }
}
