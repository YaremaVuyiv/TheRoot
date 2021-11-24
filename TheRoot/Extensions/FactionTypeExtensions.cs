using TheRoot.Data.Models;

namespace TheRoot.Extensions
{
    public static class FactionTypeExtensions
    {
        public static float GetFactionCoefficient(this FactionType faction) =>
            faction switch
            {
                FactionType.EyrieDynasties => 0.5f,
                _ => 0
            };
    }
}
