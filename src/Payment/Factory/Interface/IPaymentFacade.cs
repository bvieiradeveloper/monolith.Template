using Payment.UseCase.ProcessPayment;

namespace Payment.Factory.Interface
{
    public interface IPaymentFacade
    {
        Task<ProcessPaymentOutputDto> Process(ProcessPaymentInputDto input);
    }
}
