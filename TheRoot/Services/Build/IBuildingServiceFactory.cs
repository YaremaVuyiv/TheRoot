using TheRoot.Data.Models;

namespace TheRoot.Services.Build
{
    public interface IBuildingServiceFactory
    {
        IBuildingService Create(FactionType faction);
    }
}
