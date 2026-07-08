using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using WebcamOverlay.Camera;
using WebcamOverlay.Helpers;

namespace WebcamOverlay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CameraService _cameraService = new CameraService();
        
        public MainWindow()
        {
            InitializeComponent();

            _cameraService.FrameReceived += OnFrameReceived;
            _cameraService.Start();
        }
        
        protected override void OnClosed(EventArgs e)
        {
            _cameraService.FrameReceived -= OnFrameReceived;
            _cameraService.Dispose();

            base.OnClosed(e);
        }
        
        private void OnFrameReceived(Bitmap bitmap)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                BitmapSource bitmapSource = BitmapSourceConverter.Convert(bitmap);

                CameraImage.Source = bitmapSource;

                bitmap.Dispose();
            }));
        }
    }
}