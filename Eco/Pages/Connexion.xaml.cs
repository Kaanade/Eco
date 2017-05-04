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
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Page
    {
        public Connexion()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
           // DialogResult = false;
           // this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Write code here to authenticate user
            // If authenticated, then set DialogResult=true
            //DialogResult = true;

            //if the database has already password

            if (File.Exists("EcoDB.db"))
            {
                string conn = "Data Source=EcoDB.db;Version=3";
                SQLiteConnection connection = new SQLiteConnection(conn);
                SQLiteCommand cmd = new SQLiteCommand("Select * from Utilisateurs where login=@username and password=@password", connection);
                //SQLiteCommand cmd = new SQLiteCommand("Select * from PID", connection);
                cmd.Parameters.AddWithValue("@username", txtUserName.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Password);
                SQLiteDataAdapter adapt = new SQLiteDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                connection.Close();

                int count = ds.Tables[0].Rows.Count;
                //If count is equal to 1, then show frmMain form
                if (count == 1)
                {
                    //MessageBox.Show("Login Successful!");
                    Properties.Settings.Default.Username = txtUserName.Text;
                    Properties.Settings.Default.Save();

                    this.NavigationService.Navigate(new Uri("Pages/Home.xaml", UriKind.Relative));

                }
                else
                {
                    MessageBox.Show("Login Failed!");
                }
            }
             
        }
    }
}
