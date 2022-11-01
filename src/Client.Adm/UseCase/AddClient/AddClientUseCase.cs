using Client.Adm.Domain.Entity;
using Client.Adm.Repository.Interface;

namespace Client.Adm.UseCase.AddClient
{
    public class AddClientUseCase
    {
        private readonly IClientRepository _clientRepository;
        public AddClientUseCase(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<AddClientOutputDto>  Execute(AddClientInputDto addClientInputDto)
        {
            var input = new ClientProps
            {
                Id = addClientInputDto.Id,
                Name = addClientInputDto.Name,
                Email = addClientInputDto.Email,
                Address = addClientInputDto.Address,
                CreatedAt = addClientInputDto.CreatedAt,
                UpdatedAt = addClientInputDto.UpdatedAt,
            };


            var client = new ClientEntity(input);

            await _clientRepository.Add(client);


            return new AddClientOutputDto
            {
                Id = client._id.GetId(),
                Name = client.Name,
                Email = client.Email,
                Address = client.Address,
                CreatedAt = client.CreatedAt,
                UpdatedAt = client.UpdatedAt
            };
        }
    }
}
