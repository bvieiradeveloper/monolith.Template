using System.Collections.Generic;

namespace Store.Catalog.UseCase.FindAllPrductsUseCase
{
    public class FindAllProductsOutputDto
    {
        public IList<FindAllProductsDto> Products { get; set; }
    }

    public class FindAllProductsDto 
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long SalePrice { get; set; }
    }
}
