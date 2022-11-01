using Client.Adm.Facade.Implementation;
using Client.Adm.Repository.Implementation;
using Client.Adm.UseCase.AddClient;
using Client.Adm.UseCase.FindClient;
using InfraStructure.Context;

namespace Client.Adm.Factory
{
    public  class ClientAdmFacadeFactory
    {
        public static ClientAdmFacade Create(SharedContext _db)
        {
            var clientRepository = new ClientRepository(_db);
            var addClientUseCase = new AddClientUseCase(clientRepository);
            var findClientUseCase = new FindClientUseCase(clientRepository);

            var clientAdmFacade = new ClientAdmFacade(addClientUseCase, findClientUseCase);

            return clientAdmFacade;
        }
    }
}
