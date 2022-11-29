using Store.Catalog.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Catalog.UseCase.FindAllPrductsUseCase
{
    public class FindAllProductsUseCase
    {
        private IProductRepository _productRepository;
        internal FindAllProductsUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        internal async Task<FindAllProductsOutputDto> Execute() 
        {
            var products = await _productRepository.FindAll();

            var productsDto = products.Select(p => new FindAllProductsDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                SalePrice = p.SalePrice,
            });

            return new FindAllProductsOutputDto { Products = new List<FindAllProductsDto>(productsDto) };
        }
    }
}
