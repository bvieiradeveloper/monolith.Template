using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Catalog.Facade.Implementation
{
    public class FindProductInputDto
    {
        public string Id { get; set; }
    }

    public class FindProductOutputDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long SalesPrice { get; set; }
    }
}
