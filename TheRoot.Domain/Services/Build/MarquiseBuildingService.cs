using System;
using System.Collections.Generic;
using System.Linq;
using TheRoot.Domain.Repositories;

namespace TheRoot.Domain.Services.Build;

/*public class MarquiseBuildingService : IBuildingService
{
    private readonly IDominanceService _dominanceService;
    private readonly IClearingsRepository _clearingsRepository;

    public MarquiseBuildingService(
        IDominanceService dominanceService,
        IClearingsRepository clearingsRepository)
    {
        _dominanceService = dominanceService;
        _clearingsRepository = clearingsRepository;
    }

    public IEnumerable<int> GetClearingIdsForBuilding()
    {
        var buildingTypes = new BuildingType[]
        {
                BuildingType.Sawmill,
                BuildingType.Workshop,
                BuildingType.Recruiter
        };

        var min = buildingTypes
            .Select(type => _clearingsRepository.GetAllClearingIds()
                .SelectMany(x => _clearingsRepository.GetSlotPiecesByClearingId(x))
                .Where(slot => slot.HasValue && slot == type)
                .Count())
            .Min();

        var minWood = min switch
        {
            0 => 0,
            1 => 1,
            2 => 2,
            3 => 3,
            4 => 3,
            5 => 4,
            _ => throw new ArgumentException("There are too many buildings on map")
        };

        var clearingsWithWood =
            _tokensRepository.GetClearingIdsWithTokens(TokenType.Wood)
            .SelectMany(id => GetWoodTransportClearings(id))
            .GroupBy(id => id)
            .ToDictionary(x => x.Key, x => x.Count());

        var result = clearingsWithWood
            .Where(x => x.Value >= minWood &&
                _clearingsRepository.GetSlotPiecesByClearingId(x.Key).Any(x => x is null))
            .Select(x => x.Key);

        return result;
    }

    private IEnumerable<int> GetWoodTransportClearings(int clearingId)
    {
        var set = new HashSet<int>();
        InspectConnectedClearingsForTransportWood(clearingId, set);
        return set;
    }

    private void InspectConnectedClearingsForTransportWood(
        int clearingId,
        HashSet<int> inspectedClearings)
    {
        if (_dominanceService.GetDominantFactionInClearing(clearingId) == FactionType.MarquiseDeCat)
        {
            inspectedClearings.Add(clearingId);
            foreach (var cc in _clearingsRepository.GetConnectedClearings(clearingId))
            {
                if (inspectedClearings.Contains(cc))
                {
                    continue;
                }

                InspectConnectedClearingsForTransportWood(cc, inspectedClearings);
            }
        }
        else
        {
            return;
        }
    }
}*/
