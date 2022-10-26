using Product.Adm.Repository.ProductRepository.Interface;

namespace Product.Adm.UseCase.CheckStock
{
    public class CheckStockUseCase
    {
        private IProductRepository _productRepository;

        public CheckStockUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CheckStockOutputDto> Execute(CheckStockInputDto input)
        {
            var product = await _productRepository.Find(input.ProductId);

            return new CheckStockOutputDto
            {
                ProductId = product.Id,
                Stock = product.Stock
            };
        }
    }
}
