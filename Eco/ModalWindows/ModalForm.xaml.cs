using System;
using System.Windows;
using System.Data.SQLite;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour ModalForm.xaml
    /// </summary>
    public partial class ModalForm : Window
    {
        private bool form = false, sign = false, fnc = false, doc = false;
        int nbDoc = 0; string avancement = "";

        public ModalForm(string nomProcedure, string systeme)
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

            var cmd2 = conn.CreateCommand();

            //Read from table
            cmd2.CommandText = @"SELECT avancement FROM ProcedureEssai WHERE nomProcedure = @nomProcedure AND systeme = @systeme AND installation = @installation";
            cmd2.Parameters.AddWithValue("@nomProcedure", nomProcedure);
            cmd2.Parameters.AddWithValue("@systeme", systeme);
            cmd2.Parameters.AddWithValue("@installation", App.Current.Properties["installation"]);
            SQLiteDataReader sdr2 = cmd2.ExecuteReader();

            while (sdr2.Read())
            {
                avancement = sdr2[0].ToString();
            }
            sdr2.Close();

            labelNbDoc.Content = "["+ nbDoc.ToString() + "]";
            labelAvancement.Content = "[" + avancement + "%]";
        }

        public bool Form
        {
            get { return form; }
        }

        public bool Sign
        {
            get { return sign; }
        }

        public bool FNC
        {
            get { return fnc; }
        }

        public bool Doc
        {
            get { return doc; }
        }

        private void btnForm(object sender, EventArgs e)
        {
            form = true;
            this.Hide();
        }

        private void btnSign(object sender, EventArgs e)
        {
            sign = true;
            this.Hide();
        }

        private void btnFNC(object sender, EventArgs e)
        {
            fnc = true;
            this.Hide();
        }

        private void btnDoc(object sender, EventArgs e)
        {
            doc = true;
            this.Hide();
        }
    }
}
