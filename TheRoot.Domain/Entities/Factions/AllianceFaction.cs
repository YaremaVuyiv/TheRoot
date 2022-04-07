using TheRoot.Domain.Entities.Pieces;

namespace TheRoot.Domain.Entities.Factions;

public record AllianceFaction : Faction, IWarriorFaction, ITokenFaction, IBuildingFaction
{
    public AllianceFaction()
    {
        WarriorsContainer = new WarriorsContainer();

        var warriors = Enumerable.Repeat(new Warrior(FactionType.WoodlandAllianse), 10).ToArray();
        WarriorsContainer.AddPieces(warriors);

        TokensContainer = new TokensContainer();
        var supportTokens = Enumerable.Repeat(new Token(TokenType.Support), 10).ToArray();
        TokensContainer.AddPieces(supportTokens);

        BuildingsContainer = new BuildingsContainer();
        var allianseBases = new AllianseBase[]
        {
            new AllianseBase(ClearingType.Fox),
            new AllianseBase(ClearingType.Mouse),
            new AllianseBase(ClearingType.Rabbit)
        };
        BuildingsContainer.AddPieces(allianseBases);

        OfficersContainer = new WarriorsContainer();
        SupportCardsContainer = new CardsContainer();

        _usedOfficers = 0;
    }

    private int _usedOfficers;

    public TokensContainer TokensContainer { get; init; }

    public BuildingsContainer BuildingsContainer { get; init; }

    public WarriorsContainer WarriorsContainer { get; init; }

    public WarriorsContainer OfficersContainer { get; init; }

    public PiecesContainer<Card> SupportCardsContainer { get; init; }

    public int UsedOfficers => _usedOfficers;

    public void UseOfficer()
    {
        if (_usedOfficers >= OfficersContainer.Pieces.Count())
        {
            throw new Exception("All officers are already used");
        }

        _usedOfficers++;
    }
}
