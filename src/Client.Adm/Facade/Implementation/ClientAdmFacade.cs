using Client.Adm.Facade.Interface;
using Client.Adm.UseCase.AddClient;
using Client.Adm.UseCase.FindClient;
using FindUseCaseInput = Client.Adm.UseCase.FindClient.FindClientInputDto;

namespace Client.Adm.Facade.Implementation
{
    public class ClientAdmFacade : IClientAdmFacade
    {
        private AddClientUseCase _addClienttUseCase;
        private FindClientUseCase _findClientUseCase;
        public ClientAdmFacade(AddClientUseCase addUseCase, FindClientUseCase findUseCase)
        {
            _addClienttUseCase = addUseCase;
            _findClientUseCase = findUseCase;
        }
        public async Task Add(AddClientInputDto addClientInputDto)
        {
            await _addClienttUseCase.Execute(addClientInputDto);
        }

        public async Task<FindClientOutputDto?> Find(FindClientInputDto findClientInputDto)
        {
            try
            {
                var response = await _findClientUseCase.Execute(new FindUseCaseInput
                {
                    ClientId = findClientInputDto.ClientId,
                });

                return new()
                {
                    Id = response.Id,
                    Name = response.Name,
                    Document = response.Document,
                    Street = response.Street,
                    City = response.City,
                    Complement = response.Complement,
                    Number = response.Number,
                    State = response.State,
                    ZipCode = response.ZipCode,
                    Email = response.Email,
                    CreatedAt = response.CreatedAt,
                    UpdatedAt = response.UpdatedAt
                };
            }
            catch (Exception ex)
            {

                return new FindClientOutputDto();
            }

        }
    }
}
