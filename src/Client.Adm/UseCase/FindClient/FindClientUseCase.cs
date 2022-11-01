using Client.Adm.Repository.Interface;

namespace Client.Adm.UseCase.FindClient
{
    public class FindClientUseCase
    {
        private readonly IClientRepository _clientRepository;
        public FindClientUseCase(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<FindClientOutputDto> Execute(FindClientInputDto findClientInputDto)
        {
            var client = await _clientRepository.Find(findClientInputDto.ClientId);

            return new FindClientOutputDto
            {
                Id = client._id.GetId(),
                Name = client.Name,
                Email = client.Email,
                Address = client.Address,
                CreatedAt = client.CreatedAt,
                UpdatedAt = client.UpdatedAt,
            };
        }
    }
}
