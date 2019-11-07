using System;
using System.Security.Cryptography;

namespace ServiceLayer.Utilities
{
    public static class Crypto
    {

        public static byte[] DeriveKey(string password, byte[] salt, int hashSize, int iterations = 5000)
        {

            using (Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return rfc2898.GetBytes(hashSize);
            }
        }

        /// <summary>
        /// Generates cryptographically secure sequence of random bytes
        /// </summary>
        /// <param name="size"></param>
        /// <returns>Random bytes</returns>
        public static byte[] GenerateRandomBytes(int size)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[size];
                rng.GetBytes(randomBytes);
                return randomBytes;
            }
        }
    }
}
