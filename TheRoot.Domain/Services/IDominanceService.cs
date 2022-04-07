using TheRoot.Domain.Entities;

namespace TheRoot.Domain.Services;

public interface IDominanceService
{
    Task<FactionType?> GetDominantFactionInClearing(int clearingId);

    Task<Dictionary<int, FactionType?>> GetAllClearingsDominanceAsync();
}
