using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows;
using AForge.Video;
using AForge.Video.DirectShow;

namespace WebcamOverlay.Camera
{
    public class CameraService : IDisposable
    {
        private VideoCaptureDevice _videoDevice;
        private FilterInfoCollection _cameras;
        
        public event Action<Bitmap> FrameReceived;
        
        public void Start()
        {
            _cameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (_cameras.Count == 0)
                throw new InvalidOperationException("No camera found.");

            _videoDevice = new VideoCaptureDevice(_cameras[0].MonikerString);

            _videoDevice.NewFrame += OnNewFrame;

            _videoDevice.Start();
        }
        
        

        public void Stop()
        {
            if (_videoDevice == null)
                return;

            _videoDevice.NewFrame -= OnNewFrame;

            if (_videoDevice.IsRunning)
            {
                _videoDevice.SignalToStop();
                _videoDevice.WaitForStop();
            }

            _videoDevice = null;
        }

        public void Dispose()
        {
            Stop();
        }
        
        private void OnNewFrame(object sender, NewFrameEventArgs e)
        {
            Bitmap bitmap = (Bitmap)e.Frame.Clone();

            OnFrameReceived(bitmap);
        }
        
        protected virtual void OnFrameReceived(Bitmap bitmap)
        {
            FrameReceived?.Invoke(bitmap);
        }
    }
}