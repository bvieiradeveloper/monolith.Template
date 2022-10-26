using _Shared.Domain.ValueObject;
using InfraStructure.Context;
using InfraStructure.Model;
using InfraStructure.Model.ProductAdm;
using Moq;
using Product.Adm.Domain.Entity;
using Product.Adm.Repository.ProductRepository.Interface;
using Product.Adm.UseCase.AddProduct;
using Product.Adm.UseCase.CheckStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithTests
{
    public class CheckStockUseCaseTest
    {
        private ProductModel _productModel;
        public CheckStockUseCaseTest()
        {
            _productModel = new ProductModel
            {

                Id = "1",
                Name = "Product 1",
                Description = "Product 1 description",
                PurchasePrice = 100,
                Stock = 10,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }


        [Fact]
        public async Task ShouldFindProductById()
        {
            var productRepository = new Mock<IProductRepository>();
            var checkStockUseCase = new CheckStockUseCase(productRepository.Object);

            productRepository
                    .Setup(x => x.Find(It.IsAny<string>()))
                    .Returns(Task.FromResult(_productModel));

            var input = new CheckStockInputDto { ProductId = "1" };

            var output = await checkStockUseCase.Execute(input);

            productRepository.Verify(t => t.Find("1"), Times.Once);

            Assert.Equal(output.ProductId, "1");
            Assert.Equal(output.Stock, 10);
        } 
    }
}
