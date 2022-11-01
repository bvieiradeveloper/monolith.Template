using Client.Adm.Domain.Entity;
using Client.Adm.UseCase.FindClient;

namespace Client.Adm.Repository.Interface
{
    public interface IClientRepository
    {
        Task Add(ClientEntity entity);
        Task<ClientEntity> Find(string id);
    }
}
