namespace TheRoot.Domain.Entities.Pieces;

public record Building : BaseEntity
{
    public Building(BuildingType buildingType)
    {
        SlotPiece = buildingType;
    }

    protected Building()
    {
    }

    public BuildingType SlotPiece { get; init; }

    public FactionType? FactionType =>
        SlotPiece switch
        {
            BuildingType.Sawmill or
            BuildingType.Recruiter or
            BuildingType.Workshop => FactionType.MarquiseDeCat,
            BuildingType.AllianseBase => FactionType.WoodlandAllianse,
            BuildingType.Nest => FactionType.EyrieDynasties,
            _ => null
        };
}
