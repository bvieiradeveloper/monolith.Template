using Payment.Domain.Entity;
using Payment.UseCase.ProcessPayment;

namespace Payment.Repository.Interface
{
    public interface IPaymentRepository
    {
        Task<TransactionEntity> Save(TransactionEntity input);
    }
}
