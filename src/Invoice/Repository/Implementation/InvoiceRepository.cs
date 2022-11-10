using _Shared.Domain.ValueObject;
using InfraStructure.Context;
using Invoice.Domain.Entity;
using Invoice.Domain.ValueObject;
using Invoice.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository.Implementation
{
    public class InvoiceRepository : IInvoiceRepository
    {
        readonly SharedContext _sharedContext;
        public InvoiceRepository(SharedContext sharedContext)
        {
            _sharedContext = sharedContext;
        }
        public async Task<InvoiceEntity> Find(string id)
        {
            var response = await _sharedContext.Invoices.Where(v => v.Id == id).Include(b => b.Items).FirstAsync();

            var productProps = response.Items.Select(i => new ProductProps
            {
                Id = new Id(i.Product.Id),
                Name = i.Product.Name,
                Price = i.Product.Price,
            });

            var address = new AddressProps
            {
                Street = response.Street,
                City = response.City,
                Number = response.Number,
                State = response.State,
                ZipCode = response.ZipCode,
                Complement = response.Complement,
            };

            var invoiceProps = new InvoiceProps
            {
                Id = new Id(response.Id),
                Name = response.Name,
                Document = response.Document,
                Address = new Address(address),
                Items = productProps.Select(p => new ProductEntity(p)).ToList(),

            };

            return  new InvoiceEntity(invoiceProps);
        }

        public async Task<InvoiceEntity> Generate(InvoiceEntity invoiceEntity)
        {

            InfraStructure.Model.Invoice.Invoice invoiceProduct = new()
            {
                Id = invoiceEntity._id.GetId(),
                Name = invoiceEntity.Name,
                Document = invoiceEntity.Document,
                Street = invoiceEntity.Address.Street,
                City = invoiceEntity.Address.City,
                Number = invoiceEntity.Address.Number,
                State = invoiceEntity.Address.State,
                Complement = invoiceEntity.Address.Complement,
                ZipCode = invoiceEntity.Address.ZipCode,
                Items = invoiceEntity.Items.Select(item => new InfraStructure.Model.Invoice.InvoiceProduct
                {
                    InvoiceId = item._id.GetId(),
                    Product = new()
                    {
                        Id = item._id.GetId(),
                        Name = item.Name,
                        Price = item.Price,
                        CreatedAt = item.CreatedAt,
                        UpdatedAt = item.UpdatedAt,
                    }
                }).ToList()

            };
            await _sharedContext.AddAsync(invoiceProduct);
            await _sharedContext.SaveChangesAsync();

            return invoiceEntity;
        }
    }
}
