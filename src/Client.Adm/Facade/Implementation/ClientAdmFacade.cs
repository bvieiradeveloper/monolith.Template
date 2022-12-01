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
        public async Task<AddClientOutputDto> Add(AddClientInputDto addClientInputDto)
        {

            var response = await _addClienttUseCase.Execute(new()
            {
                City = addClientInputDto.City,
                Complement = addClientInputDto.Complement,
                CreatedAt = addClientInputDto.CreatedAt,
                Document = addClientInputDto.Document,
                Email = addClientInputDto.Email,
                Name = addClientInputDto.Name,
                Id = addClientInputDto.Id,
                Number = addClientInputDto.Number,
                State = addClientInputDto.State,
                Street = addClientInputDto.Street,
                UpdatedAt = addClientInputDto.UpdatedAt,
                ZipCode = addClientInputDto.ZipCode,

            });

            return new()
            {
                Id = response.Id,
                Name = response.Name,
                Email = response.Email,
                Document = response.Document,
                Street = response.Street,
                Complement = response.Complement,
                City = response.City,
                Number = response.Number,
                ZipCode = response.ZipCode,
                State = response.State,
                CreatedAt = response.CreatedAt,
                UpdatedAt = response.UpdatedAt,

            };
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
