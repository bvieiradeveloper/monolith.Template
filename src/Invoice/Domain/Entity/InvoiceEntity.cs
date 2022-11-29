using _Shared.Domain.Entity;
using _Shared.Domain.Interface;
using Invoice.Domain.ValueObject;

namespace Invoice.Domain.Entity
{
    public class InvoiceEntity : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Document { get; private set; }
        public Address Address { get; private set; }
        public List<ProductEntity> Items { get; private set; }
        public InvoiceEntity(InvoiceProps invoiceProps) : base(invoiceProps.Id, invoiceProps.CreatedAt, invoiceProps.UpdatedAt)
        {
            Name = invoiceProps.Name;
            Document = invoiceProps.Document;
            Address = invoiceProps.Address;
            Items = invoiceProps.Items;
        }

        public decimal Total()
        {
            return Items.Sum(i => i.Price);
        }
    }
}
