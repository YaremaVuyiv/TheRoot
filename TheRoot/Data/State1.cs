using TheRoot.Data.Models;

namespace TheRoot.Data;

public class State
{
    public List<Clearing> Clearings { get; init; }

    public int? FromClearingId { get; set; }

    public Dictionary<Models.FactionType, List<CraftingItemType>> Items { get; init; }

    public List<Slot<CraftingItemType>> CraftingSlotItems { get; set; }

    public Dictionary<int, int[]> ConnectedClearings { get; init; }

    public List<ClearingSlot> ClearingSlots { get; init; }
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

public enum CraftingItemType
{
    Bag,
    Boot,
    Sword,
    Money,
    Teapot,
    Crossbow,
    Hammer
}

//public record ItemPiece(CraftingItemType CraftingItemType);

//public record WarriorPiece(Models.FactionType Faction);

/*public record TokenPiece(TokenType TokenType)
{
    public Models.FactionType Faction => TokenType switch
    {
        TokenType.Support => Models.FactionType.WoodlandAllianse,
        TokenType.Keep or TokenType.Wood => Models.FactionType.MarquiseDeCat,
        _ => throw new ArgumentException("ex")
    };
}*/

/*public record BuildingPiece(BuildingType BuildingType)
{
    public Models.FactionType? Faction => BuildingType switch
    {
        BuildingType.AllianseBase => Models.FactionType.WoodlandAllianse,
        BuildingType.Nest => Models.FactionType.EyrieDynasties,
        BuildingType.Sawmill or
        BuildingType.Workshop or 
        BuildingType.Recruiter => Models.FactionType.MarquiseDeCat,
        _ => throw new ArgumentException("ex")
    };
}*/

//public record Ruin(ItemPiece Item);

public record Slot
{
    public Slot(
        System.Drawing.Point location,
        int height)
    {
        Location = location;
        Height = height;
    }

    public int Height { get; init; }

    public System.Drawing.Point Location { get; init; }
}

public record Slot<T> : Slot
{
    public Slot(
        System.Drawing.Point location,
        int height) : base(location, height)
    {
    }

    public T SlotPiece { get; set; }
}

public record ClearingSlot : Slot
{
    public ClearingSlot(
        int id,
        int clearingId,
        System.Drawing.Point location,
        int height) : base(location, height)
    {
        Id = id;
        ClearingId = clearingId;
    }

    public int Id { get; init; }

    public int ClearingId { get; init; }

    public BuildingType? SlotPiece { get; set; }
}

public record Clearing
{
    public Clearing(
        int id,
        System.Drawing.Point location,
        Models.ClearingType clearingType)
    {
        Id = id;
        Location = location;
        ClearingType = clearingType;
        IsActivated = false;
        Warriors = new Dictionary<Models.FactionType, int>();
        Tokens = new Dictionary<TokenType, int>();
    }

    public int Id { get; init; }

    public Dictionary<Models.FactionType, int> Warriors { get; init;}

    public Dictionary<TokenType, int> Tokens { get; init; }

    public System.Drawing.Point Location { get; init; }

    public Models.ClearingType ClearingType { get; init; }

    public bool IsActivated { get; set; }
}

public record CraftingItemsTable
{
    public System.Drawing.Point Location { get; init; }

    public List<Slot> Slots { get; init; }
}

public class State1
{
}
