using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace itsec_lab3
{
    public class Steganograph
    {
        private Byte mask = 0b11;
        public IEnumerable<byte> GetInfoFromBmp(string path)
        {
            List<byte> txt = new List<byte>();
            byte[] bmp = File.ReadAllBytes(path);

            for (int i = 54; i < bmp.Length; i += 4)
            {
                byte symbol = 0;
                for (int j = 0; j < 4; ++j)
                {
                    symbol <<= 2;
                    symbol |= (byte) (bmp[i + j] & mask);
                }

                if (symbol == 0xFF)
                    break;
                
                txt.Add(symbol);
            }

            return txt;
        }
        
        public void HideTxtIntoBmp(string bmpPath, string txtPath)
        {
            byte[] bmp = File.ReadAllBytes(bmpPath);
            byte[] txt = File.ReadAllBytes(txtPath);
            int k = 0;
            for (int i = 54; i < bmp.Length; i += 4)
            {
                byte symbol = txt[k];
                for (int j = 0; j < 4; ++j)
                {
                    bmp[i + j] &= 0xFC;
                    bmp[i + j] |= (byte) (symbol >> 6 & mask);
                    symbol <<= 2;
                }
                ++k;
                if (k >= txt.Length)
                {
                    for (int j = 0; j < 4; j++) 
                    {
                        bmp[i + j] &= 0xFC;
                        bmp[i + j] |= 0b11;
                    }
                    break;
                }
            }
            
            File.WriteAllBytes(bmpPath, bmp);
        }
    }
}