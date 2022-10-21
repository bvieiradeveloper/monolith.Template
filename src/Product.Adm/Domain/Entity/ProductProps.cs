using _Shared.Domain.ValueObject;

namespace Product.Adm.Domain.Entity
{
    public abstract class ProductProps
    {
        public Id id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long PurchasePrice { get; set; }
        public long Stock { get; set; }
    }
}
