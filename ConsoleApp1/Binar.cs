using System;
using System.Linq;

namespace ConsoleApp1
{
    public class Binar
    {
        public static string FromHexToBinar(string text)
        {
            return  String.Join(String.Empty,
                text.Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                )
            );
        }
    }
}