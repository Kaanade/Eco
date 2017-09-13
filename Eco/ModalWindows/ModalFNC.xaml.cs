using System.Windows;
using Microsoft.Win32;
using System.Windows.Documents;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour ModalFNC.xaml
    /// </summary>
    public partial class ModalFNC : Window
    {
        public bool valid;

        public ModalFNC()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnValid(object sender, RoutedEventArgs e)
        {
            valid = true;
            this.Hide();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                txtPathPDF.Text = openFileDialog.FileName;
        }

        public string Commentary
        {
            get { return new TextRange(commentary.Document.ContentStart, commentary.Document.ContentEnd).Text; ; }
        }

        public string pathPDF
        {
            get { return txtPathPDF.Text; }
        }

        public string NomFNC
        {
           get { return txtNomFNC.Text; }
        }
    }
}
