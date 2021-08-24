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
    /// Logique d'interaction pour Medecins.xaml
    /// </summary>
    public partial class Medecins : Window
    {
        Gestion_Hopital1Entities uneGestion;
        
        public Medecins(Gestion_Hopital1Entities gh)
        {
            InitializeComponent();
            uneGestion = gh;
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = cbxPatient.SelectedItem as Patient;
          
            bool trouve = false;
            foreach (Admission adm in uneGestion.Admissions.ToList())
            {
                if(adm.dateConge==null && (adm.NSS==patient.NSS))
                {
                    adm.dateConge = dateCong.SelectedDate;
                    trouve = true;
                    string typeLit;
                    int prixChambre=0;
                    string Assur = "";
                    //déterminer le nombre de jours passé à l'hopital
                    TimeSpan datePass = (DateTime)adm.dateConge - (DateTime)adm.dateAdmission;
                    int a = (int)datePass.TotalDays;
                    
                    foreach (Lit unlit in uneGestion.Lits.ToList())
                    {
                        //determiner le type de lit et le prix
                        foreach (TypeLit typeL in uneGestion.TypeLits)
                        {
                            if(typeL.idType==unlit.idType)
                            {
                                typeLit = typeL.description;
                                if(typeLit=="privé")
                                {
                                    prixChambre = 571;
                                }
                                else if(typeLit=="semi-privé")
                                {
                                    prixChambre = 267;
                                }
                            }
                        }
                        //déterminer le type d'assurance

                        //libérer le lit et mettre à jours la base de données
                        if(unlit.numeroLit==adm.numeroLit && adm.NSS==patient.NSS)
                        {
                            unlit.occupe = 0;
                            foreach (Assurance uneAssur in uneGestion.Assurances)
                            {
                                if(uneAssur.idAssurance==patient.idAssurance)
                                {
                                    Assur = uneAssur.nomCompagnie;
                                }

                            }
                            
                        }

                       
                    }
                    //A vérifier les conditions
                    try
                    {
                        uneGestion.SaveChanges();
                        if (Assur != "ramq")
                        {
                            MessageBox.Show(String.Format($"le Patient: {patient.prenom} {patient.nom}" +
                            $" a passé {a} jours.Vous disposez d'une assurance privé,aucun frais n'a été facturer. "));
                        }
                        else if (Assur == "ramq")
                        {
                            MessageBox.Show(String.Format($"le Patient: {patient.prenom} {patient.nom}" +
                            $" a passé {a} jours. Montant de séjour facturé à {prixChambre * a} Les frais facturé : téléviseur {a * 7.5}$ , téléphone {a * 42.5}$"));
                        }

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }

            /*Afficher un message pour informé le medecin si la date de congé a été ajouté
             ou le patient il dispose déjà d'une date de congé*/
            if(trouve==true)
            {
                MessageBox.Show("Date de congé ajouté avec succès!", "Information",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Le Patient a déjà une date de congé!", "Attention",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbxMedecin.DataContext = uneGestion.Medecins.ToList();
            cbxPatient.DataContext = uneGestion.Patients.ToList();
            dateCong.SelectedDate = DateTime.Now.Date;

        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
