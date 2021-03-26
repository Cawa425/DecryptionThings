using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main()
        {
            // Task1();
            //Task2();
             //Task3();
            //Task4();
            //Task5();
            FrequencyTable();
               
             //Task7();
            //Task8();
        }

        public static void FrequencyTable()
        {
            
            var key = 82;
            var plain_text =
                "british troops entered cuxhaven at 1400 on 6 may - from now on all radio traffic will cease - wishing you all the best. lt kunkel.";

            var minFittingQuotient = 0;
            var temp = Encoding.ASCII.GetBytes(key.ToString());
            Console.WriteLine();
            //Xor.SingleByteEncrypt(plain_text,key.ToString());
        }
        public static void Task5() 
        {
            var text = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";
            //var text = "ciphertext";
            //68
            Console.WriteLine((Xor.RepeatingKey(text, "88")));
            var str = new StringBuilder();
            var results = new Dictionary<int,double>();
            for (var i = 1; i < text.Length / 2; i++)
            {
                str.Append(new string(text.Skip(text.Length-i).ToArray()));
                str.Append(new string(text.Take(text.Length - i).ToArray()));
                var count = text.Select((t1, t) => t1 == str.ToString()[t] ? 1 : 0).Sum();

                var value = (double) count / text.Length;
                str.Clear();
                results.Add(i, value);
                Console.WriteLine("{0} {1}",i,Math.Round(value,3, MidpointRounding.ToEven));
            }
            var temp =  results.ToList();
           temp.Sort((x,m)=>x.Value.CompareTo(m.Value));
           temp.Reverse();
           var blocks = new List<string>();
           for (var i = 0; i < 2; i ++)
           {    
               var distance = temp[i].Key;
               var index = 0;
                var stringB = new StringBuilder();

                for (var m = 0; m < distance; m++)
                {
                    index = m;

                    while (index < text.Length)
                    {
                        stringB.Append(text[index]);
                        index += distance;
                    }
                    blocks.Add(stringB.ToString());
                    stringB.Clear();
                }
               //получаем блоки
               var b = Xor.RepeatingKey(blocks[0], " ");
                Console.WriteLine();
           }

        }
        private static void Task51()
        {
            var text = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";

            var a = text.Length; //68

            for (int i = 2; i < a / 2; i++)
            {
                if (a % i != 0) continue;
                var length = text.Length / i;
                var strs = new List<string>();

                //делим блоки попарно
                for (var t = 0; t < i; t++)
                {
                    var tmp = text.Substring(t * length, length);
                    strs.Add(tmp);
                }

                if (strs.Count % 2 != 0) continue;
                //находим расстояние
                var averageHamingDistance = 0;

                for (var t = 0; t < strs.Count; t += 2)
                {
                    var xor = Xor.XoringTwoBinars(strs[t], strs[t + 1]);
                    var HammingDistance = xor.Count(x => x == '1');
                    averageHamingDistance += HammingDistance;
                }

                averageHamingDistance /= strs.Count / 2;
                //Взламываем
                var blocks = new StringBuilder();


                for (var m = 0; m * averageHamingDistance < text.Length; m++)
                {
                    blocks.Append(new String(text.Skip(m * averageHamingDistance).Take(averageHamingDistance).ToArray()));
                }

                var kek = blocks.Length;
                Console.WriteLine("\n");
            }

            var d = a / 4; //16
        }

        
        private static void Task7()
        {
            string[] text =
            {
                "Burning 'em, if you ain't quick and nimble",
                "I go crazy when I hear a cymbal"
            };
            var key = "ICE";
            var adaptiveKey = new StringBuilder();
            adaptiveKey.Append(key);

            foreach (var str in text)
            {
                var tmp = Xor.RepeatingKey(str, key);
                Console.WriteLine(tmp);
            }
        }

        private static void Task4()
        {
            var str1 = "1c0111001f010100061a024b53535009181c";
            var str2 = "686974207468652062756c6c277320657965";

            var result = "746865206b696420646f6e277420706c6179";
            Console.WriteLine(Xor.BytePerByteFromHexToHex(str1, str2).ToLower());
            Console.WriteLine(result == Xor.BytePerByteFromHexToHex(str1, str2).ToLower());
            Console.WriteLine(Hex.FromHexString("746865206b696420646f6e277420706c6179"));
        }

        private static void Task3()
        {
            var str1 =
                "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
            Console.WriteLine(Base64.Encoder(Hex.FromHexString(str1)));
        }

        private static void Task2()
        {
            var text = "4c6f72656d20497073756d2069732073696d706c792064756d6d792074657874206f662074"
                       + "6865207072696e74696e6720616e64207479706573657474696e6720696e6475737472792e" +
                       "436865636b206f757420746869732074616c6b20696620796f75206861766e277420646f6"
                       + "e6520736f2068747470733a2f2f7777772e796f75747562652e636f6"
                       + "d2f77617463683f763d6d4b535136446a427a3377";

            Console.WriteLine(Hex.FromHexString(text));
        }

        private static void Task1()
        {
            string[] strings =
            {
                "WW91IGRpZCBpdCE=",
                "S2VlbiBvbiBnb2luZyE=",
                "VG9wIHNlY3JldCBpbmZvcm1hdGlvbiBpcyBuZWFyIHlvdS4u",
                "KRUGC5BAO5QXGIDUOJUWG23ZEBXW4ZJB",
                "Q29uZ3JhdHVsYXRpb25zISBZb3UndmUgJ2RlY3J5cHRlZCcgYWxsIG1lc3NhZ2VzIQ=="
            };
            Console.WriteLine(Base64.Decoder(strings[0]));
            Console.WriteLine(Base64.Decoder(strings[1]));
            Console.WriteLine(Base64.Decoder(strings[2]));
            Console.WriteLine(Base32.ToString(Base32.ToBytes(strings[3])));
            Console.WriteLine(Base64.Decoder(strings[4]));
        }
    }
}