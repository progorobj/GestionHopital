using Gma.QrCodeNet.Encoding;
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
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.IO;
using QRCoder;
using System.Drawing;
using Color = System.Windows.Media.Color;
using System.Drawing.Imaging;

namespace GestionHopital
{
    /// <summary>
    /// Lógica de interacción para Vaccination.xaml
    /// </summary>
    public partial class Vaccination : Window
    {
        Gestion_Hopital1Entities uneGestion;
        int idp;
        Vaccin unVaccin;
        public Vaccination(Gestion_Hopital1Entities g,int idPrep)
        {
            InitializeComponent();
            uneGestion = g;
            idp = idPrep;
            cbxVaccins.DataContext = g.Vaccins.ToList();
            unVaccin = new Vaccin();
            cbxListePatients.DataContext = g.Patients.ToList();
            cbxListeStation.DataContext = g.Stations.ToList();
            
            
            // les vaccins devront etre créer sans leur affecté des numéro de dossier au départ 
            //   de suite les numéros de dossier seront attribué lors de la création du dossier

        }




        private void numeroDossier_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void nombreDoses_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void idPrepose_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void nomVaccin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
          
        private void btnCreerAdmission_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                Patient lePatient = cbxListePatients.SelectedItem as Patient;
                int dateNaissance = DateTime.Today.Year - lePatient.dateNaissance.Year;
                DossierVaccin dossVacc = new DossierVaccin();

                if (int.Parse(nombreDoses.Text) == 0 || int.Parse(nombreDoses.Text) == 1)
                {
                    if (dateNaissance >= 12)
                    {


                        if (((int.Parse(nombreDoses.Text) == 0) &&
                            (dateDose1.SelectedDate != null)) || ((int.Parse(nombreDoses.Text) == 1) &&
                            (dateDose1.SelectedDate != null) && (dateDose2.SelectedDate != null)))
                        {
                            if (int.Parse(nombreDoses.Text) == 0)
                            {
                                
                                dossVacc.NombreDoses = 1;
                            }
                            else if(int.Parse(nombreDoses.Text) == 0)
                            {
                                dossVacc.DateDeuxiemeDose = dateDose2.SelectedDate;
                                dossVacc.NombreDoses = 2;
                                
                            }
                            nombreDoses.Text = dossVacc.NombreDoses.ToString();

                            dossVacc.NSS = int.Parse(cbxListePatients.Text);
                            dossVacc.DateCreationD = dateAdmission.SelectedDate.Value;
                            dossVacc.NombreDoses = int.Parse(nombreDoses.Text);
                            dossVacc.idPrepose = idp;
                            dossVacc.NumeroStation = int.Parse(cbxListeStation.Text);
                            dossVacc.DatePremiereDose = dateDose1.SelectedDate;
                            uneGestion.DossierVaccins.Add(dossVacc);
                            try
                            {
                                unVaccin.NumeroDossierV = dossVacc.NumeroDossierV;
                                uneGestion.SaveChanges();
                                MessageBox.Show("Dossier Vaccination ajouté avec succès Ajouté avec succès!");


                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Merci de selectionner les dates requises!", "Attention",
                                MessageBoxButton.OK, MessageBoxImage.Warning);

                        }



                    }
                    else
                    {
                        MessageBox.Show("Ce patient n'est pas eligible au vaccin!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else if (int.Parse(nombreDoses.Text) == 2)
                {
                    MessageBox.Show("Deux doses sont déjà administré a ce patient !", "Informations dose", MessageBoxButton.OK,
                                   MessageBoxImage.Information);
                }
                else if (int.Parse(nombreDoses.Text) == -1)
                {
                    MessageBox.Show("Le vaccin selectionné n'est pas de la marque que la première dose  !", "Informations dose", MessageBoxButton.OK,
                                   MessageBoxImage.Information);

                }

            }
            catch
            {
                MessageBox.Show("Merci de vérifier tout les champs requis  !", "Informations dose", MessageBoxButton.OK,
                                   MessageBoxImage.Information);
            }
           
            
           
        }
        //Générer un QR code pour le vaccin
        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            txtInfo.Text = cbxListePatients.Text + cbxVaccins.Text;
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(txtInfo.Text, QRCodeGenerator.ECCLevel.Q);
            QRCode qRCode = new QRCode(qRCodeData);
            Bitmap qrCodeImage = qRCode.GetGraphic(20);

            qrImage.Source = BitmapToImageSource(qrCodeImage);
        }

        private ImageSource BitmapToImageSource(Bitmap bitmap)
        {
            using(MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dateAdmission.SelectedDate = DateTime.Today.Date;
            dateDose1.SelectedDate = DateTime.Today.Date;
            idPrepose.Text = idp.ToString();
            idPrepose.IsEnabled = false;
            unVaccin = cbxVaccins.SelectedItem as Vaccin;

           
            


        }

        
        public  int nombreDeDoseAdministre()
        {
            int nbreDossier = 0;
            int occVacc = 0;
            DossierVaccin dossierV = new DossierVaccin();

            Patient unPatient = cbxListePatients.SelectedItem as Patient;

            foreach (DossierVaccin item in uneGestion.DossierVaccins)
            {
                if(item.NSS==unPatient.NSS)
                {
                    nbreDossier++;
                    dossierV = item;

                }
            }

           
            if(nbreDossier==1)
            {
                        Vaccin unV = new Vaccin();
                        dateDose1.SelectedDate = dossierV.DatePremiereDose;
                        foreach (Vaccin v in uneGestion.Vaccins)
                        {
                            
                            if(v.NumeroDossierV==dossierV.NumeroDossierV)
                            {
                              unV = v; 
                            }
                        }
                    
                    if(unVaccin.Marque == unV.Marque)
                    {
                       occVacc = 1;
                
                    }
                    else if(unVaccin.Marque != unV.Marque)
                    {
                    occVacc--;
                    }
                    

                
                
            }
            else if(nbreDossier==2)

            {
                occVacc = 2;
            }
            
            return occVacc;
        }
        
        private void cbxListePatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            
           
            nombreDoses.Text = nombreDeDoseAdministre().ToString();
        }

        private void cbxVaccins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            unVaccin = cbxVaccins.SelectedItem as Vaccin;
            nombreDoses.Text = nombreDeDoseAdministre().ToString();
            nomVaccin.Content = unVaccin.Marque;
        }
    }
}
