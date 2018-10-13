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
using System.IO;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour CreationProjet.xaml
    /// </summary>
    public partial class CreationProjet : Page
    {
        public CreationProjet()
        {
            InitializeComponent();
        }

        private void btnClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnRetour(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/PreparerHome.xaml", UriKind.Relative));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Voulez vous annuler la création du projet ? ","Annuler Projet", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                this.NavigationService.Navigate(new Uri("Pages/PreparerHome.xaml", UriKind.Relative));
            }
            
        }

        private void btnValid(object sender, RoutedEventArgs e)
        {
            string test = txtNomInstallation.Text;
            string conn = "Data Source=EcoDB.db;Version=3";
            SQLiteConnection connection = new SQLiteConnection(conn);
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Systeme (nomSysteme, idPID, installation) VALUES (@nomSysteme, @idPid, @nomInstallation)", connection);
            cmd.Parameters.AddWithValue("@nomInstallation", txtNomInstallation.Text);
            cmd.Parameters.AddWithValue("@idPid", txtNomPid.Text);
            cmd.Parameters.AddWithValue("@nomSysteme", txtNomSysteme.Text);
            try
            {
                cmd.ExecuteNonQuery();

                string path = AppDomain.CurrentDomain.BaseDirectory + "Projets/" + txtNomSysteme.Text;
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    // Try to create the directory.
                    Directory.Delete(path, true);
                }
                Directory.CreateDirectory(path);
                MessageBox.Show("Création du projet : " + test);
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

    }
}
