using InfraStructure.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Context
{
    public class SharedContext: DbContext
    {
        public SharedContext()
        {

        }

        public SharedContext(DbContextOptions<SharedContext> options) : base(options) 
        {
        }

        public DbSet<ProductModel> Products { get; set; }

    }
}
