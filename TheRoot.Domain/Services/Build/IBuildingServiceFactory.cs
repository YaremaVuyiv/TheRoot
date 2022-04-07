using TheRoot.Domain.Entities;

namespace TheRoot.Domain.Services.Build;

public interface IBuildingServiceFactory
{
    IBuildingService Create(FactionType faction);
}
