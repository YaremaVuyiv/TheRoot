using System.Threading.Tasks;
using TheRoot.Domain.Entities;

namespace TheRoot.Domain.Services.Dominance
{
    public interface IFactionPiecesCalculator
    {
        Task<float> GetFactionPiecesWeightAsync(FactionType faction, int clearingId);
    }
}
