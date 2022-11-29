using Payment.Facade;
using Payment.Factory.Interface;
using Payment.UseCase.ProcessPayment;
using ProcessPaymentInputDto = Payment.Facade.ProcessPaymentInputDto;
using ProcessPaymentOutputDto = Payment.Facade.ProcessPaymentOutputDto;

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
            var response = await _processPaymentUseCase.Execute(new()
            {
                Order_ID = input.Order_ID,
                Amount = input.Amount,
            });

            return new()
            {
                Amount = response.Amount,
                Order_Id = response.Order_Id,
                Status = response.Status,
                Transaction_Id = response.Transaction_Id,
                CreatedAt = response.CreatedAt,
                UpdatedAt = response.UpdatedAt  
            };
        }
    }
}
