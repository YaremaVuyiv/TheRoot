using TheRoot.Domain.Entities;
using TheRoot.Domain.Repositories;

namespace TheRoot.Domain.Services.Dominance
{
    public class DefaultFactionPiecesCalculator : IFactionPiecesCalculator
    {
        private readonly IClearingsRepository _clearingsRepository;

        public DefaultFactionPiecesCalculator(
            IClearingsRepository clearingsRepository)
        {
            _clearingsRepository = clearingsRepository;
        }

        public virtual async Task<float> GetFactionPiecesWeightAsync(FactionType faction, int clearingId)
        {
            var clearing = await _clearingsRepository.GetClearingByIdAsync(0, clearingId);

            var clearingWarriors = clearing.WarriorsContainer.Pieces
                .GroupBy(x => x.FactionType.Name)
                .ToDictionary(x => x.Key, x => x.Count());

            var result = clearing.BuildingsContainer.Pieces.Where(x => x.FactionType.Name == faction.Name).Count();

            if (clearingWarriors.TryGetValue(faction.Name, out var warriorsCount))
            {
                result += warriorsCount;
            }

            return result;
        }
    }
}
