namespace Payment.UseCase.ProcessPayment
{
    public class ProcessPaymentInputDto
    {
        public string Order_ID { get; init; } = String.Empty;
        public decimal Amount { get; init; } = 0;
    }

    public class ProcessPaymentOutputDto
    {
        public string Transaction_Id { get; init; } = String.Empty;
        public string Order_Id { get; init; } = String.Empty;
        public decimal Amount { get; init; } = 0;
        public string Status { get; init; } = String.Empty;
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}
