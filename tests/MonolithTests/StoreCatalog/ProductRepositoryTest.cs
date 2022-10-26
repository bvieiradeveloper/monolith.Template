using InfraStructure.Context;
using InfraStructure.Model.StoreCatalog;
using Store.Catalog.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithTests.StoreCatalog
{
    public class ProductRepositoryTest
    {
        private SharedContext _db;
        private List<ProductModel> _productModel = new List<ProductModel>();
        public ProductRepositoryTest()
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
            var productRepository = new ProductRepository(_db);

            await _db.ProductsCatalog.AddRangeAsync(_productModel);
            await _db.SaveChangesAsync();
            var response = await productRepository.FindAll();

            
            Assert.Equal(response[0].Id, _productModel[0].Id);
            Assert.Equal(response[0].Name, _productModel[0].Name);
            Assert.Equal(response[0].Description, _productModel[0].Description);
            Assert.Equal(response[0].SalePrice, _productModel[0].SalePrice);

            Assert.Equal(response[1].Id, _productModel[1].Id);
            Assert.Equal(response[1].Name, _productModel[1].Name);
            Assert.Equal(response[1].Description, _productModel[1].Description);
            Assert.Equal(response[1].SalePrice, _productModel[1].SalePrice);

        }

        [Fact]
        public async Task ShouldFindAProduct()
        {
            var productRepository = new ProductRepository(_db);
            await _db.ProductsCatalog.AddAsync(_productModel[0]);
            await _db.SaveChangesAsync();
            var response = await productRepository.Find("1");


            Assert.Equal(response.Id, _productModel[0].Id);
            Assert.Equal(response.Name, _productModel[0].Name);
            Assert.Equal(response.Description, _productModel[0].Description);
            Assert.Equal(response.SalePrice, _productModel[0].SalePrice);
        }
    }
}
