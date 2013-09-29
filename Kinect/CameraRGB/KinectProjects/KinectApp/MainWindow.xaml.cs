using Microsoft.Kinect;
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

namespace KinectApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private WriteableBitmap _imageSource;

        public MainWindow()
        {
            InitializeComponent();
            InitializeKinect();
        }

        private void InitializeKinect()
        {
            KinectUtil.KinectInitializer initializer = new KinectUtil.KinectInitializer();
            initializer.Initialize(
                (kinectSensor) =>
                {
                    kinectSensor.ColorStream.Enable();
                    kinectSensor.ColorFrameReady += ColorFrameReady;
                });

            chooserUI.KinectSensorChooser = initializer.KinectChooser;
        }

        private void ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame currentFrame = e.OpenColorImageFrame())
            {
                if (currentFrame != null)
                {
                    byte[] imageBytes = new byte[currentFrame.PixelDataLength];
                    currentFrame.CopyPixelDataTo(imageBytes);

                    if (_imageSource == null)
                    {
                        _imageSource = new WriteableBitmap(BitmapImage.Create(currentFrame.Width, currentFrame.Height, 96, 96,
                                                                              PixelFormats.Bgr32, null, imageBytes,
                                                                              currentFrame.Width * currentFrame.BytesPerPixel));
                        KinectCameraImage.Source = _imageSource;
                    }
                    else
                        _imageSource.WritePixels(new Int32Rect(0, 0, currentFrame.Width, currentFrame.Height), imageBytes, currentFrame.Width * currentFrame.BytesPerPixel, 0);
                }
            }
        }
    }
}
