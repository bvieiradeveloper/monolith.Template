using Payment.Domain.Entity;
using Payment.Repository.Interface;
using System.Runtime.CompilerServices;
using static Payment.Domain.Entity.TransactionEntity;
[assembly: InternalsVisibleTo("MonolithTests")]
namespace Payment.UseCase.ProcessPayment
{
    public class ProcessPaymentUseCase
    {
        readonly IPaymentRepository _paymentRepository;
        internal ProcessPaymentUseCase(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        internal async Task<ProcessPaymentOutputDto> Execute(ProcessPaymentInputDto input)
        {
            var transactionProps = new TransactionProps
            {
                Amount = input.Amount,
                Order_Id = input.Order_ID,
            };

            var transaction = new TransactionEntity(transactionProps);

            transaction.Process();

            var response = await _paymentRepository.Save(transaction);

            return new ProcessPaymentOutputDto
            {
                Transaction_Id = response._id.GetId(),
                Order_Id = response.Order_Id,
                Amount = response.Amount,
                Status = response.Status,
                CreatedAt = response.CreatedAt,
                UpdatedAt = response.UpdatedAt,
            };
        }
    }
}
