using TheRoot.Domain.Entities.Pieces;

namespace TheRoot.Domain.Entities;

#region Immutable Entiries

public record Clearing : BaseEntity
{
    protected Clearing()
    {
        WarriorsContainer = new WarriorsContainer();
        TokensContainer = new TokensContainer();
        BuildingsContainer = new BuildingsContainer();
    }

    public Clearing(ClearingType clearingType, int maxNumberOfBUildings) : this()
    {
        ClearingType = clearingType;
        MaxNumberOfBuildings = maxNumberOfBUildings;
    }

    public ClearingType ClearingType { get; init; }

    public PiecesContainer<Warrior> WarriorsContainer { get; init; }

    public TokensContainer TokensContainer { get; init; }

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

public class TurnPhase : Enumeration
{
    public static TurnPhase Birdsong = new(1, nameof(Birdsong));
    public static TurnPhase Daylight = new(2, nameof(Daylight));
    public static TurnPhase Evening = new(3, nameof(Evening));

    public TurnPhase(int id, string name) : base(id, name)
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

public record Ruin : Building
{
    public Ruin(params Item[] items) : base(BuildingType.Ruin)
    {
        RuinItemsContainer = new ItemsContainer(items);
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

public record TokensContainer : PiecesContainer<Token>
{
    public TokensContainer() : base()
    {
    }

    public TokensContainer(IEnumerable<Token> tokens) : base(tokens)
    {
    }

    public Token GetTokenByTokenType(TokenType tokenType)
    {
        var token = Pieces.FirstOrDefault(x => x.TokenType == tokenType);

        if (token == null)
        {
            throw new ArgumentException($"Token {tokenType} is missing in tokens container {Id}");
        }

        return token;
    }
}

public record BuildingsContainer : PiecesContainer<Building>
{
    public BuildingsContainer() : base()
    {
    }

    public BuildingsContainer(IEnumerable<Building> buildings) : base(buildings)
    {
    }

    public Building GetBuildingByBuildingType(BuildingType buildingType)
    {
        var building = Pieces.FirstOrDefault(x => x.SlotPiece == buildingType);

        if (building == null)
        {
            throw new ArgumentException($"Building {buildingType} is missing in buildings container {Id}");
        }

        return building;
    }
}

public record ItemsContainer : PiecesContainer<Item>
{
    public ItemsContainer() : base()
    {
    }

    public ItemsContainer(IEnumerable<Item> items) : base(items)
    {
    }

    public Item GetItemByType(ItemType itemType)
    {
        var item = Pieces.FirstOrDefault(x => x.ItemType.Id == itemType.Id);

        if (item == null)
        {
            throw new ArgumentException($"Item {itemType.Name} is missing in items container {Id}");
        }

        return item;
    }
}

public record WarriorsContainer : PiecesContainer<Warrior>
{
    public WarriorsContainer() : base()
    {
    }

    public WarriorsContainer(IEnumerable<Warrior> warriors) : base(warriors)
    {
    }

    public Warrior GetWarriorByFaction(FactionType factionType)
    {
        var warrior = Pieces.FirstOrDefault(x => x.FactionType.Id == factionType.Id);

        if (warrior == null)
        {
            throw new ArgumentException($"Warrior {factionType.Name} is missing in warriors container {Id}");
        }

        return warrior;
    }
}

public record CardsContainer : PiecesContainer<Card>
{
    public CardsContainer() : base()
    {
    }

    public CardsContainer(IEnumerable<Card> cards) : base(cards)
    {
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

    public static List<ClearingsPath> GetClearingsRiverPaths() 
    {
        var clearingsRiverPaths = new List<ClearingsPath>
        {
            new ClearingsPath(clearing3, clearing9),
            new ClearingsPath(clearing4, clearing5),
            new ClearingsPath(clearing5, clearing8),
            new ClearingsPath(clearing8, clearing9)
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

    public static List<Card> GetCards()
    {
        var cards = new List<Card>
        {
            new Card(CardSuit.Bird, CardType.Bag, new List<CostType>
            {
                CostType.Fox
            })
            {
                Id = 1
            },
            new Card(CardSuit.Rabbit, CardType.Boot, new List<CostType>
            {
                CostType.Rabbit
            })
            {
                Id = 2
            },
            new Card(CardSuit.Fox, CardType.Favor, new List<CostType>
            {
                CostType.Fox,
                CostType.Fox,
                CostType.Fox,
            })
            {
                Id = 3
            }
        };

        return cards;
    }
}

public interface ITokenFaction
{
    TokensContainer TokensContainer { get; init; }
}

public interface IWarriorFaction
{
    WarriorsContainer WarriorsContainer { get; init; }
}

public interface IBuildingFaction
{
    BuildingsContainer BuildingsContainer { get; init; }
}