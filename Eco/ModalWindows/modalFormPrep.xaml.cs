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

            var cmd = conn.CreateCommand();

            //Read from table
            cmd.CommandText = @"SELECT * FROM refDoc WHERE nomProcedure = @nomProcedure AND systeme = @systeme AND installation = @installation";
            cmd.Parameters.AddWithValue("@nomProcedure", nomProcedure);
            cmd.Parameters.AddWithValue("@systeme", systeme);
            cmd.Parameters.AddWithValue("@installation", App.Current.Properties["installation"]);
            SQLiteDataReader sdr = cmd.ExecuteReader();

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
