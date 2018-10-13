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
            try
            {
                InitializeComponent();

                string conn = "Data Source=EcoDB.db;Version=3";
                SQLiteConnection connection = new SQLiteConnection(conn);

                SQLiteCommand cmd = new SQLiteCommand("Select nomSysteme from Systeme WHERE installation = @installation", connection);
                cmd.Parameters.AddWithValue("@installation", App.Current.Properties["installation"]);

                SQLiteDataAdapter adapt = new SQLiteDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds, "Systemes");
                ListSysteme.ItemsSource = ds.Tables["Systemes"].DefaultView;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
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

        private void Row_Click(object sender, MouseButtonEventArgs e)
        {
            DataRowView drv = (DataRowView)ListSysteme.SelectedItem;
            String result = (drv["nomSysteme"]).ToString();

            this.NavigationService.Navigate(new Pid(result));
            
            
        }
    }
}
