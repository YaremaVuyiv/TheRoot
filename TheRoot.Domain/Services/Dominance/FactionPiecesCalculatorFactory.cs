using TheRoot.Domain.Entities;

namespace TheRoot.Domain.Services.Dominance
{
    public class FactionPiecesCalculatorFactory : IFactionPiecesCalculatorFactory
    {
        private readonly DefaultFactionPiecesCalculator _defaultFactionPiecesCalculator;
        private readonly EyrieFactionPiecesCalculator _eyrieFactionPiecesCalculator;

        public FactionPiecesCalculatorFactory(
            DefaultFactionPiecesCalculator defaultFactionPiecesCalculator,
            EyrieFactionPiecesCalculator eyrieFactionPiecesCalculator)
        {
            _defaultFactionPiecesCalculator = defaultFactionPiecesCalculator;
            _eyrieFactionPiecesCalculator = eyrieFactionPiecesCalculator;
        }

        public IFactionPiecesCalculator Create(FactionType faction)
        {
            return faction.Name switch
            {
                _ => _defaultFactionPiecesCalculator
            };
        }
    }
}
