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
using System.Windows.Shapes;

namespace GestionHopital
{
    /// <summary>
    /// Logique d'interaction pour Administrateur.xaml
    /// </summary>
    public partial class Administrateur : Window
    {
        Gestion_Hopital1Entities uneGestion;
        public Administrateur(Gestion_Hopital1Entities g)
        {
            InitializeComponent();
            uneGestion = g;
            cbListeMed.DataContext = uneGestion.Medecins.ToList();
           // cbxMedMod.DataContext = uneGestion.Medecins.ToList();
            cbListeMed.SelectedIndex = 0;
           // cbxMedMod.SelectedIndex = 0;

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

        private void cbListeInfirmieres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAjouterInfirmiere_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModiffierInfirmiere_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSupprimerInfirmiere_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
