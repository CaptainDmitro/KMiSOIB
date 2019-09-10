﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KMiSOIB
{
    class RSA
    {
        string message, encryptedMessage, decryptedMessage;
        public int p { get; }
        public int q { get; }
        public int d { get; }
        public int k { get; }
        public int n { get; }
        public int phi { get; }
        public int e { get; }

        public RSA(string message, int p, int q, int d, int k)
        {
            this.message = message;
            this.p = p;
            this.q = q;
            this.d = d;
            this.k = k;

            n = p * q;
            phi = (p - 1) * (q - 1);
            e = (phi * k + 1) / d;
        }

        //public string GetPublicKey() { return "(" + e + ", " + n + ")"; }
        public string GetPublicKey() { return e + " " + n ; }
        //public string GetPrivateKey() { return "(" + d + ", " + n + ")"; }
        public string GetPrivateKey() { return d + " " + n; }

        public string Encrypt()
        {
            foreach(var ch in message)
            {
                if (ch == ' ') continue;
                //int charIndex = Utills.ConvertCharToDex(ch);
                int charIndex = Alphabet.GetCharIndex33(ch);
                var res = BigInteger.ModPow(charIndex, e, n);
                encryptedMessage += res + " ";
            }

            return encryptedMessage;
        }

        public string Decrypt()
        {
            foreach (var ch in encryptedMessage.Trim().Split(' '))
            {
                var res = BigInteger.ModPow(int.Parse(ch), d, n);
                //decryptedMessage += Utills.ConverToChar((int)res);
                decryptedMessage += Alphabet.GetChar33((int)res);
            }

            return decryptedMessage;
        }

    }
}
