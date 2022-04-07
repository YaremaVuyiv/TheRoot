namespace TheRoot.Domain.Entities.Pieces;

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
