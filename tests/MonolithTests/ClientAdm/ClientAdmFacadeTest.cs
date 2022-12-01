using _Shared.Domain.ValueObject;
using Client.Adm.Domain.Entity;
using Client.Adm.Facade.Implementation;
using Client.Adm.Factory;
using Client.Adm.Repository.Implementation;
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
                Document = "0000",
                Street = "My Street",
                Number = "123",
                Complement = "aaaa",
                City = "New York",
                State = "Kingston",
                ZipCode = "12401",
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
                Document = _client.Document,
                Street = _client.Street,
                City = _client.City,
                Number = _client.Number,
                State = _client.State,
                ZipCode = _client.ZipCode,
                Complement = _client.Complement,
                CreatedAt = _client.CreatedAt,
                UpdatedAt = _client.UpdatedAt,
            };

            var client = await clientFacade.Add(input);
 
            var response = _db.Clients.Where(c => c.Id == client.Id).FirstOrDefault();

            Assert.NotNull(response);
            Assert.Equal(response.Id, client.Id);
            Assert.Equal(response.Name, _client.Name);
            Assert.Equal(response.Email, _client.Email);
            Assert.Equal(response.Street, _client.Street);
            Assert.Equal(response.Number, _client.Number);
            Assert.Equal(response.City, _client.City);
            Assert.Equal(response.ZipCode, _client.ZipCode);
            Assert.Equal(response.Document, _client.Document);
            Assert.Equal(response.Complement, _client.Complement);
            Assert.Equal(response.State, _client.State);
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
                Id = "1",
                Document = _client.Document,
                Street = _client.Street,
                City = _client.City,
                Number = _client.Number,
                State = _client.State,
                ZipCode = _client.ZipCode,
                Complement = _client.Complement,
                CreatedAt = _client.CreatedAt,
                UpdatedAt = _client.UpdatedAt,
                Email = _client.Email,
                Name = _client.Name
            });
            await _db.SaveChangesAsync();

            var output = await clientFacade.Find(new FindClientInputDto { ClientId = "1" });

            Assert.NotNull(output);
            Assert.Equal(output.Id, "1");
            Assert.Equal(output.Name, _client.Name);
            Assert.Equal(output.Email, _client.Email);
            Assert.Equal(output.Street, _client.Street);
            Assert.Equal(output.Number, _client.Number);
            Assert.Equal(output.City, _client.City);
            Assert.Equal(output.ZipCode, _client.ZipCode);
            Assert.Equal(output.Document, _client.Document);
            Assert.Equal(output.Complement, _client.Complement);
            Assert.Equal(output.State, _client.State);
            Assert.StrictEqual(output.CreatedAt, _client.CreatedAt);
            Assert.StrictEqual(output.UpdatedAt, _client.UpdatedAt);
        }
    }
}
