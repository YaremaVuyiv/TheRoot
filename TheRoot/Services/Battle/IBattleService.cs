using TheRoot.Data.Models;

namespace TheRoot.Services.Battle;

public interface IBattleService
{
    List<int> GetBattleClearings(FactionType faction);
}
