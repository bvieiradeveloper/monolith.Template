using _Shared.Domain.ValueObject;
using InfraStructure.Context;
using InfraStructure.Model;
using InfraStructure.Model.ProductAdm;
using Moq;
using Product.Adm.Domain.Entity;
using Product.Adm.Repository.ProductRepository.Implementation;
using Product.Adm.Repository.ProductRepository.Interface;
using Product.Adm.UseCase.AddProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithTests
{
    public class ProductRepositoryTest
    {
        private SharedContext _db;
        public ProductRepositoryTest()
        {
            _db = InMemoryDb.InitDb();
        }
        [Fact]
        public async Task ShouldAddAProduct()
        {
            var productRepository = new ProductRepository(_db);


            var product = new ProductModel()
            {
                Id = (new Id("")).GetId(),
                Description = "Product 1 Description",
                Name = "Product 1",
                PurchasePrice = 100,
                Stock = 200,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            
            await productRepository.Add(product);
            var response = await productRepository.Find(product.Id);

            Assert.Equal (response.Id, product.Id);
            Assert.Equal(response.Name, product.Name);
            Assert.Equal(response.Description, product.Description);
            Assert.Equal(response.PurchasePrice, product.PurchasePrice);
            Assert.Equal(response.Stock, product.Stock);
        }
        [Fact]
        public async Task ShouldFindAProduct()
        {
            var productRepository = new ProductRepository(_db);
            var product = new ProductModel()
            {
                Id = "1",
                Description = "Product 1 Description",
                Name = "Product 1",
                PurchasePrice = 100,
                Stock = 200,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            await productRepository.Add(product);
            var response = await productRepository.Find("1");

            Assert.Equal(response.Id, "1");
            Assert.Equal(response.Name, "Product 1");
            Assert.Equal(response.Description, "Product 1 Description");
            Assert.Equal(response.PurchasePrice, 100);
            Assert.Equal(response.Stock, 200);
        }
    }
}
