using TheRoot.Data;
using TheRoot.Data.Models;
using TheRoot.Extensions;
using TheRoot.Repositories;

namespace TheRoot.Services;

public class DominanceService : IDominanceService
{
    private readonly IClearingsRepository _clearingsRepository;
    private readonly IWarriorsRepository _warriorsRepository;

    public DominanceService(
        IClearingsRepository clearingsRepository,
        IWarriorsRepository warriorsRepository)
    {
        _clearingsRepository = clearingsRepository;
        _warriorsRepository = warriorsRepository;
    }

    public FactionType? GetDominantFactionInClearing(int clearingId)
    {
        var factionPiecesDictionary = _warriorsRepository.GetClearingWarriors(clearingId)
            .ToDictionary(x => x.Key, x => (float)x.Value);

        var slots = _clearingsRepository.GetSlotPiecesByClearingId(clearingId);

        foreach (var slot in slots)
        {
            if (slot.HasValue && slot != BuildingType.Ruin)
            {
                var faction = GetFactionByBuildingType(slot.Value);
                factionPiecesDictionary.TryGetValue(faction, out float count);
                factionPiecesDictionary[faction] = count + 1;
            }
        }

        foreach (var faction in factionPiecesDictionary.Keys)
        {
            factionPiecesDictionary[faction] += faction.GetFactionCoefficient();
        }

        var maxValue = factionPiecesDictionary
            .Where(x => x.Value == factionPiecesDictionary.Values.Max())
            .ToList();

        return maxValue.Count == 1 ? maxValue.First().Key : null;
    }

    private FactionType GetFactionByBuildingType(BuildingType building) =>
        building switch
        {
            BuildingType.Sawmill or
            BuildingType.Workshop or
            BuildingType.Recruiter => FactionType.MarquiseDeCat,
            BuildingType.Nest => FactionType.EyrieDynasties,
            BuildingType.AllianseBase => FactionType.WoodlandAllianse,
            _ => throw new Exception("unknown building")
        };
}
