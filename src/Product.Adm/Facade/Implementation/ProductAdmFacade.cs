using Product.Adm.Facade.Interface;
using Product.Adm.UseCase.AddProduct;
using Product.Adm.UseCase.CheckStock;
using checkoutUseCase = Product.Adm.UseCase.CheckStock.CheckStockInputDto;
namespace Product.Adm.Facade.Implementation
{
    public class ProductAdmFacade : IProductAdmFacade
    {
        private AddProductUseCase _addProductUseCase;
        private CheckStockUseCase _checkStockUseCase;
        public ProductAdmFacade(AddProductUseCase  addUseCase, CheckStockUseCase checkStockUseCase) 
        {
            _addProductUseCase = addUseCase;
            _checkStockUseCase = checkStockUseCase;
        }

        public async Task AddProduct(AddProductInputDTO input)
        {
             await _addProductUseCase.Execute(input);
        }

        public async Task<CheckStockOutputDto> CheckoutStock(CheckStockInputDto input)
        {
            var response =  await _checkStockUseCase.Execute(new checkoutUseCase
            {
                ProductId = input.ProductId,
            });

            return new()
            {
                ProductId = response.ProductId,
                Stock = response.Stock,
            };
        }
    }
}
