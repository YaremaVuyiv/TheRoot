using TheRoot.Data.Models;

namespace TheRoot.Services.Movement;

public interface IMovementServiceFactory
{
    IMovementService Create(FactionType faction);
}
