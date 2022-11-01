

using _Shared.Domain.Entity;
using _Shared.Domain.Interface;
using _Shared.Domain.ValueObject;

namespace Store.Catalog.Domain.Entity
{
    public class ProductEntity : BaseEntity, IAggregatorRoot
    {
        public ProductEntity(ProductProps productProps) : base(productProps._id, null, null)
        {
            Name = productProps.Name;
            Description = productProps.Description;
            SalesPrice = productProps.SalesPrice;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string SalesPrice { get; private set; }
        public string Stock { get; private set; }
    }

    public class ProductProps
    {
        public Id _id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string SalesPrice { get; init; }
    }
}
