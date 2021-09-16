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
    /// Logique d'interaction pour Administrateur.xaml
    /// </summary>
    public partial class Administrateur : Window
    {
        Gestion_Hopital1Entities uneGestion;
        Infirmier infirmier = new Infirmier();
        public Administrateur(Gestion_Hopital1Entities g)
        {
            InitializeComponent();
            uneGestion = g;
            cbListeMed.DataContext = uneGestion.Medecins.ToList();
           // cbxMedMod.DataContext = uneGestion.Medecins.ToList();
            cbListeMed.SelectedIndex = 0;
            // cbxMedMod.SelectedIndex = 0;
            cbListeInfirmieres.DataContext = uneGestion.Infirmiers.ToList();
            cbxListInfirmier.DataContext = uneGestion.Infirmiers.ToList();
            cbxListStations.DataContext = uneGestion.Stations.ToList();

            //Section Infirmier (LP)
            
        }

        private void btnAjouterM_Click(object sender, RoutedEventArgs e)
        {
            
            Medecin unMedecin = new Medecin();
            if (!String.IsNullOrEmpty(txtNomMedecin.Text) && !String.IsNullOrEmpty(txtPrenomMedecin.Text) )
            {
                unMedecin.nom = txtNomMedecin.Text;
                unMedecin.prenom = txtPrenomMedecin.Text;
                uneGestion.Medecins.Add(unMedecin);
                try
                {
                    uneGestion.SaveChanges();
                    MessageBox.Show("Medecin Ajouté avec succès!");
                    //cbxMedMod.DataContext = null;
                  //  cbxMedMod.DataContext = uneGestion.Medecins.ToList();
                    cbListeMed.DataContext = uneGestion.Medecins.ToList();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Merci de remplir les champs vide!","Attention",
                    MessageBoxButton.OK,MessageBoxImage.Information);
            }
           
        }
      

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Medecin unMed = cbListeMed.SelectedItem as Medecin;
            txtNomMed.Text = unMed.nom;
            txtPrenomMed.Text = unMed.prenom;

            
            txtPrenomMed.Text = unMed.prenom;
            var query =
                from l in uneGestion.Admissions                
                join c in uneGestion.Patients on l.NSS equals c.NSS                
                select new {l.idAdmission,c.NSS,c.prenom,c.nom,l.dateAdmission,l.dateChirurgie,l.dateConge };
            //dgPatient.DataContext = query.ToList();
              
            
            //definir la date du jours pour Affectation
            datePAffectation.SelectedDate = DateTime.Today;

            
        }

       

       private void btnModifier1_Click(object sender, RoutedEventArgs e)
        {
            Medecin unMed = cbListeMed.SelectedItem as Medecin;
            unMed.nom = txtNomMed.Text;
            unMed.prenom = txtPrenomMed.Text;
            try
            {

                uneGestion.SaveChanges();
                MessageBox.Show("Medecin Modifié avec succès!");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        // code a modifier
        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
          /*  if(ckbxAssur.IsChecked==true && ckbxMedecin.IsChecked == false)
            {
                var query =
                from l in uneGestion.Admissions
                join c in uneGestion.Patients on l.NSS equals c.NSS
                join a in uneGestion.Assurances on c.idAssurance equals a.idAssurance
                select new { l.idAdmission, c.NSS, c.prenom, c.nom, a.nomCompagnie, l.dateAdmission, l.dateChirurgie, l.dateConge };
                dgPatient.DataContext = query.ToList();


            }
            else if(ckbxAssur.IsChecked==true && ckbxMedecin.IsChecked==true)
            {
                var query =
                from l in uneGestion.Admissions
                join c in uneGestion.Patients on l.NSS equals c.NSS
                join a in uneGestion.Assurances on c.idAssurance equals a.idAssurance
                join m in uneGestion.Medecins on l.idMedecin equals m.idMedecin
                select new { l.idAdmission, c.NSS, c.prenom, a.nomCompagnie, m.idMedecin,m.nom,l.dateAdmission, l.dateChirurgie, l.dateConge };
                dgPatient.DataContext = query.ToList();

            }
            else if(ckbxAssur.IsChecked == false && ckbxMedecin.IsChecked == true)
            {
               var query =
               from l in uneGestion.Admissions
               join c in uneGestion.Patients on l.NSS equals c.NSS              
               join m in uneGestion.Medecins on l.idMedecin equals m.idMedecin
               select new { l.idAdmission, c.NSS, c.prenom, m.idMedecin, m.nom, l.dateAdmission, l.dateChirurgie, l.dateConge };
                dgPatient.DataContext = query.ToList();

            }
            else
            {
                var query =
                from l in uneGestion.Admissions
                join c in uneGestion.Patients on l.NSS equals c.NSS                
                select new { l.idAdmission, c.NSS, c.prenom, c.nom,l.dateAdmission, l.dateChirurgie, l.dateConge };
                dgPatient.DataContext = query.ToList();

            }*/
        }


        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
          
            Medecin unMed = cbListeMed.SelectedItem as Medecin;

            uneGestion.Medecins.Remove(unMed);

            try
            {
               

                uneGestion.SaveChanges();
                MessageBox.Show("Medecin Supprimé avec succès!");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Vérifier si ce medecin est lié à d'autre table ");
            }
        }

        private void txtNomInfirmiere_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPrenomInfirmiere_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtNomInfirmiere2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPrenomInfirmiere2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPrenomPrepose_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtNomPrepose_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtNomPrepose2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPrenomPrepose2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cbListePreposes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void refresh()
        {
            cbListeInfirmieres.DataContext = uneGestion.Infirmiers.ToList();
        }
        private void cbListeInfirmieres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Infirmier sinfirmier = (Infirmier)cbListeInfirmieres.SelectedItem;
            txtNomInfirmiere2.Text = sinfirmier.NomInf;
            txtPrenomInfirmiere2.Text = sinfirmier.PrenomInf;
        }

        private void btnAjouterInfirmiere_Click(object sender, RoutedEventArgs e)
        {
            Infirmier infirmier = new Infirmier();
            infirmier.NomInf = txtNomInfirmiere.Text;
            infirmier.PrenomInf = txtPrenomInfirmiere.Text;

            uneGestion.Infirmiers.Add(infirmier);

            try
            {
                uneGestion.SaveChanges();
                MessageBox.Show("Infirmier ajouter avec success!");
                refresh();
                txtNomInfirmiere.Text = String.Empty;
                txtPrenomInfirmiere.Text = String.Empty;

            }
            catch (Exception)
            {

                MessageBox.Show("L'infirmier n'a pas pu etre ajouter");
            }
        }

        private void btnModiffierInfirmiere_Click(object sender, RoutedEventArgs e)
        {
            
            Infirmier sinfirmier = (Infirmier)cbListeInfirmieres.SelectedItem;

            sinfirmier.NomInf = txtNomInfirmiere2.Text;
            sinfirmier.PrenomInf = txtPrenomInfirmiere2.Text;

            try
            {
                uneGestion.SaveChanges();
                MessageBox.Show("Medecin modifier avec succes!");
                refresh();
            }
            catch (Exception)
            {
                MessageBox.Show("Le Medecin n'a pas pu etre modifier");
            }
        }

        private void btnSupprimerInfirmiere_Click(object sender, RoutedEventArgs e)
        {
            Infirmier inf = cbListeInfirmieres.SelectedItem as Infirmier;
            uneGestion.Infirmiers.Remove(inf);

            try
            {
                uneGestion.SaveChanges();
                MessageBox.Show("Infirmier retirer avec success!");
                refresh();

            }
            catch (Exception)
            {

                MessageBox.Show("L'infirmier n'a pas pu etre retirer");
            }

        }

        private void btnAjouterPrepose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModiffierPrepose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSupprimerPrepose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbListeMed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbListeMed_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAjouterAffectation_Click(object sender, RoutedEventArgs e)
        {
            Affectation uneAffectation = new Affectation();

            uneAffectation.idInfirmier = int.Parse(cbListeInfirmieres.Text);
            uneAffectation.NumeroStation = int.Parse(cbxListStations.Text);
            uneAffectation.DateAffectation = datePAffectation.SelectedDate.Value;

            uneGestion.Affectations.Add(uneAffectation);

            try
            {


                uneGestion.SaveChanges();
                MessageBox.Show("Affectation ajouté avec succès!");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Vérifier les informatiosn entrées ! ");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
