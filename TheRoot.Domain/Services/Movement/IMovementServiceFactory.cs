using TheRoot.Domain.Entities;

namespace TheRoot.Domain.Services.Movement;

public interface IMovementServiceFactory
{
    IMovementService Create(FactionType faction);
}
