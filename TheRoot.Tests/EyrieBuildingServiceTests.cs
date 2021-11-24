using FluentAssertions;
using System.Collections.Generic;
using TheRoot.Data;
using TheRoot.Data.Models;
using TheRoot.Services.Build;
using Xunit;

namespace TheRoot.Tests
{
    public class EyrieBuildingServiceTests : BaseClearingsTests
    {
        private readonly IBuildingService _buildingService;

        public EyrieBuildingServiceTests()
        {
            for (int i = 1; i <= 12; i++)
            {
                ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(i))
                   .Returns(new List<BuildingType?>());
            }

            _buildingService = new EyrieBuildingService(
                DominanceServiceMock.Object,
                ClearingsRepositoryMock.Object);
        }

        [Fact]
        public void GetClearingIdsForBuilding_ShouldReturnNoneWhenAllAvailableHaveNests()
        {
            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(2))
                .Returns(FactionType.EyrieDynasties);

            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(3))
                .Returns(FactionType.EyrieDynasties);

            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(4))
                .Returns(FactionType.EyrieDynasties);

            ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(2))
                .Returns(new List<BuildingType?>
                {
                    BuildingType.Nest
                });

            ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(3))
                .Returns(new List<BuildingType?>
                {
                    BuildingType.Nest
                });

            ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(4))
                .Returns(new List<BuildingType?>
                {
                    BuildingType.Nest
                });

            var result = _buildingService.GetClearingIdsForBuilding();

            Assert.Empty(result);
        }

        [Fact]
        public void GetClearingIdsForBuilding_ShouldReturnNoneWhenAllAvailableHaveNoEmpty()
        {
            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(2))
                .Returns(FactionType.EyrieDynasties);

            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(3))
                .Returns(FactionType.EyrieDynasties);

            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(4))
                .Returns(FactionType.EyrieDynasties);

            ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(1))
                .Returns(new List<BuildingType?>
                {
                    BuildingType.Sawmill,
                    BuildingType.Sawmill
                });

            ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(2))
                .Returns(new List<BuildingType?>
                {
                    BuildingType.Sawmill
                });

            ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(3))
                .Returns(new List<BuildingType?>
                {
                    BuildingType.Sawmill,
                    BuildingType.Sawmill
                });

            var result = _buildingService.GetClearingIdsForBuilding();

            Assert.Empty(result);
        }

        [Fact]
        public void GetClearingIdsForBuilding_ShouldReturnNoneWhenNoDominant()
        {
            var result = _buildingService.GetClearingIdsForBuilding();

            Assert.Empty(result);
        }

        [Fact]
        public void GetClearingIdsForBuilding_ShouldReturnCorrectDataWhenSomeClearingsAreAvailableAndSomeNot()
        {
            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(2))
                .Returns(FactionType.EyrieDynasties);

            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(3))
                .Returns(FactionType.EyrieDynasties);

            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(4))
                .Returns(FactionType.EyrieDynasties);

            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(5))
                .Returns(FactionType.EyrieDynasties);

            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(6))
                .Returns(FactionType.EyrieDynasties);

            ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(2))
                .Returns(new List<BuildingType?>
                {
                    BuildingType.Nest
                });

            ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(3))
                .Returns(new List<BuildingType?>
                {
                    null
                });

            ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(4))
                .Returns(new List<BuildingType?>
                {
                    BuildingType.Sawmill,
                    BuildingType.Sawmill
                });

            ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(5))
                .Returns(new List<BuildingType?>
                {
                    null
                });

            ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(6))
                .Returns(new List<BuildingType?>
                {
                    null
                });

            var expectedResult = new int[]
            {
                3, 5, 6
            };

            var result = _buildingService.GetClearingIdsForBuilding();

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
