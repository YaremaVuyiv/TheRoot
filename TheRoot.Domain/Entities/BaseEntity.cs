namespace TheRoot.Domain.Entities;

public abstract record BaseEntity
{
    public int Id { get; init; }
}