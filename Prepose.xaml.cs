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

namespace GestionHopital
{
    /// <summary>
    /// Logique d'interaction pour Prepose.xaml
    /// </summary>
    public partial class Prepose : Window
    {
        Gestion_Hopital1Entities gestionH;
        public Prepose(Gestion_Hopital1Entities g)
        {
            InitializeComponent();
            gestionH = g;
        }

     

        private void btnAjouterPatient_Click(object sender, RoutedEventArgs e)
        {
            AjouterPatient ajout = new AjouterPatient(gestionH);
            ajout.ShowDialog();
        }

        private void btnRechercher_Click(object sender, RoutedEventArgs e)
        {
            bool trouve = false;
            
            string patientRechercher = txtRechercherPatient.Text;
            foreach(Patient patient in gestionH.Patients.ToList())
            {
                if (patient.NSS.ToString() == patientRechercher)
                {
                   
                    datePatR.SelectedDate = patient.dateNaissance;
                    txtNomR.Text = patient.nom;
                    txtPrenomR.Text = patient.prenom;
                    txtAdresseR.Text = patient.adresse;
                    txtVilleR.Text = patient.ville;
                    txtProvinceR.Text = patient.province;
                    txtCodePostalR.Text = patient.codePOstal;
                    txtTelephoneR.Text = patient.telephone;
                    txtIDAssR.Text = patient.idAssurance.ToString();
                    btnCreerPatient.IsEnabled = false;
                    trouve = true;
                }
                
            }
            if(trouve==false)
            {
                MessageBox.Show("Patient inéxistant ! Merci de le créer", "Attention",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                btnCreerPatient.IsEnabled = true;
            }
            
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                    }

        private void txtRechercherPatient_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnCreerAdmission_Click(object sender, RoutedEventArgs e)
        {
            AjouterAdmission uneAdmission = new AjouterAdmission(gestionH);
            uneAdmission.ShowDialog();
        }

        private void btnAdmissionVaccination_Click(object sender, RoutedEventArgs e)
        {
            Vaccination vaccination = new Vaccination();
            vaccination.ShowDialog();
            
        }
    }
}
