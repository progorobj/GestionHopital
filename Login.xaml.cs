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
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        Gestion_Hopital1Entities gh;
        
        public Login()
        {
            InitializeComponent();
            gh = new Gestion_Hopital1Entities();
        }
        string nomU;
        int pass;
        int trouve = 0;
        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                nomU = txtUtilisateur.Text;
                pass = int.Parse(txtPass.Text);
           


            if (String.IsNullOrEmpty(txtUtilisateur.Text) &&
                String.IsNullOrEmpty(txtPass.Text))
            {
                MessageBox.Show("Merci de saisir un nom d'utilisateur et un mot de passe!", "Attention",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (nomU == "admin" && pass == 1234)
            {
                trouve = 2;
            }
            else
            {
                
                foreach (Prepose item in gh.Preposes)
                {
                    if (item.Nom == nomU && item.idPrepose == pass)
                    {
                        trouve = 1;
                       
                    }

                }

                if(trouve==0)
                {
                    foreach (Medecin item in gh.Medecins)
                    {
                        if (item.nom == nomU && item.idMedecin == pass)
                        {
                            trouve = 3;
                        }

                    }


                }
               
               
              

            }




            if (trouve == 1)
            {
                Prepose unprep = new Prepose(gh,pass);
                unprep.Show();
                this.Close();

            }
            else if (trouve == 2)
            {
                Administrateur unadmin = new Administrateur(gh);
                unadmin.Show();
                this.Close();

            }
            else if (trouve == 3)
            {
                Medecins unMedecin = new Medecins(gh);
                unMedecin.Show();
                this.Close();

            }
            else                  
                MessageBox.Show("Le nom d'utilisateur ou le mot de passe est incorrect!", "Attention",
                MessageBoxButton.OK, MessageBoxImage.Information);


            }
            catch (Exception)
            {
                MessageBox.Show("Le mot de passe ne doit pas contenir de caractères!", "Attention",
               MessageBoxButton.OK, MessageBoxImage.Information);
            }



        }


    }
}
