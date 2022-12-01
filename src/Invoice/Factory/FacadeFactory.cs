using InfraStructure.Context;
using Invoice.Facade.Implementation;
using Invoice.Repository.Implementation;
using Invoice.UseCase.Find;
using Invoice.UseCase.Generate;

namespace Invoice.Factory
{
    public static class InvoiceFacadeFactory
    {
        public static  InvoiceFacade Create(SharedContext _sharedContext)
        {
            InvoiceRepository invoiceRepository = new(_sharedContext);
            GenerateInvoiceUseCase generateUseCase = new(invoiceRepository);
            FindInvoiceUseCase findInvoiceUseCase = new(invoiceRepository);

            return new(generateUseCase, findInvoiceUseCase);
        }
    }
}
