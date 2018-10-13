using System.Windows;
using System.Data;
using System.Data.SQLite;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour modalListFNC.xaml
    /// </summary>
    public partial class modalListFNC : Window
    {
        List<string> listFNCCom;
        string systeme;

        public modalListFNC(string _systeme)
        {
            InitializeComponent();
            systeme = _systeme;

            listFNCCom = new List<string>();


            string conn = "Data Source=EcoDB.db;Version=3";
            SQLiteConnection connection = new SQLiteConnection(conn);
            connection.Open();

            SQLiteCommand cmd = new SQLiteCommand("Select * from FNC WHERE systeme = @systeme AND installation = @installation", connection);
            cmd.Parameters.AddWithValue("@systeme", systeme);
            cmd.Parameters.AddWithValue("@installation", App.Current.Properties["installation"]);

            SQLiteDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                listFNCCom.Add(sdr[4].ToString());
            }
            sdr.Close();

            SQLiteDataAdapter adapt = new SQLiteDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapt.Fill(ds, "FNC");
            ListFNC.ItemsSource = ds.Tables["FNC"].DefaultView;
        }

        private void Row_Click(object sender, MouseButtonEventArgs e)
        {
            DataRowView drv = (DataRowView)ListFNC.SelectedItem;
            string nomFNC = (drv["nomFNC"]).ToString();
            string nomProcedure = (drv["nomProcedure"]).ToString();
            int i = ListFNC.SelectedIndex;

            commentary.Document.Blocks.Clear();
            commentary.AppendText(listFNCCom[i]);

            string pathImage = AppDomain.CurrentDomain.BaseDirectory + "/Projets/" + systeme + "/" + nomProcedure + "/" + nomFNC + ".png";

            ImageSource imageSource = new BitmapImage(new Uri(pathImage));
            imageFNC.Source = imageSource;
           
        }
    }
}
