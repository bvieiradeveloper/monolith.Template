using Product.Adm.Facade.Implementation;
using Product.Adm.UseCase.AddProduct;


namespace Product.Adm.Facade.Interface
{
    public interface IProductAdmFacade
    {
        Task AddProduct(AddProductInputDTO input);
        Task<CheckStockOutputDto> CheckoutStock(CheckStockInputDto input);
    }
}
