using Client.Adm.Facade.Interface;
using Client.Adm.Factory;

using InfraStructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using Product.Adm.API.Dto.Input.Client;
using System.Net;
using InMemoryDb = InfraStructure.Context.InMemoryDb;

namespace Product.Adm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {


        private readonly ILogger<ClientController> _logger;
        private readonly SharedContext _sharedContext;
        public ClientController(SharedContext sharedContext)
        {
            _sharedContext = sharedContext;
        }

        [HttpPost]
        public async Task<ActionResult> Post(AddClientInputDto request)
        {
            try
            {
                var clientFacade = ClientAdmFacadeFactory.Create(_sharedContext);

               
                return Ok(await clientFacade.Add(new()
                {
                    Name = request.Name,
                    Email = request.Email,
                    Document = request.Document,
                    Street = request.Street,
                    Number = request.Number,
                    City = request.City,
                    Complement = request.Complement,
                    State = request.State,
                    ZipCode = request.ZipCode,

                }));
            }
            catch
            {

                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            try
            {
                var clientFacade = ClientAdmFacadeFactory.Create(_sharedContext);

                return Ok(await clientFacade.Find(new(){ ClientId = id,}));
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}