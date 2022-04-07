using TheRoot.Domain.Entities;
using TheRoot.Domain.Repositories;

namespace TheRoot.Domain.Services.Movement;

public class MovementServiceFactory : IMovementServiceFactory
{
    private readonly IDominanceService _dominanceService;
    private readonly IClearingsRepository _clearingsRepository;
    private readonly DefaultMovementService _defaultMovementService;

    public MovementServiceFactory(
        IDominanceService dominanceService,
        IClearingsRepository clearingsRepository,
        DefaultMovementService defaultMovementService)
    {
        _dominanceService = dominanceService;
        _clearingsRepository = clearingsRepository;
        _defaultMovementService = defaultMovementService;
    }

    public IMovementService Create(FactionType faction) =>
        faction switch
        {
            _ => _defaultMovementService
        };
}

