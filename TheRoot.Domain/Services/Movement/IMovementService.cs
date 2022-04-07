using System.Collections.Generic;
using System.Threading.Tasks;
using TheRoot.Domain.Entities;

namespace TheRoot.Domain.Services.Movement;

public interface IMovementService
{
    Task<IEnumerable<int>> GetFromClearingIds(FactionType faction);

    Task<IEnumerable<int>> GetToClearingIds(FactionType faction, int clearingId);
}
