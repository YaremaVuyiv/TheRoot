using Microsoft.EntityFrameworkCore;
using TheRoot.Domain.Entities;
using TheRoot.Domain.Repositories;

namespace TheRoot.Infrastructure.Repositories;

public class ClearingsRepository : IClearingsRepository
{
    private readonly DataContext _dataContext;

    public Task<List<int>> GetAdjacentClearingsAsync(int clearingId)
    {
        var result = StateContainer.ClearingsPaths
            .Where(x => x.FromClearing.Id == clearingId || x.ToClearing.Id == clearingId)
            .Select(x => x.FromClearing.Id == clearingId ? x.ToClearing.Id : x.FromClearing.Id)
            .ToList();

        return Task.FromResult(result);
    }

    public Task<List<int>> GetAdjacentForestsAsync(int clearingId)
    {
        var result = StateContainer.ClearingForestPaths
            .Where(x => x.Clearing.Id == clearingId)
            .Select(x => x.Forest.Id)
            .ToList();

        return Task.FromResult(result);
    }

    public Task<List<Clearing>> GetAllClearingsAsync()
    {
        return Task.FromResult(StateContainer.Clearings);
    }

    public Task<List<int>> GetAllClearingsIdsAsync()
    {
        var result = StateContainer.Clearings
            .Select(x => x.Id)
            .ToList();

        return Task.FromResult(result);
    }

    public Task<Clearing> GetClearingByIdAsync(int id)
    {
        var result = StateContainer.Clearings
            .First(x => x.Id == id);

        return Task.FromResult(result);
    }

    public Task<List<int>> GetClearingIdsWithTokensAsync(TokenType tokenType)
    {
        var result = StateContainer.Clearings
            .Where(x => x.TokensContainer.Pieces.Any(z => z.TokenType == tokenType))
            .Select(x => x.Id)
            .ToList();

        return Task.FromResult(result);
    }

    public Task<List<int>> GetClearingIdsWithWarriorsAsync(FactionType faction)
    {
        var result = StateContainer.Clearings
            .Where(x => x.WarriorsContainer.Pieces.Any(z => z.FactionType == faction))
            .Select(x => x.Id)
            .ToList();

        return Task.FromResult(result);
    }

    public Task<Dictionary<TokenType, int>> GetClearingTokensAsync(int clearingId)
    {
        var result = StateContainer.Clearings
            .First(x => x.Id == clearingId)
            .TokensContainer
            .Pieces
            .GroupBy(x => x.TokenType)
            .ToDictionary(x => x.Key, x => x.Count());

        return Task.FromResult(result);
    }

    public Task<List<BuildingType?>> GetSlotPiecesByClearingIdAsync(int clearingId)
    {
        var result = StateContainer.Clearings
            .First(x => x.Id == clearingId)
            .BuildingsContainer
            .Pieces
            .Select(x => (BuildingType?)x.SlotPiece)
            .ToList();

        return Task.FromResult(result);
    }

    public Task<List<Building>> GetSlotsByClearingIdAsync(int clearingId)
    {
        var result = StateContainer.Clearings
            .First(x => x.Id == clearingId)
            .BuildingsContainer
            .Pieces
            .ToList();

        return Task.FromResult(result);
    }

    public Task<Dictionary<FactionType, int>> GetClearingWarriorsAsync(int clearingId)
    {
        var result = StateContainer.Clearings
            .First(x => x.Id == clearingId)
            .WarriorsContainer
            .Pieces
            .GroupBy(x => x.FactionType)
            .ToDictionary(x => x.Key, x => x.Count());

        return Task.FromResult(result);
    }

    public Task<List<FactionType>> GetFactionsAsync()
    {
        var result = _dataContext.Factions
            .Select(x => x.FactionType)
            .ToListAsync();

        return result;
    }
}
