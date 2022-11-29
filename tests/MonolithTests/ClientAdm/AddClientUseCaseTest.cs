using _Shared.Domain.ValueObject;
using Client.Adm.Domain.Entity;
using Client.Adm.Repository.Interface;
using Client.Adm.UseCase.AddClient;
using InfraStructure.Context;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithTests.ClientAdm
{
    public class AddClientUseCaseTest
    {
        private ClientEntity _client;
        private SharedContext _db;
        public AddClientUseCaseTest()
        {
            _db = InMemoryDb.InitDb();

            
            var input = new ClientProps
            {
                Id = new Id("1"),
                Name = "Client 1",
                Email = "x@x.com",
                Address = "418 Dewey St\r\nKernersville, North Carolina(NC), 27284",
            };

            _client = new ClientEntity(input);
        }
        [Fact]
        public async Task ShouldAddAClient()
        {
            var clientRepository = new Mock<IClientRepository>();
            clientRepository.Setup(x => x.Add(It.IsAny<ClientEntity>())).Returns(Task.CompletedTask);

            var useCase = new AddClientUseCase(clientRepository.Object);

            var input = new AddClientInputDto
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
            };

            var response = await useCase.Execute(input);

            clientRepository.Verify(x => x.Add(It.IsAny<ClientEntity>()), Times.AtLeastOnce);

            Assert.NotNull(response);
            Assert.NotNull(response.Id);
            Assert.Equal(response.Name, _client.Name);
            Assert.Equal(response.Email, _client.Email);
            Assert.Equal(response.Street, _client.Street);
            Assert.Equal(response.Number, _client.Number);
            Assert.Equal(response.City, _client.City);
            Assert.Equal(response.ZipCode, _client.ZipCode);
            Assert.Equal(response.Document, _client.Document);
            Assert.Equal(response.Complement, _client.Complement);
            Assert.Equal(response.State, _client.State);
        }
    }
}
