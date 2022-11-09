using _Shared.Domain.ValueObject;

namespace Payment.Domain.Entity
{
    public partial class TransactionEntity
    {
        public class TransactionProps
        {
            public Id Id { get; init; }
            public decimal Amount {get; init; }
            public string Order_Id { get; init; }
            public string Status { get; init; } = "pending";
            public DateTime CreatedAt { get; init; }
            public DateTime UpdatedAt { get; init; }
        }
    }
}
