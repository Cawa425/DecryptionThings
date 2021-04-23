using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    public class ESB
    {
        public static AesManaged aes = new AesManaged
        {
            KeySize = 128,
            BlockSize = 128,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.None,
            IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };
        public static byte[] Encrypt(string plainText,string key)
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            Console.WriteLine($"PlainText:  {plainText}");
            var buffer = Encoding.UTF8.GetBytes(plainText);
            

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            buffer = encryptor.TransformFinalBlock(buffer, 0, buffer.Length);
            Console.WriteLine($"CipherText: {Encoding.UTF8.GetString(buffer)}");
            return buffer;
        }

        public static string Decrypt(byte[] cipherText, string key)
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            Console.WriteLine($"CipherText: {Encoding.UTF8.GetString(cipherText)}");
            var decryptor  = aes.CreateDecryptor(aes.Key, aes.IV);
            
            var buffer = Encoding.UTF8.GetString(decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length));
            Console.WriteLine($"PlainText: {buffer}");
            return buffer;

        }
    }
}