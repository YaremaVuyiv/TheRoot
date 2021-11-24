using TheRoot.Data.Models;
using TheRoot.Repositories;

namespace TheRoot.Services.Movement;

public class MovementServiceFactory : IMovementServiceFactory
{
    private readonly IDominanceService _dominanceService;
    private readonly IClearingsRepository _clearingsRepository;
    private readonly IWarriorsRepository _warriorsRepository;
    private readonly DefaultMovementService _defaultMovementService;

    public MovementServiceFactory(
        IDominanceService dominanceService,
        IClearingsRepository clearingsRepository,
        IWarriorsRepository warriorsRepository,
        DefaultMovementService defaultMovementService)
    {
        _dominanceService = dominanceService;
        _clearingsRepository = clearingsRepository;
        _warriorsRepository = warriorsRepository;
        _defaultMovementService = defaultMovementService;
    }

    public IMovementService Create(FactionType faction) =>
        faction switch
        {
            _ => _defaultMovementService
        };
}

