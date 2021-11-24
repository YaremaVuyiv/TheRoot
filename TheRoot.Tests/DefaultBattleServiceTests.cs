using FluentAssertions;
using Moq;
using System.Collections.Generic;
using TheRoot.Data;
using TheRoot.Data.Models;
using TheRoot.Repositories;
using TheRoot.Services.Battle;
using Xunit;

namespace TheRoot.Tests
{
    public class DefaultBattleServiceTests : BaseClearingsTests
    {
        private readonly Mock<IWarriorsRepository> _warriorsRepositoryMock;
        private readonly Mock<ITokensRepository> _tokensRepositoryMock;

        private readonly IBattleService _battleService;

        public DefaultBattleServiceTests()
        {
            _warriorsRepositoryMock = new Mock<IWarriorsRepository>();
            _tokensRepositoryMock = new Mock<ITokensRepository>();

            for (int i = 1; i <= 12; i++)
            {
                _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(i))
                    .Returns(new Dictionary<FactionType, int>());

                _tokensRepositoryMock.Setup(x => x.GetClearingTokens(i))
                    .Returns(new Dictionary<TokenType, int>());

                ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(i))
                    .Returns(new List<BuildingType?>());
            }

            _battleService = new DefaultBattleService(
                ClearingsRepositoryMock.Object,
                _warriorsRepositoryMock.Object,
                _tokensRepositoryMock.Object);
        }

        [Fact]
        public void GetBattleClearings_ShouldReturnNoneWhenNoWarriors()
        {
            var result = _battleService.GetBattleClearings(FactionType.MarquiseDeCat);

            Assert.Empty(result);
        }

        [Fact]
        public void GetBattleClearings_ShouldReturnNoneWhenNoOtherPieces()
        {
            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(6))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.EyrieDynasties, 5 }
                });

            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(8))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.EyrieDynasties, 5 }
                });

            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(10))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.EyrieDynasties, 5 }
                });

            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(7))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.MarquiseDeCat, 5 }
                });

            _tokensRepositoryMock.Setup(x => x.GetClearingTokens(5))
                .Returns(new Dictionary<TokenType, int>
                {
                    { TokenType.Support, 1 }
                });

            ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(3))
                .Returns(new List<BuildingType?>
                {
                    BuildingType.Sawmill,
                    BuildingType.Recruiter,
                    BuildingType.AllianseBase
                });

            var result = _battleService.GetBattleClearings(FactionType.EyrieDynasties);

            Assert.Empty(result);
        }

        [Fact]
        public void GetBattleClearings_ShouldReturnClearingWithEnemyWarrior()
        {
            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(6))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.EyrieDynasties, 5 }
                });

            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(8))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.EyrieDynasties, 5 }
                });

            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(10))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.EyrieDynasties, 5 },
                    { FactionType.MarquiseDeCat, 5 }
                });

            _tokensRepositoryMock.Setup(x => x.GetClearingTokens(5))
                .Returns(new Dictionary<TokenType, int>
                {
                    { TokenType.Support, 1 }
                });

            ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(3))
                .Returns(new List<BuildingType?>
                {
                    BuildingType.Sawmill,
                    BuildingType.Recruiter,
                    BuildingType.AllianseBase
                });

            var expectedResult = new List<int> { 10 };

            var result = _battleService.GetBattleClearings(FactionType.EyrieDynasties);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GetBattleClearings_ShouldReturnClearingWithEnemyToken()
        {
            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(6))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.EyrieDynasties, 5 }
                });

            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(8))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.EyrieDynasties, 5 }
                });

            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(10))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.EyrieDynasties, 5 }
                });

            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(9))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.MarquiseDeCat, 5 }
                });

            _tokensRepositoryMock.Setup(x => x.GetClearingTokens(6))
                .Returns(new Dictionary<TokenType, int>
                {
                    { TokenType.Support, 1 }
                });

            ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(3))
                .Returns(new List<BuildingType?>
                {
                    BuildingType.Sawmill,
                    BuildingType.Recruiter,
                    BuildingType.AllianseBase
                });

            var expectedResult = new List<int> { 6 };

            var result = _battleService.GetBattleClearings(FactionType.EyrieDynasties);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GetBattleClearings_ShouldReturnClearingWithEnemyBuilding()
        {
            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(6))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.EyrieDynasties, 5 }
                });

            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(8))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.EyrieDynasties, 5 }
                });

            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(10))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.EyrieDynasties, 5 }
                });

            _warriorsRepositoryMock.Setup(x => x.GetClearingWarriors(9))
                .Returns(new Dictionary<FactionType, int>
                {
                    { FactionType.MarquiseDeCat, 5 }
                });

            _tokensRepositoryMock.Setup(x => x.GetClearingTokens(5))
                .Returns(new Dictionary<TokenType, int>
                {
                    { TokenType.Support, 1 }
                });

            ClearingsRepositoryMock.Setup(x => x.GetSlotPiecesByClearingId(8))
                .Returns(new List<BuildingType?>
                {
                    BuildingType.Sawmill,
                    BuildingType.Recruiter,
                    BuildingType.AllianseBase
                });

            var expectedResult = new List<int> { 8 };

            var result = _battleService.GetBattleClearings(FactionType.EyrieDynasties);

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
