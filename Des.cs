using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMiSOIB
{
    class Des
    {
        private string message;
        private string key;
        public string l;
        public string r;

        private readonly int[] substituteVector =
        {
            58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8, 
            57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3, 
            61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7,
        };

        public Des(string message, string key)
        {
            this.message = message;
            this.key = key;
            l = message.Substring(0, message.Length / 2);
            r = message.Substring(message.Length / 2, message.Length / 2);

            Init();
        }

        private void Init()
        {
            var msgDex = Utills.ConvertToDex(message);
            var bitMsg = string.Join("", msgDex.Select(i => Utills.ExtendToBlockSize(Convert.ToString(i, 2), 8)));
            var sbstMsg = SubstituteElements(bitMsg);
            var l = sbstMsg.Substring(0, sbstMsg.Length / 2);
            var r = sbstMsg.Substring(sbstMsg.Length / 2, sbstMsg.Length / 2);
            var extL = string.Join("", Utills.DivideIntoBlocks(l, 4).Split(' ').Select(i => ExtendBlockSize(i)));
            var extR = string.Join("", Utills.DivideIntoBlocks(r, 4).Split(' ').Select(i => ExtendBlockSize(i)));
            var keyDex = Utills.ConvertToDex(key);
            var bitKey = string.Join("", keyDex.Select(i => Convert.ToString(i, 2)));
            var sumKeyR = Utills.SumModulo2(bitKey, extR);

            Console.WriteLine($"message: {message}");
            Console.WriteLine($"msgDex:  {string.Join(" ", msgDex)}");
            Console.WriteLine($"bitMsg:  {Utills.DivideIntoBlocks(bitMsg, 4)}");
            Console.WriteLine($"sbstMsg: {Utills.DivideIntoBlocks(sbstMsg, 4)}");
            Console.WriteLine($"l:       {Utills.DivideIntoBlocks(l, 4)}");
            Console.WriteLine($"r:       {Utills.DivideIntoBlocks(r, 4)}");
            //Console.WriteLine($"extL:    {Utills.DivideIntoBlocks(extL, 6)}");
            Console.WriteLine($"extR:    {Utills.DivideIntoBlocks(extR, 6)}");
            //Console.WriteLine($"key:     {key}");
            //Console.WriteLine($"keyDex:  {string.Join(" ", keyDex)}");
            Console.WriteLine($"bitKey:  {Utills.DivideIntoBlocks(bitKey, 6)}");
            Console.WriteLine($"sumKeyR: {Utills.DivideIntoBlocks(sumKeyR, 6)}");

            for(int i = 192; i < 224; i++)
            {
                Console.WriteLine($"{i}, {Convert.ToString(i, 2)}");
            }


            //

            /*var r_div = Utills.DivideIntoBlocks(r, 4);
            var r_ext = string.Join("", r_div.Split(' ').Select(i => Utills.ExtendToBlockSize(i, 6)));
            Console.WriteLine("r_div:   " + r_div);
            Console.WriteLine("r_ext:   " + Utills.DivideIntoBlocks(r_ext, 6));

            var key_div = key;
            Console.WriteLine("key_div: " + Utills.DivideIntoBlocks(key_div, 6));

            var modulo = Utills.SumModulo2(r_ext, key_div);
            Console.WriteLine("modulo:  " + Utills.DivideIntoBlocks(modulo, 6));*/
        }

        private string ExtendBlockSize(string bitStr)
        {
            if (bitStr.Length == 6) return bitStr;
            StringBuilder extendedBitStr = new StringBuilder(6);
            //extendedBitStr.Insert(0, bitStr);
            //for (int i = bitStr.Length; i < 6; i++) extendedBitStr.Insert(0, '0');
            extendedBitStr.Insert(0, '0').Insert(0, bitStr).Insert(0, '0');

            return extendedBitStr.ToString();
        }

        private string SubstituteElements(string str)
        {
            char[] arr = new char[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                int newPos = substituteVector[i] - 1;
                arr[i] = str[newPos];
            }

            return string.Join("", arr);
        }

    }
}
