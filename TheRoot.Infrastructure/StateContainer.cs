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
        var clearing1 = new Clearing(id: 1)
        {
            ClearingType = ClearingType.Fox
        };

        var clearing2 = new Clearing(id: 2)
        {
            ClearingType = ClearingType.Mouse
        };

        var clearing3 = new Clearing(id: 3)
        {
            ClearingType = ClearingType.Rabbit
        };

        var clearing4 = new Clearing(id: 4)
        {
            ClearingType = ClearingType.Rabbit
        };

        var clearing5 = new Clearing(id: 5)
        {
            ClearingType = ClearingType.Rabbit
        };

        var clearing6 = new Clearing(id: 6)
        {
            ClearingType = ClearingType.Fox
        };

        var clearing7 = new Clearing(id: 7)
        {
            ClearingType = ClearingType.Fox
        };

        var clearing8 = new Clearing(id: 8)
        {
            ClearingType = ClearingType.Mouse
        };

        var clearing9 = new Clearing(id: 9)
        {
            ClearingType = ClearingType.Mouse
        };

        var clearing10 = new Clearing(id: 10)
        {
            ClearingType = ClearingType.Mouse
        };

        var clearing11 = new Clearing(id: 11)
        {
            ClearingType = ClearingType.Fox
        };

        var clearing12 = new Clearing(id: 12)
        {
            ClearingType = ClearingType.Rabbit
        };

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
            new Forest(1),
            new Forest(2),
            new Forest(3),
            new Forest(4),
            new Forest(5),
            new Forest(6),
            new Forest(7),
        };

        /*ClearingsPaths = new List<ClearingsPath>
        {
            new ClearingsPath(1)
            {
                FromClearing.Id = 1,
                ToClearing.Id = 2
            },
            new ClearingsPath(2)
            {
                FromClearingId = 1,
                ToClearingId = 4
            },
            new ClearingsPath(3)
            {
                FromClearingId = 1,
                ToClearingId = 5
            },
            new ClearingsPath(4)
            {
                FromClearingId = 2,
                ToClearingId = 3
            },
            new ClearingsPath(5)
            {
                FromClearingId = 2,
                ToClearingId = 6
            },
            new ClearingsPath(6)
            {
                FromClearingId = 3,
                ToClearingId = 6
            },
            new ClearingsPath(7)
            {
                FromClearingId = 3,
                ToClearingId = 7
            },
            new ClearingsPath(8)
            {
                FromClearingId = 4,
                ToClearingId = 10
            },
            new ClearingsPath(9)
            {
                FromClearingId = 5,
                ToClearingId = 6
            },
            new ClearingsPath(10)
            {
                FromClearingId = 5,
                ToClearingId = 10
            },
            new ClearingsPath(11)
            {
                FromClearingId = 6,
                ToClearingId = 8
            },
            new ClearingsPath(12)
            {
                FromClearingId = 6,
                ToClearingId = 9
            },
            new ClearingsPath(13)
            {
                FromClearingId = 7,
                ToClearingId = 9
            },
            new ClearingsPath(14)
            {
                FromClearingId = 8,
                ToClearingId = 11
            },
            new ClearingsPath(15)
            {
                FromClearingId = 8,
                ToClearingId = 12
            },
            new ClearingsPath(16)
            {
                FromClearingId = 9,
                ToClearingId = 12
            },
            new ClearingsPath(17)
            {
                FromClearingId = 10,
                ToClearingId = 11
            },
            new ClearingsPath(18)
            {
                FromClearingId = 11,
                ToClearingId = 12
            }
        };*/

        /*ClearingsRiverPaths = new List<ClearingsRiverPath>
        {
            new ClearingsRiverPath(1)
            {
                FromClearingId = 3,
                ToClearingId = 9
            },
            new ClearingsRiverPath(2)
            {
                FromClearingId = 4,
                ToClearingId = 5
            },
            new ClearingsRiverPath(3)
            {
                FromClearingId = 5,
                ToClearingId = 8
            },
            new ClearingsRiverPath(4)
            {
                FromClearingId = 8,
                ToClearingId = 9
            },
        };*/

        /*ClearingForestPaths = new List<ClearingForestPath>
        {
            new ClearingForestPath(1)
            {
                ForestId = 1,
                ClearingId = 1
            },
            new ClearingForestPath(2)
            {
                ForestId = 1,
                ClearingId = 4
            },
            new ClearingForestPath(3)
            {
                ForestId = 1,
                ClearingId = 5
            },
            new ClearingForestPath(4)
            {
                ForestId = 1,
                ClearingId = 10
            },
            new ClearingForestPath(5)
            {
                ForestId = 2,
                ClearingId = 1
            },
            new ClearingForestPath(6)
            {
                ForestId = 2,
                ClearingId = 2
            },
            new ClearingForestPath(7)
            {
                ForestId = 2,
                ClearingId = 5
            },
            new ClearingForestPath(8)
            {
                ForestId = 2,
                ClearingId = 6
            },
            new ClearingForestPath(9)
            {
                ForestId = 3,
                ClearingId = 5
            },
            new ClearingForestPath(10)
            {
                ForestId = 3,
                ClearingId = 6
            },
            new ClearingForestPath(11)
            {
                ForestId = 3,
                ClearingId = 8
            },
            new ClearingForestPath(12)
            {
                ForestId = 3,
                ClearingId = 10
            },
            new ClearingForestPath(13)
            {
                ForestId = 3,
                ClearingId = 11
            },
            new ClearingForestPath(14)
            {
                ForestId = 4,
                ClearingId = 2
            },
            new ClearingForestPath(15)
            {
                ForestId = 4,
                ClearingId = 3
            },
            new ClearingForestPath(16)
            {
                ForestId = 4,
                ClearingId = 6
            },
            new ClearingForestPath(17)
            {
                ForestId = 5,
                ClearingId = 3
            },
            new ClearingForestPath(18)
            {
                ForestId = 5,
                ClearingId = 6
            },
            new ClearingForestPath(19)
            {
                ForestId = 5,
                ClearingId = 7
            },
            new ClearingForestPath(20)
            {
                ForestId = 5,
                ClearingId = 9
            },
            new ClearingForestPath(21)
            {
                ForestId = 6,
                ClearingId = 6
            },
            new ClearingForestPath(22)
            {
                ForestId = 6,
                ClearingId = 8
            },
            new ClearingForestPath(23)
            {
                ForestId = 6,
                ClearingId = 9
            },
            new ClearingForestPath(24)
            {
                ForestId = 6,
                ClearingId = 12
            },
            new ClearingForestPath(25)
            {
                ForestId = 7,
                ClearingId = 8
            },
            new ClearingForestPath(26)
            {
                ForestId = 7,
                ClearingId = 11
            },
            new ClearingForestPath(27)
            {
                ForestId = 7,
                ClearingId = 12
            }
        };*/

        Factions = new List<FactionType>
        {
            FactionType.MarquiseDeCat,
            FactionType.EyrieDynasties,
            FactionType.WoodlandAllianse
        };
    }
}
