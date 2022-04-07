using TheRoot.Domain.Entities.Pieces;
using TheRoot.Domain.Extensions;

namespace TheRoot.Domain.Entities;

public record Game : BaseEntity
{
    protected Game()
    {
        _clearings = MapData.GetClearings();
        _forests = MapData.GetForests();
        _clearingsPaths = MapData.GetClearingsPaths();
        _clearingForestPaths = MapData.GetClearingForestPaths();
        _factions = new LinkedList<Faction>();

        var craftableItems = new Item[]
        {
            new Item(ItemType.Bag),
            new Item(ItemType.Bag),
            new Item(ItemType.Boot),
            new Item(ItemType.Boot),
            new Item(ItemType.Sword),
            new Item(ItemType.Sword),
            new Item(ItemType.Teapot),
            new Item(ItemType.Teapot),
            new Item(ItemType.Money),
            new Item(ItemType.Money),
            new Item(ItemType.Crossbow),
            new Item(ItemType.Hammer)
        };

        DeckCardsContainer = new CardsContainer();
        DiscardCardsContainer = new CardsContainer();
        CraftableItemsContainer = new ItemsContainer(craftableItems);
        _currentTurnPhase = TurnPhase.Birdsong;
    }

    public Game(IEnumerable<Faction> factions) : this()
    {

        _factions = new LinkedList<Faction>(_factions);
        _currentFactionNode = _factions.First;
    }

    private List<Clearing> _clearings;
    private List<Forest> _forests;
    private List<ClearingsPath> _clearingsPaths;
    private List<ClearingsPath> _clearingsRiverPaths;
    private List<ClearingForestPath> _clearingForestPaths;

    private LinkedList<Faction> _factions;
    private LinkedListNode<Faction> _currentFactionNode;
    private TurnPhase _currentTurnPhase;

    public PiecesContainer<Card> DeckCardsContainer { get; init; }

    public PiecesContainer<Card> DiscardCardsContainer { get; init; }

    public PiecesContainer<Item> CraftableItemsContainer { get; init; }

    public IEnumerable<Clearing> Clearings => _clearings.AsReadOnly();

    public IEnumerable<Forest> Forests => _forests.AsReadOnly();

    public IEnumerable<ClearingsPath> ClearingsPaths => _clearingsPaths.AsReadOnly();

    public IEnumerable<ClearingsPath> ClearingsRiverPaths => _clearingsRiverPaths.AsReadOnly();

    public IEnumerable<ClearingForestPath> ClearingForestPaths => _clearingForestPaths.AsReadOnly();

    public IEnumerable<Faction> Factions => _factions.ToList().AsReadOnly();

    public virtual TurnPhase CurrentTurnPhase => _currentTurnPhase;

    public virtual Faction CurrentFaction => _currentFactionNode.Value;

    public void TakeCardFromDeck(FactionType factionType)
    {
        var faction = _factions.First(x => x.FactionType == factionType);

        var rnd = new Random();
        var cardIndex = rnd.Next(0, DeckCardsContainer.Pieces.Count());
        var card = DeckCardsContainer.Pieces.ToList()[cardIndex];
        DeckCardsContainer.RemovePiecesRange(card.Id);
        faction.FactionCardsContainer.AddPieces(card);
    }

    public void NextPlayerTurn()
    {
        _currentFactionNode = _currentFactionNode.Next ?? _factions.First;
        _currentTurnPhase = TurnPhase.Birdsong;
    }

    public Faction GetFactionByFactionType(FactionType factionType)
    {
        var faction = _factions.SingleOrDefault(x => x.FactionType == factionType);

        if (faction == null)
        {
            throw new ArgumentException($"Faction {factionType.Name} is not present in {Id} Game");
        }

        return faction;
    }

    public Clearing GetClearingById(int clearingId)
    {
        var clearing = _clearings.SingleOrDefault(x => x.Id == clearingId);

        if (clearing == null)
        {
            throw new ArgumentException($"Game {Id} does not contain clearing {clearingId}");
        }

        return clearing;
    }

    private void PlaceWarrior(int clearingId, FactionType warriorType)
    {
        var faction = GetFactionByFactionType(warriorType);

        if (faction is not IWarriorFaction warriorsFaction)
        {
            throw new ArgumentException($"Faction {warriorType.Name} does not contain warriors");
        }

        var warrior = warriorsFaction.WarriorsContainer.GetWarriorByFaction(warriorType);

        var clearing = GetClearingById(clearingId);

        warriorsFaction.WarriorsContainer.RemovePiecesRange(warrior.Id);
        clearing.WarriorsContainer.AddPieces(warrior);
    }

    private void PlaceBuilding(int clearingId, BuildingType buildingType)
    {
        var factionType = buildingType.GetFactionType();
        var faction = GetFactionByFactionType(factionType);

        if (faction is not IBuildingFaction buildingsFaction)
        {
            throw new ArgumentException($"Faction {factionType.Name} does not contain buildings");
        }

        var building = buildingsFaction.BuildingsContainer.GetBuildingByBuildingType(buildingType);

        var clearing = GetClearingById(clearingId);

        buildingsFaction.BuildingsContainer.RemovePiecesRange(building.Id);
        clearing.BuildingsContainer.AddPieces(building);
    }

    private void PlaceToken(int clearingId, TokenType tokenType)
    {
        var factionType = tokenType.GetFactionType();
        var faction = GetFactionByFactionType(factionType);

        if (faction is not ITokenFaction tokensFaction)
        {
            throw new ArgumentException($"Faction {factionType.Name} does not contain tokens");
        }

        var token = tokensFaction.TokensContainer.GetTokenByTokenType(tokenType);

        var clearing = GetClearingById(clearingId);

        tokensFaction.TokensContainer.RemovePiecesRange(token.Id);
        clearing.TokensContainer.AddPieces(token);
    }

    public void PlaceTokens(int clearingId, params TokenType[] tokenTypes)
    {
        foreach (var tokenType in tokenTypes)
        {
            PlaceToken(clearingId, tokenType);
        }
    }
}

