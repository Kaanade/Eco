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
    /// Logique d'interaction pour Affecter.xaml
    /// </summary>
    public partial class Affecter : Page
    {
        public Affecter()
        {
            InitializeComponent();

            string conn = "Data Source=EcoDB.db;Version=3";
            SQLiteConnection connection = new SQLiteConnection(conn);

            //Remplir liste projets
            SQLiteCommand cmd = new SQLiteCommand("Select * from Installation", connection);
            SQLiteDataAdapter adapt = new SQLiteDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapt.Fill(ds, "Projects");
            ListProjects.ItemsSource = ds.Tables["Projects"].DefaultView;

            SQLiteCommand cmd2 = new SQLiteCommand("Select * from Utilisateurs", connection);
            SQLiteDataAdapter adapt2 = new SQLiteDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            adapt2.Fill(ds2, "Utilisateurs");
            ListUsers.ItemsSource = ds2.Tables["Utilisateurs"].DefaultView;
            

            connection.Close();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            if (this.GridSlideMenu.IsVisible)
                this.GridSlideMenu.Visibility = Visibility.Collapsed;
            else
                this.GridSlideMenu.Visibility = Visibility.Visible;
        }

        private void btnClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnRetour(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/PreparerHome.xaml", UriKind.Relative));
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView drv = (DataRowView)ListProjects.SelectedItem;
            String result = (drv["nomInstallation"]).ToString();
            MessageBoxResult messageBoxResult = MessageBox.Show("Voulez vous sélectionner le projet : " + result + " ?", "Selection Projet", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                MessageBox.Show("Oui");
            }
            else
            {
                MessageBox.Show("Non");

            }
        }

        private void btnCreationIntervenant(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/CreationIntervenant.xaml", UriKind.Relative));
        }
    }
}
