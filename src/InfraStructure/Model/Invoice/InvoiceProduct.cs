using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Model.Invoice
{
    public class InvoiceProduct
    {
        public string InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }

        public string ProductId { get; set; }
        public Product? Product { get; set; }

    }
}
