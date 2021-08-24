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
    /// Logique d'interaction pour AjouterAdmission.xaml
    /// </summary>
    public partial class AjouterAdmission : Window
    {
        Gestion_Hopital1Entities BddGestion;
        public AjouterAdmission(Gestion_Hopital1Entities gestionH)
        {
            InitializeComponent();
            BddGestion = gestionH;
        }

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          
            cbxNSS1.DataContext = BddGestion.Patients.ToList();
            cbxMedecin1.DataContext = BddGestion.Medecins.ToList();
            cbxNumeroLit1.DataContext = BddGestion.Lits.ToList();
            dpDateAdmission1.SelectedDate = DateTime.Today.Date;
            cbxChoixChambre.IsEnabled = false;
        
        }

        private void btnAjouterAdmission_Click(object sender, RoutedEventArgs e)
        {
            Lit unLit = cbxNumeroLit1.SelectedItem as Lit;
            Patient unPatient = cbxNSS1.SelectedItem as Patient;
            IEnumerable<Assurance> assurance = from a in BddGestion.Assurances
                                               where (a.idAssurance == unPatient.idAssurance)
                                               select a;
            
            //vérifier si tout les lits sont occupés
            if (unLit.occupe == 1)
            {
                int occ = 0;
                foreach (Lit l in BddGestion.Lits.ToList())
                {
                    if (l.occupe == 0)
                    {
                        occ++;

                    }
                }


                if (occ > 0)
                {
                    MessageBox.Show("Le lit que vous avez choisit est occupé, " +
                        "merci de choisir un autre lit", "Attention",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                    //il faut ajouter ici les conditions pour les lits
                }
                else
                    MessageBox.Show("On ne peut pas admettre le patient, " +
                        "les lits sont tous occupés", "Attention",
                    MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                //La liste assurance contient l'assurance du patient (taille liste 1) creer avec LINQ
                Assurance uneAssurance = null;
                foreach (Assurance item in assurance)
                {
                    uneAssurance = item;
                }


                
                    Admission uneAdmission = new Admission();

                    uneAdmission.dateAdmission = (DateTime)dpDateAdmission1.SelectedDate;
                    if (checkBPHONE1.IsChecked == true)
                    {
                        uneAdmission.telephone = 1;
                    }
                    else
                    {
                        uneAdmission.telephone = 0;
                    }



                    if (checkBTeleviseur1.IsChecked == true)
                    {
                        uneAdmission.televiseur = 1;
                    }
                    else
                    {
                        uneAdmission.televiseur = 0;
                    }

                    if (checkBChirurgieProg1.IsChecked == true)
                    {
                        uneAdmission.chirurgieProgramme = 1;
                        uneAdmission.dateChirurgie = dpDateChirurgie.SelectedDate;
                    }
                    else
                    {
                        uneAdmission.chirurgieProgramme = 0;
                    }

                   uneAdmission.NSS = int.Parse(cbxNSS1.Text);
                    uneAdmission.idMedecin = int.Parse(cbxMedecin1.Text);
                    uneAdmission.numeroLit = int.Parse(cbxNumeroLit1.Text);


                    foreach (Lit l in BddGestion.Lits)
                    {
                        if (l.numeroLit == unLit.numeroLit)
                        {
                            l.occupe = 1;

                        }
                    }

                    BddGestion.Admissions.Add(uneAdmission);
                    try
                    {
                        BddGestion.SaveChanges();
                        MessageBox.Show("Admission Ajouté avec succès!");
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }

                    if (uneAssurance.nomCompagnie == "ramq")
                    {

                        MessageBox.Show("Vous n'avez pas d'assurance privé! un extra sera facturé.",
                            "Attention", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                                  


            }
        }
        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbxNSS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void checkBChirurgieProg1_Click(object sender, RoutedEventArgs e)
        {
            if (checkBChirurgieProg1.IsChecked == true)
            {
                dpDateChirurgie.IsEnabled = true;
            }
            else
                dpDateChirurgie.IsEnabled = false;
        }

        private void checkBoxChoixChambre_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxChoixChambre.IsChecked == true)
            {
                cbxChoixChambre.IsEnabled = true;

                cbxChoixChambre.DataContext = BddGestion.TypeLits.ToList();
            }
            else
                cbxChoixChambre.IsEnabled = false;
        }
    }
}
