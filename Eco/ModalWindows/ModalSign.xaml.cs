using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour ModalSign.xaml
    /// </summary>
    public partial class ModalSign : Window
    {
        private string _path;
        private bool valid;

        public ModalSign()
        {
            InitializeComponent();
        }

        public ModalSign(string path)
        {
            InitializeComponent();
            _path = path;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Voulez vous annuler la signature ? ", "Annuler Signature", MessageBoxButton.YesNo);
      
        }

        private void btnValid(object sender, RoutedEventArgs e)
        {
            //string sigPath = System.IO.Path.GetTempFileName();
            string sigPath = _path;
            valid = true;

            MemoryStream ms = new MemoryStream();
            FileStream fs = new FileStream(sigPath, FileMode.Create);

            RenderTargetBitmap rtb = new RenderTargetBitmap(300, 200, 96d, 96d, PixelFormats.Default);
            rtb.Render(InkSign);
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            encoder.Save(fs);

            this.Close();
        }

        public bool Valid
        {
            get { return valid; }
        }

    }
}
