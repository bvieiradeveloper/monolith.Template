using _Shared.Domain.ValueObject;

namespace Invoice.Domain.ValueObject
{
    public class ProductProps
    {
        public Id? Id { get; init; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
