using InfraStructure.Context;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product.Adm.API.Controllers;
using Product.Adm.Facade.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithTests.Controller.Product
{
    public class ProductControllerTest
    {
        private readonly SharedContext _sharedContext;
        public ProductControllerTest()
        {
            _sharedContext = InMemoryDb.InitDb();
        }

        [Fact]
        public async Task ShouldCreateAProduct()
        {
            var productController = new ProductController(_sharedContext);
            var ProductA = await productController.Post(new()
            {
               id = "1",
               Name = "Product 1",
               Description = "Product 1 description",
               PurchasePrice = 10,
               Stock = 10
            });

            var ProductB = await productController.Post(new()
            {
                id = "2",
                Name = "Product 2",
                Description = "Product 2 description",
                PurchasePrice = 50,
                Stock = 4
            });

            var okResultA = Assert.IsType<OkObjectResult>(ProductA);
            var okResultB = Assert.IsType<OkObjectResult>(ProductB);

            var json = JsonConvert.SerializeObject(okResultA.Value);
            var _productA = JsonConvert.DeserializeObject<AddProductOutputDto>(json);

            json = JsonConvert.SerializeObject(okResultB.Value);
            var _productB = JsonConvert.DeserializeObject<AddProductOutputDto>(json);

            var responseA = await productController.Get(_productA.id);
            var responseB = await productController.Get(_productB.id);

            okResultA = Assert.IsType<OkObjectResult>(responseA);
            okResultB = Assert.IsType<OkObjectResult>(responseB);

            json = JsonConvert.SerializeObject(okResultA.Value);
            var valuesA = JsonConvert.DeserializeObject<CheckStockOutputDto>(json);

            json = JsonConvert.SerializeObject(okResultB.Value);
            var valuesB = JsonConvert.DeserializeObject<CheckStockOutputDto>(json);

      
            Assert.Equal(_productA.id, valuesA.ProductId);
            Assert.Equal(_productA.Stock, valuesA.Stock);

            Assert.Equal(_productB.id, valuesB.ProductId);
            Assert.Equal(_productB.Stock, valuesB.Stock);
        }
    }
}
