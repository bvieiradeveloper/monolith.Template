using _Shared.Usecase.Interface;
using _Shared.Usecase.Props;
using Product.Adm.Facade.DTO;
using Product.Adm.Facade.Interface;
using Product.Adm.UseCase.AddProduct;
using Product.Adm.UseCase.CheckStock;
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
            return await _checkStockUseCase.Execute(input);
        }
    }
}
