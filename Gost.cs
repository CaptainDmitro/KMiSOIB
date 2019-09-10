using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMiSOIB
{
    class Gost
    {
        private const int OFFSET = 191;
        private const int SPACE_CHAR_DEX = 32;

        private string message;
        private string key;
        private readonly int[,] substituteTable = new int[,]
        {
           // 0  1   2  3  4  5  6   7
            { 1, 13, 4, 6, 7, 5, 14, 4 },     // 0
            { 15, 11, 11, 12, 13, 8, 11, 10 },// 1
            { 13, 4, 10, 7, 10, 4, 4, 9 },     //2
            { 0, 1, 0, 1, 1, 13, 12, 2 },       //3
            { 5, 3, 7, 5, 0, 10, 6, 13 },       //4
            { 7, 15, 2, 15, 8, 3, 13, 8 },      //5
            { 10, 5, 1, 13, 9, 4, 15, 0 },      //6
            { 4, 9, 13, 8, 15, 2, 10, 14 },     //7
            { 9, 0, 3, 4, 14, 14, 2, 6 },       //8
            { 2, 10, 6, 10, 4, 15, 3, 11 },     //9
            { 3, 14, 8, 9, 6, 12, 8, 1 },       //10
            { 14, 7, 5, 14, 12, 7, 1, 12 },     //11
            { 6, 6, 9, 0, 11, 6, 0, 7 },        //12
            { 11, 8, 12, 3, 2, 0, 7, 15 },      //13
            { 8, 2, 15, 11, 5, 9, 5, 5 },       //14
            { 12, 12, 14, 2, 3, 11, 9, 3, },    //15
        };

        public string l0, r0, x0, fR0X0, filled, shifted, r1;

        public Gost(string message, string key)
        {
            this.message = message;
            this.key = key;
            Init();
        }

        private void Init()
        {
            var r0_str = message.Substring(message.Length / 2, message.Length / 2);
            //var r0_dex = ConvertToDex(r0_str);
            var r0_dex = Utills.ConvertToDex(r0_str);
            //r0 = string.Join("", r0_dex.Select(i => ExtendToBlockSize(Convert.ToString(i, 2), 8)));
            r0 = string.Join("", r0_dex.Select(i => Utills.ExtendToBlockSize(Convert.ToString(i, 2), 8)));

            var x0_str = key;
            //var x0_dex = ConvertToDex(x0_str);
            var x0_dex = Utills.ConvertToDex(x0_str);
            //x0 = string.Join("", x0_dex.Select(i => ExtendToBlockSize(Convert.ToString(i, 2), 8)));
            x0 = string.Join("", x0_dex.Select(i => Utills.ExtendToBlockSize(Convert.ToString(i, 2), 8)));

            //fR0X0 = SumModulo2Pow32(r0, x0);
            fR0X0 = Utills.SumModulo2Pow32(r0, x0);

            //filled = SubstituteElements(fR0X0);
            //filled = Utills.SubstituteElements(fR0X0, substituteTable);
            filled = SubstituteElements(fR0X0);

            //shifted = Shift(filled, -11);
            shifted = Utills.Shift(filled, -11);

            var l0_str = message.Substring(0, message.Length / 2);
            //var l0_dex = ConvertToDex(l0_str);
            var l0_dex = Utills.ConvertToDex(l0_str);
            //l0 = string.Join("", l0_dex.Select(i => ExtendToBlockSize(Convert.ToString(i, 2), 8)));
            l0 = string.Join("", l0_dex.Select(i => Utills.ExtendToBlockSize(Convert.ToString(i, 2), 8)));

            //r1 = SumModulo2(l0, shifted);
            r1 = Utills.SumModulo2(l0, shifted);
        }

        private string SubstituteElements(string str)
        {
            StringBuilder substitutedStr = new StringBuilder();
            string[] blocks = Utills.DivideIntoBlocks(str, 4).Split(' ');
            int col = 0;
            int row;

            foreach (var b in blocks)
            {
                row = Convert.ToInt32(b, 2);
                substitutedStr.Append(Utills.ExtendToBlockSize(Convert.ToString(substituteTable[row, col], 2), 4));
                col++;
            }

            return substitutedStr.ToString();
        }

        /*
        private List<int> ConvertToDex(string message)
        {
            var arr = new List<int>(message.Length);
            foreach (var ch in message)
            {
                if (ch == ' ')
                {
                    arr.Add(SPACE_CHAR_DEX);
                    continue;
                }

                arr.Add(Utills.ConvertCharToDex(ch) + OFFSET);
            }

            return arr;
        }

        private string Shift(string msg, int offset)
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

        private string SumModulo2Pow32(string x, string y)
        {
            StringBuilder sb = new StringBuilder(x.Length);
            int a, b, c, d;
            c = 0;

            for (int i = x.Length - 1; i > 0; i--)
            {
                if(x[i] == ' ')
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

        private string ExtendToBlockSize(string bitStr, int blockSize)
        {
            if (bitStr.Length == blockSize) return bitStr;
            StringBuilder extendedBitStr = new StringBuilder(blockSize);
            extendedBitStr.Insert(0, bitStr);
            for (int i = bitStr.Length; i < blockSize; i++) extendedBitStr.Insert(0, '0');
            return extendedBitStr.ToString();
        }

        public string DivideIntoBlocks(string bitStr, int blockSize)
        {
            StringBuilder dividedBitStr = new StringBuilder();
            for(int i = 0; i < bitStr.Length; i++)
            {
                if (i % blockSize == 0) dividedBitStr.Append(' ');
                dividedBitStr.Append(bitStr[i]);
            }

            return dividedBitStr.ToString().Trim();
        }

        private string SubstituteElements(string str)
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
        }

        private string SumModulo2(string x, string y)
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
        */
    }
}
