using System.Collections.Generic;
using System.Threading.Tasks;
using TheRoot.Domain.Entities;

namespace TheRoot.Domain.Services.Battle;

public interface IBattleService
{
    Task<List<int>> GetBattleClearings(FactionType faction);
}
