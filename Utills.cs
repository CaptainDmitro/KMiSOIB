using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMiSOIB
{
    static class Utills
    {
        const int DEFAULT_OFFSET = 191;

        static char SPACE_CHAR_DEX = ' ';
        static int leftBorder = 'А';
        static int rightBorder = 'Я';
        static int size = rightBorder - leftBorder;

        public static int ConvertCharToDex(char ch)
        {
            return ((int)ch % leftBorder % (size + 1)) + 1;
        }

        public static List<int> ConvertToDex(string message, int offset = DEFAULT_OFFSET)
        {
            var arr = new List<int>(message.Length);
            foreach (var ch in message)
            {
                if (ch == ' ')
                {
                    arr.Add(SPACE_CHAR_DEX);
                    continue;
                }

                arr.Add(Utills.ConvertCharToDex(ch) + offset);
            }

            return arr;
        }

        public static string Shift(string msg, int offset)
        {
            char[] shiftedMsg = new char[msg.Length];

            for (int i = 0; i < msg.Length; i++)
            {
                int newPos = (i + offset) % msg.Length;
                if (newPos < 0) newPos = msg.Length + newPos;
                shiftedMsg[newPos] = msg[i];
            }

            return new string(shiftedMsg);
        }

        public static string SumModulo2Pow32(string x, string y)
        {
            StringBuilder sb = new StringBuilder(x.Length);
            int a, b, c, d;
            c = 0;

            for (int i = x.Length - 1; i > 0; i--)
            {
                if (x[i] == ' ')
                {
                    sb.Insert(0, ' ');
                    continue;
                }
                a = int.Parse(x[i].ToString());
                b = int.Parse(y[i].ToString());
                d = (a + b + c) % 2;
                c = (a + b + c) / 2;
                sb.Insert(0, d);
            }
            sb.Insert(0, c);

            return sb.ToString();
        }

        public static string ExtendToBlockSize(string bitStr, int blockSize)
        {
            if (bitStr.Length == blockSize) return bitStr;
            StringBuilder extendedBitStr = new StringBuilder(blockSize);
            extendedBitStr.Insert(0, bitStr);
            for (int i = bitStr.Length; i < blockSize; i++) extendedBitStr.Insert(0, '0');
            return extendedBitStr.ToString();
        }

        public static string DivideIntoBlocks(string bitStr, int blockSize)
        {
            StringBuilder dividedBitStr = new StringBuilder();
            for (int i = 0; i < bitStr.Length; i++)
            {
                if (i % blockSize == 0) dividedBitStr.Append(' ');
                dividedBitStr.Append(bitStr[i]);
            }

            return dividedBitStr.ToString().Trim();
        }

        /*public static string SubstituteElements(string str, int[,] substituteTable)
        {
            StringBuilder substitutedStr = new StringBuilder();
            string[] blocks = DivideIntoBlocks(str, 4).Split(' ');
            int col = 0;
            int row;

            foreach (var b in blocks)
            {
                row = Convert.ToInt32(b, 2);
                substitutedStr.Append(ExtendToBlockSize(Convert.ToString(substituteTable[row, col], 2), 4));
                col++;
            }

            return substitutedStr.ToString();
        }*/

        /*public static string SubstituteElements(string str, int[] substituteTable)
        {
            char[] arr = new char[str.Length];

            for(int i = 0; i < str.Length; i++)
            {
                int newPos = substituteTable[i] - 1;
                arr[i] = str[newPos];
            }

            return string.Join("", arr);
        }*/

        public static string SumModulo2(string x, string y)
        {
            StringBuilder res = new StringBuilder(x.Length);
            int a, b;

            for (int i = x.Length - 1; i >= 0; i--)
            {
                if (x[i] == ' ')
                {
                    res.Insert(0, ' ');
                    continue;
                }
                a = int.Parse(x[i].ToString());
                b = int.Parse(y[i].ToString());
                res.Insert(0, a ^ b);
            }

            return res.ToString();
        }
    }
}
