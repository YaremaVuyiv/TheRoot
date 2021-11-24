namespace TheRoot.Data.Models.Cards;

public abstract record CraftableCard : Card
{
    public abstract List<CardSuitType> Cost { get; }

    public abstract object Effect { get; }
}
