using _Shared.Usecase.Interface;
using _Shared.Usecase.Props;
using Product.Adm.Facade.DTO;
using Product.Adm.Facade.Interface;
using Product.Adm.UseCase.AddProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Adm.Facade.Implementation
{
    public class ProductAdmFacade : IProductAdmFacade
    {
        private AddProductUseCase _addProductUseCase;
        public ProductAdmFacade(AddProductUseCase  addUseCase)
        {
            _addProductUseCase = addUseCase;
        }

        public async Task AddProduct(AddProductInputDTO input)
        {
             await _addProductUseCase.Execute(input);
        }

        public Task<CheckStockFacadeOutputDTO> CheckoutStock(CheckStockFacadeInputDTO input)
        {
            throw new NotImplementedException();
        }
    }
}
