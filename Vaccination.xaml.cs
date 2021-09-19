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
        public Vaccination(Gestion_Hopital1Entities g,int idPrep)
        {
            InitializeComponent();
            uneGestion = g;
            idp = idPrep;
            cbxListePatients.DataContext = g.Patients.ToList();
            cbxListeStation.DataContext = g.Stations.ToList();
            cbxVaccins.DataContext = g.Vaccins.ToList();
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
            DossierVaccin dossVacc = new DossierVaccin();

            Patient lePatient = cbxListePatients.SelectedItem as Patient;
            int dateNaissance = DateTime.Today.Year - lePatient.dateNaissance.Year;
            Vaccin unVaccin = cbxVaccins.SelectedItem as Vaccin;

            int marqueVerification = 0;

            foreach (DossierVaccin item in uneGestion.DossierVaccins)
            {
                if(item.NSS== int.Parse(cbxListePatients.Text))
                {
                    foreach (Vaccin v in uneGestion.Vaccins)
                    {
                        if(v.Marque==unVaccin.Marque)
                        {
                            marqueVerification = 1;
                        }
                    }
                }
            }

            int nbreDoseValide = 0;
            //pour vérifier si la date a été selectionné ou nom
         
                if (dateNaissance >= 12)
                {


                    if (unVaccin.DossierVaccin == null)
                    {
                                if (String.IsNullOrEmpty(nombreDoses.Text) )
                                {
                                    MessageBox.Show("Merci de saisir le nombre de vaccin!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);

                                }
                                else if (int.Parse(nombreDoses.Text) == 1 )
                                {
                                    if(dateDose1.SelectedDate==null)
                                        {
                                                MessageBox.Show("Merci de selectionner la date de la première dose !", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                                        }
                                    else
                                        {
                                            dossVacc.DatePremiereDose = dateDose1.SelectedDate.Value;
                                            nbreDoseValide = 1;

                                        }
                                        
                                   

                                }

                             
                                else if((int.Parse(nombreDoses.Text) == 2) && (dateDose1.SelectedDate != null)
                                                   &&  (dateDose2.SelectedDate!= null))
                                {
                                    if(marqueVerification==1)
                                    {
                                      dossVacc.DatePremiereDose = dateDose1.SelectedDate.Value;
                                      dossVacc.DateDeuxiemeDose = dateDose2.SelectedDate.Value;
                                      nbreDoseValide = 1;

                                    }
                                    else
                                    {
                                     MessageBox.Show("le vaccin selectionné n'est pas de la même marque que le vaccin administré " +
                                     "dans la première dose!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }
                                    

                                }
                                else
                                {
                                    MessageBox.Show("Vérifier le nombre de vaccin , ainsi selectionné les dates! ", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }


                           if(nbreDoseValide==1 && !String.IsNullOrEmpty(cbxListePatients.Text)
                               && !String.IsNullOrEmpty(cbxListeStation.Text) && !String.IsNullOrEmpty(idPrepose.Text)
                               && dateAdmission.SelectedDate.Value!=null)
                            {

                                dossVacc.NSS = int.Parse(cbxListePatients.Text);
                                dossVacc.DateCreationD = dateAdmission.SelectedDate.Value;
                                dossVacc.NombreDoses = int.Parse(nombreDoses.Text);
                                dossVacc.idPrepose = idp;
                                dossVacc.NumeroStation = int.Parse(cbxListeStation.Text);

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
                                MessageBox.Show("Merci de vérifier les champs non valide", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                       

                    }
                    else
                        MessageBox.Show("Ce vaccin est déjà associé à un dossier de vaccination", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);



                }

                else
                    MessageBox.Show("Ce patient n'est pas eligible au vaccin!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);



           
            
           
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
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
        }

       
    }
}
