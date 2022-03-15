namespace TheRoot.Domain.Entities;

#region Immutable Entiries
public abstract record BaseEntity 
{
    public int Id { get; init; }
}

public record Clearing : BaseEntity
{
    protected Clearing()
    {
        WarriorsContainer = new PiecesContainer<Warrior>();
        TokensContainer = new PiecesContainer<Token>();
        BuildingsContainer = new PiecesContainer<Building>();
    }

    public Clearing(ClearingType clearingType, int maxNumberOfBUildings) : this()
    {
        ClearingType = clearingType;
        MaxNumberOfBuildings = maxNumberOfBUildings;
    }

    public ClearingType ClearingType { get; init; }

    public PiecesContainer<Warrior> WarriorsContainer { get; init; }

    public PiecesContainer<Token> TokensContainer { get; init; }

    public PiecesContainer<Building> BuildingsContainer { get; init; }

    public int MaxNumberOfBuildings { get; init; }
}

public record ClearingsPath : BaseEntity
{
    protected ClearingsPath()
    {
    }

    public ClearingsPath(Clearing fromClearing, Clearing toClearing)
    {
        FromClearing = fromClearing;
        ToClearing = toClearing;
    }

    public Clearing FromClearing { get; init; }

    public Clearing ToClearing { get; init; }
}

public record ClearingsRiverPath : BaseEntity
{
    protected ClearingsRiverPath()
    {
    }

    public ClearingsRiverPath(Clearing fromClearing, Clearing toClearing)
    {
        FromClearing = fromClearing;
        ToClearing = toClearing;
    }

    public Clearing FromClearing { get; init; }

    public Clearing ToClearing { get; init; }
}

public record ClearingForestPath : BaseEntity
{
    protected ClearingForestPath()
    {
    }

    public ClearingForestPath(Clearing clearing, Forest forest)
    {
        Clearing = clearing;
        Forest = forest;
    }

    public Clearing Clearing { get; init; }

    public Forest Forest { get; init; }
}
#endregion

public class FactionType : Enumeration
{
    public static FactionType MarquiseDeCat = new(1, nameof(MarquiseDeCat));
    public static FactionType EyrieDynasties = new(2, nameof(EyrieDynasties));
    public static FactionType WoodlandAllianse = new(3, nameof(WoodlandAllianse));

    public FactionType(int id, string name) 
        : base(id, name)
    {
    }
}

public class ClearingType : Enumeration
{
    public static ClearingType Fox = new(1, nameof(Fox));
    public static ClearingType Rabbit = new(2, nameof(Rabbit));
    public static ClearingType Mouse = new(3, nameof(Mouse));

    public ClearingType(int id, string name) : base(id, name)
    {
    }
}

public class CardSuit : Enumeration
{
    public static CardSuit Fox = new(1, nameof(Fox));
    public static CardSuit Rabbit = new(2, nameof(Rabbit));
    public static CardSuit Mouse = new(3, nameof(Mouse));
    public static CardSuit Bird = new(4, nameof(Bird));

    public CardSuit(int id, string name) : base(id, name)
    {
    }
}

public class ItemType : Enumeration
{
    public static ItemType Bag = new(1, nameof(Bag));
    public static ItemType Boot = new(2, nameof(Boot));
    public static ItemType Sword = new(3, nameof(Sword));
    public static ItemType Money = new(4, nameof(Money));
    public static ItemType Teapot = new(5, nameof(Teapot));
    public static ItemType Crossbow = new(6, nameof(Crossbow));
    public static ItemType Hammer = new(7, nameof(Hammer));
    public static ItemType Torch = new(8, nameof(Torch));

    public ItemType(int id, string name) : base(id, name)
    {
    }
}

public enum BuildingType
{
    Sawmill,
    Workshop,
    Recruiter,
    Nest,
    AllianseBase,
    Ruin
}

public enum TokenType
{
    Wood,
    Keep,
    Support
}

public class CostType : Enumeration
{
    public static CostType Fox = new(1, nameof(Fox));
    public static CostType Mouse = new(2, nameof(Mouse));
    public static CostType Rabbit = new(3, nameof(Rabbit));
    public static CostType Any = new(4, nameof(Any));

    public CostType(int id, string name)
        : base(id, name)
    {
    }
}

public enum CardType
{
    Hammer,
    Armorers,
    Sword,
    Money,
    BetterBorrowBank,
    Ambush,
    Crossbow,
    Dominance,
    Bag,
    BrutalTactics,
    Cobbler,
    CodeBreakers,
    CommandWarren,
    Favor,
    Teapot,
    Boot,
    RoyalClaim,
    Sappers,
    ScoutingParty,
    StandAndDeliver,
    TaxCollector
}

public record Item : BaseEntity
{
    protected Item()
    {
    }

    public Item(ItemType itemType)
    {
        ItemType = itemType;
    }

    public ItemType ItemType { get; init; }
}

public record Forest : BaseEntity
{
}

public record Card : BaseEntity
{
    protected Card()
    {
        _costTypeList = new List<CostType>();
    }

    public Card(CardSuit cardSuit,
        CardType cardType,
        IEnumerable<CostType> cost)
    {
        _costTypeList = cost.ToList();
        CardSuit = cardSuit;
        CardType = cardType;
    }

    private List<CostType> _costTypeList;

    public CardSuit CardSuit { get; init; }

    public CardType CardType { get; init; }

    public IEnumerable<CostType> Cost => _costTypeList.AsEnumerable();
}

/// <summary>
/// Mutable entities
/// </summary>
public record Warrior : BaseEntity
{
    public Warrior(FactionType faction)
    {
        FactionType = faction;
    }

    protected Warrior()
    {
    }

    public FactionType FactionType { get; init; }
}

public record Building : BaseEntity
{
    public Building(BuildingType buildingType)
    {
        SlotPiece = buildingType;
    }

    protected Building()
    {
    }

    public BuildingType SlotPiece { get; set; }

    public FactionType? Faction =>
        SlotPiece switch
        {
            BuildingType.Sawmill or
            BuildingType.Recruiter or
            BuildingType.Workshop => FactionType.MarquiseDeCat,
            BuildingType.AllianseBase => FactionType.WoodlandAllianse,
            BuildingType.Nest => FactionType.EyrieDynasties,
            _ => null
        };
}

public record Ruin : Building
{
    public Ruin() : base(BuildingType.Ruin)
    {
        RuinItemsContainer = new PiecesContainer<Item>();
    }

    public PiecesContainer<Item> RuinItemsContainer { get; init; }
}

public record AllianseBase : Building
{
    public AllianseBase(ClearingType clearingType) : base(BuildingType.AllianseBase)
    {
        ClearingType = clearingType;
    }

    protected AllianseBase() : base()
    {
    }

    public ClearingType ClearingType { get; init; }
}

public record Token : BaseEntity
{
    public Token(TokenType tokenType)
    {
        TokenType = tokenType;
    }

    protected Token()
    {
    }

    public TokenType TokenType { get; init; }

    public FactionType Faction =>
        TokenType switch
        {
            TokenType.Wood or TokenType.Keep => FactionType.MarquiseDeCat,
            TokenType.Support => FactionType.WoodlandAllianse,
            _ => throw new Exception("Unsopported token")
        };
}

public abstract record Faction : BaseEntity
{
    private List<CraftingPiece> _craftingPieces;
    private int _score;

    protected Faction()
    {
        _craftingPieces = new List<CraftingPiece>();
        FactionCardsContainer = new PiecesContainer<Card>();
        FactionItemsContainer = new PiecesContainer<Item>();
        _score = 0;
    }

    public Faction(FactionType factionType) : this()
    {
        FactionType = factionType;
    }

    public FactionType FactionType { get; init; }

    public PiecesContainer<Card> FactionCardsContainer { get; init; }

    public IEnumerable<CraftingPiece> CraftingPieces => _craftingPieces.AsReadOnly();

    public PiecesContainer<Item> FactionItemsContainer { get; init; }

    public int Score => _score;
}

public record MarquiseFaction : Faction
{
    protected MarquiseFaction() : base()
    {
        TokensContainer = new PiecesContainer<Token>();
        BuildingsContainer = new PiecesContainer<Building>();

        var warriors = Enumerable.Repeat(new Warrior(FactionType.MarquiseDeCat), 25).ToArray();
        WarriorsContainer = new PiecesContainer<Warrior>(warriors);

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

    public PiecesContainer<Warrior> WarriorsContainer { get; init; }

    public PiecesContainer<Token> TokensContainer { get; init; }

    public PiecesContainer<Building> BuildingsContainer { get; set; }
}

public record EyrieFaction : Faction
{
    public EyrieFaction()
    {
        WarriorsContainer = new PiecesContainer<Warrior>();
        BuildingsContainer = new PiecesContainer<Building>();

        var warriors = Enumerable.Repeat(new Warrior(FactionType.EyrieDynasties), 20).ToArray();
        WarriorsContainer.AddPieces(warriors);

        var nests = Enumerable.Repeat(new Building(BuildingType.Nest), 7).ToArray();
        BuildingsContainer.AddPieces(nests);
    }

    public PiecesContainer<Warrior> WarriorsContainer { get; init; }

    public PiecesContainer<Building> BuildingsContainer { get; set; }
}

public record AllianceFaction : Faction
{
    public AllianceFaction()
    {
        WarriorsContainer = new PiecesContainer<Warrior>();

        var warriors = Enumerable.Repeat(new Warrior(FactionType.WoodlandAllianse), 10).ToArray();
        WarriorsContainer.AddPieces(warriors);

        TokensContainer = new PiecesContainer<Token>();
        var supportTokens = Enumerable.Repeat(new Token(TokenType.Support), 10).ToArray();
        TokensContainer.AddPieces(supportTokens);

        BuildingsContainer = new PiecesContainer<Building>();
        var allianseBases = new AllianseBase[]
        {
            new AllianseBase(ClearingType.Fox),
            new AllianseBase(ClearingType.Mouse),
            new AllianseBase(ClearingType.Rabbit)
        };
        BuildingsContainer.AddPieces(allianseBases);

        OfficersContainer = new PiecesContainer<Warrior>();
        SupportCardsContainer = new PiecesContainer<Card>();

        _usedOfficers = 0;
    }

    private int _usedOfficers;

    public PiecesContainer<Token> TokensContainer { get; init; }

    public PiecesContainer<Building> BuildingsContainer { get; set; }

    public PiecesContainer<Warrior> WarriorsContainer { get; init; }

    public PiecesContainer<Warrior> OfficersContainer { get; init; }

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

public record CraftingPiece : BaseEntity
{
    protected CraftingPiece()
    {
    }

    public CraftingPiece(ClearingType clearingType)
    {
        _isActivated = false;
        ClearingType = clearingType;
    }

    private bool _isActivated;

    public ClearingType ClearingType { get; init; }

    public bool IsActivated => _isActivated;

    public void Activate() => _isActivated = true;

    public void Deactivate() => _isActivated = false;
}

public record Game : BaseEntity
{
    protected Game()
    {
        _clearings = MapData.GetClearings();
        _forests = MapData.GetForests();
        _clearingsPaths = MapData.GetClearingsPaths();
        _clearingForestPaths = MapData.GetClearingForestPaths();
        _factions = new List<Faction>();

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

        DeckCardsContainer = new PiecesContainer<Card>();
        DiscardCardsContainer = new PiecesContainer<Card>();
        CraftableItemsContainer = new PiecesContainer<Item>(craftableItems);
    }

    public Game(IEnumerable<Faction> factions) : this()
    {
        _factions = factions.ToList();
    }

    private List<Clearing> _clearings;
    private List<Forest> _forests;
    private List<ClearingsPath> _clearingsPaths;
    private List<ClearingsRiverPath> _clearingsRiverPaths;
    private List<ClearingForestPath> _clearingForestPaths;
    private List<Faction> _factions;

    public PiecesContainer<Card> DeckCardsContainer { get; init; }

    public PiecesContainer<Card> DiscardCardsContainer { get; init; }

    public PiecesContainer<Item> CraftableItemsContainer { get; init; }

    public IEnumerable<Clearing> Clearings => _clearings.AsReadOnly();

    public IEnumerable<Forest> Forests => _forests.AsReadOnly();

    public IEnumerable<ClearingsPath> ClearingsPaths => _clearingsPaths.AsReadOnly();

    public IEnumerable<ClearingsRiverPath> ClearingsRiverPaths => _clearingsRiverPaths.AsReadOnly();

    public IEnumerable<ClearingForestPath> ClearingForestPaths => _clearingForestPaths.AsReadOnly();

    public IEnumerable<Faction> Factions => _factions.AsReadOnly();
}

public record PiecesContainer<T> : BaseEntity where T : BaseEntity
{
    public PiecesContainer()
    {
        _pieces = new List<T>();
    }

    public PiecesContainer(IEnumerable<T> pieces)
    {
        _pieces = pieces.ToList();
    }

    private readonly List<T> _pieces;

    public IEnumerable<T> Pieces => _pieces.AsReadOnly();

    public void RemovePiece(int pieceId)
    {
        var index = _pieces.FindIndex(x => x.Id == pieceId);
        if (index < 0)
        {
            throw new ArgumentException($"Piece {typeof(T)} with id {pieceId} doens't exist");
        }

        _pieces.RemoveAt(index);
    }

    public void AddPieces(params T[] pieces)
    {
        if (pieces == null)
        {
            throw new ArgumentException("pieces are null");
        }

        var intersection = pieces.Intersect(_pieces).ToList();
        if (intersection.Count > 0)
        {
            throw new ArgumentException($"Piece {typeof(T)} with Id:{intersection.First().Id} already exists");
        }

        _pieces.AddRange(pieces);
    }

    public void RemovePiecesRange(IEnumerable<T> pieces)
    {
        var exception = pieces.Except(_pieces).ToList();
        if (exception.Count > 0)
        {
            throw new ArgumentException($"Piece {typeof(T)} with Id:{exception.First().Id} is not possible to delete");
        }

        _pieces.RemoveAll(x => pieces.Any(i => i.Id == x.Id));
    }
}

public static class MapData
{
    private static readonly Clearing clearing1 = new(ClearingType.Fox, 2)
    {
        Id = 1,
    };

    private static readonly Clearing clearing2 = new(ClearingType.Mouse, 2)
    {
        Id = 2,
    };

    private static readonly Clearing clearing3 = new(ClearingType.Rabbit, 1)
    {
        Id = 3,
    };

    private static readonly Clearing clearing4 = new(ClearingType.Rabbit, 2)
    {
        Id = 4,
    };

    private static readonly Clearing clearing5 = new(ClearingType.Rabbit, 2)
    {
        Id = 5,
    };

    private static readonly Clearing clearing6 = new(ClearingType.Fox, 2)
    {
        Id = 6,
    };

    private static readonly Clearing clearing7 = new(ClearingType.Fox, 2)
    {
        Id = 7,
    };

    private static readonly Clearing clearing8 = new(ClearingType.Mouse, 2)
    {
        Id = 8,
    };

    private static readonly Clearing clearing9 = new(ClearingType.Mouse, 3)
    {
        Id = 9,
    };

    private static readonly Clearing clearing10 = new(ClearingType.Mouse, 2)
    {
        Id = 10,
    };

    private static readonly Clearing clearing11 = new(ClearingType.Fox, 2)
    {
        Id = 11,
    };

    private static readonly Clearing clearing12 = new(ClearingType.Rabbit, 1)
    {
        Id = 12,
    };

    private static readonly Forest forest1 = new()
    {
        Id = 1
    };
    private static readonly Forest forest2 = new()
    {
        Id = 2
    };
    private static readonly Forest forest3 = new()
    {
        Id = 3
    };
    private static readonly Forest forest4 = new()
    {
        Id = 4
    };
    private static readonly Forest forest5 = new()
    {
        Id = 5
    };
    private static readonly Forest forest6 = new()
    {
        Id = 6
    };
    private static readonly Forest forest7 = new()
    {
        Id = 7
    };

    public static List<Clearing> GetClearings()
    {
        var clearings = new List<Clearing>
        {
            clearing1,
            clearing2,
            clearing3,
            clearing4,
            clearing5,
            clearing6,
            clearing7,
            clearing8,
            clearing9,
            clearing10,
            clearing11,
            clearing12
        };

        return clearings;
    }

    public static List<Forest> GetForests()
    {
        var forests = new List<Forest>
        {
            forest1,
            forest2,
            forest3,
            forest4,
            forest5,
            forest6,
            forest7
        };

        return forests;
    }

    public static List<ClearingsPath> GetClearingsPaths()
    {
        var clearingsPaths = new List<ClearingsPath>
        {
            new ClearingsPath(clearing1, clearing2),
            new ClearingsPath(clearing1, clearing4),
            new ClearingsPath(clearing1, clearing5),
            new ClearingsPath(clearing2, clearing3),
            new ClearingsPath(clearing2, clearing6),
            new ClearingsPath(clearing3, clearing6),
            new ClearingsPath(clearing3, clearing7),
            new ClearingsPath(clearing4, clearing10),
            new ClearingsPath(clearing5, clearing6),
            new ClearingsPath(clearing5, clearing10),
            new ClearingsPath(clearing6, clearing8),
            new ClearingsPath(clearing6, clearing9),
            new ClearingsPath(clearing7, clearing9),
            new ClearingsPath(clearing8, clearing11),
            new ClearingsPath(clearing8, clearing12),
            new ClearingsPath(clearing9, clearing12),
            new ClearingsPath(clearing10, clearing11),
            new ClearingsPath(clearing11, clearing12)
        };

        return clearingsPaths;
    }

    public static List<ClearingsRiverPath> GetClearingsRiverPaths() 
    {
        var clearingsRiverPaths = new List<ClearingsRiverPath>
        {
            new ClearingsRiverPath(clearing3, clearing9),
            new ClearingsRiverPath(clearing4, clearing5),
            new ClearingsRiverPath(clearing5, clearing8),
            new ClearingsRiverPath(clearing8, clearing9)
        };

        return clearingsRiverPaths;
    }

    public static List<ClearingForestPath> GetClearingForestPaths()
    {
        var clearingForestPaths = new List<ClearingForestPath>
        {
            new ClearingForestPath(clearing1, forest1),
            new ClearingForestPath(clearing4, forest1),
            new ClearingForestPath(clearing5, forest1),
            new ClearingForestPath(clearing10, forest1),
            new ClearingForestPath(clearing1, forest2),
            new ClearingForestPath(clearing2, forest2),
            new ClearingForestPath(clearing5, forest2),
            new ClearingForestPath(clearing6, forest2),
            new ClearingForestPath(clearing5, forest3),
            new ClearingForestPath(clearing6, forest3),
            new ClearingForestPath(clearing8, forest3),
            new ClearingForestPath(clearing10, forest3),
            new ClearingForestPath(clearing11, forest3),
            new ClearingForestPath(clearing2, forest4),
            new ClearingForestPath(clearing3, forest4),
            new ClearingForestPath(clearing6, forest4),
            new ClearingForestPath(clearing3, forest5),
            new ClearingForestPath(clearing6, forest5),
            new ClearingForestPath(clearing7, forest5),
            new ClearingForestPath(clearing9, forest5),
            new ClearingForestPath(clearing6, forest6),
            new ClearingForestPath(clearing8, forest6),
            new ClearingForestPath(clearing9, forest6),
            new ClearingForestPath(clearing12, forest6),
            new ClearingForestPath(clearing8, forest7),
            new ClearingForestPath(clearing11, forest7),
            new ClearingForestPath(clearing12, forest7)
        };

        return clearingForestPaths;
    }
}