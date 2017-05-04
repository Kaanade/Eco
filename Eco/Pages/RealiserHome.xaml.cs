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
using System.Data;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour RealiserHome.xaml
    /// </summary>
    public partial class RealiserHome : Page
    {
        public RealiserHome()
        {
            InitializeComponent();
        }

        private void btnClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnRetour(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/Home.xaml", UriKind.Relative));
        }

        private void btnPid(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/Pid.xaml", UriKind.Relative));
        }

        private void btnDoc(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new Uri("Pages/RealDocumentation.xaml", UriKind.Relative));
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView drv = (DataRowView)ListProjects.SelectedItem;
            String result = (drv["nomInstallation"]).ToString();
            MessageBoxResult messageBoxResult = MessageBox.Show("Voulez vous sélectionner le projet : " + result + " ?", "Selection Projet", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                MessageBox.Show("Oui");
                //Properties.Settings.Default.Project = result;
            }
            else
            {
                MessageBox.Show("Non");
            }
        }
    }
}
