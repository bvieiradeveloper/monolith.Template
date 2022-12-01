using InfraStructure.Context;
using Product.Adm.Repository.ProductRepository.Implementation;
using Product.Adm.Facade.Implementation;
using _Shared.Domain.ValueObject;
using Product.Adm.Factory;
using InfraStructure.Model;
using Product.Adm.UseCase.CheckStock;
using InfraStructure.Model.ProductAdm;
using CheckStockInputDto = Product.Adm.Facade.Implementation.CheckStockInputDto;

namespace MonolithTests
{
    public class ProductAdmFacadeTest
    {
        private SharedContext _db;
        private ProductModel _productModel;
        public ProductAdmFacadeTest()
        {
            _db = InMemoryDb.InitDb();
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
        public async Task ShouldCreateAProduct()
        {
            var productRepository = new ProductRepository(_db);
            ProductAdmFacade productFactory =  ProductAdmFacadeFactory.Create(_db);

            AddProductInputDto input = new AddProductInputDto
            {
                id = "",
                Name = "Product 1",
                Description = "Product 1 description",
                PurchasePrice = 100,
                Stock = 10
            };

            await productFactory.AddProduct(input);
            var response = await productRepository.Find(input.id);

            Assert.NotNull(input.id);
            Assert.Equal(response.Id, input.id);
            Assert.Equal(response.Name, "Product 1");
            Assert.Equal(response.Description, "Product 1 description");
            Assert.Equal(response.PurchasePrice, 100);
            Assert.Equal(response.Stock, 10);
        }

        [Fact]
        public async Task ShouldFindAProduct()
        {
            ProductAdmFacade productFactory = ProductAdmFacadeFactory.Create(_db);

            AddProductInputDto inputCreate = new AddProductInputDto
            {
                id = "1",
                Name = "Product 1",
                Description = "Product 1 description",
                PurchasePrice = 100,
                Stock = 10
            };

            await productFactory.AddProduct(inputCreate);

            var inputFind = new CheckStockInputDto { ProductId = "1" };

            var output = await productFactory.CheckoutStock(inputFind);

            Assert.Equal(output.ProductId, "1");
            Assert.Equal(output.Stock, 10);
        }
    }
}
