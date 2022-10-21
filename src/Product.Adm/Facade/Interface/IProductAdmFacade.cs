using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _Shared.Usecase.Props;
using Product.Adm.Facade.DTO;
using Product.Adm.UseCase.AddProduct;

namespace Product.Adm.Facade.Interface
{
    public interface IProductAdmFacade
    {
        Task AddProduct(AddProductInputDTO input);
        Task<CheckStockFacadeOutputDTO> CheckoutStock(CheckStockFacadeInputDTO input);
    }
}
