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
using System.ComponentModel;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Home : Page, INotifyPropertyChanged
    {
        private string _login;

        public event PropertyChangedEventHandler PropertyChanged;

        public string login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                OnPropertyChanged("test");
            }

        }
        public Home()
        {
            
            InitializeComponent();
            //labellLogin.Content = Properties.Settings.Default.Username;
            DataContext = this;
        }

        private void btnPrep(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/PreparerHome.xaml", UriKind.Relative));
        }

        private void btnReal(object sender, EventArgs e)
        {
            ModalRealiserHome modalRealHome = new ModalRealiserHome();
            modalRealHome.ShowDialog();
            
            if (modalRealHome.Valid)
            {
                string systeme = modalRealHome.RSysteme;
                this.NavigationService.Navigate(new Pid(modalRealHome.RSysteme));
            }
                
        }

        private void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void btnClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


    }
}


