using TheRoot.Domain.Entities;

namespace TheRoot.Domain.Repositories
{
    public interface IFactionsRepository
    {
        Task<List<FactionType>> GetFactionsAsync();
    }
}
