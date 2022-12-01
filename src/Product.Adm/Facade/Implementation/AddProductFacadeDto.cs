using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Adm.Facade.Implementation
{
    public  class AddProductOutputDto
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long PurchasePrice { get; set; }
        public long Stock { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class AddProductInputDto
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long PurchasePrice { get; set; }
        public long Stock { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
