using Store.Catalog.Facade.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Catalog.Facade.Implementation;
using Store.Catalog.UseCase.FindAllPrductsUseCase;
using Store.Catalog.UseCase.FindProductUseCase;

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
            var response = await _findProductUseCase.Execute(new() { Id = input.Id });
            return new()
            {
                Id = response.Id,
                Name = response.Name,
                Description = response.Description,
                SalesPrice = response.SalesPrice,
            } ;
        }

        public async Task<FindAllProductsOutputDto> FindAll()
        {
            var response = await _findAllProductsUseCase.Execute();
            return new() {
                Products = response.Products.Select(r => new FindAllProductsDto { Id = r.Id, Name = r.Name, Description = r.Description, SalePrice = r.SalePrice }).ToList(),
            };
        }
    }
}
