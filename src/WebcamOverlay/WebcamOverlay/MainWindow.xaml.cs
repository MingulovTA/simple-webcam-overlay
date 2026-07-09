using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WebcamOverlay.Camera;
using WebcamOverlay.Helpers;

namespace WebcamOverlay
{
    public partial class MainWindow : Window
    {
        private readonly CameraService _cameraService = new CameraService();

        public MainWindow()
        {
            InitializeComponent();
            _cameraService.FrameReceived += OnFrameReceived;
            ShowCameraSelection();
        }

        protected override void OnClosed(EventArgs e)
        {
            _cameraService.FrameReceived -= OnFrameReceived;
            _cameraService.Dispose();

            base.OnClosed(e);
        }

        private void ShowCameraSelection()
        {
            CameraListPanel.Children.Clear();
            var cameras = _cameraService.GetAvailableCameras();
            foreach (var camera in cameras)
            {
                var button = new Button
                {
                    Content = camera.Name, Margin = new Thickness(0, 0, 0, 10), Padding = new Thickness(10),
                    FontSize = 18
                };
                button.Click += (sender, args) => StartCamera(camera);
                CameraListPanel.Children.Add(button);
            }
        }
        
        private void StartCamera(CameraInfo camera)
        {
            CameraListPanel.Visibility = Visibility.Collapsed;
            CameraImage.Visibility = Visibility.Visible;

            _cameraService.Start(camera);
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