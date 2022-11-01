using Client.Adm.Domain.Entity;
using Client.Adm.Repository.Interface;
using Client.Adm.UseCase.FindClient;
using InfraStructure.Context;
using InfraStructure.Model.ClientAdm;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithTests.ClientAdm
{
    public class FindClientUseCaseTest
    {
        private readonly SharedContext _db;
        private ClientEntity _client;
        public FindClientUseCaseTest()
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
        public async Task ShouldFindAClient()
        {
            var clientRepository = new Mock<IClientRepository>();
            clientRepository.Setup(x => x.Find(_client._id.GetId())).Returns(Task.FromResult(_client));
            var useCase = new FindClientUseCase(clientRepository.Object);
            var response = await useCase.Execute(new FindClientInputDto { ClientId = _client._id.GetId() });

            clientRepository.Verify(x => x.Find(_client._id.GetId()), Times.AtLeastOnce);

            Assert.NotNull(response);
            Assert.NotNull(response.Id);
            Assert.Equal(response.Name, _client.Name);
            Assert.Equal(response.Email, _client.Email);
            Assert.Equal(response.Address, _client.Address);
            Assert.StrictEqual(response.CreatedAt, _client.CreatedAt);
            Assert.StrictEqual(response.UpdatedAt, _client.UpdatedAt);

        }
    }
}
