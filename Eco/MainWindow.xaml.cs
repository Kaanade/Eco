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

namespace Eco
{
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                SQLiteCommand cmdRead; SQLiteDataReader sdrRead;
                string conn = "Data Source=EcoDB.db;Version=3";
                SQLiteConnection connection = new SQLiteConnection(conn);
                connection.Open();

                cmdRead = new SQLiteCommand("SELECT idUtilisateur, nomUtilisateur, prenomUtilisateur, fonction, mail, niveau FROM Utilisateurs WHERE login = @login", connection);
                cmdRead.Parameters.AddWithValue("@login", "mberth");
                sdrRead = cmdRead.ExecuteReader();
                while (sdrRead.Read())
                {
                    App.Current.Properties["userID"] = Convert.ToInt32(sdrRead["idUtilisateur"]);
                    App.Current.Properties["userName"] = Convert.ToString(sdrRead["nomUtilisateur"]);
                    App.Current.Properties["userFirstName"] = Convert.ToString(sdrRead["prenomUtilisateur"]);
                    App.Current.Properties["userFct"] = Convert.ToString(sdrRead["fonction"]);
                    App.Current.Properties["userMail"] = Convert.ToString(sdrRead["mail"]);
                    App.Current.Properties["niveau"] = Convert.ToInt32(sdrRead["niveau"]);
                }
                connection.Close();
                
                NavigationFrame.Navigate(new Home());

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message); 
            }
            
        }
    }
}
