using Product.Adm.Facade.Implementation;


namespace Product.Adm.Facade.Interface
{
    public interface IProductAdmFacade
    {
        Task<AddProductOutputDto> AddProduct(AddProductInputDto input);
        Task<CheckStockOutputDto> CheckoutStock(CheckStockInputDto input);
    }
}
