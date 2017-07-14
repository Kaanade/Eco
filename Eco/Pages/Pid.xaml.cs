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
        private int i = 0;
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

        private void openPdf(object sender, RoutedEventArgs e)
        {
            string nameBtn = (sender as Button).Content.ToString();

            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                //string path = "F:/Travail/Alternance/Exemple/Tests/" + nameBtn+ ".pdf";
                string path = AppDomain.CurrentDomain.BaseDirectory + "/Projets/" + systeme + "/"+ nameBtn + ".pdf";
                Uri pdf = new Uri(path, UriKind.RelativeOrAbsolute);
                process.StartInfo.FileName = pdf.LocalPath;
                process.Start();
                process.WaitForExit();

                using (var reader = new PdfReader(path))
                {
                    var fields = reader.AcroFields.Fields;

                    foreach (var key in fields.Keys)
                    {
                        var value = reader.AcroFields.GetField(key);
                        Console.WriteLine(key + " : " + value);
                    }
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Impossible d'ouvrir le fichier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }



        }


        private void btnTestPdf(object sender, RoutedEventArgs e)
        {
            i++;
            //ModalProcedure modalwindow = new ModalProcedure();

            //Nullable<bool> dialogResult = modalwindow.ShowDialog();

            //DialogBox dialogBox = new DialogBox();


            System.Windows.Controls.Button newBtn = new Button();

            newBtn.Content = "FT12-1 - SRI 001 MO" + i;
            newBtn.Name = "btnTestProc" + i;
            newBtn.Click += openPdf;
            newBtn.Width = 200;
            newBtn.Height = 100;
            newBtn.Padding = new Thickness(2.0);
            newBtn.Margin = new Thickness(2.0);

            //procGrid.Width = 200;
            Canvas.SetLeft(newBtn, (i-1) * 200);

            procGrid.Children.Add(newBtn);

            

           /* try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                string path = "F:/test.pdf";
                //string path = AppDomain.CurrentDomain.BaseDirectory + "test.pdf";
                Uri pdf = new Uri(path, UriKind.RelativeOrAbsolute);
                process.StartInfo.FileName = pdf.LocalPath;
                process.Start();
                process.WaitForExit();
            }
            catch (Exception error)
            {
                MessageBox.Show("Could not open the file.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }*/
        }

        private void btnTestTranslate(object sender, RoutedEventArgs e)
        {
            foreach (Pin pin in vecPin)
            {
                
                TranslateTransform tt = new TranslateTransform();
                
                pin.rectangle.RenderTransform = tt;
         
                tt.X = 100 + pin.deplacement.X;
                pin.deplacement.X += 100;
         
            }
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
                            string pathPDF = modalProc.pathPDF;

                            string conn = "Data Source=EcoDB.db;Version=3";
                            SQLiteConnection connection = new SQLiteConnection(conn);
                            connection.Open();
                            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Procedure (nomProcedure, numProcedure,pathPDF) VALUES (@nomProcedure,@numProcedure,@pathPDF)", connection);
                            cmd.Parameters.AddWithValue("@nomProcedure", nomProcedure);
                            cmd.Parameters.AddWithValue("@numProcedure", numProcedure);
                            cmd.Parameters.AddWithValue("@pathPDF", pathPDF);

                            try
                            {
                                int a = cmd.ExecuteNonQuery();

                                if (a == 0)
                                {
                                    MessageBox.Show("Erreur, veuillez vérifier vos informations.");
                                    modalProc.Visibility = Visibility.Visible;
                                }
                                else
                                {
                                    modalProc.Close();

                                    File.Copy(@pathPDF, @"Projets/" + systeme + "/" + System.IO.Path.GetFileName(pathPDF));
                                }

                                connection.Close();
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }

                        }
                    }
                }
                else
                {

                }

                //addRectangle(origin);
                
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
                foreach (Pin pin in vecPin)
                {
                    TranslateTransform tt = new TranslateTransform();
                    pin.rectangle.RenderTransform = tt;
                    
                    tt.X = v.X + pin.deplacement.X;
                    tt.Y = v.Y + pin.deplacement.Y;

                    newPos = new Point(pin.position.X + v.X , pin.position.Y + v.Y );
                    pin.position = newPos;
                    pin.deplacement.X += v.X;
                    pin.deplacement.Y += v.Y;


                }
            }
            
        }

        //Zoom 
        private void CanvasWheel(object sender, MouseWheelEventArgs e)
        {
            

            double zoom = e.Delta > 0 ? 0.2 : -0.2;
            inczoom += zoom;
            //Console.WriteLine("Zoom : " + inczoom);
            Point relative = e.GetPosition(child);
            relative.Y -= 160;
            

            foreach (Pin pin in vecPin)
            {
                TransformGroup group = new TransformGroup();
                ScaleTransform st = new ScaleTransform();
                group.Children.Add(st);
                TranslateTransform tt = new TranslateTransform();
                group.Children.Add(tt);
                pin.rectangle.RenderTransform = tt;



                //tt = GetTranslateTransform(pin.rectangle);
                //st = GetScaleTransform(pin.rectangle);


                //pin.position.X = pin.position.X + ((relative.X - last.X) / pin.zoom) - pin.rectangle.Width/2;
                //pin.position.Y = pin.position.Y + ((relative.Y - last.Y) / pin.zoom) - pin.rectangle.Height/2;

                /*pin.scale += zoom;
                pin.rectangle.Width = pin.scale * pin.width;
                pin.rectangle.Height = pin.scale * pin.height;

                st.ScaleX = pin.scale;
                st.ScaleY = pin.scale;*/

                //tt.X = -(relative.X - pin.position.X) / (pin.scale * 10);
                //tt.Y =  (relative.Y - pin.position.Y) / (pin.scale);

                transformX = relative.X - pin.position.X;
                tt.X = -transformX * zoom + pin.deplacement.X;
                pin.deplacement.X -= transformX * zoom;



                transformY = relative.Y - pin.position.Y ;
                tt.Y = -transformY * zoom + pin.deplacement.Y;
                pin.deplacement.Y -= transformY * zoom;

                //Console.WriteLine("TransformX : " + transformX * zoom + "  |  TransformY : " + transformY * zoom);

                int a = 0;

            }

            last.X = relative.X;
            last.Y = relative.Y;
            

        }

        // Ajouter la Pin au vecteur
        public void addRectangle(Point start)
        {
            Pin newPin = new Pin(start);
            vecPin.Add(newPin);
            canvasPid.Children.Add(newPin.rectangle);

        }

        //Creation forme
        private void initializeProcedures(string _nomSysteme)
        {
            //string path = System.AppDomain.CurrentDomain.BaseDirectory;
            systeme = _nomSysteme;
            
            SQLiteConnection conn = new SQLiteConnection("Data Source=EcoDB.db;Version=3");
            conn.Open();
            
            var command = conn.CreateCommand();

            //Read from table
            command.CommandText = "SELECT idPID FROM Systeme WHERE nomSysteme = '@nomSysteme'";
            command.Parameters.AddWithValue("@nomSysteme", _nomSysteme);
            SQLiteDataReader sdr = command.ExecuteReader();
            string idPid = sdr.GetValue(0).ToString();

            //BitmapImage image = new BitmapImage(new Uri("Projets/" + _nomSysteme + "/" + sdr.GetString(0) + ".png", UriKind.Relative));
            BitmapImage image = new BitmapImage(new Uri("F:/Travail/Alternance/Test/pidTest.png", UriKind.Absolute));
            myPid.Source = image;

            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=EcoDB.db;Version=3"));

            List<string> ImportedFiles = new List<string>();
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=EcoDB.db;Version=3"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    fmd.CommandText = @"SELECT DISTINCT nomProcedure FROM Procedure";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader r = fmd.ExecuteReader();
                    while (r.Read())
                    {
                        ImportedFiles.Add(Convert.ToString(r["nomProcedure"]));

                        i++;
                        string name = System.IO.Path.GetFileNameWithoutExtension(Convert.ToString(r["nomProcedure"]));

                        System.Windows.Controls.Button newBtn = new Button();
                        newBtn.Content = name;
                        newBtn.Name = "btn" + i.ToString();
                        newBtn.Click += openPdf;
                        newBtn.Width = 200;
                        newBtn.Height = 100;
                        newBtn.Padding = new Thickness(2.0);
                        newBtn.Margin = new Thickness(2.0);

                        //procGrid.Width = (i + 1) * 200;
                        Canvas.SetLeft(newBtn, (i - 1) * 200);
                        procGrid.Children.Add(newBtn);
                    }
                }
            }


            /*string path = "F:/Travail/Alternance/Exemple/Tests";

            DirectoryInfo dirInfo = new DirectoryInfo(path);

            FileInfo[] info = dirInfo.GetFiles("*.pdf", SearchOption.AllDirectories);

            foreach (FileInfo file  in info)
            {
                i++;
                string name = System.IO.Path.GetFileNameWithoutExtension(file.Name);
                
                System.Windows.Controls.Button newBtn = new Button();
                newBtn.Content = name;
                newBtn.Name = "btn" + i.ToString();
                newBtn.Click += openPdf;
                newBtn.Width = 200;
                newBtn.Height = 100;
                newBtn.Padding = new Thickness(2.0);
                newBtn.Margin = new Thickness(2.0);

                //procGrid.Width = (i + 1) * 200;
                Canvas.SetLeft(newBtn, (i-1) * 200);
                procGrid.Children.Add(newBtn);
            }*/


            
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
