using TheRoot.Domain.Entities.Pieces;

namespace TheRoot.Domain.Entities.Factions;

public record EyrieFaction : Faction, IWarriorFaction, IBuildingFaction
{
    public EyrieFaction()
    {
        WarriorsContainer = new WarriorsContainer();
        BuildingsContainer = new BuildingsContainer();

        var warriors = Enumerable.Repeat(new Warrior(FactionType.EyrieDynasties), 20).ToArray();
        WarriorsContainer.AddPieces(warriors);

        var nests = Enumerable.Repeat(new Building(BuildingType.Nest), 7).ToArray();
        BuildingsContainer.AddPieces(nests);
    }

    public WarriorsContainer WarriorsContainer { get; init; }

    public BuildingsContainer BuildingsContainer { get; init; }
}
