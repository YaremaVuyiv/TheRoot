using TheRoot.Domain.Entities;

namespace TheRoot.Domain.Services.Dominance
{
    public interface IFactionPiecesCalculatorFactory
    {
        IFactionPiecesCalculator Create(FactionType faction);
    }
}
