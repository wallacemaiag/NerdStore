using Microsoft.EntityFrameworkCore;
using NS.Clients.API.Models;
using NS.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NS.Clients.API.Data.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientsContext _context;

        public ClientRepository(ClientsContext context)
        {
            _context = context;
        }

        public IUnityOfWork unityOfWork => _context;

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _context.Clients.AsNoTracking().ToListAsync();
        }

        public async Task<Client> GetByCpf(string cpf)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
        }

        public void Add(Client client)
        {
            _context.Clients.Add(client);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
