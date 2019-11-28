using DatabaseLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Common
{
    static class DbUtilities
    {
        /// <summary>
        /// Creates DBContext which refers to new empty database
        /// </summary>
        /// <returns>Created <see cref="OrhedgeContext"/></returns>
        public static OrhedgeContext CreateNewContext()
        {
            DbContextOptionsBuilder<OrhedgeContext> ctxOpts = new DbContextOptionsBuilder<OrhedgeContext>();
            ctxOpts.UseInMemoryDatabase(Guid.NewGuid().ToString());
            return new OrhedgeContext(ctxOpts.Options);
        }
    }
}
