using _Shared.Domain.Entity;
using _Shared.Domain.Interface;

namespace Product.Adm.Domain.Entity
{
    public class ProductEntity : BaseEntity, IAggregatorRoot
    {
        public ProductEntity(AddProductInputDto productProps) : base(productProps.id, null, null)
        {
            Name = productProps.Name;
            Description = productProps.Description; 
            PurchasePrice = productProps.PurchasePrice; 
            Stock = productProps.Stock; 
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public long PurchasePrice { get; private set; }
        public long Stock { get; private set; }

        public void SetName(string name)
        {
            Name = name.Trim();
        }

        public void SetDescription(string description)
        {
            Description = description.Trim();
        }

        public void SetPurchasePrice(long purchasePrace)
        {
            PurchasePrice = purchasePrace;
        }

        public void SetStock(long stock)
        {
            Stock = stock;
        }
    }
}
