using System;
using System.Windows;
using System.Data.SQLite;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour modalFormPrep.xaml
    /// </summary>
    public partial class ModalFormPrep : Window
    {
        private bool form = false, suppr = false,  doc = false;
        int nbDoc = 0; string avancement = "";

        public ModalFormPrep(string nomProcedure, string systeme)
        {
            InitializeComponent();


            SQLiteConnection conn = new SQLiteConnection("Data Source=EcoDB.db;Version=3");
            conn.Open();

            var command = conn.CreateCommand();

            //Read from table
            command.CommandText = @"SELECT * FROM refDoc WHERE nomProcedure = @nomProcedure AND systeme = @systeme";
            command.Parameters.AddWithValue("@nomProcedure", nomProcedure);
            command.Parameters.AddWithValue("@systeme", systeme);
            SQLiteDataReader sdr = command.ExecuteReader();

            while (sdr.Read())
            {
                nbDoc++;
            }
            sdr.Close();
            

            labelNbDoc.Content = "[" + nbDoc.ToString() + "]";
        }

        public bool Form
        {
            get { return form; }
        }

        public bool Doc
        {
            get { return doc; }
        }

        public bool Suppr
        {
            get { return suppr; }
        }

        private void btnForm(object sender, EventArgs e)
        {
            form = true;
            this.Hide();
        }
        
        private void btnDoc(object sender, EventArgs e)
        {
            doc = true;
            this.Hide();
        }

        private void btnSuppr(object sender, EventArgs e)
        {
            suppr = true;
            this.Hide();
        }

    }
}
