using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour ModalDoc.xaml
    /// </summary>
    public partial class ModalDoc : Window
    {

        private bool valid;
        private string doc, typeDoc;
        List<string> typeDocList;


        public ModalDoc(string nomProcedure, string systeme)
        {
            InitializeComponent();

            typeDocList = new List<string>();

            SQLiteConnection conn = new SQLiteConnection("Data Source=EcoDB.db;Version=3");
            conn.Open();

            var cmd = conn.CreateCommand();

            //Read from table
            cmd.CommandText = @"SELECT * FROM refDoc WHERE nomProcedure = @nomProcedure AND systeme = @systeme and installation = @installation";
            cmd.Parameters.AddWithValue("@nomProcedure", nomProcedure);
            cmd.Parameters.AddWithValue("@systeme", systeme);
            cmd.Parameters.AddWithValue("@installation", App.Current.Properties["installation"]);
            SQLiteDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                typeDocList.Add(sdr[1].ToString());
                comboDoc.Items.Add(sdr[3]);
            }
        }

        public bool Valid
        {
            get { return valid; }
        }

        public string Doc
        {
            get { return doc; }
        }

        public string TypeDoc
        {
            get { return typeDoc; }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnValid(object sender, RoutedEventArgs e)
        {
            typeDoc = typeDocList[comboDoc.SelectedIndex];
            doc = comboDoc.SelectedItem.ToString();
            valid = true;
            this.Hide();
        }
    }
}
