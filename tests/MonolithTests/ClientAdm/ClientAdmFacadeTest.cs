using _Shared.Domain.ValueObject;
using Client.Adm.Domain.Entity;
using Client.Adm.Factory;
using Client.Adm.Repository.Implementation;
using Client.Adm.UseCase.AddClient;
using InfraStructure.Context;
using InfraStructure.Model.ClientAdm;
using Product.Adm.Facade.Implementation;
using Product.Adm.Factory;
using Product.Adm.UseCase.CheckStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithTests.ClientAdm
{
    public class ClientAdmFacadeTest
    {
        private SharedContext _db;
        private ClientEntity _client;
        public ClientAdmFacadeTest()
        {
            _db = InMemoryDb.InitDb();
            var input = new ClientProps
            {
                Name = "Client 1",
                Email = "Xunit@x.com",
                Address = "418 Dewey St\r\nKernersville, North Carolina(NC), 27284",
            };

            _client = new ClientEntity(input);
        }

        [Fact]
        public async Task ShouldCreateAClient()
        {
            var clientRepository = new ClientRepository(_db);
            var clientFacade = ClientAdmFacadeFactory.Create(_db);

            AddClientInputDto input = new AddClientInputDto
            {
               Id = _client._id,
               Name = _client.Name,
               Email = _client.Email,
               Address = _client.Address,
               CreatedAt = _client.CreatedAt,
               UpdatedAt = _client.UpdatedAt,
            };

            await clientFacade.Add(input);
            var id = _client._id.GetId();
            var response = _db.Clients.Where(c=>c.Id == id).FirstOrDefault();

            Assert.NotNull(response);
            Assert.Equal(response.Id, _client._id.GetId());
            Assert.Equal(response.Name, _client.Name);
            Assert.Equal(response.Email, _client.Email);
            Assert.Equal(response.Address, _client.Address);
            Assert.StrictEqual(response.CreatedAt, _client.CreatedAt);
            Assert.StrictEqual(response.UpdatedAt, _client.UpdatedAt);
        }

        [Fact]
        public async Task ShouldFindAProduct()
        {
            var clientRepository = new ClientRepository(_db);
            var clientFacade = ClientAdmFacadeFactory.Create(_db);


            _db.Add(new ClientModel
            {
                Id = _client._id.GetId(),
                Address = _client.Address,
                CreatedAt = _client.CreatedAt,
                UpdatedAt = _client.UpdatedAt,
                Email = _client.Email,
                Name = _client.Name
            });
            await _db.SaveChangesAsync();

            var output = await clientFacade.Find(new Client.Adm.UseCase.FindClient.FindClientInputDto { ClientId = _client._id.GetId()});

            Assert.NotNull(output);
            Assert.Equal(output.Id, _client._id.GetId());
            Assert.Equal(output.Name, _client.Name);
            Assert.Equal(output.Email, _client.Email);
            Assert.Equal(output.Address, _client.Address);
            Assert.StrictEqual(output.CreatedAt, _client.CreatedAt);
            Assert.StrictEqual(output.UpdatedAt, _client.UpdatedAt);
        }
    }
}
