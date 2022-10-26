using Store.Catalog.Facade.Interface;
using Store.Catalog.UseCase;
using Store.Catalog.UseCase.FindAllPrductsUseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Catalog.Facade.Implementation
{
    public class ProductStoreCatalogFacade : IProductStoreCatalogFacade
    {
        private FindAllProductsUseCase _findAllProductsUseCase;
        private FindProductUseCase _findProductUseCase;

        public ProductStoreCatalogFacade(FindAllProductsUseCase findAllProductsUseCase, FindProductUseCase findProductUseCase)
        {
            _findAllProductsUseCase = findAllProductsUseCase;
            _findProductUseCase = findProductUseCase;
        }

        public async Task<FindProductOutputDto> Find(FindProductInputDto input)
        {
            return await _findProductUseCase.Execute(input);
        }

        public async Task<FindAllProductsOutputDto> FindAll()
        {
            return await _findAllProductsUseCase.Execute();
        }
    }
}
