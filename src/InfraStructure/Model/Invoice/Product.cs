using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Model.Invoice
{
    public class Product
    {
        public string Id { get; init; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<InvoiceProduct>? InvoiceProduct { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
