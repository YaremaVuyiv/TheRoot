namespace TheRoot.Data.Models.Cards;

public abstract record Card
{
    public abstract CardSuitType SuitType { get; init; }
}
