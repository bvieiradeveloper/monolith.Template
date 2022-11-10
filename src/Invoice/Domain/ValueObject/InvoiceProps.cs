using _Shared.Domain.ValueObject;
using Invoice.Domain.Entity;

namespace Invoice.Domain.ValueObject
{
    public class InvoiceProps
    {
        public Id? Id { get; init; }
        public string Name { get; init; }
        public string Document { get; init; }
        public Address Address { get; init; }
        public List<ProductEntity> Items { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }

    }
}
