using TheRoot.Data;
using TheRoot.Data.Models;
using TheRoot.Repositories;

namespace TheRoot.Services.Build;

public class EyrieBuildingService : IBuildingService
{
    private readonly IDominanceService _dominanceService;
    private readonly IClearingsRepository _clearingsRepository;

    public EyrieBuildingService(
        IDominanceService dominanceService,
        IClearingsRepository clearingsRepository)
    {
        _dominanceService = dominanceService;
        _clearingsRepository = clearingsRepository;
    }

    public IEnumerable<int> GetClearingIdsForBuilding() =>
        _clearingsRepository.GetAllClearingIds()
            .Where(id =>
            {
                var slotPieces = _clearingsRepository.GetSlotPiecesByClearingId(id);

                return !HasClearingNestBuilding(slotPieces) &&
                HasClearingEmptySlots(slotPieces) &&
                IsClearingDominantByEyrie(id);
            });

    private static bool HasClearingNestBuilding(IEnumerable<BuildingType?> slots) =>
        slots.Any(slot => slot == BuildingType.Nest);

    private static bool HasClearingEmptySlots(IEnumerable<BuildingType?> slots) =>
        slots.Any(slot => slot == null);

    private bool IsClearingDominantByEyrie(int clearingId) =>
        _dominanceService.GetDominantFactionInClearing(clearingId) == FactionType.EyrieDynasties;
}
