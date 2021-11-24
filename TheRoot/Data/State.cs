using TheRoot.Data.Models;

namespace TheRoot.Test.Data
{
    /*public record State(
        Dictionary<int, ClearingModel> Clearings,
        Dictionary<int, CraftingItemModel> CraftingItems,
        Dictionary<int, ClearingWarriorsModel> ClearingWarriors,
        Dictionary<int, ClearingTokensModel> ClearingTokens,
        Dictionary<int, SlotModel> Slots);*/

    public class State
    {
        public List<Clearing> Clearings { get; init; }

        public List<ClearingSlot> ClearingSlots { get; init; }

        public int? FromClearingId { get; set; }

        public Dictionary<FactionType, List<Piece>> Items { get; set; }

        public List<Slot> CraftingSlotItems { get; set; }

        public Dictionary<int, int[]> ConnectedClearings { get; init; }

        public List<WarriorPiece> Warriors { get; init; }

        public List<TokenPiece> Tokens { get; init; }
    }

    public class CraftingPiece
    {
        public ClearingType ClearingType { get; }
    }

    public abstract class Piece
    {
        public abstract string Image { get; }
    }

    public abstract class FactionPiece : Piece
    {
        public abstract FactionType Faction { get; }
    }

    public class Vagabond : FactionPiece
    {
        public override FactionType Faction => throw new NotImplementedException();

        public override string Image => throw new NotImplementedException();
    }

    public abstract class WarriorPiece : FactionPiece
    {
        public WarriorPiece(int clearingId)
        {
            ClearingId = clearingId;
        }

        public int ClearingId { get; set; }
    }

    public abstract class TokenPiece : FactionPiece
    {
        public TokenPiece(int clearingId)
        {
            ClearingId = clearingId;
        }

        public int ClearingId { get; init; }
    }

    public class SupportToken : TokenPiece
    {
        public SupportToken(int clearingId) : base(clearingId)
        {
        }

        public override string Image => "Support.png";

        public override FactionType Faction => FactionType.WoodlandAllianse;
    }

    public class WoodToken : TokenPiece
    {
        public WoodToken(int clearingId) : base(clearingId)
        {
        }

        public override string Image => "Support.png";

        public override FactionType Faction => FactionType.WoodlandAllianse;
    }

    public class KeepToken : TokenPiece
    {
        public KeepToken(int clearingId) : base(clearingId)
        {
        }

        public override FactionType Faction => FactionType.MarquiseDeCat;

        public override string Image => "marquise.png";
    }

    public class AllianseWarrior : WarriorPiece
    {
        public AllianseWarrior(int clearingId) : base(clearingId)
        {
        }

        public override string Image => "allianse.png";

        public override FactionType Faction => FactionType.WoodlandAllianse;
    }

    public class MarquiseWarrior : WarriorPiece
    {
        public MarquiseWarrior(int clearingId) : base(clearingId)
        {
        }

        public override string Image => "marquise.png";

        public override FactionType Faction => FactionType.MarquiseDeCat;
    }

    public class EyrieWarrior : WarriorPiece
    {
        public EyrieWarrior(int clearingId) : base(clearingId)
        {
        }

        public override string Image => "eyrie.png";

        public override FactionType Faction => FactionType.EyrieDynasties;
    }

    public class Ruin : Piece
    {
        public Ruin(Piece item)
        {
            Item = item;
        }

        public override string Image => "Buildings/ruin.png";

        public Piece Item { get; init; }
    }

    public class SwordItem : Piece
    {
        public override string Image => "Items/sword.png";
    }

    public class BagItem : Piece
    {
        public override string Image => "Items/bag.png";
    }

    public class TeapotItem : Piece
    {
        public override string Image => "Items/teapot.png";
    }

    public class BootItem : Piece
    {
        public override string Image => "Items/boot.png";
    }

    public class CrossbowItem : Piece
    {
        public override string Image => "Items/crossbow.png";
    }

    public class HammerItem : Piece
    {
        public override string Image => "Items/hammer.png";
    }

    public class MoneyItem : Piece
    {
        public override string Image => "Items/money.png";
    }

    public class NestBuilding : FactionPiece
    {
        public override FactionType Faction => FactionType.EyrieDynasties;

        public override string Image => "Buildings/Eyrie/Nest.png";
    }

    public class AllianseBaseBuilding : FactionPiece
    {
        public override FactionType Faction => FactionType.WoodlandAllianse;

        public override string Image => "Buildings/Allianse/allianse_base.png";
    }

    public class SawmillBuilding : FactionPiece
    {
        public override FactionType Faction => FactionType.MarquiseDeCat;

        public override string Image => "Buildings/Marquise/sawmill.png";
    }

    public class RecruiterBuilding : FactionPiece
    {
        public override FactionType Faction => FactionType.MarquiseDeCat;

        public override string Image => "Buildings/Marquise/recruiter.png";
    }

    public class WorkShopBuilding : FactionPiece
    {
        public override FactionType Faction => FactionType.MarquiseDeCat;

        public override string Image => "Buildings/Marquise/workshop.png";
    }

    public class Slot
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

        public Piece SlotPiece { get; set; }
    }

    public class ClearingSlot : Slot
    {
        public ClearingSlot(
            int slotId,
            int clearingId,
            System.Drawing.Point location,
            int height): base(location, height)
        {
            SlotId = slotId;
            ClearingId = clearingId;
        }

        public int SlotId { get; init; }

        public int ClearingId { get; init; }
    }

    public class Clearing
    {
        public Clearing(
            int id,
            System.Drawing.Point location,
            ClearingType clearingType)
        {
            Id = id;
            Location = location;
            ClearingType = clearingType;
            IsActivated = false;
        }

        public int Id { get; init; }

        public System.Drawing.Point Location { get; init; }

        public ClearingType ClearingType { get; init; }

        public bool IsActivated { get; set; }
    }

    public class CraftingItemsTable
    {
        public System.Drawing.Point Location { get; init; }

        public List<Slot> Slots { get; init; }
    }
}
