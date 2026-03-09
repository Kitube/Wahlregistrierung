using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace Wahlregistrierung.Services
{
    public class CameraService
    {
        private VideoCapture? capture;

        public bool StartCamera(int cameraIndex = 0)
        {
            StopCamera();

            capture = new VideoCapture(cameraIndex, VideoCaptureAPIs.DSHOW);

            if (!capture.IsOpened())
            {
                StopCamera();
                return false;
            }

            // Try to request a better resolution / FPS from the camera driver
            capture.Set(VideoCaptureProperties.FrameWidth, 1280);
            capture.Set(VideoCaptureProperties.FrameHeight, 720);
            capture.Set(VideoCaptureProperties.Fps, 30);

            return true;
        }

        public Bitmap? GetFrame()
        {
            if (capture == null || !capture.IsOpened())
                return null;

            using var frame = new Mat();
            capture.Read(frame);

            if (frame.Empty())
                return null;

            return BitmapConverter.ToBitmap(frame);
        }

        public void StopCamera()
        {
            capture?.Release();
            capture?.Dispose();
            capture = null;
        }

        public static string TestCameraIndexes()
        {
            var result = new System.Text.StringBuilder();

            for (int i = 0; i < 6; i++)
            {
                try
                {
                    using var test = new VideoCapture(i);
                    bool opened = test.IsOpened();
                    result.AppendLine($"Camera {i}: {(opened ? "OK" : "Not available")}");
                }
                catch (Exception ex)
                {
                    result.AppendLine($"Camera {i}: Error - {ex.Message}");
                }
            }

            return result.ToString();
        }
    }
}