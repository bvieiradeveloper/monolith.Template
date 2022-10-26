using InfraStructure.Context;
using Store.Catalog.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Catalog.UseCase
{
    public class FindProductUseCase
    {
        private IProductRepository _productRepository;
        public FindProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<FindProductOutputDto> Execute(FindProductInputDto findProductInputDto)
        {
            var product = await _productRepository.Find(findProductInputDto.Id);

            return new FindProductOutputDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                SalesPrice = product.SalePrice
            };
        }
    }
}
