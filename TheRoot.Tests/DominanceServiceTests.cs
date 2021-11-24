using Moq;
using System.Collections.Generic;
using TheRoot.Data;
using TheRoot.Data.Models;
using TheRoot.Repositories;
using TheRoot.Services;
using Xunit;

namespace TheRoot.Tests
{
    public class DominanceServiceTests
    {
        private readonly IDominanceService _dominanceService;
        private readonly Mock<IClearingsRepository> _clearingsRepositoryMock;
        private readonly Mock<IWarriorsRepository> _warriorsRepositoryMock;

        public DominanceServiceTests()
        {
            _clearingsRepositoryMock = new Mock<IClearingsRepository>();
            _warriorsRepositoryMock = new Mock<IWarriorsRepository>();

            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(5))
                .Returns(new Dictionary<FactionType, int>());

            _clearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(5))
                .Returns(new List<BuildingType?>());

            _dominanceService = new DominanceService(
                _clearingsRepositoryMock.Object,
                _warriorsRepositoryMock.Object);
        }

        [Fact]
        public void GetDominantFactionInClearing_ShouldReturnCorrectFactionWithOnlyOneBuilding()
        {
            _clearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(5))
                .Returns(new List<BuildingType?>
                {
                    BuildingType.Sawmill
                });

            var result = _dominanceService.GetDominantFactionInClearing(5);
            Assert.Equal(FactionType.MarquiseDeCat, result);
        }

        [Fact]
        public void GetDominantFactionInClearing_ShouldReturnCorrectFactionWithOnlyOneWarrior()
        {
            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(5))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.MarquiseDeCat, 1 }
                });

            var result = _dominanceService.GetDominantFactionInClearing(5);
            Assert.Equal(FactionType.MarquiseDeCat, result);
        }

        [Fact]
        public void GetDominantFactionInClearing_ShouldReturnNullWhenNoPieces()
        {
            var result = _dominanceService.GetDominantFactionInClearing(5);
            Assert.Null(result);
        }

        [Fact]
        public void GetDominantFactionInClearing_ShouldReturnNullWithSameNumberOfBuildings()
        {
            _clearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(5))
                .Returns(new List<BuildingType?>
                {
                    BuildingType.Sawmill,
                    BuildingType.AllianseBase
                });

            var result = _dominanceService.GetDominantFactionInClearing(5);
            Assert.Null(result);
        }

        [Fact]
        public void GetDominantFactionInClearing_ShouldReturnNullWithSameNumberOfWarriors()
        {
            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(5))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.MarquiseDeCat, 1 },
                    { FactionType.WoodlandAllianse, 1 }
                });

            var result = _dominanceService.GetDominantFactionInClearing(5);
            Assert.Null(result);
        }

        [Fact]
        public void GetDominantFactionInClearing_ShouldReturnEyrieWithSameNumberOfBuildings()
        {
            _clearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(5))
                .Returns(new List<BuildingType?>
                {
                    BuildingType.Sawmill,
                    BuildingType.Nest
                });

            var result = _dominanceService.GetDominantFactionInClearing(5);
            Assert.Equal(FactionType.EyrieDynasties, result);
        }

        [Fact]
        public void GetDominantFactionInClearing_ShouldReturnEyrieWithSameNumberOfWarriors()
        {
            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(5))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.MarquiseDeCat, 1 },
                    { FactionType.EyrieDynasties, 1 }
                });

            var result = _dominanceService.GetDominantFactionInClearing(5);
            Assert.Equal(FactionType.EyrieDynasties, result);
        }

        [Fact]
        public void GetDominantFactionInClearing_ShouldReturnNonEyrieWhenMoreNonEyrieBuildings()
        {
            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(8))
                .Returns(new Dictionary<FactionType, int>());

            _clearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(8))
                .Returns(new List<BuildingType?>
                {
                    BuildingType.Sawmill,
                    BuildingType.Nest,
                    BuildingType.Recruiter
                });

            var result = _dominanceService.GetDominantFactionInClearing(8);
            Assert.Equal(FactionType.MarquiseDeCat, result);
        }

        [Fact]
        public void GetDominantFactionInClearing_ShouldReturnNonEyrieWhenMoreNonEyrieWarriors()
        {
            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(5))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.MarquiseDeCat, 2 },
                    { FactionType.EyrieDynasties, 1 }
                });

            var result = _dominanceService.GetDominantFactionInClearing(5);
            Assert.Equal(FactionType.MarquiseDeCat, result);
        }

        [Fact]
        public void GetDominantFactionInClearing_ShouldReturnCorrectFactionWithMultipleFactions()
        {
            _clearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(8))
                .Returns(new List<BuildingType?>
                {
                    BuildingType.Sawmill,
                    BuildingType.Nest,
                    BuildingType.AllianseBase
                });

            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(8))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.MarquiseDeCat, 2 },
                    { FactionType.WoodlandAllianse, 3 },
                    { FactionType.EyrieDynasties, 2 }
                });

            var result = _dominanceService.GetDominantFactionInClearing(8);
            Assert.Equal(FactionType.WoodlandAllianse, result);
        }
    }
}
