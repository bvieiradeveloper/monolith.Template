using InfraStructure.Context;
using Invoice.Factory;
using Microsoft.AspNetCore.Mvc;
using Product.Adm.API.Dto.Input.Product;
using Product.Adm.Factory;

namespace Product.Adm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {

        private readonly ILogger<InvoiceController> _logger;
        private readonly SharedContext _sharedContext;
        public InvoiceController(SharedContext sharedContext)
        {
            _sharedContext = sharedContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            try
            {
                var productFacade = InvoiceFacadeFactory.Create(_sharedContext);


                return Ok(await productFacade.FindInvoice(new() { Id = id }));
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
