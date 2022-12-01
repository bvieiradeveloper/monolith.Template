using Checkout.Repository.Interface;
using Checkout.UseCase.Checkout;
using Client.Adm.Facade.Implementation;
using Client.Adm.Factory;
using InfraStructure.Context;
using Invoice.Facade.Implementation;
using Invoice.Factory;
using Microsoft.AspNetCore.Mvc;
using Payment.Facade;
using Payment.Factory.Implementation;
using Product.Adm.API.Dto.Input.Checkout;
using Product.Adm.API.Dto.Input.Product;
using Product.Adm.Facade.Implementation;
using Product.Adm.Factory;
using Store.Catalog.Facade.Implementation;
using Store.Catalog.Factory;
using PlaceOrderInputDto = Product.Adm.API.Dto.Input.Checkout.PlaceOrderInputDto;
using PlaceOrderProductInputDto = Checkout.UseCase.Checkout.PlaceOrderProductInputDto;

namespace Product.Adm.API.Controllers
{
    public class CkeckoutController : ControllerBase
    {

        private readonly ILogger<CkeckoutController> _logger;
        private readonly SharedContext _sharedContext;
        private readonly ClientAdmFacade _clientAdmFacade;
        private readonly ProductStoreCatalogFacade _productStoreCatalogFacade;
        private readonly ProductAdmFacade _productAdmFacade;
        private readonly ICheckoutRepository _checkoutRepository;
        private readonly InvoiceFacade _invoiceFacade;
        private readonly PaymentFacade _paymentFacade;
        private readonly PlaceOrderUseCase _placeOrderUseCase;
        public CkeckoutController(SharedContext sharedContext,
                                  ICheckoutRepository checkoutRepository)
        {
            _sharedContext = sharedContext;
            _clientAdmFacade = ClientAdmFacadeFactory.Create(sharedContext);
            _productAdmFacade = ProductAdmFacadeFactory.Create(sharedContext);
            _productStoreCatalogFacade = ProductStoreCatalogFacadeFactory.Create(sharedContext);
            _checkoutRepository = checkoutRepository;
            _invoiceFacade = InvoiceFacadeFactory.Create(sharedContext);
            _paymentFacade = PaymentFacadeFactory.Create(sharedContext);
            _placeOrderUseCase = new PlaceOrderUseCase(_clientAdmFacade, _productAdmFacade, _productStoreCatalogFacade, _paymentFacade, _invoiceFacade, _checkoutRepository);
        }

        [HttpPost]
        public async Task<ActionResult> Post(PlaceOrderInputDto request)
        {
            try
            {
                return Ok(await _placeOrderUseCase.Execute(new() { ClientId = request.ClientId, Products = request.Products.Select(r => new PlaceOrderProductInputDto() { ProductId = r.ProductId }).ToList() }));
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
