using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Data;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour ModalProcedure.xaml
    /// </summary>
    public partial class ModalProcedure : Window
    {
        private bool valid = false;

        public ModalProcedure()
        {
            InitializeComponent();
            
            SQLiteConnection conn = new SQLiteConnection("Data Source=EcoDB.db;Version=3");
            conn.Open();
            
            var command = conn.CreateCommand();

            //Read from table
            command.CommandText = @"SELECT idDocEquipement, titreDocEquipement FROM DocEquipement";
            SQLiteDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                comboEquipement.Items.Add(sdr[1]);
            }
           
        }

        public string NomProc
        {
            get { return txtNomProcedure.Text; }
        }

        public int NumProc
        {
            get { return Int32.Parse(txtNumProcedure.Text); }
        }

        public int NumProcPrec
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(txtNumProcedurePrec.Text))
                    return Int32.Parse(txtNumProcedurePrec.Text);
                else
                    return 0;
            }
        }

       

        public bool Valid
        {
            get { return valid; }
        }

        public string TypeEquipement
        {
            get { return comboEquipement.SelectedItem.ToString(); }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnValid(object sender, RoutedEventArgs e)
        {
            valid = true;
            this.Hide();
        }

       


    }
}
