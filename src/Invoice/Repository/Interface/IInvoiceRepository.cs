using Invoice.Domain.Entity;

namespace Invoice.Repository.Interface
{
    public interface IInvoiceRepository
    {
        Task<InvoiceEntity> Generate(InvoiceEntity invoiceEntity);
        Task<InvoiceEntity> Find(string id);
    }
}
