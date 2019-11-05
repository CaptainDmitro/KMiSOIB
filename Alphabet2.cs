﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMiSOIB
{
    static class Alphabet2
    {
        private static int spaceNum = 32;
        public static int GetCharCode(char ch)
        {
            int chBase10;

            if (ch >= 'а' && ch <= 'я') chBase10 = ch % 1072 + 224;
            else if (ch >= 'А' && ch <= 'Я') chBase10 = ch % 1040 + 192;
            else if (ch == ' ') chBase10 = spaceNum; //Wrong fucking space number
            else throw new Exception($"Неподдерживаемый символ {ch}");

            return chBase10;
        }

        public static char GetChar(int num)
        {
            if (num >= 192 && num <= 223) return (char)(num + 1040 - 1);
            else if (num >= 224 && num <= 255) return (char)(num + 1072 - 1);
            else if (num == 32) return ' ';
            else throw new Exception($"Не существует символа с кодом {num}");
        }
    }
}