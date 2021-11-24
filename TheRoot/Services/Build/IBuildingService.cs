using System.Collections.Generic;

namespace TheRoot.Services.Build;

public interface IBuildingService
{
    IEnumerable<int> GetClearingIdsForBuilding();
}
