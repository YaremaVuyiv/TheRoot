using Moq;
using System.Collections.Generic;
using TheRoot.Data.Models;
using TheRoot.Repositories;
using TheRoot.Services;

namespace TheRoot.Tests
{
    public class BaseClearingsTests
    {
        protected Mock<IClearingsRepository> ClearingsRepositoryMock { get; init; }
        protected Mock<IDominanceService> DominanceServiceMock { get; init; }

        protected BaseClearingsTests()
        {
            ClearingsRepositoryMock = new Mock<IClearingsRepository>();
            DominanceServiceMock = new Mock<IDominanceService>();

            ClearingsRepositoryMock.Setup(x => x.GetAllClearingIds())
                .Returns(new List<int>
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
                });

            ClearingsRepositoryMock.Setup(x => x.GetConnectedClearings(1))
                .Returns(new List<int>
                {
                    2, 4, 5
                });

            ClearingsRepositoryMock.Setup(x => x.GetConnectedClearings(2))
                .Returns(new List<int>
                {
                    1, 3, 6
                });

            ClearingsRepositoryMock.Setup(x => x.GetConnectedClearings(3))
                .Returns(new List<int>
                {
                    2, 6, 7
                });

            ClearingsRepositoryMock.Setup(x => x.GetConnectedClearings(4))
                .Returns(new List<int>
                {
                    1, 10
                });

            ClearingsRepositoryMock.Setup(x => x.GetConnectedClearings(5))
                .Returns(new List<int>
                {
                    1, 6, 10
                });

            ClearingsRepositoryMock.Setup(x => x.GetConnectedClearings(6))
                .Returns(new List<int>
                {
                    2, 3, 5, 8, 9
                });

            ClearingsRepositoryMock.Setup(x => x.GetConnectedClearings(7))
                .Returns(new List<int>
                {
                    3, 9
                });

            ClearingsRepositoryMock.Setup(x => x.GetConnectedClearings(8))
                .Returns(new List<int>
                {
                    6, 11, 12
                });

            ClearingsRepositoryMock.Setup(x => x.GetConnectedClearings(9))
                .Returns(new List<int>
                {
                    6, 7, 12
                });

            ClearingsRepositoryMock.Setup(x => x.GetConnectedClearings(10))
                .Returns(new List<int>
                {
                    4, 5, 11
                });

            ClearingsRepositoryMock.Setup(x => x.GetConnectedClearings(11))
                .Returns(new List<int>
                {
                    8, 10, 12
                });

            ClearingsRepositoryMock.Setup(x => x.GetConnectedClearings(12))
                .Returns(new List<int>
                {
                    8, 9, 11
                });

            for (int i = 1; i <= 12; i++)
            {
                DominanceServiceMock.Setup(x => x.GetDominantFactionInClearing(i))
                    .Returns((FactionType?)null);
            }
        }
    }
}
