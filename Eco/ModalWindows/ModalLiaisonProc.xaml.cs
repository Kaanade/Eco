using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour ModalLiaisonProc.xaml
    /// </summary>
    public partial class ModalLiaisonProc : Window
    {
        public ModalLiaisonProc()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnValid(object sender, RoutedEventArgs e)
        {
            /*valid = true;
            this.Hide();*/
        }

    }
}
