using TheRoot.Data.Models;
using TheRoot.Extensions;
using TheRoot.Repositories;

namespace TheRoot.Services.Battle;

public class DefaultBattleService : IBattleService
{
    private readonly IClearingsRepository _clearingsRepository;
    private readonly IWarriorsRepository _warriorsRepository;
    private readonly ITokensRepository _tokensRepository;

    public DefaultBattleService(
        IClearingsRepository clearingsRepository,
        IWarriorsRepository warriorsRepository,
        ITokensRepository tokensRepository)
    {
        _clearingsRepository = clearingsRepository;
        _warriorsRepository = warriorsRepository;
        _tokensRepository = tokensRepository;
    }

    public List<int> GetBattleClearings(FactionType faction)
    {
        var result = new List<int>();

        var clearingIds = _clearingsRepository.GetAllClearingIds();

        foreach (var id in clearingIds)
        {
            var warriors = _warriorsRepository.GetClearingWarriors(id);

            if (!warriors.ContainsKey(faction) || warriors[faction] <= 0)
            {
                continue;
            }

            if (warriors.Any(x => x.Key != faction && x.Value > 0))
            {
                result.Add(id);
                continue;
            }

            var tokens = _tokensRepository.GetClearingTokens(id);

            if (tokens.Any(x => x.Key.GetFactionByTokenType() != faction && x.Value > 0))
            {
                result.Add(id);
                continue;
            }

            var buildings = _clearingsRepository.GetSlotPiecesByClearingId(id);

            if (buildings.Any(x => x.HasValue && x.Value.GetFaction() != faction))
            {
                result.Add(id);
            }
        }

        return result;
    }
}
