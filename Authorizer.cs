using System;
//using System.Security.Cryptography;
using System.Text;

namespace OlympFoodClient
{

    public static class Authorizer
    {
        private static byte[] ClientSHAKey { get; } = BitConverter.GetBytes(0x67452301EFCDAB89);
        //private static byte[] ServerSHAKey { get; } = BitConverter.GetBytes(0x98BADCFE10325476);
        private static ulong PublicKeyExponent { get; } = 4207;
        //private static ulong PrivateKeyExponent { get; } = 343;
        private static ulong KeyModule { get; } = 55973;

        //public static string GetHashFromStringValue(string value, byte[] shakey)
        //{
        //    byte[] valueBytes = Encoding.UTF8.GetBytes(value);
        //    HMACSHA1 hmac = new HMACSHA1(shakey);
        //    byte[] hashBytes = hmac.ComputeHash(valueBytes);
        //    string resultHash = Encoding.UTF8.GetString(hashBytes);
        //    return resultHash;
        //}

        public static string EncryptStringByRSA(string entryString)
        {
            byte[] encryptedBytes = new byte[40];
            byte[] entryBytes = Encoding.UTF8.GetBytes((entryString + "####################").Substring(0, 20));
            for (int i = 0; i < 20; i++)
            {
                ulong word = FastExp(entryBytes[i], PublicKeyExponent, KeyModule);
                byte byte1 = (byte)(word >> 8);
                byte byte2 = (byte)word;
                encryptedBytes[i * 2 + 0] = byte1;
                encryptedBytes[i * 2 + 1] = byte2;
            }
            //string encryptedString = Encoding.UTF8.GetString(encryptedBytes);
            string encryptedString = Encoding.UTF8.GetString(encryptedBytes, 0, 40);
            return encryptedString;
        }

        public static string DecryptStringByRSA(string entryString)
        {
            byte[] entryBytes = Encoding.UTF8.GetBytes(entryString);
            byte[] decryptedBytes = new byte[20];
            for (int i = 0; i < 20; i++)
            {
                ulong temp = 0;
                temp = temp | entryBytes[i * 2 + 0];
                temp = temp << 8;
                temp = temp | entryBytes[i * 2 + 1];
                //ulong rez = FastExp(temp, PrivateKeyExponent, KeyModule);
                ulong rez = FastExp(temp, PublicKeyExponent, KeyModule);
                decryptedBytes[i] = (byte)rez;
            }
            //string decryptedString = Encoding.UTF8.GetString(decryptedBytes);
            string decryptedString = Encoding.UTF8.GetString(decryptedBytes, 0, 20);
            return decryptedString;
        }

        private static ulong FastExp(ulong a, ulong z, ulong n)
        {
            ulong a1 = a;
            ulong z1 = z;
            ulong x = 1;
            while (z1 != 0)
            {
                while (z1 % 2 == 0)
                {
                    z1 = z1 / 2;
                    a1 = (a1 * a1) % n;
                }
                z1 = z1 - 1;
                x = ((x * a1) + n) % n;
            }
            return x;
        }

        public static string EncryptStringByBase64(string entryString)
        {
            byte[] entryBytes = Encoding.UTF8.GetBytes(entryString);
            string encryptedString = Convert.ToBase64String(entryBytes);
            encryptedString = encryptedString.Replace('/', '+');
            return encryptedString;
        }

        public static string DecryptStringByBase64(string entryString)
        {
            string decryptedString = entryString;
            decryptedString = decryptedString.Replace('+', '/');
            byte[] decryptedBytes = Convert.FromBase64String(decryptedString);
            decryptedString = Encoding.UTF8.GetString(decryptedBytes, 0, decryptedBytes.Length);
            return decryptedString;
        }

    }
}
