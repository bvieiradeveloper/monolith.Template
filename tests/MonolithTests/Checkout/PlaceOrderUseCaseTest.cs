using _Shared.Domain.ValueObject;
using Checkout.Repository.Implementation;
using Checkout.Repository.Interface;
using Checkout.UseCase.Checkout;
using Client.Adm.Facade.Interface;
using Invoice.Facade.Interface;
using Moq;
using Payment.Factory.Interface;
using Product.Adm.Facade.Implementation;
using Product.Adm.Facade.Interface;
using Store.Catalog.Facade.Implementation;
using Store.Catalog.Facade.Interface;
using FindClientInputDto = Client.Adm.Facade.Implementation.FindClientInputDto;
using FindClientOutputDto = Client.Adm.Facade.Implementation.FindClientOutputDto;
using CheckoutProduct = Checkout.Domain.Entity.Product;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Client.Adm.Domain.Entity;
using Invoice.UseCase.Generate;
using Payment.Facade;
using Checkout.Domain.Entity;
using System.IO;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace MonolithTests.Checkout
{
    public class PlaceOrderUseCaseTest
    {
        private Mock<IClientAdmFacade>  clientFacade = new Mock<IClientAdmFacade>();
        private Mock<IProductAdmFacade> productFacade = new Mock<IProductAdmFacade>();
        private Mock<IProductStoreCatalogFacade> catalogFacade = new Mock<IProductStoreCatalogFacade>();
        private Mock<IPaymentFacade> paymentFacade = new Mock<IPaymentFacade>();
        private Mock<IInvoiceFacade> invoiceFacade = new Mock<IInvoiceFacade>();
        private Mock<ICheckoutRepository> checkoutRepository = new Mock<ICheckoutRepository>();


        #region Place a Order
        [Fact]
        public async Task ShouldNotBeApproved()
        {
            clientFacade.Setup(x => x.Find(It.IsAny<FindClientInputDto>())).Returns(Task.FromResult(new FindClientOutputDto()
            {
                Id = "1c",
                Name = "Client 0",
                Document = "0000",
                Email = "client@user.com",
                Street = "some address",
                Number = "1",
                Complement = "",
                City = "some city",
                State = "some state",
                ZipCode = "000",
            }));


            invoiceFacade.Setup(x => x.Generate(It.IsAny<GenerateInvoiceInputDto>())).Returns(Task.FromResult(new GenerateInvoiceOutputDto()
            {
                Id = "1i",
            }));

            productFacade.SetupSequence(x => x.CheckoutStock(It.IsAny<CheckStockInputDto>())).Returns(
             Task.FromResult(new CheckStockOutputDto { ProductId = "1", Stock = 1 })).Returns(Task.FromResult(new CheckStockOutputDto { ProductId = "2", Stock = 1 }));

            catalogFacade.SetupSequence(x => x.Find(It.IsAny<FindProductInputDto>())).Returns(() =>
            {
                return Task.FromResult(new FindProductOutputDto
                {
                    Id = "1",
                    Name = "Product 1",
                    Description = "Some Description",
                    SalesPrice = 40,
                });
            }).Returns(() =>
            {
                return Task.FromResult(new FindProductOutputDto
                {
                    Id = "2",
                    Name = "Product ",
                    Description = "Some Description",
                    SalesPrice = 30
                   
                });

            });

            paymentFacade.Setup(x => x.Process(It.IsAny<ProcessPaymentInputDto>())).Returns(Task.FromResult(new ProcessPaymentOutputDto
            {
                Transaction_Id = "1t",
                Order_Id = "1o",
                Amount = 100,
                Status = "error",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            }));

            var placeOrderUseCase = new PlaceOrderUseCase(clientFacade.Object,
                                              productFacade.Object,
                                              catalogFacade.Object,
                                              paymentFacade.Object,
                                              invoiceFacade.Object,
                                              checkoutRepository.Object);


            var p = new Mock<PlaceOrderUseCase>(clientFacade.Object,
                                    productFacade.Object,
                                    catalogFacade.Object,
                                    paymentFacade.Object,
                                    invoiceFacade.Object,
                                    checkoutRepository.Object);

            var response = await placeOrderUseCase.Execute(new()
            {
                ClientId = "1c",
                Products = new List<PlaceOrderProductInputDto>()
                {
                    new PlaceOrderProductInputDto() { ProductId = "1" },
                    new PlaceOrderProductInputDto() { ProductId = "2" },
                }
            });;

            Assert.Null(response.InvoiceId);
            Assert.Equal(70, response.Total);
            Assert.Equal(new List<string>()
                {
                    { "1" },
                    { "2" },
                }, response.Products.ToList());

            clientFacade.Verify(v => v.Find(It.IsAny<FindClientInputDto>()), Times.Once);
            clientFacade.Verify(v => v.Find(It.Is<FindClientInputDto>(v=> v.ClientId == "1c")), Times.Once);
            checkoutRepository.Verify(v => v.AddOrder(It.IsAny<Order>()), Times.Once);
            paymentFacade.Verify(v => v.Process(It.IsAny<ProcessPaymentInputDto>()), Times.Once);
            paymentFacade.Verify(v => v.Process(It.Is<ProcessPaymentInputDto>(p=> p.Order_ID == response.Id && p.Amount == response.Total)), Times.Once);
            invoiceFacade.Verify(v => v.Generate(It.IsAny<GenerateInvoiceInputDto>()), Times.Never);


        }

        [Fact]
        public async Task ShouldBeApproved()
        {
            clientFacade.Setup(x => x.Find(It.IsAny<FindClientInputDto>())).Returns(Task.FromResult(new FindClientOutputDto()
            {
                Id = "1c",
                Name = "Client 0",
                Document = "0000",
                Email = "client@user.com",
                Street = "some address",
                Number = "1",
                Complement = "",
                City = "some city",
                State = "some state",
                ZipCode = "000",
            }));


            invoiceFacade.Setup(x => x.Generate(It.IsAny<GenerateInvoiceInputDto>())).Returns(Task.FromResult(new GenerateInvoiceOutputDto()
            {
                Id = "1i",
            }));

            productFacade.SetupSequence(x => x.CheckoutStock(It.IsAny<CheckStockInputDto>())).Returns(
             Task.FromResult(new CheckStockOutputDto { ProductId = "1", Stock = 1 })).Returns(Task.FromResult(new CheckStockOutputDto { ProductId = "2", Stock = 1 }));

            catalogFacade.SetupSequence(x => x.Find(It.IsAny<FindProductInputDto>())).Returns(() =>
            {
                return Task.FromResult(new FindProductOutputDto
                {
                    Id = "1",
                    Name = "Product 1",
                    Description = "Some Description",
                    SalesPrice = 40,
                });
            }).Returns(() =>
            {
                return Task.FromResult(new FindProductOutputDto
                {
                    Id = "2",
                    Name = "Product 2",
                    Description = "Some Description",
                    SalesPrice = 30

                });

            });

            paymentFacade.Setup(x => x.Process(It.IsAny<ProcessPaymentInputDto>())).Returns(Task.FromResult(new ProcessPaymentOutputDto
            {
                Transaction_Id = "1t",
                Order_Id = "1o",
                Amount = 100,
                Status = "approved",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            }));

            var placeOrderUseCase = new PlaceOrderUseCase(clientFacade.Object,
                                              productFacade.Object,
                                              catalogFacade.Object,
                                              paymentFacade.Object,
                                              invoiceFacade.Object,
                                              checkoutRepository.Object);


            var p = new Mock<PlaceOrderUseCase>(clientFacade.Object,
                                    productFacade.Object,
                                    catalogFacade.Object,
                                    paymentFacade.Object,
                                    invoiceFacade.Object,
                                    checkoutRepository.Object);

            var response = await placeOrderUseCase.Execute(new()
            {
                ClientId = "1c",
                Products = new List<PlaceOrderProductInputDto>()
                {
                    new PlaceOrderProductInputDto() { ProductId = "1" },
                    new PlaceOrderProductInputDto() { ProductId = "2" },
                }
            }); ;

            Assert.Equal("1i", response.InvoiceId);
            Assert.Equal(70, response.Total);
            Assert.Equal(new List<string>()
                {
                    { "1" },
                    { "2" },
                }, response.Products.ToList());

            clientFacade.Verify(v => v.Find(It.IsAny<FindClientInputDto>()), Times.Once);
            clientFacade.Verify(v => v.Find(It.Is<FindClientInputDto>(v => v.ClientId == "1c")), Times.Once);
            checkoutRepository.Verify(v => v.AddOrder(It.IsAny<Order>()), Times.Once);
            paymentFacade.Verify(v => v.Process(It.IsAny<ProcessPaymentInputDto>()), Times.Once);
            paymentFacade.Verify(v => v.Process(It.Is<ProcessPaymentInputDto>(p => p.Order_ID == response.Id && p.Amount == response.Total)), Times.Once);
            invoiceFacade.Verify(v => v.Generate(It.IsAny<GenerateInvoiceInputDto>()), Times.Once);
            invoiceFacade.Verify(v => v.Generate(It.Is<GenerateInvoiceInputDto>(p =>

                p.Name == "Client 0" &&
                p.Document == "0000" &&
                p.Street == "some address" &&
                p.Number == "1" &&
                p.Complement == "" &&
                p.City == "some city" &&
                p.State == "some state" &&
                p.ZipCode == "000" &&
                p.Items[0].Id == "1" &&
                p.Items[0].Name == "Product 1" &&
                p.Items[0].Price == 40 &&
                p.Items[1].Id == "2" &&
                p.Items[1].Name == "Product 2" &&
                p.Items[1].Price == 30
            )), Times.Once) ;


        }
        #endregion

        #region ValidateProductMethod

        [Fact]
        public async Task ShouldThrowAnErrorIfNoProductsAreSelected()
        {

            var placeOrderUseCase = new PlaceOrderUseCase(clientFacade.Object,
                                                          productFacade.Object,
                                                          catalogFacade.Object,
                                                          paymentFacade.Object,
                                                          invoiceFacade.Object,
                                                          checkoutRepository.Object);

            var input = new PlaceOrderInputDto
            {
                ClientId = "0",
                Products = new List<PlaceOrderProductInputDto>(),
            };

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await placeOrderUseCase.ValidateProducts(input));

            Assert.Equal("No products selected.", ex.Message);
        }

        [Fact]
        public async Task ShouldThrowAErrorWhenProductIsOutOfStock()
        {
            clientFacade.Setup(x => x.Find(It.IsAny<FindClientInputDto>())).Returns(() => Task.FromResult(new FindClientOutputDto() { Id = "1" }));

            productFacade.Setup(x => x.CheckoutStock(It.IsAny<CheckStockInputDto>())).Returns((CheckStockInputDto e) =>
                 Task.FromResult(new CheckStockOutputDto { ProductId = e.ProductId, Stock = e.ProductId == "1" ? 0 : 1 }
            ));

            var placeOrderUseCase = new PlaceOrderUseCase(clientFacade.Object,
                                                          productFacade.Object,
                                                          catalogFacade.Object,
                                                          paymentFacade.Object,
                                                          invoiceFacade.Object,
                                                          checkoutRepository.Object);

            var input = new PlaceOrderInputDto
            {
                ClientId = "0",
                Products = new List<PlaceOrderProductInputDto>() { new() { ProductId = "1" } },
            };

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await placeOrderUseCase.Execute(input));

            Assert.Equal("Product 1 is not avaliable in stock.", ex.Message);

            input = new PlaceOrderInputDto
            {
                ClientId = "0",
                Products = new List<PlaceOrderProductInputDto>() { new() { ProductId = "0" }, new() { ProductId = "1" } },
            };


            ex = await Assert.ThrowsAsync<ArgumentException>(async () => await placeOrderUseCase.Execute(input));

            Assert.Equal("Product 1 is not avaliable in stock.", ex.Message);

            productFacade.Verify(x => x.CheckoutStock(It.IsAny<CheckStockInputDto>()), Times.Exactly(3));

            input = new PlaceOrderInputDto
            {
                ClientId = "0",
                Products = new List<PlaceOrderProductInputDto>() { new() { ProductId = "0" }, new() { ProductId = "1" }, new() { ProductId = "2" } },
            };

            ex = await Assert.ThrowsAsync<ArgumentException>(async () => await placeOrderUseCase.Execute(input));

            Assert.Equal("Product 1 is not avaliable in stock.", ex.Message);

            productFacade.Verify(x => x.CheckoutStock(It.IsAny<CheckStockInputDto>()), Times.Exactly(5));
        }
        #endregion

        #region ExecuteMethod
        [Fact]
        public async Task ShouldThrowAErrorWhenClientNotFounded()
        {

            clientFacade.Setup(x => x.Find(It.IsAny<FindClientInputDto>())).Returns(() => Task.FromResult(new FindClientOutputDto()));

            var placeOrderUseCase = new PlaceOrderUseCase(clientFacade.Object,
                                                          productFacade.Object,
                                                          catalogFacade.Object,
                                                          paymentFacade.Object,
                                                          invoiceFacade.Object,
                                                          checkoutRepository.Object);

            var input = new PlaceOrderInputDto {
                ClientId = "0",
                Products = new List<PlaceOrderProductInputDto>(),
            };

     
            var ex = await Assert.ThrowsAsync<NullReferenceException>(async () => await placeOrderUseCase.Execute(input));

            Assert.Equal("Client not found.", ex.Message);
        }

        [Fact]
        public async Task ShouldThrowAnErrorWhenProductsAreNotValid()
        {
            clientFacade.Setup(x => x.Find(It.IsAny<FindClientInputDto>())).Returns(() => Task.FromResult(new FindClientOutputDto() { Id = "1" }));

            var placeOrderUseCase = new PlaceOrderUseCase(clientFacade.Object,
                                                          productFacade.Object,
                                                          catalogFacade.Object,
                                                          paymentFacade.Object,
                                                          invoiceFacade.Object,
                                                          checkoutRepository.Object);

            var input = new PlaceOrderInputDto
            {
                ClientId = "0",
                Products = new List<PlaceOrderProductInputDto>(),
            };

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await placeOrderUseCase.Execute(input));

            Assert.Equal("No products selected.", ex.Message);
        }



        #endregion

        #region GetProductMethod

        [Fact]
        public async Task ShouldThrowAErrorWhenProductNotFound()
        {
            clientFacade.Setup(x => x.Find(It.IsAny<FindClientInputDto>())).Returns(() => Task.FromResult(new FindClientOutputDto() { Id = "1" }));

            productFacade.Setup(x => x.CheckoutStock(It.IsAny<CheckStockInputDto>())).Returns(
                 Task.FromResult(new CheckStockOutputDto { ProductId = "1", Stock = 1}));

            catalogFacade.Setup(x => x.Find(It.IsAny<FindProductInputDto>())).Returns(Task.FromResult<FindProductOutputDto>(null));

            var placeOrderUseCase = new PlaceOrderUseCase(clientFacade.Object,
                                                          productFacade.Object,
                                                          catalogFacade.Object,
                                                          paymentFacade.Object,
                                                          invoiceFacade.Object,
                                                          checkoutRepository.Object);

            var input = new PlaceOrderInputDto
            {
                ClientId = "0",
                Products = new List<PlaceOrderProductInputDto>() { new() { ProductId = "0" } },

            };
            var ex = await Assert.ThrowsAsync<NullReferenceException>(async () => await placeOrderUseCase.Execute(input));

            Assert.Equal("Product not found.", ex.Message);
        }


        [Fact]
        public async Task ShouldReturnAProduct()
        {
            catalogFacade.Setup(x => x.Find(It.IsAny<FindProductInputDto>())).Returns(Task.FromResult<FindProductOutputDto>(new FindProductOutputDto
            {
                Id = "0",
                Name = "Product 0",
                Description = "Product 0 description",
                SalesPrice = 0,
            }));

            var placeOrderUseCase = new PlaceOrderUseCase(clientFacade.Object,
                                                          productFacade.Object,
                                                          catalogFacade.Object,
                                                          paymentFacade.Object,
                                                          invoiceFacade.Object,
                                                          checkoutRepository.Object);


            var response = await placeOrderUseCase.GetProduct("0");

            Assert.Equal("0", response._id.GetId());
            Assert.Equal("Product 0", response.Name);
            Assert.Equal("Product 0 description", response.Description);
            Assert.Equal(0, response.SalesPrice);


            catalogFacade.Verify(x => x.Find(It.IsAny<FindProductInputDto>()), Times.Exactly(1));
        }
        #endregion
    }
}
