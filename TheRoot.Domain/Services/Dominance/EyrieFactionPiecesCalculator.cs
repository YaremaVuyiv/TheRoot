using System.Threading.Tasks;
using TheRoot.Domain.Entities;
using TheRoot.Domain.Repositories;

namespace TheRoot.Domain.Services.Dominance
{
    public class EyrieFactionPiecesCalculator : DefaultFactionPiecesCalculator
    {
        public EyrieFactionPiecesCalculator(IClearingsRepository clearingsRepository) : base(clearingsRepository)
        {
        }

        public override async Task<float> GetFactionPiecesWeightAsync(FactionType faction, int clearingId)
        {
            var result = await base.GetFactionPiecesWeightAsync(faction, clearingId);

            return result + 0.5f;
        }
    }
}
