using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace itsec_lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Steganograph stg = new Steganograph();
            File.WriteAllBytes(@"D:\txt.txt",stg.GetInfoFromBmp(@"D:\1.bmp").ToArray());
            stg.HideTxtIntoBmp(@"D:\1_1.bmp", @"D:\wop.txt");
            File.WriteAllBytes(@"D:\1wop.txt",stg.GetInfoFromBmp(@"D:\1_1.bmp").ToArray());
        }
    }
}