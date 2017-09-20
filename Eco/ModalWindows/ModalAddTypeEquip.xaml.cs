using System.Windows;
using Microsoft.Win32;
using System.Data;
using System.Data.SQLite;
using iTextSharp.text.pdf;
using System;
using System.Text.RegularExpressions;
using System.IO;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour ModalAddTypeEquip.xaml
    /// </summary>
    public partial class ModalAddTypeEquip : Window
    {
        string pathTemplate = "", pathDoc = "";

        public ModalAddTypeEquip()
        {
            InitializeComponent();
        }

        private void btnOpenTemplate_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                txtPathTemplate.Text = openFileDialog.FileName;
                pathTemplate = openFileDialog.FileName;
            }
                
        }

        private void btnOpenDoc_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                txtPathDoc.Text = openFileDialog.FileName;
                pathDoc = openFileDialog.FileName;
            }
                
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
            SQLiteCommand cmd,cmd2;
            //cmd = new SQLiteCommand("CREATE TABLE \"TestTable\" ( `idTestTable` INTEGER PRIMARY KEY AUTOINCREMENT, `testColumn` TEXT ); ", connection);
            
            
            
            if (Regex.IsMatch(pathTemplate, "^.+\\.(([pP][dD][fF]))$"))
            {
            
                using (var reader = new PdfReader(pathTemplate))
                {
                    var fields = reader.AcroFields.Fields;
                    AcroFields fieldsType = reader.AcroFields;
                    int field_type = 0;
                    string type = Regex.Replace(txtTypeEquip.Text, @"\s+", "");
                    string createSQL = "CREATE TABLE \"" + type + "\" ('id" + type + "' INTEGER PRIMARY KEY AUTOINCREMENT, 'nomProcedure' TEXT, 'systeme' TEXT";



                    foreach (var key in fields.Keys)
                    {
                        field_type = fieldsType.GetFieldType(key.ToString());

                        //Console.WriteLine(key + " : " + field_type);

                        createSQL += ", `" + key + "` TEXT";
                    }

                    createSQL += " );";
                    //System.IO.Path.GetFileName(dbf_File);
                    cmd = new SQLiteCommand(createSQL, connection);

                    cmd2 = new SQLiteCommand("INSERT INTO DocEquipement (titreDocEquipement, urlDocEquipement, urlFT) VALUES (@titreDocEquipement,@urlDocEquipement, @urlFT)", connection);
                    cmd2.Parameters.AddWithValue("@titreDocEquipement", txtTypeEquip.Text);
                    cmd2.Parameters.AddWithValue("@urlDocEquipement", System.IO.Path.GetFileName(pathDoc));
                    cmd2.Parameters.AddWithValue("@urlFT", System.IO.Path.GetFileName(pathTemplate));

                    try
                    {
                        
                        int a = cmd.ExecuteNonQuery();
                        int b = cmd2.ExecuteNonQuery();

                        string pathCopyFT = AppDomain.CurrentDomain.BaseDirectory + "/Doc/FT/"+ System.IO.Path.GetFileName(pathTemplate);
                        string pathCopyDoc = AppDomain.CurrentDomain.BaseDirectory + "/Doc/Equipement/" + System.IO.Path.GetFileName(pathDoc);
                        

                        File.Copy(pathTemplate, pathCopyFT);
                        File.Copy(pathDoc, pathCopyDoc);

                        MessageBox.Show("Le type d'équipement : " + type + " a été ajouté.");

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }



                    connection.Close();
                    }
                this.Hide();
            }
            else
            {
                MessageBox.Show("Le format du Template n'est pas valide. Rappel : ce doit-être un formulaire PDF");
                return;
            }
            //valid = true;
            

        }
    }
}
