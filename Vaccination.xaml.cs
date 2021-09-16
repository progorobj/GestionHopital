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

                dossVacc.NSS = int.Parse(cbxListePatients.Text);                
                dossVacc.DateCreationD = dateAdmission.SelectedDate.Value;
                dossVacc.NombreDoses = int.Parse(nombreDoses.Text);
                dossVacc.DatePremiereDose = dateDose1.SelectedDate.Value;

            if (int.Parse(nombreDoses.Text) == 2)
            {
                dossVacc.DateDeuxiemeDose = dateDose2.SelectedDate.Value;
            }

                dossVacc.idPrepose = idp;
                dossVacc.NumeroStation = int.Parse(cbxListeStation.Text);


            //Attribuer le numero dossier a un vaccin

            Vaccin unVaccin = cbxVaccins.SelectedItem as Vaccin;
            unVaccin.NumeroDossierV = dossVacc.NumeroDossierV;



                uneGestion.DossierVaccins.Add(dossVacc);
                
            try
                {
                    uneGestion.SaveChanges();
                    MessageBox.Show("Dossier Vaccination ajouté avec succès Ajouté avec succès!");
                   

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            
           
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
            idPrepose.Text = idp.ToString();
            idPrepose.IsEnabled = false;
        }
    }
}
