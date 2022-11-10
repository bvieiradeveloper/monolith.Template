using Invoice.UseCase.Generate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.UseCase.Find
{
    public class FindInvoiceInputDto
    {
        public string Id { get; init; }
    }
    public class FindInvoiceOutputDto
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string Document { get; init; }
        public FindInvoiceAddressOutputDto Address { get; init; }
        public List<FindInvoiceProductOutputDto> Items { get; init; }
        public decimal? Total { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
    public class FindInvoiceAddressOutputDto
    {
        public string Street { get; init; }
        public string Number { get; init; }
        public string Complement { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string ZipCode { get; init; }
    }

    public class FindInvoiceProductOutputDto
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
    }

}
