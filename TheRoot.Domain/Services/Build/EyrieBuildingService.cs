using TheRoot.Domain.Entities;
using TheRoot.Domain.Repositories;

namespace TheRoot.Domain.Services.Build;

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

    public async Task<IEnumerable<int>> GetClearingIdsForBuildingAsync()
    {
        var clearings = await _clearingsRepository.GetAllClearingsAsync();
        var clearingsDominance = await _dominanceService.GetAllClearingsDominanceAsync();

        var result = clearings
            .Where(x =>
            {
                var slotPieces = x.BuildingsContainer.Pieces.Select(s => s.SlotPiece);

                return !HasClearingNestBuilding(slotPieces) &&
                HasClearingEmptySlots(slotPieces) &&
                clearingsDominance[x.Id] == FactionType.EyrieDynasties;
            })
            .Select(x => x.Id);

        return result;
    }

    private static bool HasClearingNestBuilding(IEnumerable<BuildingType> slots) =>
        slots.Any(slot => slot == BuildingType.Nest);

    private static bool HasClearingEmptySlots(IEnumerable<BuildingType> slots) =>
        slots.Any(slot => slot == null);

    /*private bool IsClearingDominantByEyrie(int clearingId) =>
        (int)_dominanceService.GetDominantFactionInClearing(clearingId) == 1;*/
}
