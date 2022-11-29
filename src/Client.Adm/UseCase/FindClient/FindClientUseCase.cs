using Client.Adm.Repository.Interface;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MonolithTests.ClientAdm")]
namespace Client.Adm.UseCase.FindClient
{

    public class FindClientUseCase
    {
        private readonly IClientRepository _clientRepository;
        public FindClientUseCase(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        internal async Task<FindClientOutputDto> Execute(FindClientInputDto findClientInputDto)
        {
            var client = await _clientRepository.Find(findClientInputDto.ClientId);

            return new FindClientOutputDto
            {
                Id = client._id.GetId(),
                Name = client.Name,
                Email = client.Email,
                Document = client.Document,
                Street = client.Street,
                City = client.City,
                Complement = client.Complement,
                Number = client.Number,
                State = client.State,
                ZipCode = client.ZipCode,
                CreatedAt = client.CreatedAt,
                UpdatedAt = client.UpdatedAt,
            };
        }
    }
}
