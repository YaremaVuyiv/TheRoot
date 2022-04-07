using TheRoot.Domain.Entities;
using TheRoot.Domain.Repositories;
using TheRoot.Domain.Services.Dominance;

namespace TheRoot.Domain.Services;

public class DominanceService : IDominanceService
{
    private readonly IFactionsRepository _factionsRepository;
    private readonly IFactionPiecesCalculatorFactory _factionPiecesCalculatorFactory;
    private readonly IClearingsRepository _clearingsRepository;

    public DominanceService(
        IFactionPiecesCalculatorFactory factionPiecesCalculatorFactory,
        IFactionsRepository factionsRepository,
        IClearingsRepository clearingsRepository)
    {
        _factionPiecesCalculatorFactory = factionPiecesCalculatorFactory;
        _factionsRepository = factionsRepository;
        _clearingsRepository = clearingsRepository;
    }

    public async Task<Dictionary<int, FactionType?>> GetAllClearingsDominanceAsync()
    {
        var clearings = await _clearingsRepository.GetAllClearingsAsync();
        var clearingIds = clearings.Select(x => x.Id);

        var tasks = clearingIds
            .Select(x => GetDominantFactionInClearing(x)
                .ContinueWith(task => new KeyValuePair<int, FactionType?>(x, task.Result)));

        var clearingsDominance = await Task.WhenAll(tasks);
        var result = new Dictionary<int, FactionType?>(clearingsDominance);

        return result;
    }

    public async Task<FactionType?> GetDominantFactionInClearing(int clearingId)
    {
        var factions = await _factionsRepository.GetFactionsAsync();

        var tasks = factions.Select(faction =>
        {
            var factionCalculator = _factionPiecesCalculatorFactory.Create(faction);

            var piecesCount = factionCalculator.GetFactionPiecesWeightAsync(faction, clearingId)
                .ContinueWith(task =>
                {
                    return new KeyValuePair<FactionType, float>(faction, task.Result);
                });

            return piecesCount;
        });

        var factionPieces = await Task.WhenAll(tasks);
        var factionPiecesDictionary = factionPieces.ToDictionary(x => x.Key, x => x.Value);

        var maxValue = factionPiecesDictionary
                    .Where(x => x.Value == factionPiecesDictionary.Values.Max())
                    .ToList();

        FactionType? rulerFaction = maxValue.Count == 1 ? maxValue.First().Key : null;

        return rulerFaction;
    }
}
