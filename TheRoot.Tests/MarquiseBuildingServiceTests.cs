using FluentAssertions;
using Moq;
using System.Collections.Generic;
using TheRoot.Data;
using TheRoot.Data.Models;
using TheRoot.Repositories;
using TheRoot.Services.Build;
using Xunit;

namespace TheRoot.Tests;

public class MarquiseBuildingServiceTests : BaseClearingsTests
{
    private readonly Mock<ITokensRepository> _tokensRepositoryMock;
    private readonly IBuildingService _buildingService;

    public MarquiseBuildingServiceTests()
    {
        _tokensRepositoryMock = new Mock<ITokensRepository>();

        for (int i = 1; i <= 12; i++)
        {
            ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(i))
                .Returns(new List<BuildingType?>());
        }

        _buildingService = new MarquiseBuildingService(
            DominanceServiceMock.Object,
            ClearingsRepositoryMock.Object,
            _tokensRepositoryMock.Object);
    }

    [Fact]
    public void GetClearingIdsForBuilding_ShouldReturnCorrectClearings()
    {
        ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(5))
            .Returns(new List<BuildingType?>
            {
                    BuildingType.Sawmill,
                    BuildingType.Workshop
            });

        ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(6))
            .Returns(new List<BuildingType?>
            {
                    BuildingType.Recruiter,
                    BuildingType.Workshop
            });

        ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(7))
            .Returns(new List<BuildingType?>
            {
                    BuildingType.Recruiter
            });

        ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(1))
            .Returns(new List<BuildingType?>
            {
                    null
            });

        _tokensRepositoryMock.Setup(x => x.GetClearingIdsWithTokens(TokenType.Wood))
            .Returns(new List<int>
            {
                    12
            });

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(12))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(9))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(6))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(2))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(1))
            .Returns(FactionType.MarquiseDeCat);

        var expectedResult = new int[]
        {
                1
        };

        var result = _buildingService.GetClearingIdsForBuilding();

        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void GetClearingIdsForBuilding_ShouldReturnCorrectMultipleClearings()
    {
        ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(5))
            .Returns(new List<BuildingType?>
            {
                    BuildingType.Sawmill,
                    BuildingType.Recruiter
            });

        ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(6))
            .Returns(new List<BuildingType?>
            {
                    BuildingType.Recruiter,
                    BuildingType.Workshop,
                    BuildingType.Sawmill
            });

        ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(7))
            .Returns(new List<BuildingType?>
            {
                    BuildingType.Recruiter
            });

        ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(1))
            .Returns(new List<BuildingType?>
            {
                null
            });

        _tokensRepositoryMock.Setup(x => x.GetClearingIdsWithTokens(TokenType.Wood))
            .Returns(new List<int>
            {
                10,
                12
            });

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(12))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(9))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(6))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(2))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(1))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(4))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(10))
            .Returns(FactionType.MarquiseDeCat);

        var expectedResult = new int[]
        {
                1
        };

        var result = _buildingService.GetClearingIdsForBuilding();

        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void GetClearingIdsForBuilding_ShouldReturnNoneWhenNoCorrectPathForMultipleWoodTokens()
    {
        ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(5))
            .Returns(new List<BuildingType?>
            {
                    BuildingType.Sawmill,
                    BuildingType.Workshop
            });

        ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(6))
            .Returns(new List<BuildingType?>
            {
                    BuildingType.Recruiter,
                    BuildingType.Workshop,
                    BuildingType.Sawmill
            });

        ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(7))
            .Returns(new List<BuildingType?>
            {
                    BuildingType.Recruiter
            });

        ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(1))
            .Returns(new List<BuildingType?>
            {
                    null
            });

        _tokensRepositoryMock.Setup(x => x.GetClearingIdsWithTokens(TokenType.Wood))
            .Returns(new List<int>
            {
                12,
                10
            });

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(12))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(9))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(6))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(2))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(1))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(10))
            .Returns(FactionType.MarquiseDeCat);

        var result = _buildingService.GetClearingIdsForBuilding();

        Assert.Empty(result);
    }

    [Fact]
    public void GetClearingIdsForBuilding_ShouldReturnNoneWhenNoEmptySlotsToBuild()
    {
        ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(5))
            .Returns(new List<BuildingType?>
            {
                    BuildingType.Sawmill,
                    BuildingType.Workshop
            });

        ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(6))
            .Returns(new List<BuildingType?>
            {
                    BuildingType.Recruiter,
                    BuildingType.Workshop
            });

        ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(7))
            .Returns(new List<BuildingType?>
            {
                    BuildingType.Recruiter, 
                    null
            });

        _tokensRepositoryMock.Setup(x => x.GetClearingIdsWithTokens(TokenType.Wood))
            .Returns(new List<int>
            {
                    12
            });

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(12))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(9))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(6))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(2))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(1))
            .Returns(FactionType.MarquiseDeCat);

        var result = _buildingService.GetClearingIdsForBuilding();

        Assert.Empty(result);
    }

    [Fact]
    public void GetClearingIdsForBuilding_ShouldReturnNoneWhenNoPathToTransportWood()
    {
        ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(5))
            .Returns(new List<BuildingType?>
            {
                    BuildingType.Sawmill,
                    BuildingType.Workshop
            });

        ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(6))
            .Returns(new List<BuildingType?>
            {
                    BuildingType.Recruiter,
                    BuildingType.Workshop
            });

        ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(7))
            .Returns(new List<BuildingType?>
            {
                    BuildingType.Recruiter
            });

        _tokensRepositoryMock.Setup(x => x.GetClearingIdsWithTokens(TokenType.Wood))
            .Returns(new List<int>
            {
                    12
            });

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(12))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(9))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(6))
            .Returns(FactionType.MarquiseDeCat);

        DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(1))
            .Returns(FactionType.MarquiseDeCat);

        var result = _buildingService.GetClearingIdsForBuilding();

        Assert.Empty(result);
    }
}
