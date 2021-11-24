using System.Collections.Generic;
using TheRoot.Data.Models;

namespace TheRoot.Services.Movement;

public interface IMovementService
{
    IEnumerable<int> GetFromClearingIds(FactionType faction);

    IEnumerable<int> GetToClearingIds(FactionType faction, int clearingId);
}
