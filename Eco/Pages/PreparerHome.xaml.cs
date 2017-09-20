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
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System.IO;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour PreparerHome.xaml
    /// </summary>
    public partial class PreparerHome : Page
    {
        public PreparerHome()
        {
            InitializeComponent();

            string conn = "Data Source=EcoDB.db;Version=3";
            SQLiteConnection connection = new SQLiteConnection(conn);
            SQLiteCommand cmd = new SQLiteCommand("Select * from Installation", connection);

            if(Convert.ToInt32(App.Current.Properties["niveau"]) != 0)
            {
                but_Exportdb.Visibility = Visibility.Collapsed;
            }
            
        }

        //Navigation
        private void btnCreerProjet(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/CreationProjet.xaml", UriKind.Relative));
        }

        private void btnAffecter(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/Affecter.xaml", UriKind.Relative));
        }

        private void btnAddEquip(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new Uri("Pages/ImportationEquipement.xaml", UriKind.Relative));

            ModalAddTypeEquip modalAddTypeEquip = new ModalAddTypeEquip();
            modalAddTypeEquip.ShowDialog();
        }
       

        private void btnClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnRetour(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/Home.xaml", UriKind.Relative));
        }

        private void prepPid(object sender, MouseButtonEventArgs e)
        {
         
            /*MessageBoxResult messageBoxResult = MessageBox.Show("Voulez vous sélectionner le projet : " + result + " ?", "Selection Projet", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                MessageBox.Show("Oui");
                //Properties.Settings.Default.Project = result;
            }
            else
            {
                MessageBox.Show("Non");
            }*/

            //ModalSysteme modalSysteme = new ModalSysteme(installation);
            //modalSysteme.ShowDialog();

            //if(modalSysteme.Valid)
            //    this.NavigationService.Navigate(new PidPrep(modalSysteme.Systeme));


        }

        private void btnSelSys(object sender, RoutedEventArgs e)
        {
            ModalPIDHome modalPIDHome = new ModalPIDHome();
            modalPIDHome.ShowDialog();

            if (modalPIDHome.Valid)
            {
                string systeme = modalPIDHome.RSysteme;
                this.NavigationService.Navigate(new PidPrep(modalPIDHome.RSysteme));
            }
        }

        private void btnExportDB(object sender, RoutedEventArgs e)
        {
            string path = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;
            }
            
            string text = File.ReadAllText(path);
            text = text.Replace("AUTOINCREMENT", "AUTO_INCREMENT");
            text = text.Replace("TEXT", "varchar(100) COLLATE utf8_bin NOT NULL");
            text = text.Replace("INTEGER", "int(11)");
            text = text.Replace("\"", "");
            text = text.Replace("`", "");
            text = text.Replace("BEGIN TRANSACTION;", "");
            text = text.Replace("COMMIT;", "");
            File.WriteAllText(path, text);

            MessageBox.Show("Exportation Réussie");

        }


    }
}
