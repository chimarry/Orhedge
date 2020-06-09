using DatabaseLayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Helpers;
using System;

namespace UnitTests.Common
{
    static class Utilities
    {
        /// <summary>
        /// Creates DBContext which refers to new empty database
        /// </summary>
        /// <returns>Created <see cref="OrhedgeContext"/></returns>
        public static OrhedgeContext CreateNewContext()
        {
            DbContextOptionsBuilder<OrhedgeContext> ctxOpts = new DbContextOptionsBuilder<OrhedgeContext>();
            ctxOpts.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new OrhedgeContext(ctxOpts.Options);
            DataGenerator.Initialize(context);
            return context;
        }

        public static (string hash, string salt) CreateHashAndSalt(string password)
        {
            byte[] salt = Crypto.GenerateRandomBytes(Constants.PASSWORD_HASH_SIZE);
            string saltBase64 = Convert.ToBase64String(salt);
            return (Crypto.CreateHash(password, salt, Constants.PASSWORD_HASH_SIZE), saltBase64);
        }
    }
}
