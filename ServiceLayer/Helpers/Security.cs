using System;
using System.Security.Cryptography;

namespace ServiceLayer.Helpers
{
    /// <summary>
    /// This class provides static methods that do the work related to security mechanisms, 
    /// such as generating random bytes or creating the hash.
    /// </summary>
    public static class Security
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
        /// <param name="size">Length of the result (in bytes)</param>
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

        /// <summary>
        /// Creates hash from password and salt
        /// </summary>
        /// <param name="password">User password</param>
        /// <param name="salt">Salt</param>
        /// <param name="hashSize">Output hash size in bytes</param>
        /// <returns>Password hash</returns>
        public static string CreateHash(string password, byte[] salt, int hashSize)
            => Convert.ToBase64String(DeriveKey(password, salt, hashSize));

    }
}
