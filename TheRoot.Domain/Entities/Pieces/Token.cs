namespace TheRoot.Domain.Entities.Pieces;

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

    public FactionType FactionType =>
        TokenType switch
        {
            TokenType.Wood or TokenType.Keep => FactionType.MarquiseDeCat,
            TokenType.Support => FactionType.WoodlandAllianse,
            _ => throw new Exception("Unsopported token")
        };
}
