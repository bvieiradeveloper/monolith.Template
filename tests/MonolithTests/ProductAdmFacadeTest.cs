using InfraStructure.Context;
using Product.Adm.Repository.ProductRepository.Implementation;
using Product.Adm.UseCase.AddProduct;
using Product.Adm.Facade.Implementation;
using _Shared.Domain.ValueObject;
using Product.Adm.Factory;

namespace MonolithTests
{
    public class ProductAdmFacadeTest
    {
        private SharedContext _db;
        public ProductAdmFacadeTest()
        {
            _db = InMemoryDb.InitDb();
        }

        [Fact]
        public async Task ShouldCreateAProduct()
        {
            var productRepository = new ProductRepository(_db);
            ProductAdmFacade productFactory =  ProductAdmFacadeFactory.Create(_db);

            AddProductInputDTO input = new AddProductInputDTO
            {
                id = new Id(""),
                Name = "Product 1",
                Description = "Product 1 description",
                PurchasePrice = 100,
                Stock = 10
            };

            await productFactory.AddProduct(input);
            var response = await productRepository.Find(input.id.GetId());

            Assert.NotNull(input.id.GetId());
            Assert.Equal(response.Id, input.id.GetId());
            Assert.Equal(response.Name, "Product 1");
            Assert.Equal(response.Description, "Product 1 description");
            Assert.Equal(response.PurchasePrice, 100);
            Assert.Equal(response.Stock, 10);
        }
    }
}
