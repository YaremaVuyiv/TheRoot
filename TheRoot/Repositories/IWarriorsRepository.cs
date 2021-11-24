using TheRoot.Data;
using TheRoot.Data.Models;

namespace TheRoot.Repositories
{
    public interface IWarriorsRepository
    {
        List<int> GetClearingIdsWithWarriors(FactionType faction);

        Dictionary<FactionType, int> GetClearingWarriors(int clearingId);
    }

    public class WarriorsRepository : IWarriorsRepository
    {
        private readonly StateContainer _stateContainer;

        public WarriorsRepository(StateContainer stateContainer)
        {
            _stateContainer = stateContainer;
        }

        public Dictionary<FactionType, int> GetClearingWarriors(int clearingId) =>
            _stateContainer.State.Clearings
                .First(x => x.Id == clearingId)
                .Warriors;

        public List<int> GetClearingIdsWithWarriors(FactionType faction) =>
            _stateContainer.State.Clearings
                .Where(x => x.Warriors.ContainsKey(faction) && x.Warriors[faction] > 0)
                .Select(x => x.Id)
                .ToList();
    }
}
