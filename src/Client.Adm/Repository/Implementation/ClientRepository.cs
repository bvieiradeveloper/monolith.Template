using _Shared.Domain.ValueObject;
using Client.Adm.Domain.Entity;
using Client.Adm.Repository.Interface;
using Client.Adm.UseCase.FindClient;
using InfraStructure.Context;
using InfraStructure.Model.ClientAdm;

namespace Client.Adm.Repository.Implementation
{
    public class ClientRepository : IClientRepository
    {
        private readonly SharedContext _sharedContext;
        public ClientRepository(SharedContext sharedContext)
        {
            _sharedContext = sharedContext;
        }
        public async Task Add(ClientEntity entity)
        {
             _sharedContext.Clients.Add(new ClientModel
             {
                 Id = entity._id.GetId(),
                 Address = entity.Address,
                 CreatedAt = entity.CreatedAt,
                 UpdatedAt = entity.UpdatedAt,
                 Email = entity.Email,
                 Name = entity.Name 
             });

            await _sharedContext.SaveChangesAsync(); 
        }

        public async Task<ClientEntity> Find(string id)
        {
            var result =  await _sharedContext.Clients.FindAsync(id);

            var input = new ClientProps
            {
                Id = new Id(result.Id),
                Name = result.Name,
                Address = result.Address,
                Email = result.Email,
                UpdatedAt = result.UpdatedAt,
                CreatedAt = result.CreatedAt,
            };

            return new ClientEntity(input);
        }
    }
}
