using TheRoot.Data;
using TheRoot.Data.Models;

namespace TheRoot.Repositories
{
    public interface ITokensRepository
    {
        Dictionary<TokenType, int> GetClearingTokens(int clearingId);

        List<int> GetClearingIdsWithTokens(TokenType tokenType);
    }

    public class TokensRepository : ITokensRepository
    {
        private readonly StateContainer _stateContainer;

        public TokensRepository(StateContainer stateContainer)
        {
            _stateContainer = stateContainer;
        }

        public Dictionary<TokenType, int> GetClearingTokens(int clearingId) =>
            _stateContainer.State.Clearings
                .First(x => x.Id == clearingId)
                .Tokens;

        public List<int> GetClearingIdsWithTokens(TokenType tokenType) =>
            _stateContainer.State.Clearings
                .Where(x => x.Tokens.ContainsKey(tokenType) && x.Tokens[tokenType] > 0)
                .Select(x => x.Id)
                .ToList();
    }
}
