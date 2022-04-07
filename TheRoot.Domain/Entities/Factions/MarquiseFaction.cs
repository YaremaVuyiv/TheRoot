using TheRoot.Domain.Entities.Pieces;

namespace TheRoot.Domain.Entities.Factions;

public record MarquiseFaction : Faction, IWarriorFaction, IBuildingFaction, ITokenFaction
{
    protected MarquiseFaction() : base()
    {
        TokensContainer = new TokensContainer();
        BuildingsContainer = new BuildingsContainer();

        var warriors = Enumerable.Repeat(new Warrior(FactionType.MarquiseDeCat), 25).ToArray();
        WarriorsContainer = new WarriorsContainer(warriors);

        var sawmills = Enumerable.Repeat(new Building(BuildingType.Sawmill), 6).ToArray();
        var workshops = Enumerable.Repeat(new Building(BuildingType.Workshop), 6).ToArray();
        var recruiters = Enumerable.Repeat(new Building(BuildingType.Recruiter), 6).ToArray();
        BuildingsContainer.AddPieces(sawmills);
        BuildingsContainer.AddPieces(workshops);
        BuildingsContainer.AddPieces(recruiters);

        var keepToken = new Token(TokenType.Keep);
        var woodTokens = Enumerable.Repeat(new Token(TokenType.Wood), 8).ToArray();
        TokensContainer.AddPieces(keepToken);
        TokensContainer.AddPieces(woodTokens);
    }

    public WarriorsContainer WarriorsContainer { get; init; }

    public TokensContainer TokensContainer { get; init; }

    public BuildingsContainer BuildingsContainer { get; init; }
}
