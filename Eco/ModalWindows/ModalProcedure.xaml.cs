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

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour ModalProcedure.xaml
    /// </summary>
    public partial class ModalProcedure : Window
    {
        private bool valid = false;

        public ModalProcedure()
        {
            InitializeComponent();
        }

        public string NomProc
        {
            get { return txtNomProcedure.Text; }
        }

        public int NumProc
        {
            get { return Int32.Parse(txtNumProcedure.Text); }
        }
        public string pathPDF
        {
            get { return txtPathPDF.Text; }
        }

        public bool Valid
        {
            get { return valid; }
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




        //private void btnValid(object sender, RoutedEventArgs e)
        //{

        




        //}


    }
}
