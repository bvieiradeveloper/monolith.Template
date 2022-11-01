using Client.Adm.Domain.Entity;
using Client.Adm.Repository.Implementation;
using InfraStructure.Context;
using InfraStructure.Model.ClientAdm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithTests.ClientAdm
{
    public class ClientRepositoryTest
    {
        private readonly SharedContext _db;
        private ClientEntity _client;
        public ClientRepositoryTest()
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
        public async Task ShouldAddAClient()
        {
            var clientRepository = new ClientRepository(_db);

            await clientRepository.Add(_client);

            var response = (ClientModel)_db.Find(typeof(ClientModel), _client._id.GetId());


            Assert.NotNull(response);
            Assert.Equal(response.Id, _client._id.GetId());
            Assert.Equal(response.Name, _client.Name);
            Assert.Equal(response.Email, _client.Email);
            Assert.Equal(response.Address, _client.Address);
            Assert.StrictEqual(response.CreatedAt, _client.CreatedAt);
            Assert.StrictEqual(response.UpdatedAt, _client.UpdatedAt);
        }

        [Fact]
        public async Task ShouldFindAClient()
        {
            var clientRepository = new ClientRepository(_db);
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

            

            var response = await clientRepository.Find(_client._id.GetId());


            Assert.NotNull(response);
            Assert.Equal(response._id.GetId(), _client._id.GetId());
            Assert.Equal(response.Name, _client.Name);
            Assert.Equal(response.Email, _client.Email);
            Assert.Equal(response.Address, _client.Address);
            Assert.StrictEqual(response.CreatedAt, _client.CreatedAt);
            Assert.StrictEqual(response.UpdatedAt, _client.UpdatedAt);
        }
    }
}
