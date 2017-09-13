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

            var command = conn.CreateCommand();

            //Read from table
            command.CommandText = @"SELECT nomProcedure FROM Procedure WHERE systeme = @systeme";
            command.Parameters.AddWithValue("@systeme", systeme);
            SQLiteDataReader sdr = command.ExecuteReader();

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
