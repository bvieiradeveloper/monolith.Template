using _Shared.Domain.ValueObject;

namespace Product.Adm.API.Dto.Input.Product
{
    public class ProductAdmInputDto
    {
        public string? id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long PurchasePrice { get; set; }
        public long Stock { get; set; }
    }
}
