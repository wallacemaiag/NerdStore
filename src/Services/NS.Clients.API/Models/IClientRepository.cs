using NS.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NS.Clients.API.Models
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<IEnumerable<Client>> GetAll();
        Task<Client> GetByCpf(string cpf);
        void Add(Client client);
    }
}
