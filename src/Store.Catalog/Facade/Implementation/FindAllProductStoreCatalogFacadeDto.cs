using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Catalog.Facade.Implementation
{
    public class FindAllProductsOutputDto
    {
        public IList<FindAllProductsDto> Products { get; set; }
    }

    public class FindAllProductsDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long SalePrice { get; set; }
    }
}
