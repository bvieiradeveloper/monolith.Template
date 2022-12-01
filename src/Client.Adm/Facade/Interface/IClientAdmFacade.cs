using Client.Adm.Facade.Implementation;


namespace Client.Adm.Facade.Interface
{
    public interface IClientAdmFacade
    {
        Task<AddClientOutputDto> Add(AddClientInputDto addClientInputDto);
        Task<FindClientOutputDto> Find(FindClientInputDto findClientInputDto);
    }
}
