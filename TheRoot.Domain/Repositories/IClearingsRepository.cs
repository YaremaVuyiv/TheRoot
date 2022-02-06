using TheRoot.Domain.Entities;

namespace TheRoot.Domain.Repositories;

public interface IClearingsRepository
{
    Task<Clearing> GetClearingByIdAsync(int id);

    Task<List<int>> GetAdjacentClearingsAsync(int clearingId);

    Task<List<int>> GetAdjacentForestsAsync(int clearingId);

    Task<List<Clearing>> GetAllClearingsAsync();

    Task<List<int>> GetAllClearingsIdsAsync();

    Task<List<BuildingType?>> GetSlotPiecesByClearingIdAsync(int clearingId);

    Task<List<Building>> GetSlotsByClearingIdAsync(int clearingId);

    Task<Dictionary<TokenType, int>> GetClearingTokensAsync(int clearingId);

    Task<List<int>> GetClearingIdsWithTokensAsync(TokenType tokenType);

    Task<List<int>> GetClearingIdsWithWarriorsAsync(FactionType faction);

    Task<Dictionary<FactionType, int>> GetClearingWarriorsAsync(int clearingId);

    Task<List<FactionType>> GetFactionsAsync();
}
