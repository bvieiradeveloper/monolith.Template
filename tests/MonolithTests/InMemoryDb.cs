using InfraStructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithTests
{
    public static class InMemoryDb
    {
        static SharedContext db;
        static SharedContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<SharedContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;
            return new SharedContext(options);
        }

        public static SharedContext InitDb()
        {
            db = InMemoryDb.GetMemoryContext();
            db.Database.EnsureDeleted();
            return db;
        }
    }
}
