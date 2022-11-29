using _Shared.Domain.ValueObject;
using Checkout.Domain.Entity;
using Checkout.Repository.Interface;
using Client.Adm.Facade.Implementation;
using Client.Adm.Facade.Interface;
using Invoice.Facade.Interface;
using Invoice.UseCase.Generate;
using Payment.Factory.Interface;
using Product.Adm.Facade.Interface;
using Store.Catalog.Facade.Interface;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using CheckoutClient = Checkout.Domain.Entity.Client;
using CheckoutProduct = Checkout.Domain.Entity.Product;

[assembly: InternalsVisibleTo("MonolithTests")]
namespace Checkout.UseCase.Checkout
{
    public class PlaceOrderUseCase
    {
        private readonly IClientAdmFacade _clientAdmFacade;
        private readonly IProductAdmFacade _productAdmFacade;
        private readonly IProductStoreCatalogFacade _catalogFacade;
        private readonly IPaymentFacade _paymentFacade;
        private readonly IInvoiceFacade _invoiceFacade;
        private readonly ICheckoutRepository _checkoutRepository;
        public PlaceOrderUseCase(IClientAdmFacade clientAdmFacade,
                                 IProductAdmFacade productAdmFacade,
                                 IProductStoreCatalogFacade catalogFacade,
                                 IPaymentFacade paymentFacade,
                                 IInvoiceFacade invoiceFacade,
                                 ICheckoutRepository checkoutRepository)
        {
            _clientAdmFacade = clientAdmFacade;
            _productAdmFacade = productAdmFacade;
            _catalogFacade = catalogFacade;
            _paymentFacade = paymentFacade;
            _invoiceFacade = invoiceFacade;
            _checkoutRepository = checkoutRepository;
        }

        public async Task<PlaceOrderOutputDto> Execute(PlaceOrderInputDto input)
        {

            var client = await _clientAdmFacade.Find(new FindClientInputDto { ClientId = input.ClientId });

            if (client.Id is null)
                throw new NullReferenceException("Client not found.");

            await ValidateProducts(input);

            List<CheckoutProduct> products = new();

            foreach (var p in input.Products.ToList())
            {
                products.Add(await GetProduct(p.ProductId));
            }

            CheckoutClient myClient = new CheckoutClient(new()
            {
                Id = new Id(client.Id),
                Name = client.Name,
                Email = client.Email,
                Document = client.Document,
                Street = client.Street,
                Number = client.Number,
                Complement = client.Complement,
                City = client.City,
                State = client.State,
                ZipCode = client.ZipCode,
            });

            Order order = new Order(new()
            {
                Client = myClient,
                Products = products,
            });

            var payment = await _paymentFacade.Process(new()
            {
                Order_ID = order._id.GetId(),
                Amount = order.Total(),
            });

            var invoice = payment.Status.Equals("approved") ? await _invoiceFacade.Generate(new()
            {
                Name = client.Name,
                Document = client.Document,
                Street = client.Street,
                Number = client.Number,
                Complement = client.Complement,
                City = client.City,
                State = client.State,
                ZipCode = client.ZipCode,
                Items = products.Select(p => new GenerateInvoiceProductInputDto { Id = p._id.GetId(), Name = p.Name, Price = p.SalesPrice }).ToList(),
            }) : new GenerateInvoiceOutputDto();

            if (payment.Status.Equals("approved"))
                order.Approve();

            await _checkoutRepository.AddOrder(order);


            return new()
            {
                Id = order._id.GetId(),
                InvoiceId = invoice?.Id,
                Status = order?.Status,
                Products = products.Select(p => p._id.GetId()).ToList(),
                Total = order.Total(),
            };
        }
        internal async Task ValidateProducts(PlaceOrderInputDto input)
        {
            if (input.Products.Count() == 0)
                throw new ArgumentException("No products selected.");

            foreach (var p in input.Products.ToList())
            {
                var product = await _productAdmFacade.CheckoutStock(new()
                {
                    ProductId = p.ProductId,
                });

                if (product.Stock <= 0)
                {
                    throw new ArgumentException($"Product {product.ProductId} is not avaliable in stock.");
                }
            }
        }

        internal async Task<CheckoutProduct> GetProduct(string productId)
        {
            var product = await _catalogFacade.Find(new() { Id = productId });

            if (product is null)
                throw new NullReferenceException("Product not found.");

            return new CheckoutProduct(new()
            {
                Id = new Id(product.Id),
                Name = product.Name,
                Description = product.Description,
                SalesPrice = product.SalesPrice,
            });
        }
    }
}
