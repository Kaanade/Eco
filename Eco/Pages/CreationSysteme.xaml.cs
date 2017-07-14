using System;
using System.IO;
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
using Microsoft.Win32; 
using System.Data;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour CreationSysteme.xaml
    /// </summary>
    public partial class CreationSysteme : Page
    {
        public CreationSysteme()
        {
            InitializeComponent();
        }
        

        private void btnClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnRetour(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/ImportationEquipement.xaml", UriKind.Relative));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Voulez vous annuler la création du système ? ", "Annuler Système", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                this.NavigationService.Navigate(new Uri("Pages/PreparerHome.xaml", UriKind.Relative));
            }

        }

        private void btnDocSys(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                txtPathDoc.Text = openFileDialog.FileName;
        }

        private void btnPidSys(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                txtPathPID.Text = openFileDialog.FileName;
        }

        private void btnValid(object sender, RoutedEventArgs e)
        {
            System.IO.Directory.CreateDirectory("Projets");
            System.IO.Directory.CreateDirectory("Projets/" + txtNomSysteme.Text);

            File.Copy(@txtPathPID.Text, @"Projets/" + txtNomSysteme.Text + "/" + System.IO.Path.GetFileName(txtPathPID.Text));

            string nomSysteme = txtNomSysteme.Text;
           // string pathDoc = modalProc.NumProc;
            string nomPid = System.IO.Path.GetFileNameWithoutExtension(txtPathPID.Text);

            string conn = "Data Source=EcoDB.db;Version=3";
            SQLiteConnection connection = new SQLiteConnection(conn);
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Systeme (nomSysteme, idPID) VALUES (@nomSysteme,@nomPid)", connection);
            cmd.Parameters.AddWithValue("@nomSysteme", nomSysteme);
            cmd.Parameters.AddWithValue("@nomPid", nomPid);

            try
            {
                int a = cmd.ExecuteNonQuery();

                if (a == 0)
                {
                    MessageBox.Show("Erreur, veuillez vérifier vos informations.");
                }
                else
                {
                    this.NavigationService.Navigate(new Uri("Pages/ImportationEquipement.xaml", UriKind.Relative));
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
