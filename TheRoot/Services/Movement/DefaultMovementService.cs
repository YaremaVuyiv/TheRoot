using TheRoot.Data.Models;
using TheRoot.Repositories;

namespace TheRoot.Services.Movement;

public class DefaultMovementService : IMovementService
{
    private readonly IClearingsRepository _clearingsRepository;
    private readonly IWarriorsRepository _warriorsRepository;
    private readonly IDominanceService _dominanceService;

    public DefaultMovementService(
        IClearingsRepository clearingsRepository,
        IWarriorsRepository warriorsRepository,
        IDominanceService dominanceService)
    {
        _clearingsRepository = clearingsRepository;
        _warriorsRepository = warriorsRepository;
        _dominanceService = dominanceService;
    }

    public virtual IEnumerable<int> GetFromClearingIds(FactionType faction)
    {
        var dominantClearings = _clearingsRepository.GetAllClearingIds()
            .Where(id => _dominanceService.GetDominantFactionInClearing(id) == faction)
            .ToList();

        var connectedClearings = dominantClearings
            .SelectMany(_clearingsRepository.GetConnectedClearings)
            .ToList();

        dominantClearings.AddRange(connectedClearings);

        var warriors = _warriorsRepository.GetClearingIdsWithWarriors(faction);

        return dominantClearings
            .Distinct()
            .Join(warriors, dc => dc, w => w, (dc, w) => dc)
            .Distinct();
    }

    public virtual IEnumerable<int> GetToClearingIds(FactionType faction, int clearingId)
    {
        if (!_warriorsRepository.GetClearingWarriors(clearingId).Any(x => x.Key == faction && x.Value > 0))
        {
            return Enumerable.Empty<int>();
        }

        if (_dominanceService.GetDominantFactionInClearing(clearingId) == faction)
        {
            return _clearingsRepository.GetConnectedClearings(clearingId);
        }

        return _clearingsRepository.GetConnectedClearings(clearingId)
            .Where(x => _dominanceService.GetDominantFactionInClearing(x) == faction);
    }
}
