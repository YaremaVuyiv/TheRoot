namespace TheRoot.Domain.Entities;

public abstract record Faction : BaseEntity
{
    private List<CraftingPiece> _craftingPieces;
    private int _score;

    protected Faction()
    {
        _craftingPieces = new List<CraftingPiece>();
        FactionCardsContainer = new CardsContainer();
        FactionItemsContainer = new ItemsContainer();
        _score = 0;
    }

    public Faction(FactionType factionType) : this()
    {
        FactionType = factionType;
    }

    public FactionType FactionType { get; init; }

    public PiecesContainer<Card> FactionCardsContainer { get; init; }

    public IEnumerable<CraftingPiece> CraftingPieces => _craftingPieces.AsReadOnly();

    public ItemsContainer FactionItemsContainer { get; init; }

    public int Score => _score;
}