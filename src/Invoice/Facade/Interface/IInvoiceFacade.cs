using Invoice.UseCase.Find;
using Invoice.UseCase.Generate;

namespace Invoice.Facade.Interface
{
    public interface IInvoiceFacade
    {
        Task<GenerateInvoiceOutputDto> Generate(GenerateInvoiceInputDto input);
        Task<FindInvoiceOutputDto> FindInvoice(FindInvoiceInputDto input);
    }
}
