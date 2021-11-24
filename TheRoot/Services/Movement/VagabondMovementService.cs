using System.Collections.Generic;
using TheRoot.Data.Models;
using TheRoot.Repositories;

namespace TheRoot.Services.Movement;

public class VagabondMovementService : DefaultMovementService
{
    public VagabondMovementService(
        IDominanceService dominanceService,
        IClearingsRepository clearingsRepository,
        IWarriorsRepository warriorsRepository) : base(clearingsRepository, warriorsRepository, dominanceService)
    {
    }

    public override IEnumerable<int> GetFromClearingIds(FactionType faction)
    {
        return new int[] { 1 };
    }

    public override IEnumerable<int> GetToClearingIds(FactionType faction, int clearingId) =>
        new int[] { 1 };
}
