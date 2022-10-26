using _Shared.Domain.ValueObject;
using InfraStructure.Context;
using Microsoft.EntityFrameworkCore;
using Moq;
using Product.Adm.Domain.Entity;
using Product.Adm.Repository.ProductRepository.Implementation;
using Product.Adm.Repository.ProductRepository.Interface;
using Product.Adm.UseCase.AddProduct;

namespace MonolithTests
{
    public class AddProductUseCaseTest
    {
        [Fact]
        public async Task ShouldAddAProduct()
        {

            //dynamic productRepository = _db.Database.IsInMemory() ? new ProductRepository(_db) :  new Mock<IProductRepository>() ;
            var productRepository = new Mock<IProductRepository>();
            var usecase = new AddProductUseCase(productRepository.Object);

            AddProductInputDTO input = new AddProductInputDTO
            {
                id = new Id(""),
                Name = "Product 1",
                Description = "Product 1 description",
                PurchasePrice = 100,
                Stock = 10
            };

            var response  = await usecase.Execute(input);

            Assert.NotNull(response.id);
            Assert.Equal(response.Name, input.Name);
            Assert.Equal(response.Description, input.Description);
            Assert.Equal(response.PurchasePrice, input.PurchasePrice);
            Assert.Equal(response.Stock, input.Stock);
        }
    }
}