using System.Windows;
using Microsoft.Win32;
using System.Windows.Documents;
using System.Data.SQLite;
using System;
using System.IO;
using iTextSharp.text.pdf;
using System.Collections.Generic;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour ModalFNC.xaml
    /// </summary>
    public partial class ModalFNC : Window
    {
        private bool valid;
        private string nomFNC = "", nomProcedure, systeme;
        private string pathTemplateFNC = AppDomain.CurrentDomain.BaseDirectory + "/Doc/FNC/FNC.pdf";
        private string pathTemp = AppDomain.CurrentDomain.BaseDirectory + "Temp/tempFNC.pdf";
        private List<string> pathPictures;

        public ModalFNC(string _nomProcedure, string _systeme)
        {
            InitializeComponent();

            nomProcedure = _nomProcedure;
            systeme = _systeme;

            File.Copy(pathTemplateFNC, pathTemp);
            this.Closed += new EventHandler(ModalFnc_CLosed);

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnValid(object sender, RoutedEventArgs e)
        {
            string conn = "Data Source=EcoDB.db;Version=3";
            SQLiteConnection connection = new SQLiteConnection(conn);
            connection.Open();


            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO FNC (nomProcedure, systeme, installation) VALUES (@nomProcedure,@systeme, @installation)", connection);
            cmd.Parameters.AddWithValue("@nomProcedure", nomProcedure);
            cmd.Parameters.AddWithValue("@systeme", systeme);
            cmd.Parameters.AddWithValue("@installation", App.Current.Properties["installation"]);

            
            int a = cmd.ExecuteNonQuery();

            if (a == 0)
            {
                MessageBox.Show("Erreur, veuillez vérifier vos informations.");
            }
            else
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "/Projets/" + systeme + "/" + nomProcedure + "/" + nomProcedure + "FNC.pdf";
                File.Copy(pathTemp, path);
                using (var reader = new PdfReader(path))
                {
                    var fields = reader.AcroFields.Fields;
                    AcroFields fieldsType = reader.AcroFields;
                    int field_type = 0;

                    foreach (var key in fields.Keys)
                    {
                        var value = reader.AcroFields.GetField(key);

                        field_type = fieldsType.GetFieldType(key.ToString());
                        if (field_type == 2)
                            Console.WriteLine("Value checkbox : " + value);

                        SQLiteCommand cmd2;

                        cmd2 = new SQLiteCommand("UPDATE FNC SET " + key + " = @value WHERE nomProcedure = @nomProcedure AND systeme = @systeme AND installation = @installation", connection);
                        cmd2.Parameters.AddWithValue("@value", value);
                        cmd2.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                        cmd2.Parameters.AddWithValue("@systeme", systeme);
                        cmd2.Parameters.AddWithValue("@installation", App.Current.Properties["installation"]);

                        try
                        {
                            cmd2.ExecuteNonQuery();
                           
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }

                    }

                    MessageBox.Show("FNC enregistrée");
                    if (File.Exists(pathTemp))
                    {
                        File.Delete(pathTemp);
                    }
                }

                connection.Close();


            //foreach (String file in openFileDialog.FileNames)
            //{
            //    MessageBox.Show(file);
            //}

                this.Close();
        }
    }
        
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images |*.jpg;*.jpeg;*.png;*.bmp |JPG (*.jpg,*.jpeg)|*.jpg;*.jpeg|TIFF (*.tif,*.tiff)|*.tif;*.tiff|PNG (*.png)| *.png";
            if (openFileDialog.ShowDialog() == true)
                txtPathPDF.Text = openFileDialog.FileName;
        }

        private void btnOpenFNC(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            Uri pdf = new Uri(pathTemp, UriKind.RelativeOrAbsolute);
            process.StartInfo.FileName = pdf.LocalPath;
            process.Start();
            process.WaitForExit();
            
        }


        public string pathPDF
        {
            get { return txtPathPDF.Text; }
        }

      

        public bool Valid
        {
            get { return valid; }
        }

        void ModalFnc_CLosed(object sender, EventArgs e)
        {
            File.Delete(pathTemp);
        }
    }
}
