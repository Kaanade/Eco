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
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Data.OleDb;

using WPFCustomMessageBox;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour PidPrep.xaml
    /// </summary>
    public partial class PidPrep : Page
    {
        public PidPrep(string _nomSysteme)
        {
            InitializeComponent();
            initializeProcedures(_nomSysteme);
        }

        private UIElement child = null;
        private Point origin, last, newPos;
        private List<Pin> vecPin = new List<Pin>();
        private double transformX, transformY, zoom = 1, inczoom = 0;
        public static bool pinning = false;

        List<string> listProc { get; set; }

        private string systeme;

        public bool Pinning
        {
            get { return pinning; }
            set { pinning = value; }
        }

        

        private void btnClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnRetour(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/PreparerHome.xaml", UriKind.Relative));
        }

        private void btnAddPin(object sender, RoutedEventArgs e)
        {
            border.Pinning = true;
            pinning = true;
        }


        private void btnImportProc(object sender, RoutedEventArgs e)
        {
            string excelPath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                excelPath = openFileDialog.FileName;

                // string excelPath = "F:/Travail/Alternance/Test/Template_Procedure.xlsx";
                OleDbConnection Conn;
                OleDbCommand Cmd;
                int a = 0;

                string impSysteme, impNomProc;

                string conn = "Data Source=EcoDB.db;Version=3";
                SQLiteConnection connection = new SQLiteConnection(conn);
                connection.Open();
                SQLiteCommand cmdIns, cmd2, cmd3, cmd4;

                string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + ";Extended Properties=Excel 12.0;Persist Security Info=True";
                Conn = new OleDbConnection(excelConnectionString);

                Conn.Open();
                Cmd = new OleDbCommand();
                Cmd.Connection = Conn;
                Cmd.CommandText = "Select * from [Procedures$]";
                var Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    if (Reader["systeme"].ToString() == "")
                        continue;

                    try
                    {
                        cmdIns = new SQLiteCommand("INSERT INTO ProcedureEssai (nomProcedure, systeme, numProcedure, typeEquipement, installation) VALUES (@nomProcedure,@systeme, @numProcedure, @typeEquipement, @installation)", connection);
                        cmdIns.Parameters.AddWithValue("@nomProcedure", Reader["nomProcedure"].ToString());
                        cmdIns.Parameters.AddWithValue("@systeme", Reader["systeme"].ToString());
                        cmdIns.Parameters.AddWithValue("@installation", Reader["installation"].ToString());
                        cmdIns.Parameters.AddWithValue("@numProcedure", Convert.ToInt32(Reader["numProcedure"]));
                        cmdIns.Parameters.AddWithValue("@typeEquipement", Reader["typeEquipement"].ToString());


                        cmd2 = new SQLiteCommand("INSERT INTO refDoc (typeDoc, nomProcedure, nomDoc, systeme, installation) VALUES ('Equipement', @nomProcedure, @nomDoc, @systeme, @installation)", connection);
                        cmd2.Parameters.AddWithValue("@nomProcedure", Reader["nomProcedure"].ToString());
                        cmd2.Parameters.AddWithValue("@nomDoc", Reader["typeEquipement"].ToString());
                        cmd2.Parameters.AddWithValue("@systeme", systeme);
                        cmd2.Parameters.AddWithValue("@installation", Reader["installation"].ToString());

                        cmd3 = new SQLiteCommand("INSERT INTO refFT (nomProcedure, systeme, installation) VALUES (@nomProcedure, @systeme, @installation)", connection);
                        cmd3.Parameters.AddWithValue("@nomProcedure", Reader["nomProcedure"].ToString());
                        cmd3.Parameters.AddWithValue("@systeme", systeme);
                        cmd3.Parameters.AddWithValue("@installation", Reader["installation"].ToString());

                        cmd4 = new SQLiteCommand("INSERT INTO " + Regex.Replace(Reader["typeEquipement"].ToString(), @"\s+", "") + " (nomProcedure, systeme, installation) VALUES (@nomProcedure, @systeme, @installation)", connection);
                        cmd4.Parameters.AddWithValue("@nomProcedure", Reader["nomProcedure"].ToString());
                        cmd4.Parameters.AddWithValue("@systeme", systeme);
                        cmd4.Parameters.AddWithValue("@installation", Reader["installation"].ToString());

                        cmdIns.ExecuteNonQuery();
                        cmd2.ExecuteNonQuery();
                        cmd3.ExecuteNonQuery();
                        cmd4.ExecuteNonQuery();
                    }
                    catch (Exception e2)
                    {
                        MessageBox.Show(e2.ToString());
                        return;
                    }






                    string pathTemplate = AppDomain.CurrentDomain.BaseDirectory + "Doc/FT/" + Reader["typeEquipement"].ToString() + ".pdf";
                    string pathProc = AppDomain.CurrentDomain.BaseDirectory + "Projets/" + systeme + "/" + Reader["nomProcedure"].ToString();
                    // Determine whether the directory exists.
                    if (!Directory.Exists(pathProc))
                    {
                        // Try to create the directory.
                        DirectoryInfo di = Directory.CreateDirectory(pathProc);
                    }

                    pathProc += "/" + Reader["nomProcedure"].ToString() + ".pdf";

                    if (Reader["typeEquipement"].ToString() == "Moteur MT")
                    {
                        PdfReader pdfReader = new PdfReader(pathTemplate);
                        PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(pathProc, FileMode.Create));
                        AcroFields pdfFormFields = pdfStamper.AcroFields;

                        pdfFormFields.SetField("puissance", Reader["Puissance"].ToString());
                        pdfFormFields.SetField("vitesse", Reader["Vitesse"].ToString());
                        pdfFormFields.SetField("Site", Reader["Site"].ToString());
                        pdfFormFields.SetField("TrancheUnite", Reader["TrancheUnite"].ToString());
                        pdfFormFields.SetField("SystemeElementaire", Reader["SystemeElementaire"].ToString());
                        pdfFormFields.SetField("codeEquipement", Reader["codeEquipement"].ToString());
                        pdfStamper.Close();
                    }
                    else
                    {
                        File.Copy(pathTemplate, pathProc);
                    }
                    
                }
                Reader.Close();
                Conn.Close();

                MessageBox.Show("Importation Réussie");
            }

           

            this.NavigationService.Navigate(new PidPrep(systeme));
        }

        private void openPdf(object sender, RoutedEventArgs e)
        {
            string nomProcedure = (sender as Button).Tag.ToString();
            double nbAvancement = 0, nbFields = 0;

            ModalFormPrep modalFormPrep = new ModalFormPrep(nomProcedure, systeme);
            modalFormPrep.ShowDialog();

            bool form = modalFormPrep.Form;
            bool suppr = modalFormPrep.Suppr;
            bool doc = modalFormPrep.Doc;

            string conn = "Data Source=EcoDB.db;Version=3";
            SQLiteConnection connection = new SQLiteConnection(conn);
            connection.Open();

            if (form)
            {
                try
                {
                    modalFormPrep.Close();
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    //string path = "F:/Travail/Alternance/Exemple/Tests/" + nomProcedure+ ".pdf";
                    string path = AppDomain.CurrentDomain.BaseDirectory + "/Projets/" + systeme + "/" + nomProcedure + "/" + nomProcedure + ".pdf";
                    Uri pdf = new Uri(path, UriKind.RelativeOrAbsolute);
                    process.StartInfo.FileName = pdf.LocalPath;
                    process.Start();
                    process.WaitForExit();
                    

                }

                catch (Exception error)
                {
                    MessageBox.Show("Impossible d'ouvrir le fichier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            else if (doc)
            {
                string path = "";
                ModalDoc modalDoc = new ModalDoc(nomProcedure, systeme);
                modalDoc.ShowDialog();

                string document = modalDoc.Doc;
                string typeDoc = modalDoc.TypeDoc;

                var command = connection.CreateCommand();



                if (typeDoc == "Equipement")
                {
                    //Read from table
                    command.CommandText = @"SELECT urlDocEquipement FROM DocEquipement WHERE titreDocEquipement = @document";
                    //command.CommandText = @"SELECT idPID FROM Systeme WHERE idSysteme = 3";
                    command.Parameters.AddWithValue("@document", document);
                    SQLiteDataReader sdr = command.ExecuteReader();
                    string urlDoc = "";

                    while (sdr.Read())
                    {
                        urlDoc = sdr.GetString(0);
                        break;
                    }

                    path = AppDomain.CurrentDomain.BaseDirectory + "/Doc/Equipement/" + urlDoc;

                }
                else
                {

                }
                if(path != "")
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    Uri docPath = new Uri(path, UriKind.RelativeOrAbsolute);
                    process.StartInfo.FileName = docPath.LocalPath;
                    process.Start();
                    process.WaitForExit();
                }
            }
            else if (suppr)
            {
                if (CustomMessageBox.ShowOKCancel(
                   "Voulez vous vraiment supprimer cette procédure ?",
                   "Suppression Procédure",
                   "Valider",
                   "Annuler") == MessageBoxResult.OK)
                {
                    string typeEquipement = "";

                    SQLiteCommand cmdRead = new SQLiteCommand("SELECT typeEquipement FROM ProcedureEssai WHERE nomProcedure = @nomProcedure AND systeme = @systeme", connection);
                    cmdRead.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                    cmdRead.Parameters.AddWithValue("@systeme", systeme);
                    SQLiteDataReader sdrRead = cmdRead.ExecuteReader();


                    while (sdrRead.Read())
                    {
                        typeEquipement = sdrRead[0].ToString();
                    }

                    SQLiteCommand cmd = new SQLiteCommand("DELETE FROM ProcedureEssai WHERE nomProcedure = @nomProcedure AND systeme = @systeme AND installation = @installation; ", connection);
                    cmd.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                    cmd.Parameters.AddWithValue("@systeme", systeme);
                    cmd.Parameters.AddWithValue("@installation", App.Current.Properties["installation"]);

                    SQLiteCommand cmd2 = new SQLiteCommand("DELETE FROM RefFT WHERE nomProcedure = @nomProcedure AND systeme = @systeme AND installation = @installation; ", connection);
                    cmd2.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                    cmd2.Parameters.AddWithValue("@systeme", systeme);
                    cmd2.Parameters.AddWithValue("@installation", App.Current.Properties["installation"]);

                    SQLiteCommand cmd3 = new SQLiteCommand("DELETE FROM ProcedureBypass WHERE nomProcedure = @nomProcedure AND systeme = @systeme AND installation = @installation; ", connection);
                    cmd3.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                    cmd3.Parameters.AddWithValue("@systeme", systeme);
                    cmd3.Parameters.AddWithValue("@installation", App.Current.Properties["installation"]);

                    SQLiteCommand cmd4 = new SQLiteCommand("DELETE FROM RefDoc WHERE nomProcedure = @nomProcedure AND systeme = @systeme AND installation = @installation; ", connection);
                    cmd4.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                    cmd4.Parameters.AddWithValue("@systeme", systeme);
                    cmd4.Parameters.AddWithValue("@installation", App.Current.Properties["installation"]);

                    SQLiteCommand cmd5 = new SQLiteCommand("DELETE FROM FNC WHERE nomProcedure = @nomProcedure AND systeme = @systeme AND installation = @installation; ", connection);
                    cmd5.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                    cmd5.Parameters.AddWithValue("@systeme", systeme);
                    cmd5.Parameters.AddWithValue("@installation", App.Current.Properties["installation"]);

                    SQLiteCommand cmd6 = new SQLiteCommand("DELETE FROM " + Regex.Replace(typeEquipement, @"\s+", "") + " WHERE nomProcedure = @nomProcedure AND systeme = @systeme AND installation = @installation; ", connection);
                    cmd6.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                    cmd6.Parameters.AddWithValue("@systeme", systeme);
                    cmd6.Parameters.AddWithValue("@installation", App.Current.Properties["installation"]);

                    int a = cmd.ExecuteNonQuery();
                    int b = cmd2.ExecuteNonQuery();
                    int c = cmd3.ExecuteNonQuery();
                    int d = cmd4.ExecuteNonQuery();
                    int f = cmd5.ExecuteNonQuery();
                    int g = cmd6.ExecuteNonQuery();

                    string path = AppDomain.CurrentDomain.BaseDirectory + "Projets/" + systeme + "/" + nomProcedure;
                    // Determine whether the directory exists.
                    if (Directory.Exists(path))
                    {
                        // Try to create the directory.
                        Directory.Delete(path, true);
                    }
                    

                    this.NavigationService.Navigate(new PidPrep(systeme));
                }
                else
                {

                }
            }

            modalFormPrep.Close();
            connection.Close();

        }


        private void CanvasMouseDown(object sender, MouseButtonEventArgs e)
        {
            origin = e.GetPosition(this);
            Console.WriteLine("Y : " + e.GetPosition(this).Y);

            if (pinning)
            {
                pinning = false;
                ModalChoixProcedure modalChoixProc = new ModalChoixProcedure();
                modalChoixProc.ShowDialog();

                bool creer = modalChoixProc.Creer;
                bool choix = modalChoixProc.Choix;

                string conn = "Data Source=EcoDB.db;Version=3";
                SQLiteConnection connection = new SQLiteConnection(conn);
                connection.Open();

                if (creer)
                {
                    modalChoixProc.Close();
                    ModalProcedure modalProc = new ModalProcedure();
                    modalProc.ShowDialog();

                    bool valid = modalProc.Valid;

                    if (valid)
                    {
                        if (!Regex.IsMatch(modalProc.NumProc.ToString(), @"^[0-9]+$"))
                        {
                            MessageBox.Show("Erreur, veuillez vérifier le numéro de procédure.");
                        }
                        else
                        {
                            string nomProcedure = modalProc.NomProc;
                            int numProcedure = modalProc.NumProc;
                            int numProcedurePrec = modalProc.NumProcPrec;
                            string typeEquipement = modalProc.TypeEquipement;
                            string pathPDF = AppDomain.CurrentDomain.BaseDirectory + "Doc/FT/FT-" + typeEquipement +".pdf";
                            string path = AppDomain.CurrentDomain.BaseDirectory + "Projets/" + systeme + "/" + nomProcedure;


                            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO ProcedureEssai (nomProcedure,systeme, installation, numProcedure, posX, posY, numProcedurePrec, typeEquipement) VALUES (@nomProcedure, @systeme, @installation, @numProcedure,  @posX, @posY, @numProcedurePrec, @typeEquipement)", connection);
                            cmd.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                            cmd.Parameters.AddWithValue("@systeme", systeme);
                            cmd.Parameters.AddWithValue("@installation", App.Current.Properties["installation"]);
                            cmd.Parameters.AddWithValue("@numProcedure", numProcedure);
                            cmd.Parameters.AddWithValue("@posX", origin.X);
                            cmd.Parameters.AddWithValue("@posY", origin.Y);
                            cmd.Parameters.AddWithValue("@numProcedurePrec", numProcedurePrec);
                            cmd.Parameters.AddWithValue("@typeEquipement", typeEquipement);


                            SQLiteCommand cmd2 = new SQLiteCommand("INSERT INTO refDoc (typeDoc, nomProcedure, nomDoc, systeme, installation) VALUES ('Equipement', @nomProcedure, @nomDoc, @systeme, @installation)", connection);
                            cmd2.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                            cmd2.Parameters.AddWithValue("@nomDoc", typeEquipement);
                            cmd2.Parameters.AddWithValue("@systeme", systeme);
                            cmd2.Parameters.AddWithValue("@installation", App.Current.Properties["installation"]);

                            SQLiteCommand cmd3 = new SQLiteCommand("INSERT INTO refFT (nomProcedure, systeme, installation) VALUES (@nomProcedure, @systeme, @installation)", connection);
                            cmd3.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                            cmd3.Parameters.AddWithValue("@systeme", systeme);
                            cmd3.Parameters.AddWithValue("@installation", App.Current.Properties["installation"]);

                            SQLiteCommand cmd4 = new SQLiteCommand("INSERT INTO " + Regex.Replace(typeEquipement, @"\s+", "") + " (nomProcedure, systeme, installation) VALUES (@nomProcedure, @systeme, @installation)", connection);
                            cmd4.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                            cmd4.Parameters.AddWithValue("@systeme", systeme);
                            cmd4.Parameters.AddWithValue("@installation", App.Current.Properties["installation"]);



                            try
                            {
                                int a = cmd.ExecuteNonQuery();
                                int b = cmd2.ExecuteNonQuery();
                                int c = cmd3.ExecuteNonQuery();
                                int d = cmd4.ExecuteNonQuery();

                                if (a == 0 || b == 0)
                                {
                                    MessageBox.Show("Erreur, veuillez vérifier vos informations.");
                                    modalProc.Visibility = Visibility.Visible;
                                }
                                else
                                {


                                    modalProc.Close();

                                    // Determine whether the directory exists.
                                    if (!Directory.Exists(path))
                                    {
                                        // Try to create the directory.
                                        DirectoryInfo di = Directory.CreateDirectory(path);
                                    }

                                    File.Copy(@pathPDF, path + "/" + nomProcedure + ".pdf");

                                    this.NavigationService.Navigate(new PidPrep(systeme));
                                }


                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("ex.Message");
                            }

                            connection.Close();

                        }
                    }
                }
                else if(choix)
                {
                    ModalPlacePin modalPlacePin = new ModalPlacePin(systeme);
                    modalPlacePin.ShowDialog();
                    
                    if(modalPlacePin.Valid)
                    {
                        SQLiteCommand cmd = new SQLiteCommand("UPDATE ProcedureEssai  SET  posX = @posX, posY = @posY WHERE nomProcedure = @nomProcedure AND systeme = @systeme", connection);
                        cmd.Parameters.AddWithValue("@posX", origin.X);
                        cmd.Parameters.AddWithValue("@posY", origin.Y);
                        cmd.Parameters.AddWithValue("@nomProcedure", modalPlacePin.NomProcedure);
                        cmd.Parameters.AddWithValue("@systeme", systeme);
                        

                        cmd.ExecuteNonQuery();

                        this.NavigationService.Navigate(new Pid(systeme));
                    }

                }


            }
            else if (!pinning)
                border.Pinning = false;


        }

        private void CanvasMouseUp(object sender, MouseButtonEventArgs e)
        {

            System.Windows.Vector v = e.GetPosition(this) - origin;

            if (pinning)
            {
                pinning = false;
                // border.Pinning = false;
            }
            else
            {
                if (inczoom < 0.1 && inczoom > -0.1)
                {
                    foreach (Pin pin in vecPin)
                    {
                        TranslateTransform tt = new TranslateTransform();
                        pin.bodyImage.RenderTransform = tt;

                        tt.X = v.X + pin.deplacement.X;
                        tt.Y = v.Y + pin.deplacement.Y;

                        newPos = new Point(pin.position.X + v.X, pin.position.Y + v.Y);
                        pin.position = newPos;
                        pin.deplacement.X += v.X;
                        pin.deplacement.Y += v.Y;


                    }
                }

            }

        }

        //Zoom 
        private void CanvasWheel(object sender, MouseWheelEventArgs e)
        {
            
            double zoom = e.Delta > 0 ? 0.2 : -0.2;
            inczoom += zoom;
            Console.WriteLine("Zoom : " + Math.Round(inczoom, 2));
            Point relative = e.GetPosition(child);
            relative.Y -= 160;

            if (inczoom < 0.1 && inczoom > -0.1)
            {

                //foreach (Pin pin in vecPin)
                //{
                //    pin.bodyImage.Visibility = Visibility.Visible;

                //}

                this.NavigationService.Navigate(new PidPrep(systeme));
            }
            else
            {
                foreach (Pin pin in vecPin)
                {
                    pin.bodyImage.Visibility = Visibility.Collapsed;

                }
            }
            

            last.X = relative.X;
            last.Y = relative.Y;
        }

        // Ajouter la Pin au vecteur
        public void addRectangle(Point start, int signed, int avancement)
        {
            Pin newPin = new Pin(start, signed,avancement);
            vecPin.Add(newPin);
            canvasPid.Children.Add(newPin.bodyImage);

        }

        //Creation forme
        private void initializeProcedures(string _nomSysteme)
        {
            try
            {
                //string path = System.AppDomain.CurrentDomain.BaseDirectory;
                systeme = _nomSysteme;
                labelSysteme.Content = systeme;
                double i = 0;
                BitmapImage carBitmap = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images/logo_tes2.png", UriKind.Absolute));
                imgLogo.Source = carBitmap;

                listProc = new List<string>();
                SQLiteConnection conn = new SQLiteConnection(@"Data Source=EcoDB.db;Version=3");
                conn.Open();

                var command = conn.CreateCommand();

                //Read from table
                command.CommandText = @"SELECT idPID FROM Systeme WHERE nomSysteme = @nomSysteme";
                command.Parameters.AddWithValue("@nomSysteme", systeme);
                SQLiteDataReader sdr = command.ExecuteReader();
                string idPid = "";
                while (sdr.Read())
                {
                    idPid = sdr.GetString(0);
                }
                sdr.Close();


                double avancementTotal = 0;
                string avancementSysteme = "";


                BitmapImage image = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Projets/" + _nomSysteme + "/" + idPid + ".png", UriKind.Absolute));
                myPid.Source = image;
                SQLiteConnection connect = new SQLiteConnection(@"Data Source=EcoDB.db;Version=3");
                var fmd = connect.CreateCommand();
                connect.Open();

                fmd.CommandText = @"SELECT nomProcedure, posX, posY, signed, avancement FROM ProcedureEssai WHERE systeme = @systeme";
                fmd.Parameters.AddWithValue("@systeme", systeme);

                SQLiteDataReader r = fmd.ExecuteReader();
                while (r.Read())
                {

                    i++;
                    string name = System.IO.Path.GetFileNameWithoutExtension(Convert.ToString(r["nomProcedure"]));
                    listProc.Add(name);

                    addRectangle(new Point(Convert.ToInt16(r["posX"]), Convert.ToInt16(r["posY"])), Convert.ToInt16(r["signed"]), Convert.ToInt16(r["avancement"]));

                    avancementTotal += Convert.ToDouble(r["avancement"]);
                }

                templ.ItemsSource = listProc;
                if (i != 0)
                {
                    labelAvancementSysteme.Content = "Avancement : [" + Convert.ToString(Math.Round((avancementTotal / i))) + "%]";
                    progressBarAvSys.Value = Math.Round((avancementTotal / i));
                }
            }
            catch(Exception e)
            {
            }
            
            


        }


        private ScaleTransform GetScaleTransform(UIElement element)
        {
            return (ScaleTransform)((TransformGroup)element.RenderTransform).Children.First(tr => tr is ScaleTransform);
        }

        private TranslateTransform GetTranslateTransform(UIElement element)
        {
            return (TranslateTransform)((TransformGroup)element.RenderTransform).Children.First(tr => tr is TranslateTransform);
        }
    }
}

