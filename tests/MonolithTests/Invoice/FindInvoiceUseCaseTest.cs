using _Shared.Domain.ValueObject;
using Invoice.Domain.Entity;
using Invoice.Domain.ValueObject;
using Invoice.Repository.Interface;
using Invoice.UseCase.Find;
using Moq;

namespace MonolithTests.Invoice
{
    public class FindInvoiceUseCaseTest
    {
        private InvoiceEntity _invoice;
        private List<ProductEntity> _products = new();
        private Address _address;
        public FindInvoiceUseCaseTest()
        {
            List<ProductProps> inputProduct = new()
            {
                new ProductProps
                {
                    Id = new Id(Guid.NewGuid().ToString()),
                    Name = "Product 1",
                    Price = 500,
                },
                new ProductProps
                {
                    Id = new Id(Guid.NewGuid().ToString()),
                    Name = "Product 2",
                    Price = 750,
                }
            };

            _products.AddRange(inputProduct.Select(props => new ProductEntity(props)));

            AddressProps _addressProps = new()
            {
                Street = "Street 1",
                Number = "123",
                Complement = "Next to drugstore",
                City = "City 1",
                State = "SO",
                ZipCode = "123654987",
            };

            _address = new(_addressProps);

            InvoiceProps _invoiceProps = new()
            {
                Id = new Id(Guid.NewGuid().ToString()),
                Name = "Invoice 1",
                Document = "1234567890",
                Address = _address,
                Items = _products,
            };

            _invoice = new(_invoiceProps);
        }

        [Fact]
        public async Task ShouldFindAInvoice()
        {
            var invoiceRepository = new Mock<IInvoiceRepository>();

            invoiceRepository
                .Setup(s => s.Find(It.IsAny<string>()))
                .Returns(() => Task.FromResult(_invoice));

            var useCase = new FindInvoiceUseCase(invoiceRepository.Object);

            var response = await useCase.Execute(new FindInvoiceInputDto { Id = _invoice._id.GetId() });


            Assert.NotNull(response);

            Assert.NotNull(response.Id);
            Assert.Equal(_invoice.Name, response.Name);
            Assert.Equal(_invoice.Document, response.Document);
            Assert.Equal(_invoice.Address.Street, response.Address.Street);
            Assert.Equal(_invoice.Address.ZipCode, response.Address.ZipCode);
            Assert.Equal(_invoice.Address.City, response.Address.City);
            Assert.Equal(_invoice.Address.State, response.Address.State);
            Assert.Equal(_invoice.Address.Complement, response.Address.Complement);
            Assert.Equal(_invoice.Address.Number, response.Address.Number);
            Assert.Equal(_invoice.Total(), response.Total);

            Assert.Equal(2, response.Items.Count);

            Assert.Equal(_invoice.Items.First()._id.GetId(), response.Items.First().Id);
            Assert.Equal(_invoice.Items.First().Name, response.Items.First().Name);
            Assert.Equal(_invoice.Items.First().Price, response.Items.First().Price);

            Assert.Equal(_invoice.Items.Last()._id.GetId(), response.Items.Last().Id);
            Assert.Equal(_invoice.Items.Last().Name, response.Items.Last().Name);
            Assert.Equal(_invoice.Items.Last().Price, response.Items.Last().Price);
        }
    }
}
