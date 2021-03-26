using System;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class Xor
    {
        public static string XoringTwoBinars(string text,string key)
        {
            var str = new StringBuilder();

            for (var i = 0; i < text.Length; i++)
            {
                str.Append(text[i] != key[i] ? 1 : 0);
            }

            return str.ToString();
        }
        public static string BytePerByteFromHexToHex(string plaintext, string pad)
        {
            //В бинарный вид
            string binarystring = Binar.FromHexToBinar(plaintext);
            string binarykey = Binar.FromHexToBinar(pad);
            //Xor, 1 если разные, иначе 0

            var xored = XoringTwoBinars(binarystring,binarykey);
            //Соединяем
            var hex = string.Join("",
                Enumerable
                    .Range(0, xored.ToString().Length / 8)
                    .Select(i =>
                        Convert
                            .ToByte(xored
                                .ToString()
                                .Substring(i * 8, 8), 2)
                            .ToString("X2")));
            return hex;
        }
        
        
        public static string RepeatingKey(string str, string key)
        {
            var adaptiveKey = new StringBuilder();
            while (adaptiveKey.ToString().Length!=str.Length)
            {
                foreach (var t in key.Where(t => adaptiveKey.ToString().Length != str.Length))
                {
                    adaptiveKey.Append(t);
                }
            }
           return BytePerByteFromHexToHex(Hex.ToHexString(str), Hex.ToHexString(adaptiveKey.ToString())).ToLower();
        }

        // public static string SingleByteDecryptWithOutKey(string text)
        // {
        //     return 
        // }
        // public static string SingleByteEncrypt(string text, byte key)
        // {
        //     byte[] b = Encoding.ASCII.GetBytes(text);
        //    // byte[] a = Encoding.ASCII.GetBytes(key);
        //     //return Encoding.ASCII.GetString(b.Select((x,z) => x ^ a[z]));
        // }
    }
}