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

        public async Task<AddClientOutputDto> Execute(AddClientInputDto addClientInputDto)
        {
            var input = new ClientProps
            {
                Id = addClientInputDto.Id,
                Name = addClientInputDto.Name,
                Email = addClientInputDto.Email,
                Document = addClientInputDto.Document,
                Street = addClientInputDto.Street,
                City = addClientInputDto.City,
                Number = addClientInputDto.Number,
                State = addClientInputDto.State,
                ZipCode = addClientInputDto.ZipCode,
                Complement = addClientInputDto.Complement,
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
                Document = client.Document,
                Street = client.Street,
                City = client.City,
                Number = client.Number,
                State = client.State,
                ZipCode = client.ZipCode,
                Complement = client.Complement,
                CreatedAt = client.CreatedAt,
                UpdatedAt = client.UpdatedAt
            };
        }
    }
}
