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
    /// Logique d'interaction pour AjouterPatient.xaml
    /// </summary>
    public partial class AjouterPatient : Window
    {
        Gestion_Hopital1Entities magestion;
        
        public AjouterPatient(Gestion_Hopital1Entities gestionH)
        {
            InitializeComponent();
            magestion = gestionH;
        }

        private void btnRechercher_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtRechercherPatient_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnAjouterPatient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Patient unPatient = new Patient();
                //unPatient.NSS = int.Parse(txtNSSA.Text);
                unPatient.nom = txtNomA.Text;
                unPatient.dateNaissance = (DateTime)datePatA.SelectedDate;
                unPatient.prenom = txtPrenomA.Text;
                unPatient.adresse = txtAdresseA.Text;
                unPatient.ville = txtVilleA.Text;
                unPatient.province = txtProvinceA.Text;
                unPatient.codePOstal = txtCodePostalA.Text;
                unPatient.telephone = txtTelephoneA.Text;
                unPatient.idAssurance = int.Parse(cbxIdAssurance.Text);

                magestion.Patients.Add(unPatient);

                try
                {
                    magestion.SaveChanges();
                    MessageBox.Show("Patient Ajouté avec succès!");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }


            catch (Exception)
            {
                MessageBox.Show("Il ya des champs non rempli", "Attention", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }



        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbxIdAssurance.DataContext = magestion.Assurances.ToList();
        }
    }
}
