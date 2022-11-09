using InfraStructure.Context;
using InfraStructure.Model.Payment;
using Payment.Domain.Entity;
using Payment.Repository.Interface;

namespace Payment.Repository.Implementation
{
    public class PaymentRepository : IPaymentRepository
    {
        readonly @SharedContext _sharedContext;
        public PaymentRepository(SharedContext sharedContext)
        {
            _sharedContext = sharedContext;
        }
        public async Task<TransactionEntity> Save(TransactionEntity input)
        {
            await _sharedContext.Transactions.AddAsync(new Transaction
            {
                Id = input._id.GetId(),
                Order_Id = input.Order_Id,
                Amount = input.Amount,
                Status = input.Status,
                CreatedAt = input.CreatedAt,
                UpdatedAt = input.UpdatedAt,
            });

            await _sharedContext.SaveChangesAsync();

            return input;
        }
    }
}
