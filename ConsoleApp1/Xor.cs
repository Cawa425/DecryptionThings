using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public static class Xor
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
                    .Range(0, xored.Length / 8)
                    .Select(i =>
                        Convert
                            .ToByte(xored
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

        public static string SingleByteXorEncrypt(string text,string key)
        {
            text.ToLower();
            var keys = Convert.ToByte(key);

            var bytes = Encoding.ASCII.GetBytes(text);
                var resultBytes = bytes.Select(x=>Convert.ToByte(x^keys)).ToArray();

                var result = Encoding.ASCII.GetString(resultBytes);
            return result;
        }
        
        // public void e(string i)
        // {
        //     var a = "";
        //     foreach (var d in i.ToUpper().GroupBy(x => x).OrderByDescending(u => u.Count()))
        //     {
        //         if (d.Key < 91 && d.Key > 64)
        //         {
        //             a += d.Key;
        //         }
        //     }
        //     for (int x = 65; x < 91; x++)
        //     {
        //         if (!a.Contains((char)x))
        //         {
        //             a += (char)x;
        //         }
        //     }
        //     a.Dump();
        // }Dump
    }
}