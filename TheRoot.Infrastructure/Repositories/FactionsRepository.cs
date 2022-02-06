using TheRoot.Domain.Entities;
using TheRoot.Domain.Repositories;

namespace TheRoot.Infrastructure.Repositories
{
    public class FactionsRepository : IFactionsRepository
    {
        public Task<List<FactionType>> GetFactionsAsync()
        {
            return Task.FromResult(StateContainer.Factions);
        }
    }
}
