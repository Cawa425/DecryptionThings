using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    public static class CFB
    {
        public static void Test(string original, string key, string IV)
        {
            var aes = new RijndaelManaged
            {
                Mode = CipherMode.CFB,
                Key = Encoding.UTF8.GetBytes(key),
                IV = Encoding.UTF8.GetBytes(IV)
            };

            
            // Encrypt the string to an array of bytes. 
            var encrypted = EncryptStringToBytes(original, aes.Key, aes.IV);

            // Decrypt the bytes to a string. 
            var roundtrip = DecryptStringFromBytes(encrypted, aes.Key, aes.IV);

            //Display the original data and the decrypted data.
            Console.WriteLine("Original:   {0}", original);
            Console.WriteLine("Ciphered:   {0}", string.Concat(encrypted));
            Console.WriteLine("Round Trip: {0}", roundtrip);
        }

        private static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            var aes = new RijndaelManaged
            {
                Mode = CipherMode.CFB,
                Key = Key,
                IV = IV,
            };

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            // Create the streams used for encryption. 
            using var msEncrypt = new MemoryStream();
            using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }

            return msEncrypt.ToArray();
        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            var aes = new RijndaelManaged
            {
                Mode = CipherMode.CFB,
                Key = Key,
                IV = IV,
            };
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            // Create the streams used for decryption. 
            using var msDecrypt = new MemoryStream(cipherText);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            return srDecrypt.ReadToEnd();
        }
    }
}