using Checkout.Repository.Implementation;
using Checkout.UseCase.Checkout;
using Client.Adm.Facade.Implementation;
using InfraStructure.Context;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product.Adm.API.Controllers;
using Product.Adm.API.Dto.Input.Checkout;
using Product.Adm.Facade.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaceOrderOutputDto = Checkout.UseCase.Checkout.PlaceOrderOutputDto;
using PlaceOrderProductInputDto = Product.Adm.API.Dto.Input.Checkout.PlaceOrderProductInputDto;

namespace MonolithTests.Controller.Checkout
{
    public class CheckoutControllerTest
    {
        private readonly SharedContext _sharedContext;
        public CheckoutControllerTest()
        {
            _sharedContext = InMemoryDb.InitDb();
        }
        [Fact]
        public async Task ShouldCheckout()
        {
            var clientController = new ClientController(_sharedContext);

            var client = await clientController.Post(new()
            {
                Name = "Client 1",
                Email = "xx@gmail",
                Document = "0000",
                Street = "My Street",
                Number = "123",
                Complement = "aaaa",
                City = "New York",
                State = "Kingston",
                ZipCode = "12401",
            });

            var okResult = Assert.IsType<OkObjectResult>(client);

            var json = JsonConvert.SerializeObject(okResult.Value);
            var _client = JsonConvert.DeserializeObject<AddClientOutputDto>(json);

            var productController = new ProductController(_sharedContext);

            var ProductA = await productController.Post(new()
            {
                id = "1",
                Name = "Product 1",
                Description = "Product 1 description",
                PurchasePrice = 100,
                Stock = 10
            });

            var ProductB = await productController.Post(new()
            {
                id = "2",
                Name = "Product 2",
                Description = "Product 2 description",
                PurchasePrice = 200,
                Stock = 4
            });

            var okResultA = Assert.IsType<OkObjectResult>(ProductA);
            var okResultB = Assert.IsType<OkObjectResult>(ProductB);

            json = JsonConvert.SerializeObject(okResultA.Value);
            var _productA = JsonConvert.DeserializeObject<AddProductOutputDto>(json);

            json = JsonConvert.SerializeObject(okResultB.Value);
            var _productB = JsonConvert.DeserializeObject<AddProductOutputDto>(json);

            await _sharedContext.ProductsCatalog.AddAsync(new()
            {
                Id = "1",
                Description = "Product",
                Name = "Product 1",
                SalePrice = 100,
            });

            await _sharedContext.ProductsCatalog.AddAsync(new()
            {
                Id = "2",
                Description = "Product",
                Name = "Product 2",
                SalePrice = 200,
            });

            var checkoutController = new CkeckoutController(_sharedContext, new CheckoutRepository(_sharedContext));

            var response = await checkoutController.Post(new() { ClientId = _client.Id, Products = new List<PlaceOrderProductInputDto>()
                    {
                        new PlaceOrderProductInputDto
                        {
                            ProductId = "1",
                        },
                        new PlaceOrderProductInputDto
                        {
                            ProductId = "2"
                        }
                    }
                }
            );


            okResult = Assert.IsType<OkObjectResult>(response);

            json = JsonConvert.SerializeObject(okResult.Value);
            var _order = JsonConvert.DeserializeObject<PlaceOrderOutputDto>(json);

            Assert.Equal(300, _order.Total);
            Assert.Equal(new List<string>()
                {
                    { "1" },
                    { "2" },
                }, _order.Products.ToList());

        }
    }
}
