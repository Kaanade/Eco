using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Input;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour ModalRealiserHome.xaml
    /// </summary>
    public partial class ModalPIDHome : Window
    {
        private bool valid;
        private string rSysteme, rSite;
        List<Systeme> systemeList;

        public ModalPIDHome()
        {
            InitializeComponent();

            systemeList = new List<Systeme>();
            string conn = "Data Source=EcoDB.db;Version=3";
            SQLiteConnection connection = new SQLiteConnection(conn);
            connection.Open();

            SQLiteCommand cmd = new SQLiteCommand("Select nomSysteme, site from Systeme", connection);
            /*SQLiteDataAdapter adapt = new SQLiteDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapt.Fill(ds, "Systemes");
            ListSysteme.ItemsSource = ds.Tables["Systemes"].DefaultView;*/

            SQLiteDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                systemeList.Add(new Systeme(Convert.ToString(r["nomSysteme"]), Convert.ToString(r["site"])));
            }

            ListSysteme.ItemsSource = systemeList;
        }

        public class Systeme
        {
            public string NomSysteme { get; set; }
            public string Site { get; set; }

            public Systeme(string nomSysteme, string site)
            {
                NomSysteme = nomSysteme;
                Site = site;
            }
        }

         public bool Valid
        {
            get { return valid; }
        }

        public string RSysteme
        {
            get { return rSysteme; }
        }

        public string RSite
        {
            get { return rSite; }
        }





        private void pid(object sender, MouseButtonEventArgs e)
        {
            Systeme drv = (Systeme)ListSysteme.SelectedItem;
            rSysteme = drv.NomSysteme;
            rSite = drv.Site;

            valid = true;
            this.Hide();
            
        }


    }
    
}
