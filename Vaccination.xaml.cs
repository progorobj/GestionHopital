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
        public Vaccination()
        {
            InitializeComponent();
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
    }
}
