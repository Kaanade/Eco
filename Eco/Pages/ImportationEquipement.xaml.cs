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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour ImportationEquipement.xaml
    /// </summary>
    public partial class ImportationEquipement : Page
    {
        public ImportationEquipement()
        {
            InitializeComponent();
        }

        private void btnClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnRetour(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/PreparerHome.xaml", UriKind.Relative));
        }

        private void btnCreaSys(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/CreationSysteme.xaml", UriKind.Relative));
        }
    }
}
