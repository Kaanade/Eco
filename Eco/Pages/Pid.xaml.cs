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
using iTextSharp.text;
using WPFCustomMessageBox;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour Pid.xaml
    /// </summary>
    public partial class Pid : Page
    {
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

        public Pid()
        {
            InitializeComponent();
            //initializeProcedures();
           
        }

        public Pid(string _nomSysteme)
        {
            InitializeComponent();
            initializeProcedures(_nomSysteme);

        }

        private void btnClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnRetour(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/RealiserHome.xaml", UriKind.Relative));
        }

        private void btnAddPin(object sender, RoutedEventArgs e)
        {
            border.Pinning = true;
            pinning = true;
        }

        private void btnSeeFNC (object sender, RoutedEventArgs e)
        {
            modalListFNC modalListFNC = new modalListFNC(systeme);
            modalListFNC.ShowDialog();

        }

        private void btnExport(object sender, RoutedEventArgs e)
        {
            List<string> pdf = new List<string>();
            pdf.Add("C:/Users/Matthieu/Documents/Visual Studio 2015/Projects/Eco/Eco/bin/Debug/Projets/Test3/excelTestProc8/excelTestProc8.pdf");
            pdf.Add("C:/Users/Matthieu/Documents/Visual Studio 2015/Projects/Eco/Eco/bin/Debug/Projets/Test3/excelTestProc9/excelTestProc9.pdf");

            byte[] mergedPdf = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document document = new Document())
                {
                    using (PdfCopy copy = new PdfCopy(document, ms))
                    {
                        document.Open();

                        for (int i = 0; i < pdf.Count; ++i)
                        {
                            string pathTemp = AppDomain.CurrentDomain.BaseDirectory + "Temp/tempMerge.pdf";
                            PdfReader reader = new PdfReader(pdf[i]);
                            PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(pathTemp, FileMode.Create));
                            pdfStamper.FormFlattening = true;
                            pdfStamper.Close();
                            reader.Close();

                            PdfReader reader2 = new PdfReader(pathTemp);
                            
                            // loop over the pages in that document
                            int n = reader.NumberOfPages;
                            for (int page = 0; page < n;)
                            {
                                copy.AddPage(copy.GetImportedPage(reader2, ++page));
                            }
                            reader2.Close();
                            File.Delete(pathTemp);


                        }
                    }
                }
                mergedPdf = ms.ToArray();
                File.WriteAllBytes(@"F:/Travail/Alternance/Test/MyFileName.pdf", mergedPdf);
            }

        }



        private void openPdf(object sender, RoutedEventArgs e)
        {
            string nomProcedure = (sender as Button).Tag.ToString();
            string nomProcPrec = "";
            double nbAvancement = 0, nbFields = 0;
            int precSigned = 0, numPrec = 0;
            SQLiteCommand cmdRead; SQLiteDataReader sdrRead;
            string conn = "Data Source=EcoDB.db;Version=3";
            SQLiteConnection connection = new SQLiteConnection(conn);
            connection.Open();

            cmdRead = new SQLiteCommand("SELECT numProcedurePrec FROM Procedure WHERE nomProcedure = @nomProcedure and systeme = @systeme", connection);
            cmdRead.Parameters.AddWithValue("@nomProcedure", nomProcedure);
            cmdRead.Parameters.AddWithValue("@systeme", systeme);
            sdrRead = cmdRead.ExecuteReader();
            while (sdrRead.Read())
            {
                numPrec = Convert.ToInt32(sdrRead[0]);
            }

            if(numPrec != 0)
            {
                cmdRead = new SQLiteCommand("SELECT t2.signed, t2.nomProcedure, t1.bypass FROM Procedure t1 JOIN Procedure t2 ON t1.numProcedurePrec = t2.numProcedure WHERE t1.nomProcedure = @nomProcedure and t1.systeme = @systeme", connection);
                cmdRead.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                cmdRead.Parameters.AddWithValue("@systeme", systeme);
                sdrRead = cmdRead.ExecuteReader();

                while (sdrRead.Read())
                {
                    if (Convert.ToInt32(sdrRead[0]) == 1 || Convert.ToInt32(sdrRead[2]) == 1)
                    {
                        precSigned = 1;

                    }
                    else
                    {
                        nomProcPrec = sdrRead[1].ToString();
                    }
                }

                if (precSigned == 0)
                {
                    //MessageBox.Show("Impossible veuillez d'abord remplir le formulaire : " + nomProcPrec);
                    //return;

                    if(CustomMessageBox.ShowOKCancel(
                    "Impossible veuillez d'abord remplir le formulaire : " + nomProcPrec,
                    "Procédure précédente à réaliser nécessaire",
                    "Continuer",
                    "Annuler") == MessageBoxResult.OK)
                    {
                        ModalPassPrec modalPassPrec = new ModalPassPrec();
                        modalPassPrec.ShowDialog();


                        if (modalPassPrec.Valid)
                        {
                            SQLiteCommand cmd;

                            cmd = new SQLiteCommand("UPDATE Procedure SET bypass = 1 WHERE nomProcedure = @nomProcedure AND systeme = @systeme", connection);
                            cmd.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                            cmd.Parameters.AddWithValue("@systeme", systeme);
                            int a = cmd.ExecuteNonQuery();

                            cmd  = new SQLiteCommand("INSERT INTO ProcedureBypass (nomProcedure, systeme, user, commentary) VALUES (@nomProcedure,@systeme, @user, @commentary)", connection);
                            cmd.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                            cmd.Parameters.AddWithValue("@systeme", systeme);
                            cmd.Parameters.AddWithValue("@user", App.Current.Properties["userID"]);
                            cmd.Parameters.AddWithValue("@commentary", modalPassPrec.Commentary);
                            a = cmd.ExecuteNonQuery();

                        }
                        else
                            return;
                    }
                    else
                        return;
                    

                }
            }


            
                
            

            ModalForm modalForm = new ModalForm(nomProcedure, systeme);
            modalForm.ShowDialog();

            bool form = modalForm.Form;
            bool sign = modalForm.Sign;
            bool fncm = modalForm.FNC;
            bool doc = modalForm.Doc;

            

            


            if (form)
            {
                try
                {
                    modalForm.Close();
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    //string path = "F:/Travail/Alternance/Exemple/Tests/" + nomProcedure+ ".pdf";
                    string path = AppDomain.CurrentDomain.BaseDirectory + "/Projets/" + systeme + "/" + nomProcedure + "/" + nomProcedure + ".pdf";
                    Uri pdf = new Uri(path, UriKind.RelativeOrAbsolute);
                    process.StartInfo.FileName = pdf.LocalPath;
                    process.Start();
                    process.WaitForExit();

                    

                    using (var reader = new PdfReader(path))
                    {
                        var fields = reader.AcroFields.Fields;
                        bool refFt = false;

                        

                        cmdRead = new SQLiteCommand("PRAGMA table_info(refFT)", connection);
                        sdrRead = cmdRead.ExecuteReader();

                        foreach (var key in fields.Keys)
                        {
                            var value = reader.AcroFields.GetField(key);
                            
                            


                            while (sdrRead.Read())
                            {
                                if(sdrRead[1].ToString() == key)
                                {
                                    refFt = true;
                                    break;
                                }
                            }

                            SQLiteCommand cmd;

                            if (refFt)
                            {
                                cmd = new SQLiteCommand("UPDATE refFT SET " + key + " = @value WHERE nomProcedure = @nomProcedure AND systeme = @systeme", connection);
                            }
                            else
                            {
                                if (value != "")
                                {
                                    nbAvancement++;
                                }
                                nbFields++;
                                cmd = new SQLiteCommand("UPDATE MoteurMT SET " + key + " = @value WHERE nomProcedure = @nomProcedure AND systeme = @systeme", connection);
                            }
                            
                            
                            cmd.Parameters.AddWithValue("@value", value);
                            cmd.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                            cmd.Parameters.AddWithValue("@systeme", systeme);
                            Console.WriteLine(key + " : " + value);

                            try
                            {
                                int a = cmd.ExecuteNonQuery();

                                //if (cmd.ExecuteNonQuery() == 0)
                                //    Console.WriteLine("Erreur - Key : " + key + " - Value : " + value);



                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }

                            refFt = false;
                        }

                        double avancement = 100 * (double)(nbAvancement / nbFields);

                        SQLiteCommand cmd2;
                        cmd2 = new SQLiteCommand("UPDATE Procedure SET avancement = @avancement WHERE nomProcedure = @nomProcedure AND systeme = @systeme", connection);
                        cmd2.Parameters.AddWithValue("@avancement", avancement);
                        cmd2.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                        cmd2.Parameters.AddWithValue("@systeme", systeme);

                        int b = cmd2.ExecuteNonQuery();

                    }
                    
                }

                catch (Exception error)
                {
                    //MessageBox.Show("Impossible d'ouvrir le fichier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else if (sign)
            {
                modalForm.Close();
                string path = AppDomain.CurrentDomain.BaseDirectory + "/Projets/" + systeme + "/" + nomProcedure + "/" + nomProcedure + "_sign.bmp";

                ModalSign modalSign = new ModalSign(path);
                modalSign.ShowDialog();

                if(modalSign.Valid)
                {
                    SQLiteCommand cmd = new SQLiteCommand("UPDATE Procedure SET  signed = 1 WHERE nomProcedure = @nomProcedure", connection);
                    cmd.Parameters.AddWithValue("@nomProcedure", nomProcedure);

                    try
                    {
                        int a = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    this.NavigationService.Navigate(new Pid(systeme));
                }

            }
            else if (fncm)
            {
                

                modalForm.Close();
                ModalFNC modalFNC = new ModalFNC();
                modalFNC.ShowDialog();

                if (modalFNC.Valid)
                {
                    string commentary = modalFNC.Commentary;
                    string nomFNC = modalFNC.NomFNC;

                    //SQLiteCommand cmd = new SQLiteCommand("UPDATE Procedure SET fnc = 1, fncCOm = @fncCom WHERE nomProcedure = @nomProc", connection);
                    //cmd.Parameters.AddWithValue("@fncCom", fncCom);
                    //cmd.Parameters.AddWithValue("@nomProc", nomProcedure);

                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO FNC (nomProcedure, systeme, nomFNC, commentary) VALUES (@nomProcedure,@systeme, @nomFNC, @commentary)", connection);
                    cmd.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                    cmd.Parameters.AddWithValue("@systeme", systeme);
                    cmd.Parameters.AddWithValue("@nomFNC", nomFNC);
                    cmd.Parameters.AddWithValue("@commentary", commentary);

                    try
                    {
                        int a = cmd.ExecuteNonQuery();

                        if (a == 0)
                        {
                            MessageBox.Show("Erreur, veuillez vérifier vos informations.");
                            modalFNC.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            string path = AppDomain.CurrentDomain.BaseDirectory + "/Projets/" + systeme + "/" + nomProcedure + "/" + nomFNC;
                            MessageBox.Show("FNC enregistrée");
                            
                            File.Copy(modalFNC.pathPDF, path + ".png");

                        }

                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
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

               System.Diagnostics.Process process = new System.Diagnostics.Process();
               Uri docPath = new Uri(path, UriKind.RelativeOrAbsolute);
               process.StartInfo.FileName = docPath.LocalPath;
               process.Start();
               process.WaitForExit();


            }




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
                            string pathPDF = modalProc.pathPDF;
                            string typeEquipement = modalProc.TypeEquipement;
                            string path = AppDomain.CurrentDomain.BaseDirectory + "Projets/" + systeme + "/" + nomProcedure ;

                            string conn = "Data Source=EcoDB.db;Version=3";
                            SQLiteConnection connection = new SQLiteConnection(conn);
                            connection.Open();
                            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Procedure (nomProcedure,systeme, numProcedure, posX, posY, numProcedurePrec, typeEquipement) VALUES (@nomProcedure, @systeme, @numProcedure,  @posX, @posY, @numProcedurePrec, @typeEquipement)", connection);
                            cmd.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                            cmd.Parameters.AddWithValue("@systeme", systeme);
                            cmd.Parameters.AddWithValue("@numProcedure", numProcedure);
                            cmd.Parameters.AddWithValue("@posX", origin.X);
                            cmd.Parameters.AddWithValue("@posY", origin.Y);
                            cmd.Parameters.AddWithValue("@numProcedurePrec", numProcedurePrec);
                            cmd.Parameters.AddWithValue("@typeEquipement", typeEquipement);

                            SQLiteCommand cmd2 = new SQLiteCommand("INSERT INTO refDoc (typeDoc, nomProcedure, nomDoc, systeme) VALUES ('Equipement', @nomProcedure, @nomDoc, @systeme)", connection);
                            cmd2.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                            cmd2.Parameters.AddWithValue("@nomDoc", typeEquipement);
                            cmd2.Parameters.AddWithValue("@systeme", systeme);

                            SQLiteCommand cmd3 = new SQLiteCommand("INSERT INTO refFT (nomProcedure, systeme) VALUES (@nomProcedure, @systeme)", connection);
                            cmd3.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                            cmd3.Parameters.AddWithValue("@systeme", systeme);

                            SQLiteCommand cmd4 = new SQLiteCommand("INSERT INTO "+ Regex.Replace(typeEquipement, @"\s+", "") + " (nomProcedure, systeme) VALUES (@nomProcedure, @systeme)", connection);
                            cmd4.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                            cmd4.Parameters.AddWithValue("@systeme", systeme);



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

                                    this.NavigationService.Navigate(new Pid(systeme));
                                }


                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }

                            connection.Close();

                        }
                    }
                }
                else
                {

                }
                
                
            }
            else if(!pinning)
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
            Console.WriteLine("Zoom : " + Math.Round(inczoom,2));
            Point relative = e.GetPosition(child);
            relative.Y -= 160;

            if (inczoom < 0.1 && inczoom > -0.1)
            {
                
                foreach (Pin pin in vecPin)
                {
                    pin.bodyImage.Visibility = Visibility.Visible;

                }
            }
            else
            {
                foreach (Pin pin in vecPin)
                {
                        pin.bodyImage.Visibility = Visibility.Collapsed;
                    
                }
            }
             

            //foreach (Pin pin in vecPin)
            //{

            //    TransformGroup group = new TransformGroup();
            //    ScaleTransform st = new ScaleTransform();
            //    group.Children.Add(st);
            //    TranslateTransform tt = new TranslateTransform();
            //    group.Children.Add(tt);
            //    pin.rectangle.RenderTransform = tt;



            //    //tt = GetTranslateTransform(pin.rectangle);
            //    //st = GetScaleTransform(pin.rectangle);


            //    //pin.position.X = pin.position.X + ((relative.X - last.X) / pin.zoom) - pin.rectangle.Width/2;
            //    //pin.position.Y = pin.position.Y + ((relative.Y - last.Y) / pin.zoom) - pin.rectangle.Height/2;

            //    /*pin.scale += zoom;
            //    pin.rectangle.Width = pin.scale * pin.width;
            //    pin.rectangle.Height = pin.scale * pin.height;

            //    st.ScaleX = pin.scale;
            //    st.ScaleY = pin.scale;*/

            //    //tt.X = -(relative.X - pin.position.X) / (pin.scale * 10);
            //    //tt.Y =  (relative.Y - pin.position.Y) / (pin.scale);

            //    transformX = relative.X - pin.position.X;
            //    tt.X = -transformX * zoom + pin.deplacement.X;
            //    pin.deplacement.X -= transformX * zoom;



            //    transformY = relative.Y - pin.position.Y;
            //    tt.Y = -transformY * zoom + pin.deplacement.Y;
            //    pin.deplacement.Y -= transformY * zoom;

            //    //Console.WriteLine("TransformX : " + transformX * zoom + "  |  TransformY : " + transformY * zoom);
            //}

            last.X = relative.X;
            last.Y = relative.Y;
        }

        // Ajouter la Pin au vecteur
        public void addRectangle(Point start, int signed)
        {
            Pin newPin = new Pin(start, signed);
            vecPin.Add(newPin);
            canvasPid.Children.Add(newPin.bodyImage);

        }

        //Creation forme
        private void initializeProcedures(string _nomSysteme)
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
                
            fmd.CommandText = @"SELECT nomProcedure, posX, posY, signed, avancement FROM Procedure WHERE systeme = @systeme";
            fmd.Parameters.AddWithValue("@systeme", systeme);

            SQLiteDataReader r = fmd.ExecuteReader();
            while (r.Read())
            {

                i++;
                string name = System.IO.Path.GetFileNameWithoutExtension(Convert.ToString(r["nomProcedure"]));
                listProc.Add(name);

                addRectangle(new Point( Convert.ToInt16(r["posX"]), Convert.ToInt16(r["posY"])), Convert.ToInt16(r["signed"]));

                avancementTotal += Convert.ToDouble(r["avancement"]);
            }

            templ.ItemsSource = listProc;

            labelAvancementSysteme.Content = "Avancement : [" + Convert.ToString(Math.Round((avancementTotal / i))) + "%]";
            progressBarAvSys.Value = Math.Round((avancementTotal / i));

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
