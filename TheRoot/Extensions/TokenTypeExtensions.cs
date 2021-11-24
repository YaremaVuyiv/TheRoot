using TheRoot.Data.Models;

namespace TheRoot.Extensions
{
    public static class TokenTypeExtensions
    {
        public static FactionType GetFactionByTokenType(this TokenType tokentype) =>
            tokentype switch
            {
                TokenType.Wood => FactionType.MarquiseDeCat,
                TokenType.Keep => FactionType.MarquiseDeCat,
                TokenType.Support => FactionType.WoodlandAllianse,
                _ => throw new System.Exception("Invalid enum")
            };
    }
}
