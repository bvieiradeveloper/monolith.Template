namespace Product.Adm.API.Dto.Input.Checkout
{
    public class PlaceOrderInputDto
    {
        public string ClientId { get; init; }
        public IList<PlaceOrderProductInputDto> Products { get; init; }
    }

    public class PlaceOrderOutputDto
    {
        public string Id { get; set; }
        public string InvoiceId { get; set; }
        public string Status { get; set; }
        public string Total { get; set; }
    }

    public class PlaceOrderProductInputDto
    {
        public string ProductId { get; init; }
    }
}
