using Wahlregistrierung.Enums;

namespace Wahlregistrierung.Config
{
    public class AppSettings
    {
        public CameraScanMode CameraMode { get; set; } = CameraScanMode.ManualTrigger;
        public int CameraIndex { get; set; } = 1;

        // Slightly slower = more stable while OCR is running
        public int ScanIntervalMs { get; set; } = 250;

        public int CooldownMs { get; set; } = 1500;
        public int ConsecutiveFramesRequired { get; set; } = 3;

        // Not used directly anymore for the OCR crop,
        // but keep them for later settings support.
        public double CropWidthRatio { get; set; } = 0.70;
        public double CropHeightRatio { get; set; } = 0.20;

        public static AppSettings CreateDefault()
        {
            return new AppSettings();
        }
    }
}