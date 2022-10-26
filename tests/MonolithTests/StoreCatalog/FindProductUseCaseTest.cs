using InfraStructure.Model.StoreCatalog;
using Moq;
using Store.Catalog.Repository.Interface;
using Store.Catalog.UseCase;

namespace MonolithTests.StoreCatalog
{
    public class FindProductUseCaseTest
    {
        private ProductModel _productModel;
        public FindProductUseCaseTest()
        {
            _productModel = new ProductModel
            {

                Id = "1",
                Name = "Product 1",
                Description = "Product 1 description",
                SalePrice = 100,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        [Fact]
        public async Task ShouldFindAProduct()
        {
            var productRepository = new Mock<IProductRepository>();

            productRepository.Setup(p => p.Find(It.IsAny<string>())).Returns(Task.FromResult(_productModel));

            var findUseCase = new FindProductUseCase(productRepository.Object);

            var response = await findUseCase.Execute(new FindProductInputDto { Id = "1" });

            productRepository.Verify(t => t.Find("1"), Times.Once);

            Assert.Equal(response.Id, _productModel.Id);
            Assert.Equal(response.Name, _productModel.Name);
            Assert.Equal(response.Description, _productModel.Description);
            Assert.Equal(response.SalesPrice, _productModel.SalePrice);
        }
    }
}
