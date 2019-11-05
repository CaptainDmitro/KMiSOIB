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
        public string msgBinary;
        public string sbstMsg;
        public string extendedBlockR;
        public string keyBinary;
        public string sumKeyAndExtR;
        public string anotherSubstitute;
        public string concatRAndL;
        public string substSum;

        private readonly int[] substituteVector1 =
        {
            58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8, 
            57, 49, 41, 33, 25, 17,  9, 1, 59, 51, 43, 35, 27, 19, 11, 3, 
            61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7
        };

        private readonly int[,] substituteVector2 =
        {
            { 14,  4, 13, 1,  2, 15, 11,  8,  3, 10,  6, 12,  5,  9, 0,  7 },
            {  0, 15,  7, 4, 14,  2, 13,  1, 10,  6, 12, 11,  9,  5, 3,  8 },
            {  4,  1, 14, 8, 13,  6,  2, 11, 15, 12,  9,  7,  3, 10, 5,  0 },
            { 15, 12,  8, 2,  4,  9,  1,  7,  5, 11,  3, 14, 10,  0, 6, 13 }
        };

        private readonly int[] substituteVector3 =
        {
            40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31,
            38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29,
            36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27,
            34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41,  9, 49, 17, 57, 25
        };

        public Des(string message, string key)
        {
            this.message = message;
            this.key = key;
            l = message.Substring(0, message.Length / 2);
            r = message.Substring(message.Length / 2, message.Length / 2);

            Init();
        }

        private void Init3()
        {
            
            sbstMsg = "1001101010001011101001000100111101101011101101011111001101101011";
            l = sbstMsg.Substring(0, sbstMsg.Length / 2);
            r = sbstMsg.Substring(sbstMsg.Length / 2, sbstMsg.Length / 2);
            var extended = ExtendBlockSize(r);
            keyBinary = "100101011001010101001100100111100101100100111111";
            var sum = Utills.Modulo2(extended, keyBinary, 48);
            var anotherSubs = AnotherOneSubstituteElements(sum, substituteVector2);
            var sumRL = anotherSubs + l;
            substSum = SubstituteElements(sumRL, substituteVector3);

            Console.WriteLine($"    sbstMsg: {Utills.BinaryFormat(sbstMsg, 4)}");
            Console.WriteLine($"          l: {Utills.BinaryFormat(l, 4)}");
            Console.WriteLine($"          r: {Utills.BinaryFormat(r, 4)}");
            Console.WriteLine($"   extended: {Utills.BinaryFormat(extended, 6)}");
            Console.WriteLine($"        key: {Utills.BinaryFormat(keyBinary, 6)}");
            Console.WriteLine($"        sum: {Utills.BinaryFormat(sum, 6)}");
            Console.WriteLine($"anotherSubs: {Utills.BinaryFormat(anotherSubs, 8)}");
            Console.WriteLine($"      sumRL: {Utills.BinaryFormat(sumRL, 8)}");
            Console.WriteLine($"   substSum: {Utills.BinaryFormat(substSum, 8)}");

        }

        private void Init()
        {
            msgBinary = Utills.StickedBinaryMsg(message);
            sbstMsg = SubstituteElements(msgBinary, substituteVector1);
            //sbstMsg = "1001101010001011101001000100111101101011101101011111001101101011";
            l = sbstMsg.Substring(0, sbstMsg.Length / 2);
            r = sbstMsg.Substring(sbstMsg.Length / 2, sbstMsg.Length / 2);
            extendedBlockR = ExtendBlockSize(r);
            keyBinary = Utills.StickedBinaryMsg(key);
            //keyBinary = "100101011001010101001100100111100101100100111111";
            Console.WriteLine(extendedBlockR.Length + " " + keyBinary.Length);
            sumKeyAndExtR = Utills.Modulo2(extendedBlockR, keyBinary, 48);
            anotherSubstitute = AnotherOneSubstituteElements(sumKeyAndExtR, substituteVector2);
            concatRAndL = anotherSubstitute + l;
            substSum = SubstituteElements(concatRAndL, substituteVector3);
        }

        private string ExtendBlockSize(string bitStr)
        {
            string[] formattedBitStr = Utills.BinaryFormat(bitStr, 4).Split(' ');
            StringBuilder extendedBitStr;
            StringBuilder result = new StringBuilder(48);

            for (int i = 0; i < formattedBitStr.Length; i++)
            {
                extendedBitStr = new StringBuilder(6);

                extendedBitStr.Append(formattedBitStr[(i - 1 + 8) % 8].ElementAt(3));
                extendedBitStr.Append(formattedBitStr[i]);
                extendedBitStr.Append(formattedBitStr[(i + 1 + 8) % 8].ElementAt(0));

                result.Append(extendedBitStr);
            }

            return result.ToString();
        }

        private string SubstituteElements(string str, int[] substituteVector)
        {
            char[] arr = new char[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                int newPos = substituteVector[i] - 1;
                arr[i] = str[newPos];
            }

            return string.Join("", arr);
        }

        private string AnotherOneSubstituteElements(string str, int[,] substituteVector)
        {
            string[] binaryString = Utills.BinaryFormat(str, 6).Split(' ');
            StringBuilder result = new StringBuilder();
            int col, row;

            for (int i = 0; i < binaryString.Length; i++)
            {
                col = Convert.ToInt32(binaryString[i].Substring(1, 4), 2);
                row = Convert.ToInt32(string.Concat(binaryString[i].ElementAt(0), binaryString[i].ElementAt(5)), 2);
                result.Append(Convert.ToString(substituteVector[row, col], 2).PadLeft(4, '0'));
            }

            return result.ToString();
        }

    }
}
