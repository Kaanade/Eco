﻿using System;
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
using System.Data;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.IO;

namespace Eco
{
    /// <summary>
    /// Logique d'interaction pour CreationIntervenant.xaml
    /// </summary>
    public partial class CreationIntervenant : Page
    {
        public CreationIntervenant()
        {
            InitializeComponent();
        }

        private void btnClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnRetour(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/Affecter.xaml", UriKind.Relative));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Voulez vous annuler la création du projet ? ", "Annuler Projet", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                this.NavigationService.Navigate(new Uri("Pages/PreparerHome.xaml", UriKind.Relative));
            }

        }

        private void btnValid(object sender, RoutedEventArgs e)
        {
            string nomUtilisateur = txtNomUtilisateur.Text;
            string conn = "Data Source=EcoDB.db;Version=3";
           /* SQLiteConnection connection = new SQLiteConnection(conn);
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Installation (nomInstallation) VALUES (@nomInstallation)", connection);
            cmd.Parameters.AddWithValue("@nomInstallation", txtNomUtilisateur.Text);*/
            try
            {
               /* cmd.ExecuteNonQuery();
                connection.Close();*/
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }


        }
    }
}
