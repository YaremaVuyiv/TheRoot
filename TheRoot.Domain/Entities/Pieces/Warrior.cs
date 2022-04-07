namespace TheRoot.Domain.Entities.Pieces;

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
