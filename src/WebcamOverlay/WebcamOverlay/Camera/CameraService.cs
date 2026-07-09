using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
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
        
        public void Start(CameraInfo camera)
        {
            if (camera == null)
                throw new ArgumentNullException(nameof(camera));

            Stop();

            _videoDevice = new VideoCaptureDevice(camera.MonikerString);

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
        
        public IReadOnlyList<CameraInfo> GetAvailableCameras()
        {
            FilterInfoCollection cameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            return cameras
                .Cast<FilterInfo>()
                .Select(camera => new CameraInfo(camera.Name, camera.MonikerString))
                .ToList();
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