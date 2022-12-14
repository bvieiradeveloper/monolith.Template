namespace Product.Adm.UseCase.CheckStock
{
    public class CheckStockInputDto
    {
        public string ProductId { get; init; }
    }

    public class CheckStockOutputDto
    {
        public string ProductId { get; init; }
        public long Stock { get; init; }
    }
}
