using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Windows;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour ModalPlacePin.xaml
    /// </summary>
    public partial class ModalPlacePin : Window
    {
        private bool valid;
        private string nomProc;

        public ModalPlacePin(string systeme)
        {
            InitializeComponent();

            SQLiteConnection conn = new SQLiteConnection("Data Source=EcoDB.db;Version=3");
            conn.Open();

            var cmd = conn.CreateCommand();

            //Read from table
            cmd.CommandText = @"SELECT nomProcedure FROM ProcedureEssai WHERE systeme = @systeme AND installation = @installation";
            cmd.Parameters.AddWithValue("@systeme", systeme);
            cmd.Parameters.AddWithValue("@installation", App.Current.Properties["installation"]);
            Console.WriteLine(App.Current.Properties["installation"]);
            SQLiteDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                comboProc.Items.Add(sdr[0]);
            }

        }

        

        public bool Valid
        {
            get { return valid; }
        }

        public string NomProcedure
        {
            get { return nomProc; }
        }
        

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnValid(object sender, RoutedEventArgs e)
        {
            nomProc = comboProc.SelectedItem.ToString();
            valid = true;
            this.Hide();
        }
    }
}
