using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheRoot.Domain.Services.Build;

public interface IBuildingService
{
    Task<IEnumerable<int>> GetClearingIdsForBuildingAsync();
}
