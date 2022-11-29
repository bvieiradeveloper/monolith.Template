using Client.Adm.Facade.Implementation;
using Client.Adm.UseCase.AddClient;


namespace Client.Adm.Facade.Interface
{
    public interface IClientAdmFacade
    {
        Task Add(AddClientInputDto addClientInputDto);
        Task<FindClientOutputDto> Find(FindClientInputDto findClientInputDto);
    }
}
