using TheRoot.Data;
using TheRoot.Data.Models;

namespace TheRoot.Extensions
{
    public static class BuildingTypeExtensions
    {
        public static FactionType GetFaction(this BuildingType building) =>
            building switch
            {
                BuildingType.Sawmill or
                BuildingType.Workshop or
                BuildingType.Recruiter => FactionType.MarquiseDeCat,
                BuildingType.Nest => FactionType.EyrieDynasties,
                BuildingType.AllianseBase => FactionType.WoodlandAllianse,
                _ => throw new ArgumentException("invalid building")
            };
    }
}
