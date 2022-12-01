using InfraStructure.Context;
using Store.Catalog.Facade.Implementation;
using Store.Catalog.Facade.Interface;
using Store.Catalog.Repository.Implementation;
using Store.Catalog.UseCase;
using Store.Catalog.UseCase.FindAllPrductsUseCase;
using Store.Catalog.UseCase.FindProductUseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Catalog.Factory
{
    public static class ProductStoreCatalogFacadeFactory
    {
        public static ProductStoreCatalogFacade Create(SharedContext _db)
        {
            var productRepository = new ProductRepository(_db);
            var findProductUseCase = new FindProductUseCase(productRepository);
            var findAllProductsUseCase = new FindAllProductsUseCase(productRepository);

            var productAdmFacade = new ProductStoreCatalogFacade(findAllProductsUseCase, findProductUseCase);

            return productAdmFacade;
        }
    }
}
