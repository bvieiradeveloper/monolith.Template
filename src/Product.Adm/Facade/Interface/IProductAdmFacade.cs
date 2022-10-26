using Product.Adm.UseCase.AddProduct;
using Product.Adm.UseCase.CheckStock;

namespace Product.Adm.Facade.Interface
{
    public interface IProductAdmFacade
    {
        Task AddProduct(AddProductInputDTO input);
        Task<CheckStockOutputDto> CheckoutStock(CheckStockInputDto input);
    }
}
