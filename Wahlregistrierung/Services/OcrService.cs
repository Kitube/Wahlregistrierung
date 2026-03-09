using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Tesseract;

namespace Wahlregistrierung.Services
{
    public class OcrService
    {
        public string ReadIdFromImage(Bitmap image)
        {
            using var engine = new TesseractEngine(AppPaths.TessdataDirectory, "eng", EngineMode.Default);

            engine.SetVariable("tessedit_char_whitelist", "0123456789");
            engine.DefaultPageSegMode = PageSegMode.SingleWord;

            image.Save("debug_ocr.png");

            using var pix = Pix.LoadFromMemory(BitmapToBytes(image));
            using var page = engine.Process(pix);

            string raw = page.GetText() ?? "";

            var match = Regex.Match(raw, @"0\d{7}");
            return match.Success ? match.Value : "";
        }

        private byte[] BitmapToBytes(Bitmap bitmap)
        {
            using var ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
    }
}