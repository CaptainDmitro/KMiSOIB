using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMiSOIB
{
    class BitNum
    {
        private string strPr;
        private int blockSize;

        public BitNum(string strPr, int blockSize = 8)
        {
            this.strPr = strPr;
        }

        private string ExtendToBlockSize(string bitStr)
        {
            if (bitStr.Length == blockSize) return bitStr;
            StringBuilder extendedBitStr = new StringBuilder(blockSize);
            extendedBitStr.Insert(0, bitStr);
            for (int i = bitStr.Length; i < blockSize; i++) extendedBitStr.Insert(0, '0');
            return extendedBitStr.ToString();
        }
    }
}
