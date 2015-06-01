using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace OvenImageConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Pass 1 parameter, input filename");
                return;
            }

            Bitmap SourceImage = new Bitmap(args[0]);

            using (FileStream OutputFile = File.Create(Path.GetFileNameWithoutExtension(args[0])))
            {
                int Ptr = 0;
                for (int X = 0; X < 320; X++)
                {
                    for (int Y = 0; Y < 240; Y++)
                    {
                        ushort Colour = Color565(SourceImage.GetPixel(X, Y).R, SourceImage.GetPixel(X, Y).G, SourceImage.GetPixel(X, Y).B);

                        OutputFile.Write(new byte[] { SourceImage.GetPixel(X, Y).R, SourceImage.GetPixel(X, Y).G, SourceImage.GetPixel(X, Y).B }, 0, 3);
                        Ptr += 3;
                    }
                }
            }


        }

        private static ushort Color565(byte r, byte g, byte b)
        {
            return (ushort)(((r & 0xF8) << 8) | ((g & 0xFC) << 3) | (b >> 3));
        }
    }
}
