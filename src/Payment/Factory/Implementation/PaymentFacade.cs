using Payment.Factory.Interface;
using Payment.UseCase.ProcessPayment;

namespace Payment.Factory.Implementation
{
    public class PaymentFacade : IPaymentFacade
    {
        readonly ProcessPaymentUseCase _processPaymentUseCase;
        public PaymentFacade(ProcessPaymentUseCase processPaymentUseCase)
        {
            _processPaymentUseCase = processPaymentUseCase;
        }
        public async Task<ProcessPaymentOutputDto> Process(ProcessPaymentInputDto input)
        {
            return await _processPaymentUseCase.Execute(input);
        }
    }
}
