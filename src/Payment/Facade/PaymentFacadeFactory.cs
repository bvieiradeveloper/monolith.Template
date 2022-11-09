using InfraStructure.Context;
using Payment.Factory.Implementation;
using Payment.Repository.Implementation;
using Payment.UseCase.ProcessPayment;

namespace Payment.Facade
{
    public class PaymentFacadeFactory
    {

        public static PaymentFacade Create(SharedContext sharedContext)
        {
            var paymentRepository = new PaymentRepository(sharedContext);
            var useCase = new ProcessPaymentUseCase(paymentRepository);

            return new PaymentFacade(useCase);
        }
    }
}
