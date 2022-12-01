using Client.Adm.Factory;
using InfraStructure.Context;
using Microsoft.AspNetCore.Mvc;
using Product.Adm.API.Dto.Input.Product;
using Product.Adm.Factory;

namespace Product.Adm.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly SharedContext _sharedContext;
        public ProductController(SharedContext sharedContext)
        {
            _sharedContext = sharedContext;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProductAdmInputDto request)
        {
            try
            {
                var productFacade = ProductAdmFacadeFactory.Create(_sharedContext);


                return Ok(await productFacade.AddProduct(new()
                {
                    id = request?.id,
                    Name = request.Name,
                    Description = request.Description,
                    PurchasePrice = request.PurchasePrice,
                    Stock = request.Stock

                }));
            }
            catch
            {

                return StatusCode(500);
            }
        }

        [HttpGet("/{id}")]
        public async Task<ActionResult> Get(string id)
        {
            try
            {
                var productFacade = ProductAdmFacadeFactory.Create(_sharedContext);


                return Ok(await productFacade.CheckoutStock(new() {ProductId = id}));
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
