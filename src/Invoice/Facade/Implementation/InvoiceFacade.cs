using Invoice.Facade.Interface;
using Invoice.UseCase.Find;
using Invoice.UseCase.Generate;

namespace Invoice.Facade.Implementation
{
    public class InvoiceFacade : IInvoiceFacade
    {
        readonly GenerateInvoiceUseCase _generateInvoiceUseCase;
        readonly FindInvoiceUseCase _findInvoiceUseCase;
        public InvoiceFacade(GenerateInvoiceUseCase generateInvoiceUseCase,
                             FindInvoiceUseCase findInvoiceUseCase)
        {
            _generateInvoiceUseCase = generateInvoiceUseCase;
            _findInvoiceUseCase = findInvoiceUseCase;
        }
        public async Task<FindInvoiceOutputDto> FindInvoice(FindInvoiceInputDto input)
        {
            return await _findInvoiceUseCase.Execute(input);
        }

        public async Task<GenerateInvoiceOutputDto> Generate(GenerateInvoiceInputDto input)
        {
            return await _generateInvoiceUseCase.Execute(input);
        }
    }
}
