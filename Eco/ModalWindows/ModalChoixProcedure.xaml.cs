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

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour ModalChoixProcedure.xaml
    /// </summary>
    public partial class ModalChoixProcedure : Window
    {
        private bool creer = false, choix = false;



        public ModalChoixProcedure()
        {
            InitializeComponent();
        }

        public bool Creer
        {
            get { return creer; }
        }

        public bool Choix
        {
            get { return choix; }
        }

        private void btnCreerProc(object sender, EventArgs e)
        {
            creer = true;
            this.Hide();
        }

        private void btnChoixProc(object sender, EventArgs e)
        {
            choix = true;
            this.Hide();
        }
    }

   
}
