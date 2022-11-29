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

namespace MonolithTests.Checkout
{
    public class PlaceOrderUseCaseTestBase
    {
        private Mock<IClientAdmFacade> clientFacade = new Mock<IClientAdmFacade>();
        private Mock<IProductAdmFacade> productFacade = new Mock<IProductAdmFacade>();
        private Mock<IProductStoreCatalogFacade> catalogFacade = new Mock<IProductStoreCatalogFacade>();
        private Mock<IPaymentFacade> paymentFacade = new Mock<IPaymentFacade>();
        private Mock<IInvoiceFacade> invoiceFacade = new Mock<IInvoiceFacade>();
        private Mock<ICheckoutRepository> checkoutRepository = new Mock<ICheckoutRepository>();


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

            var input = new PlaceOrderInputDto
            {
                ClientId = "0",
                Products = new List<PlaceOrderProductInputDto>(),
            };


            var ex = await Assert.ThrowsAsync<NullReferenceException>(async () => await placeOrderUseCase.Execute(input));

            Assert.Equal("Client not found.", ex.Message);
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





        [Fact]
        public async Task ShouldThrowAErrorWhenProductNotFound()
        {
            clientFacade.Setup(x => x.Find(It.IsAny<FindClientInputDto>())).Returns(() => Task.FromResult(new FindClientOutputDto() { Id = "1" }));

            productFacade.Setup(x => x.CheckoutStock(It.IsAny<CheckStockInputDto>())).Returns(
                 Task.FromResult(new CheckStockOutputDto { ProductId = "1", Stock = 1 }));

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
    }
}