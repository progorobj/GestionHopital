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
        string pass;
        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            nomU = txtUtilisateur.Text;
            pass = txtPass.Text;
            if (String.IsNullOrEmpty(nomU) &&
                string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Merci de sair un nom d'utilisateur et un mot de passe!", "Attention",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (nomU == "prep" && pass == "prep")
                {
                    Prepose unprep = new Prepose(gh);
                    unprep.Show();
                    this.Close();
                    
                }
                else if (nomU == "admin" && pass == "admin")
                {
                    Administrateur unadmin = new Administrateur(gh);
                    unadmin.Show();
                    this.Close();
                    
                }
                else if (nomU == "med" && pass == "med")
                {
                    Medecins unMedecin = new Medecins(gh);
                    unMedecin.Show();
                    this.Close();

                }
                else
                    MessageBox.Show("Le nom d'utilisateur ou le mot de passe est incorrect!", "Attention",
                    MessageBoxButton.OK, MessageBoxImage.Information);



            }
        }

        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
