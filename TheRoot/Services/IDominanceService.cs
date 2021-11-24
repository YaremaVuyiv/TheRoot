using TheRoot.Data;
using TheRoot.Data.Models;

namespace TheRoot.Services;

public interface IDominanceService
{
    FactionType? GetDominantFactionInClearing(int clearingId);
}
