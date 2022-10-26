using InfraStructure.Context;
using InfraStructure.Model.StoreCatalog;
using Moq;
using Store.Catalog.Repository.Interface;
using Store.Catalog.UseCase.FindAllPrductsUseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithTests.StoreCatalog
{
    public class FindAllProductsUseCaseTest
    {
        private SharedContext _db;
        private IList<ProductModel> _productModel = new List<ProductModel>();
        public FindAllProductsUseCaseTest()
        {
            _db = InMemoryDb.InitDb();

            _productModel.Add(new ProductModel
            {
                Id = "1",
                Name = "Product 1",
                Description = "Product 1 description",
                SalePrice = 100,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });

            _productModel.Add(new ProductModel
            {
                Id = "2",
                Name = "Product 2",
                Description = "Product 2 description",
                SalePrice = 200,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
        }

        [Fact]
        public async Task ShouldFindAllProducts()
        {
            var productRepository = new Mock<IProductRepository>();

            productRepository.Setup(x => x.FindAll())
                .Returns(Task.FromResult(_productModel));

            var findAllUseCase = new FindAllProductsUseCase(productRepository.Object);

            var response = await findAllUseCase.Execute();

            productRepository.Verify(t => t.FindAll(), Times.Once);

            Assert.Equal(response.Products[0].Id, _productModel[0].Id);
            Assert.Equal(response.Products[0].Name, _productModel[0].Name);
            Assert.Equal(response.Products[0].Description, _productModel[0].Description);
            Assert.Equal(response.Products[0].SalePrice, _productModel[0].SalePrice);

            Assert.Equal(response.Products[1].Id, _productModel[1].Id);
            Assert.Equal(response.Products[1].Name, _productModel[1].Name);
            Assert.Equal(response.Products[1].Description, _productModel[1].Description);
            Assert.Equal(response.Products[1].SalePrice, _productModel[1].SalePrice);
        }
    }
}
