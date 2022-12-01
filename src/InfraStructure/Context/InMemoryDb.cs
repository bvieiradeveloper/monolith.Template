using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Context
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
            db = GetMemoryContext();
            db.Database.EnsureDeleted();
            return db;
        }
    }
}
