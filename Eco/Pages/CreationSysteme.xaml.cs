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
using Microsoft.Win32;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour CreationSysteme.xaml
    /// </summary>
    public partial class CreationSysteme : Page
    {
        public CreationSysteme()
        {
            InitializeComponent();
        }
        

        private void btnClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnRetour(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/ImportationEquipement.xaml", UriKind.Relative));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Voulez vous annuler la création du système ? ", "Annuler Système", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                this.NavigationService.Navigate(new Uri("Pages/PreparerHome.xaml", UriKind.Relative));
            }

        }

        private void btnDocSys(object sender, RoutedEventArgs e)
        {
            string docsysPath;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                docsysPath = openFileDialog.FileName;
        }

        private void btnValid(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
