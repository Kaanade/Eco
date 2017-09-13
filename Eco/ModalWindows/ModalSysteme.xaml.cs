using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Windows;


namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour ModalSysteme.xaml
    /// </summary>
    public partial class ModalSysteme : Window
    {
        private bool valid;
        private string systeme;

        public ModalSysteme(string Site)
        {
            InitializeComponent();
            

            SQLiteConnection conn = new SQLiteConnection("Data Source=EcoDB.db;Version=3");
            conn.Open();

            var command = conn.CreateCommand();

            //Read from table
            command.CommandText = @"SELECT nomSysteme FROM systeme WHERE Site = @Site";
            command.Parameters.AddWithValue("@Site", Site);
            SQLiteDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                comboSysteme.Items.Add(sdr[0]);
            }

            sdr.Close();
            conn.Close();
        }

        public bool Valid
        {
            get { return valid; }
        }

        public string Systeme
        {
            get { return systeme; }
        }
        

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnValid(object sender, RoutedEventArgs e)
        {
            systeme = comboSysteme.SelectedItem.ToString();
            valid = true;
            this.Hide();
        }
    }
}
