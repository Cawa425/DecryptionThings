using System;
using System.Text;

namespace ConsoleApp1
{
    public class Base64
    {
        public static string Decoder(string input)
        {
            byte[] data = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(data);
        }
        public static string Encoder(string input)
        {
            byte[] data =Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(data);
        }
    }
}