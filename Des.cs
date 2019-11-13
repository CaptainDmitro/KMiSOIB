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

        //Начальная перестановка
        private readonly int[] permuteTable1 =
        {
            58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8,
            57, 49, 41, 33, 25, 17,  9, 1, 59, 51, 43, 35, 27, 19, 11, 3,
            61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7
        };

        //Подстановочная таблица для r + key
        private readonly int[][,] sBlockTable =
            {
                //S 1
                new int[,]
                {
                    { 14,  4, 13, 1,  2, 15, 11,  8,  3, 10,  6, 12,  5,  9, 0,  7 },
                    {  0, 15,  7, 4, 14,  2, 13,  1, 10,  6, 12, 11,  9,  5, 3,  8 },
                    {  4,  1, 14, 8, 13,  6,  2, 11, 15, 12,  9,  7,  3, 10, 5,  0 },
                    { 15, 12,  8, 2,  4,  9,  1,  7,  5, 11,  3, 14, 10,  0, 6, 13 }
                },

                //S 2
                new int[,]
                {
                    { 15,  1,  8, 14,  6, 11,  3,  4,  9, 7,  2, 13, 12, 0,  5, 10 },
                    {  3, 13,  4,  7, 15,  2,  8, 14, 12, 0,  1, 10,  6, 9, 11,  5 },
                    {  0, 14,  7, 11, 10,  4, 13,  1,  5, 8, 12,  6,  9, 3,  2, 15 },
                    { 13,  8, 10,  1,  3, 15,  4,  2, 11, 6,  7, 12,  0, 5, 14,  9 },
                },

                //S3
                new int[,]
                {
                    { 10,  0,  9, 14, 6,  3, 15,  5,  1, 13, 12,  7, 11,  4,  2,  8 },
                    { 13,  7,  0,  9, 3,  4,  6, 10,  2,  8,  5, 14, 12, 11, 15,  1 },
                    { 13,  6,  4,  9, 8, 15,  3,  0, 11,  1,  2, 12,  5, 10, 14,  7 },
                    {  1, 10, 13,  0, 6,  9,  8,  7,  4, 15, 14,  3, 11,  5,  2, 12 }
                },

                //S4
                new int[,]
                {
                    {  7, 13, 14, 3,  0,  6,  9, 10,  1, 2, 8,  5, 11, 12,  4, 15 },
                    { 13,  8, 11, 5,  6, 15,  0,  3,  4, 7, 2, 12,  1, 10, 14,  9 },
                    { 10,  6,  9, 0, 12, 11,  7, 13, 15, 1, 3, 14,  5,  2,  8,  4 },
                    {  3, 15,  0, 6, 10,  1, 13,  8,  9, 4, 5, 11, 12,  7,  2, 14 }
                },

                //S5
                new int[,]
                {
                    {  2, 12,  4,  1,  7, 10, 11,  6,  8,  5,  3, 15, 13, 0, 14,  9 },
                    { 14, 11,  2, 12,  4,  7, 13,  1,  5,  0, 15, 10,  3, 9,  8,  6 },
                    {  4,  2,  1, 11, 10, 13,  7,  8, 15,  9, 12,  5,  6, 3,  0, 14 },
                    { 11,  8, 12,  7,  1, 14,  2, 13,  6, 15,  0,  9, 10, 4,  5,  3 }
                },

                //S6
                new int[,]
                {
                    { 12,  1, 10, 15, 9,  2,  6,  8,  0, 13,  3,  4, 14,  7,  5, 11 },
                    { 10, 15,  4,  2, 7, 12,  9,  5,  6,  1, 13, 14,  0, 11,  3,  8 },
                    {  9, 14, 15,  5, 2,  8, 12,  3,  7,  0,  4, 10,  1, 13, 11,  6 },
                    {  4,  3,  2, 12, 9,  5, 15, 10, 11, 14,  1,  7,  6,  0,  8, 13 }
                },

                //S7
                new int[,]
                {
                    {  4, 11,  2, 14, 15, 0,  8, 13,  3, 12, 9,  7,  5, 10, 6,  1 },
                    { 13,  0, 11,  7,  4, 9,  1, 10, 14,  3, 5, 12,  2, 15, 8,  6 },
                    {  1,  4, 11, 13, 12, 3,  7, 14, 10, 15, 6,  8,  0,  5, 9,  2 },
                    {  6, 11, 13,  8,  1, 4, 10,  7,  9,  5, 0, 15, 14,  2, 3, 12 }
                },

                //S8
                new int[,]
                {
                    { 13,  2,  8, 4,  6, 15, 11,  1, 10,  9,  3, 14,  5,  0, 12,  7 },
                    {  1, 15, 13, 8, 10,  3,  7,  4, 12,  5,  6, 11,  0, 14,  9,  2 },
                    {  7, 11,  4, 1,  9, 12, 14,  2,  0,  6, 10, 13, 15,  3,  5,  8 },
                    {  2,  1, 14, 7,  4, 10,  8, 13, 15, 12,  9,  0,  3,  5,  6, 11 }
                }
            };

        //Конечная перестановка
        private readonly int[] permuteTable2 =
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

        private void Init()
        {
            msgBinary = Utills.StickedBinaryMsg(message);
            sbstMsg = PermuteElements(msgBinary, permuteTable1);
            l = sbstMsg.Substring(0, sbstMsg.Length / 2);
            r = sbstMsg.Substring(sbstMsg.Length / 2, sbstMsg.Length / 2);
            extendedBlockR = ExtendBlockSize(r);
            keyBinary = dropBits(Utills.StickedBinaryMsg(key));
            sumKeyAndExtR = Utills.Modulo2(extendedBlockR, keyBinary, 48);
            anotherSubstitute = SBlockSubstitution(sumKeyAndExtR, sBlockTable);
            concatRAndL = string.Concat(anotherSubstitute + l);
            substSum = PermuteElements(concatRAndL, permuteTable2);
        }

        //Расширение 4-битных блоков r до 6 бит
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

        //Удаление из 56-битного исходного ключа 8, 16, 24, 32, 40, 48, 55, 56 биты; получаем 48-битный ключ
        private string dropBits(string key)
        {
            string formattedKey = key.PadLeft(56, '0');
            StringBuilder resKey = new StringBuilder(48);

            for (int i = 0; i < key.Length; i++)
            {
                if ( !((i + 1) % 8 == 0 || i == 54) )
                {
                    resKey.Append(formattedKey[i]);
                }
            }

            return resKey.ToString();
        }

        //Функция перестановки
        private string PermuteElements(string str, int[] substituteVector)
        {
            char[] arr = new char[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                int newPos = substituteVector[i] - 1;
                arr[i] = str[newPos];
            }

            return string.Join("", arr);
        }

        //Функция подстановки для r ^ key
        private string SBlockSubstitution(string str, int[][,] sBlockTable)
        {
            string[] binaryString = Utills.BinaryFormat(str, 6).Split(' ');
            StringBuilder result = new StringBuilder();
            int col, row;

            for (int i = 0; i < binaryString.Length; i++)
            {
                col = Convert.ToInt32(binaryString[i].Substring(1, 4), 2);
                row = Convert.ToInt32(string.Concat(binaryString[i].ElementAt(0), binaryString[i].ElementAt(5)), 2);
                result.Append(Convert.ToString(sBlockTable[i][row, col], 2).PadLeft(4, '0'));
            }

            return result.ToString();
        }

    }
}
