using TheRoot.Domain.Entities;

namespace TheRoot.Domain.Extensions
{
    public static class TokenTypeExtensions
    {
        public static FactionType GetFactionType(this TokenType tokenType)
        {
            return tokenType switch
            {
                TokenType.Keep or TokenType.Wood => FactionType.MarquiseDeCat,
                TokenType.Support => FactionType.EyrieDynasties,
                _ => throw new ArgumentException($"Token type {tokenType} is missing")
            };
        }
    }
}
