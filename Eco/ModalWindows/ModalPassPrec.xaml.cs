using System.Windows;
using Microsoft.Win32;
using System.Windows.Documents;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour ModalPassPrec.xaml
    /// </summary>
    public partial class ModalPassPrec : Window
    {
        private bool valid;

        public ModalPassPrec()
        {
            InitializeComponent();

            txtNomUser.Content = App.Current.Properties["userName"].ToString() + " " + App.Current.Properties["userFirstName"].ToString();
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

        

        public string Commentary
        {
            get { return new TextRange(commentary.Document.ContentStart, commentary.Document.ContentEnd).Text; ; }
        }

        
       

        public bool Valid
        {
            get { return valid; }
        }
    }
}
