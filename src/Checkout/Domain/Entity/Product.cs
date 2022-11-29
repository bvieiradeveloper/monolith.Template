using _Shared.Domain.Entity;
using _Shared.Domain.Interface;
using _Shared.Domain.ValueObject;

namespace Checkout.Domain.Entity
{
    public class ProductProps
    {
        public Id? Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal SalesPrice { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
    public class Product : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Description { get; private set; }
        public decimal SalesPrice { get; private set; }

        public Product()
        {

        }
        public Product(ProductProps props) : base(props.Id, props.CreatedAt, props.UpdatedAt)
        {
            Name = props.Name;
            Description = props.Description;
            SalesPrice = props.SalesPrice;
        }
    }
}
