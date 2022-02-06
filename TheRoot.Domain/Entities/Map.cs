namespace TheRoot.Domain.Entities;

#region Immutable Entiries
public abstract record BaseEntity(int Id);

public record Clearing : BaseEntity
{
    public Clearing(
        int id) : base(id)
    {
    }

    public ClearingType ClearingType { get; init; }

    private List<Warrior> _clearingWarriors;
    public IReadOnlyCollection<Warrior> ClearingWarriors => _clearingWarriors;

    private List<Token> _clearingTokens;
    public IReadOnlyCollection<Token> ClearingTokens => _clearingTokens;

    private List<Building> _clearingSlots;
    public IReadOnlyCollection<Building> Slots => _clearingSlots;

    public int MaxNumberOfBuildings { get; init; }

    /*public Dictionary<FactionType, int> GetFactionsPieces()
    {
        var dict = _clearingWarriors
            .ToDictionary(x => x.FactionType, x => x.Count);

        foreach (var slot in _clearingSlots)
        {
            if (!slot.Faction.HasValue)
            {
                continue;
            }

            if (dict.TryGetValue(slot.Faction.Value, out var count))
            {
                dict[slot.Faction.Value] += count;
            }
            else
            {
                dict[slot.Faction.Value] = count;
            }
        }

        return dict;
    }

    public void CreateBuilding(BuildingType building)
    {
        var emptySlot = _clearingSlots.FirstOrDefault(x => x.SlotPiece == null);

        if (emptySlot != null)
        {
            emptySlot.SlotPiece = building;
        }
        else
        {
            throw new ArgumentException($"No empty slot in clearing {Id}");
        }
    }

    public void DestroyBuilding(BuildingType building)
    {
        var slot = _clearingSlots.FirstOrDefault(x => x.SlotPiece == building);

        if (slot != null)
        {
            slot.SlotPiece = null;
        }
        else
        {
            throw new ArgumentException($"No {building} was found in clearing {Id}");
        }
    }

    public void AddToken(TokenType token)
    {
        if (Tokens.ContainsKey(token))
        {
            throw new ArgumentException($"There is already {token} in  clearing {Id}");
        }
        else
        {
            Tokens.Add(token, 1);
        }
    }

    public void RemoveToken(TokenType token)
    {
        if (Tokens.ContainsKey(token))
        {
            Tokens.Remove(token);
        }
        else
        {
            throw new ArgumentException($"There is no {token} in  clearing {Id}");
        }
    }

    public void IncrementToken(TokenType token, int count)
    {
        if (Tokens.ContainsKey(token))
        {
            Tokens[token] += count;
        }
        else
        {
            throw new ArgumentException($"There is no {token} in  clearing {Id}");
        }
    }

    public void DecrementToken(TokenType token, int count)
    {
        if (Tokens.ContainsKey(token))
        {
            if (Tokens[token] < count)
            {
                throw new ArgumentException($"There is {Tokens[token]} count of {token} in  clearing {Id}. But you try to remove {count}");
            }
            else if (Tokens[token] == count)
            {
                RemoveToken(token);
            }
            else
            {
                Tokens[token] -= count;
            }
        }
        else
        {
            throw new ArgumentException($"There is no {token} in  clearing {Id}");
        }
    }

    public void AddWarriors(FactionType faction, int count)
    {
        if (Warriors.TryGetValue(faction, out var warriorsCount))
        {
            Warriors[faction] = warriorsCount + count;
        }
        else
        {
            Warriors.Add(faction, count);
        }
    }

    public void RemoveWarriors(FactionType faction, int count)
    {
        if (Warriors.TryGetValue(faction, out var warriorsCount))
        {
            Warriors[faction] = warriorsCount - count < 0 ?
                throw new ArgumentException(
                    $"Clearing {Id} has {warriorsCount} warriors, but {count} is attempted to remove") :
                warriorsCount - count;
        }
        else
        {
            throw new ArgumentException($"No warriors of {faction} are in clearing {Id}");
        }
    }*/
}

public record ClearingsPath : BaseEntity
{
    public ClearingsPath(int id) : base(id)
    {

    }

    public Clearing FromClearing { get; init; }

    public Clearing ToClearing { get; init; }
}

public record ClearingsRiverPath : BaseEntity
{
    public ClearingsRiverPath(int id) : base(id)
    {

    }

    public Clearing FromClearing { get; init; }

    public Clearing ToClearing { get; init; }
}

public record ClearingForestPath : BaseEntity
{
    public ClearingForestPath(int id) : base(id)
    {

    }

    public Clearing Clearing { get; init; }

    public Forest Forest { get; init; }
}
#endregion

#region Mutable Entities
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

public record Forest : BaseEntity
{
    public Forest(
        int id) : base(id)
    {
    }
}

public record Card : BaseEntity
{
    public Card(int id) : base(id)
    {
    }

    public CardSuit CardSuit { get; init; }

    public CardType CardType { get; init; }

    public virtual ICollection<CostType> Cost { get; init; }
}

/// <summary>
/// Mutable entities
/// </summary>
public record Warrior : BaseEntity
{
    public Warrior(int id) : base(id)
    {
    }

    public FactionType FactionType { get; init; }
}

public record Building : BaseEntity
{
    public Building(int id) : base(id)
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

public record Token : BaseEntity
{
    public Token(int id) : base(id)
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

public record Faction : BaseEntity
{
    public Faction(int id) : base(id)
    {
    }

    public FactionType FactionType { get; set; }

    public ICollection<Card> Cards { get; set; }

    public ICollection<ClearingType> UsedCraftingPieces { get; set; }

    public ICollection<ItemType> Items { get; set; }
}

public record WarriorsFaction : Faction
{
    public WarriorsFaction(int id) : base(id)
    {
    }

    public ICollection<Warrior> Warriors { get; set; }
}

public record MarquiseFaction : WarriorsFaction
{
    public MarquiseFaction(int id) : base(id)
    {
    }

    public ICollection<Token> Tokens { get; set; }

    public ICollection<Building> Buildings { get; set; }
}

public record EyrieFaction : Faction
{
    public EyrieFaction(int id) : base(id)
    {
    }

    public ICollection<Building> Buildings { get; set; }
}

public record AllianceFaction : Faction
{
    public AllianceFaction(int id) : base(id)
    {
    }

    public ICollection<Token> Tokens { get; set; }

    public ICollection<Building> Buildings { get; set; }

    public ICollection<Warrior> Officers { get; set; }

    public ICollection<Card> Support { get; set; }

    public int UsedOfficers { get; set; }
}

public record Game : BaseEntity
{
    public Game(int id) : base(id)
    {
    }

    public ICollection<Card> Deck { get; set; }

    public ICollection<Card> Discard { get; set; }

    public ICollection<ItemType> CraftableItems { get; set; }
}