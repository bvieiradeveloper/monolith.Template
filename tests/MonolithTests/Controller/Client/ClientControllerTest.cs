using Client.Adm.Facade.Implementation;
using InfraStructure.Context;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product.Adm.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MonolithTests.Controller.Client
{
    public class ClientControllerTest
    {
        private readonly SharedContext _sharedContext;
        public ClientControllerTest()
        {
            _sharedContext = InMemoryDb.InitDb();
        }
        [Fact]
        public async Task ShouldCreateAClient()
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
                City=  "New York",
                State=  "Kingston",
                ZipCode=  "12401",
            });


            var okResult = Assert.IsType<OkObjectResult>(client);

            var json = JsonConvert.SerializeObject(okResult.Value);
            var _client = JsonConvert.DeserializeObject<AddClientOutputDto>(json);

            var response = await clientController.Get(_client.Id);

            okResult = Assert.IsType<OkObjectResult>(response);

            json = JsonConvert.SerializeObject(okResult.Value);
            var values = JsonConvert.DeserializeObject<AddClientOutputDto>(json);

            Assert.NotNull(values);
            Assert.NotNull(values.Id);
            Assert.Equal(values.Name, _client.Name);
            Assert.Equal(values.Email, _client.Email);
            Assert.Equal(values.Street, _client.Street);
            Assert.Equal(values.Number, _client.Number);
            Assert.Equal(values.City, _client.City);
            Assert.Equal(values.ZipCode, _client.ZipCode);
            Assert.Equal(values.Document, _client.Document);
            Assert.Equal(values.Complement, _client.Complement);
            Assert.Equal(values.State, _client.State);
            Assert.StrictEqual(values.CreatedAt, _client.CreatedAt);
            Assert.StrictEqual(values.UpdatedAt, _client.UpdatedAt);
        }
    }
}
