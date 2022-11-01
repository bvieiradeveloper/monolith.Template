using Client.Adm.Facade.Interface;
using Client.Adm.UseCase.AddClient;
using Client.Adm.UseCase.FindClient;

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

        public async Task<FindClientOutputDto> Find(FindClientInputDto findClientInputDto)
        {
            return await _findClientUseCase.Execute(findClientInputDto);
        }
    }
}
