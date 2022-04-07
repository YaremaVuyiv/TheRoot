using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRoot.Domain.Entities;

namespace TheRoot.Domain.Extensions
{
    public static class BuildingTypeExtensions
    {
        public static FactionType GetFactionType(this BuildingType buildingType)
        {
            return buildingType switch
            {
                BuildingType.Sawmill
                    or BuildingType.Recruiter
                    or BuildingType.Workshop => FactionType.MarquiseDeCat,
                BuildingType.Nest => FactionType.EyrieDynasties,
                BuildingType.AllianseBase => FactionType.WoodlandAllianse,
                _ => throw new ArgumentException($"Building type {buildingType} is missing")
            };
        }
    }
}
