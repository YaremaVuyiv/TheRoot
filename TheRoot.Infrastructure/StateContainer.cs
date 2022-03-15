using TheRoot.Domain.Entities;

namespace TheRoot.Infrastructure;

public static class StateContainer
{
    public static List<Clearing> Clearings { get; set; }

    public static List<Forest> Forests { get; set; }

    public static List<ClearingsPath> ClearingsPaths { get; set; }

    public static List<ClearingsRiverPath> ClearingsRiverPaths { get; set; }

    public static List<ClearingForestPath> ClearingForestPaths { get; set; }

    public static List<FactionType> Factions { get; set; }

    static StateContainer()
    {
        var clearing1 = new Clearing(ClearingType.Fox, 2)
        {
            Id = 1,
            ClearingType = ClearingType.Fox
        };

        var clearing2 = new Clearing(ClearingType.Mouse, 2)
        {
            Id = 2,
            ClearingType = ClearingType.Mouse
        };

        var clearing3 = new Clearing(ClearingType.Rabbit, 1)
        {
            Id = 3,
            ClearingType = ClearingType.Rabbit
        };

        var clearing4 = new Clearing(ClearingType.Rabbit, 2)
        {
            Id = 4,
            ClearingType = ClearingType.Rabbit
        };

        var clearing5 = new Clearing(ClearingType.Rabbit, 2)
        {
            Id = 5,
            ClearingType = ClearingType.Rabbit
        };

        var clearing6 = new Clearing(ClearingType.Fox, 2)
        {
            Id = 6,
            ClearingType = ClearingType.Fox
        };

        var clearing7 = new Clearing(ClearingType.Fox, 2)
        {
            Id = 7,
            ClearingType = ClearingType.Fox
        };

        var clearing8 = new Clearing(ClearingType.Mouse, 2)
        {
            Id = 8,
            ClearingType = ClearingType.Mouse
        };

        var clearing9 = new Clearing(ClearingType.Mouse, 3)
        {
            Id = 9,
            ClearingType = ClearingType.Mouse
        };

        var clearing10 = new Clearing(ClearingType.Mouse, 2)
        {
            Id = 10,
            ClearingType = ClearingType.Mouse
        };

        var clearing11 = new Clearing(ClearingType.Fox, 2)
        {
            Id = 11,
            ClearingType = ClearingType.Fox
        };

        var clearing12 = new Clearing(ClearingType.Rabbit, 1)
        {
            Id = 12,
            ClearingType = ClearingType.Rabbit
        };

        var forest1 = new Forest();
        var forest2 = new Forest();
        var forest3 = new Forest();
        var forest4 = new Forest();
        var forest5 = new Forest();
        var forest6 = new Forest();
        var forest7 = new Forest();

        Clearings = new List<Clearing>
        {
            clearing1,
            clearing2,
            clearing3,
            clearing4,
            clearing5,
            clearing6,
            clearing7,
            clearing8,
            clearing9,
            clearing10,
            clearing11,
            clearing12
        };

        Forests = new List<Forest>
        {
            forest1,
            forest2,
            forest3,
            forest4,
            forest5,
            forest6,
            forest7
        };

        ClearingsPaths = new List<ClearingsPath>
        {
            new ClearingsPath(clearing1, clearing2),
            new ClearingsPath(clearing1, clearing4),
            new ClearingsPath(clearing1, clearing5),
            new ClearingsPath(clearing2, clearing3),
            new ClearingsPath(clearing2, clearing6),
            new ClearingsPath(clearing3, clearing6),
            new ClearingsPath(clearing3, clearing7),
            new ClearingsPath(clearing4, clearing10),
            new ClearingsPath(clearing5, clearing6),
            new ClearingsPath(clearing5, clearing10),
            new ClearingsPath(clearing6, clearing8),
            new ClearingsPath(clearing6, clearing9),
            new ClearingsPath(clearing7, clearing9),
            new ClearingsPath(clearing8, clearing11),
            new ClearingsPath(clearing8, clearing12),
            new ClearingsPath(clearing9, clearing12),
            new ClearingsPath(clearing10, clearing11),
            new ClearingsPath(clearing11, clearing12)
        };

        ClearingsRiverPaths = new List<ClearingsRiverPath>
        {
            new ClearingsRiverPath(clearing3, clearing9),
            new ClearingsRiverPath(clearing4, clearing5),
            new ClearingsRiverPath(clearing5, clearing8),
            new ClearingsRiverPath(clearing8, clearing9)
        };

        ClearingForestPaths = new List<ClearingForestPath>
        {
            new ClearingForestPath(clearing1, forest1),
            new ClearingForestPath(clearing4, forest1),
            new ClearingForestPath(clearing5, forest1),
            new ClearingForestPath(clearing10, forest1),
            new ClearingForestPath(clearing1, forest2),
            new ClearingForestPath(clearing2, forest2),
            new ClearingForestPath(clearing5, forest2),
            new ClearingForestPath(clearing6, forest2),
            new ClearingForestPath(clearing5, forest3),
            new ClearingForestPath(clearing6, forest3),
            new ClearingForestPath(clearing8, forest3),
            new ClearingForestPath(clearing10, forest3),
            new ClearingForestPath(clearing11, forest3),
            new ClearingForestPath(clearing2, forest4),
            new ClearingForestPath(clearing3, forest4),
            new ClearingForestPath(clearing6, forest4),
            new ClearingForestPath(clearing3, forest5),
            new ClearingForestPath(clearing6, forest5),
            new ClearingForestPath(clearing7, forest5),
            new ClearingForestPath(clearing9, forest5),
            new ClearingForestPath(clearing6, forest6),
            new ClearingForestPath(clearing8, forest6),
            new ClearingForestPath(clearing9, forest6),
            new ClearingForestPath(clearing12, forest6),
            new ClearingForestPath(clearing8, forest7),
            new ClearingForestPath(clearing11, forest7),
            new ClearingForestPath(clearing12, forest7)
        };

        Factions = new List<FactionType>
        {
            FactionType.MarquiseDeCat,
            FactionType.EyrieDynasties,
            FactionType.WoodlandAllianse
        };
    }
}
