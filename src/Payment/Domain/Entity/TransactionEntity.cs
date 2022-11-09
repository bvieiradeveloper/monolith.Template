using _Shared.Domain.Entity;
using _Shared.Domain.Interface;

namespace Payment.Domain.Entity
{
    public partial class TransactionEntity : BaseEntity, IAggregatorRoot
    {

        public decimal Amount { get; private set; }
        public string Order_Id { get; private set; }
        public string Status { get; private set; }
        public TransactionEntity(TransactionProps transactionProps) : base(transactionProps.Id, transactionProps.CreatedAt, transactionProps.UpdatedAt)
        {
            Amount = transactionProps.Amount;
            Order_Id = transactionProps.Order_Id;
            Status = transactionProps.Status;
            Validate();
        }

        public void Validate()
        {
            if(Amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than 0");
            }
        }

        public void Approve()
        {
            Status = "approved";
        }

        public void Decline()
        {
            Status = "declined";
        }

        public void Process()
        {
            if (Amount >= 100)
            {
                Approve();
            }
            else
            {
                Decline();
            }
        }
    }
}
