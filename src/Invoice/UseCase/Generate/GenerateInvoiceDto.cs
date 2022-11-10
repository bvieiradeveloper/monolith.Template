using Invoice.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.UseCase.Generate
{
    public class GenerateInvoiceInputDto
    {
        public string? Id { get; init; }
        public string Name { get; init; }
        public string Document { get; init; }
        public string Street { get; init; }
        public string Number { get; init; }
        public string Complement { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string ZipCode { get; init; }
        public List<GenerateInvoiceProductInputDto> Items { get; init; }
    }

    public class GenerateInvoiceProductInputDto 
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
    }

    public class GenerateInvoiceOutputDto
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string Document { get; init; }
        public string Street { get; init; }
        public string Number { get; init; }
        public string Complement { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string ZipCode { get; init; }
        public List<GenerateInvoiceProductInputDto> Items { get; init; }
        public decimal? Total { get; init; }
    }


}
