using _Shared.Domain.ValueObject;
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

        public async Task<AddProductOutputDto> AddProduct(AddProductInputDto input)
        {
            var response = await _addProductUseCase.Execute(new()
            {
                id = new Id(input.id),
                Name = input.Name,
                Description = input.Description,
                PurchasePrice = input.PurchasePrice,
                Stock = input.Stock,
            });

             return new()
             {
                 id = response.id.GetId(),
                 Name = response.Name,
                 Description= response.Description,
                 PurchasePrice= response.PurchasePrice,
                 Stock= response.Stock,
                 CreatedAt = DateTime.Now,
                 UpdatedAt = DateTime.Now,
             };
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
