using TheRoot.Domain.Entities;
using TheRoot.Domain.Repositories;
using TheRoot.Domain.Extensions;

namespace TheRoot.Domain.Services.Battle;

public class DefaultBattleService : IBattleService
{
    private readonly IClearingsRepository _clearingsRepository;

    public DefaultBattleService(
        IClearingsRepository clearingsRepository)
    {
        _clearingsRepository = clearingsRepository;
    }

    public async Task<List<int>> GetBattleClearings(FactionType faction)
    {
        var result = new List<int>();

        var clearingIds = await _clearingsRepository.GetAllClearingsIdsAsync();

        foreach (var id in clearingIds)
        {
            var warriors = await _clearingsRepository.GetClearingWarriorsAsync(id);

            if (!warriors.ContainsKey(faction) || warriors[faction] <= 0)
            {
                continue;
            }

            if (warriors.Any(x => x.Key != faction && x.Value > 0))
            {
                result.Add(id);
                continue;
            }

            var tokens = await _clearingsRepository.GetClearingTokensAsync(id);

            if (tokens.Any(x => x.Key.GetFactionType() != faction && x.Value > 0))
            {
                result.Add(id);
                continue;
            }

            var buildings = await _clearingsRepository.GetSlotsByClearingIdAsync(id);

            if (buildings.Any(x => x != null && x.FactionType != faction))
            {
                result.Add(id);
            }
        }

        return result;
    }
}
