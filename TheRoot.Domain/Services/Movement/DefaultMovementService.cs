using TheRoot.Domain.Entities;
using TheRoot.Domain.Repositories;

namespace TheRoot.Domain.Services.Movement;

public class DefaultMovementService : IMovementService
{
    private readonly IClearingsRepository _clearingsRepository;
    private readonly IDominanceService _dominanceService;

    public DefaultMovementService(
        IClearingsRepository clearingsRepository,
        IDominanceService dominanceService)
    {
        _clearingsRepository = clearingsRepository;
        _dominanceService = dominanceService;
    }

    public virtual async Task<IEnumerable<int>> GetFromClearingIds(FactionType faction)
    {
        var clearings = await _clearingsRepository.GetAllClearingsAsync();
        var allClearingIds = clearings.Select(x => x.Id);

        var dominantClearingIds = await GetDominantClearings(allClearingIds, faction);

        var connectedClearingsTasks = dominantClearingIds
            .Select(x => _clearingsRepository.GetAdjacentClearingsAsync(x));

        var connectedClearingsResult = await Task.WhenAll(connectedClearingsTasks);
        var connectedClearings = connectedClearingsResult.SelectMany(x => x).Distinct().ToList();

        dominantClearingIds.AddRange(connectedClearings);

        var warriors = clearings
            .Where(x => x.WarriorsContainer.Pieces
            .GroupBy(x => x.FactionType.Name)
            .Any(w => w.Key == faction.Name && w.Count() > 0))
            .Select(x => x.Id);

        return dominantClearingIds
            .Distinct()
            .Join(warriors, dc => dc, w => w, (dc, w) => dc)
            .Distinct();
    }

    public virtual async Task<IEnumerable<int>> GetToClearingIds(FactionType faction, int clearingId)
    {
        var clearingWarriors = await _clearingsRepository.GetClearingWarriorsAsync(clearingId);

        if (!clearingWarriors.Any(x => x.Key == faction && x.Value > 0))
        {
            return Enumerable.Empty<int>();
        }

        var dominantFaction = await _dominanceService.GetDominantFactionInClearing(clearingId);

        var adjacentClearings = await _clearingsRepository.GetAdjacentClearingsAsync(clearingId);

        if (dominantFaction == faction)
        {
            
            return adjacentClearings;
        }

        var dominantClearings = await GetDominantClearings(adjacentClearings, faction);

        return dominantClearings;
    }

    private async Task<List<int>> GetDominantClearings(IEnumerable<int> clearingIds, FactionType faction)
    {
        var dominantCLearingsTasks = clearingIds
            .Select(x => _dominanceService.GetDominantFactionInClearing(x)
            .ContinueWith(task => new { Id = x, Faction = task.Result }));

        var dominantClearings = await Task.WhenAll(dominantCLearingsTasks);

        var dominantClearingIds = dominantClearings
            .Where(x => x.Faction == faction)
            .Select(x => x.Id)
            .ToList();

        return dominantClearingIds;
    }
}
