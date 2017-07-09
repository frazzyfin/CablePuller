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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
using ImageMagick;
using CablePuller.Utils;

namespace CablePuller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Directory.CreateDirectory("ConvertedImages");
        }

        public static BitmapImage BitmapFromUri(Uri source)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            return bitmap;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF Files (*.pdf)|*.pdf";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                txtPDFpath.Text = filename;
            }
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        { 
            pdfImage.Source = null;
            pdfImage.Visibility = Visibility.Hidden;

            string path;
            // Validate the path
            try
            {
                path = System.IO.Path.GetFullPath(txtPDFpath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid path", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string imagePath = Utils.PDFUtils.convertPDFtoPNG(path);

            // Show the pdf image we just made on the window
            BitmapImage img = BitmapFromUri(new Uri(imagePath));
            pdfImage.Source = img;
            pdfImage.Visibility = Visibility.Visible;
        }
    }
}