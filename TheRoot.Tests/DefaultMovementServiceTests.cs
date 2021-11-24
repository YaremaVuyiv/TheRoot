using FluentAssertions;
using Moq;
using System.Collections.Generic;
using TheRoot.Data;
using TheRoot.Data.Models;
using TheRoot.Repositories;
using TheRoot.Services.Movement;
using Xunit;

namespace TheRoot.Tests
{
    public class DefaultMovementServiceTests : BaseClearingsTests
    {
        private readonly Mock<IWarriorsRepository> _warriorsRepositoryMock;
        private readonly IMovementService _movementService;

        public DefaultMovementServiceTests()
        {
            _warriorsRepositoryMock = new Mock<IWarriorsRepository>();

            _movementService = new DefaultMovementService(
                ClearingsRepositoryMock.Object,
                _warriorsRepositoryMock.Object,
                DominanceServiceMock.Object);
        }

        [Fact]
        public void GetFromClearingIds_ShouldReturnEmptyEnumerableWhenNoClearingIsAvailable()
        {
            _warriorsRepositoryMock.Setup(x => x.GetClearingIdsWithWarriors(FactionType.MarquiseDeCat))
                .Returns(new List<int>());

            var result = _movementService.GetFromClearingIds(FactionType.MarquiseDeCat);

            Assert.Empty(result);
        }

        [Fact]
        public void GetFromClearingIds_ShouldReturnOnlyClearingWhenOneWarriorIsDominating()
        {
            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(1))
                .Returns(FactionType.EyrieDynasties);
            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(3))
                .Returns(FactionType.MarquiseDeCat);

            _warriorsRepositoryMock.Setup(x => x.GetClearingIdsWithWarriors(FactionType.EyrieDynasties))
                .Returns(new List<int>
                {
                    1
                });

            var expectedResult = new int[]
            {
                1
            };

            var result = _movementService.GetFromClearingIds(FactionType.EyrieDynasties);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GetFromClearingIds_ShouldReturnCorrectClearingsWhenDominatingInConnectedClearing()
        {
            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(1))
                .Returns(FactionType.EyrieDynasties);

            _warriorsRepositoryMock.Setup(x => x.GetClearingIdsWithWarriors(FactionType.EyrieDynasties))
                .Returns(new List<int>
                {
                    2, 4, 5
                });

            var expectedResult = new int[]
            {
                2, 4, 5
            };

            var result = _movementService.GetFromClearingIds(FactionType.EyrieDynasties);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GetFromClearingIds_ShouldReturnCorrectClearingsWhenDominatingInConnectedClearings()
        {
            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(1))
                .Returns(FactionType.EyrieDynasties);
            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(4))
                .Returns(FactionType.EyrieDynasties);

            _warriorsRepositoryMock.Setup(x => x.GetClearingIdsWithWarriors(FactionType.EyrieDynasties))
                .Returns(new List<int>
                {
                    1, 2, 4, 5, 10
                });

            var expectedResult = new int[]
            {
                1, 2, 4, 5, 10
            };

            var result = _movementService.GetFromClearingIds(FactionType.EyrieDynasties);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GetFromClearingIds_ShouldReturnCorrectClearingsWhenNotAllClearingsContainWarriors()
        {
            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(6))
                .Returns(FactionType.EyrieDynasties);

            _warriorsRepositoryMock.Setup(x => x.GetClearingIdsWithWarriors(FactionType.EyrieDynasties))
                .Returns(new List<int>
                {
                    2, 3, 9
                });

            var expectedResult = new int[]
            {
                2, 3, 9
            };

            var result = _movementService.GetFromClearingIds(FactionType.EyrieDynasties);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GetToClearingIds_ShouldReturnEmptyWhenNoWarriors()
        {
            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(1))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.MarquiseDeCat, 1 },
                    { FactionType.WoodlandAllianse, 1 }
                });

            var result = _movementService.GetToClearingIds(FactionType.EyrieDynasties, 1);

            Assert.Empty(result);
        }

        [Fact]
        public void GetToClearingIds_ShouldReturnAllConnectedClearingIfRuling()
        {
            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(6))
                .Returns(FactionType.EyrieDynasties);

            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(6))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.EyrieDynasties, 1 }
                });

            var expectedResult = new int[]
            {
                2, 3, 5, 8, 9
            };

            var result = _movementService.GetToClearingIds(FactionType.EyrieDynasties, 6);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GetToClearingIds_ShouldReturnOnlyClearingsWhereRuling()
        {
            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(5))
                .Returns(FactionType.EyrieDynasties);

            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(3))
                .Returns(FactionType.EyrieDynasties);

            DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(8))
                .Returns(FactionType.EyrieDynasties);

            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(6))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.EyrieDynasties, 2 }
                });

            var expectedResult = new int[]
            {
                5, 3, 8
            };

            var result = _movementService.GetToClearingIds(FactionType.EyrieDynasties, 6);

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
