using Client.Adm.UseCase.AddClient;
using Client.Adm.UseCase.FindClient;

namespace Client.Adm.Facade.Interface
{
    public interface IClientAdmFacade
    {
        Task Add(AddClientInputDto addClientInputDto);
        Task<FindClientOutputDto> Find(FindClientInputDto findClientInputDto);
    }
}
