
using Payment.Facade;

namespace Payment.Factory.Interface
{
    public interface IPaymentFacade
    {
        Task<ProcessPaymentOutputDto> Process(ProcessPaymentInputDto input);
    }
}
