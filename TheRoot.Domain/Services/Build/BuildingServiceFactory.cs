using System;

/*namespace TheRoot.Services.Build
{
    public class BuildingServiceFactory : IBuildingServiceFactory
    {
        private readonly MarquiseBuildingService _marquiseBuildingService;
        private readonly EyrieBuildingService _eyrieBuildingService;

        public BuildingServiceFactory(
            MarquiseBuildingService marquiseBuildingService,
            EyrieBuildingService eyrieBuildingService)
        {
            _marquiseBuildingService = marquiseBuildingService;
            _eyrieBuildingService = eyrieBuildingService;
        }

        public IBuildingService Create(FactionType faction) =>
            faction switch
            {
                FactionType.MarquiseDeCat => _marquiseBuildingService,
                FactionType.EyrieDynasties => _eyrieBuildingService,
                _ => throw new ArgumentException("invalid faction")
            };
    }
}*/
