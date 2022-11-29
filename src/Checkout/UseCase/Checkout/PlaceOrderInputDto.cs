namespace Checkout.UseCase.Checkout
{
    public class PlaceOrderInputDto
    {
        public string ClientId { get; init; }
        public IList<PlaceOrderProductInputDto> Products { get; init; }
    }

    public class PlaceOrderOutputDto
    {
        public string Id { get; init; }
        public string InvoiceId { get; init; }
        public string Status { get; init; }
        public decimal Total { get; init; }
        public IList<string> Products { get; init; }
    }

    public class PlaceOrderProductInputDto
    {
        public string ProductId { get; init; }
    }
}
