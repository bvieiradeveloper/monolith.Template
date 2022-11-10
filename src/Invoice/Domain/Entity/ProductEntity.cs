using _Shared.Domain.Entity;
using _Shared.Domain.ValueObject;
using Invoice.Domain.ValueObject;

namespace Invoice.Domain.Entity
{

    public class ProductEntity : BaseEntity
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public ProductEntity(ProductProps productProps) : base(productProps.Id, productProps.CreatedAt, productProps.UpdatedAt)
        {
            Name = productProps.Name;
            Price = productProps.Price;
        }
    }
}
