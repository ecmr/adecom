using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdECon
{
    public static class help
    {
        public static Bitmap generateQr(int width, int height, string text)
        {
            var bw = new ZXing.BarcodeWriter();
            var encodeOptons = new ZXing.Common.EncodingOptions() { Width = width, Height = height, Margin = 0 };
            bw.Options = encodeOptons;
            bw.Format = ZXing.BarcodeFormat.QR_CODE;
            return new Bitmap(bw.Write(text));

        }
    }
}
